//=============================================================================
// ArLIB Logger : Logger Instance Classs
// Introduction :
//  Create a console instance and redirect logs to it.
// 
// Code And Concept By ArHShRn
// https://github.com/ArHShRn
//
// Release Log :
//  Just created this class. It's all empty.
//
//=============================================================================

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace ArLib.ARConsole
{
    /// <summary>
    /// <para>Log message levels.</para>
    /// <para>This controls the message color.</para>
    /// </summary>
    public enum MsgLevel
    {
        /// <summary>
        /// White
        /// </summary>
        Default = -1,
        /// <summary>
        /// Cyan
        /// </summary>
        Redirected,
        /// <summary>
        /// Green
        /// </summary>
        Harmless,
        /// <summary>
        /// Yellow
        /// </summary>
        Further,
        /// <summary>
        /// Red
        /// </summary>
        Critical
    };

    /// <summary>
    /// The log console class
    /// </summary>
    public static class LogHelper
    {
        /// <summary>
        /// Process of CMD
        /// </summary>
        private static Process procCMD         = null;
        /// <summary>
        /// ProcessStartInfo of CMD
        /// </summary>
        private static ProcessStartInfo psiCMD = null;

        /// <summary>
        /// <para>Is CMD successfully initialized.</para>
        /// <para>
        /// Related both to if user wants to create CMD
        /// and to if ProcessStartInfo of CMD is successfully initialized.
        /// </para> 
        /// </summary>
        private static bool bInitedCMD     = false;
        /// <summary>
        /// Does current running proc has a cui console?
        /// <para>Default: true</para>
        /// </summary>
        private static bool bHasCuiConsole    = true;
        /// <summary>
        /// Is current console created as a GUI console?
        /// </summary>
        private static bool bGuiConsole    = false;
        /// <summary>
        /// Is gui console disposed?
        /// </summary>
        private static bool bGuiDisposed   = false;
        /// <summary>
        /// Did user call ReleaseConsole();
        /// </summary>
        private static bool _bReleased = true;
        /// <summary>
        /// Did user call ReleaseConsole();
        /// </summary>
        public static bool Released
        {
            get
            {
                return _bReleased;
            }
        }
        /// <summary>
        /// Do all Async tasks complete?
        /// </summary>
        private static bool bTasksComplete
        {
            get
            {
                return pendingCount == 0;
            }
        }

        /// <summary>
        /// For get/set usage.
        /// </summary>
        private static string _Prefix = "";
        /// <summary>
        /// Default prefix of the log.
        /// </summary>
        private static readonly string _Default_Prefix = "[CuiConsole] ";
        /// <summary>
        /// <para>Prefix of the log.</para>
        /// <para>Default prefix is "[Log] "</para>
        /// <para>The log message will like
        /// [Log] This is a sample log.</para>
        /// </summary>
        public static string Prefix
        {
            get
            {
                if (_Prefix == "")
                    return _Default_Prefix;
                else
                    return _Prefix;
            }
            set
            {
                _Prefix = "[" + value + "] ";
            }
        }
        private static string Title = "";

        /// <summary>
        /// <para>The exception message of errors occured before the console is created.</para>
        /// <para>All errors will be logged according to their own occuring time after initialization.</para>
        /// </summary>
        private static Queue<string> preErrMsg = new Queue<string>();

        /// <summary>
        /// The form style logger
        /// </summary>
        private static GuiConsole guiConsole = null;

        //----------Async GUI Message Loop
        #region Async_GUI_Message_Loop
        /// <summary>
        /// <para>A delegate.</para>
        /// <para>Used for async creating Gui console.</para>
        /// </summary>
        private delegate void CreateGuiConsoleDelegate(string customPrefix, string title);
        /// <summary>
        /// A callback that would inform us if Gui console is disposed.
        /// </summary>
        private static AsyncCallback CreateGuiConsoleCallBack;
        /// <summary>
        /// IAsyncResult of CreateGuiConsoleDelegate's invoke.
        /// </summary>
        private static IAsyncResult CreateGuiConsoleResult = null;
        /// <summary>
        /// The instance of the function to create a Gui console.
        /// </summary>
        private static CreateGuiConsoleDelegate delegateCreateGuiConsole = null;
        #endregion Async_GUI_Message_Loop

        //----------Async Executing Commands
        #region Async_Executing_Commands
        private delegate void AsyncExecuteCMDDelegate(string command);
        private static AsyncExecuteCMDDelegate delegateAsyncExecuteCMD = null;
        private static AsyncCallback AsyncExecuteCMDCallback = null;
        private static IAsyncResult AsyncExecuteCMDResult = null;
        private static Queue<IAsyncResult> PendingAsyncCommands = new Queue<IAsyncResult>();
        private static int pendingCount
        {
            get
            {
                return PendingAsyncCommands.Count;
            }
        }
        #endregion Async_Executing_Commands

        private static ConsoleColor DefaultColor       = ConsoleColor.White;
        private static ConsoleColor RedirectedColor    = ConsoleColor.Cyan;
        private static ConsoleColor HarmlessColor      = ConsoleColor.Green;
        private static ConsoleColor FurtherColor       = ConsoleColor.Yellow;
        private static ConsoleColor CriticalColor      = ConsoleColor.Red;

        /// <summary>
        /// <para>Create a logger instance.</para>
        /// <para>The user will decide if this instance responds to CMD commands.</para>
        /// <para>The user will decide if this instance uses customized prefix.</para>
        /// <para>The user will decide if this instance is a GUI console</para>
        /// </summary>
        /// <param name="bUseCMD">set true to let the instance responds to CMD commands.</param>
        /// <param name="customPrefix">set your willing prefix.</param>
        /// <param name="bGUI">set true to create GUI console</param>
        /// <param name="title">The console title.</param>
        public static bool CreateConsole(bool bUseCMD, string customPrefix, bool bGUI, string title = "ArHShRn Logger")
        {
            bInitedCMD = bUseCMD && InitPsiCMD();
            bHasCuiConsole = true;
            bGuiConsole = bGUI;
            bGuiDisposed = false;
            _bReleased = false;

            Prefix = customPrefix;
            Title = title;

            try
            {
                Thread.CurrentThread.Name = title;
                if (bGuiConsole)
                {
                    CreateGuiConsole(customPrefix, title);
                    bHasCuiConsole = false;
                }
                else
                    bHasCuiConsole = !CreateCuiConsole(title);

                return true;
            }
            catch(Exception ex)
            {
                preErrMsg.Enqueue(ex.Message);
                return false;
            }
            finally
            {
                LogCreatingStatus();
            }
        }
        /// <summary>
        /// Release created console.
        /// If the app has already a console then leave it.
        /// </summary>
        public static void ReleaseConsole()
        {
            if (bGuiConsole)
            {
                if (!bGuiDisposed)
                {
                    guiConsole.Dispose();
                    bGuiDisposed = true;
                }
                else
                    MessageBox.Show("GUI Console is already disposed.", Prefix);

                return;
            }

            ConsoleHelper.ReleaseConsole();
            _bReleased = true;
        }

        /// <summary>
        /// Initialize ProcessStartInfo of CMD.
        /// Will store a exception msg that occurs before the console is created.
        /// </summary>
        /// <returns></returns>
        private static bool InitPsiCMD()
        {
            try
            {
                procCMD = new Process();
                psiCMD = new ProcessStartInfo();

                psiCMD.FileName = "cmd.exe";
                psiCMD.UseShellExecute = false; //Don't launch with shell 
                psiCMD.RedirectStandardInput = true;
                psiCMD.RedirectStandardOutput = true;
                psiCMD.RedirectStandardError = true;
                psiCMD.CreateNoWindow = true; //Don't create new window

                //Debug
                //throw new Exception("Test Exception In InitPsiCMD");

                return true;
            }
            catch (Exception ex)
            {
                preErrMsg.Enqueue(ex.Message);
                return false;
            }
        }
        /// <summary>
        /// Log all errors that occured before the console is created.
        /// </summary>
        private static void LogCreatingStatus()
        {
            string errmsg = "";

            while (preErrMsg.Count != 0)
            {
                errmsg = preErrMsg.Dequeue();
                if (errmsg == null) break;

                Log("-Previous Error- " + errmsg, MsgLevel.Critical);
            }
            Log("Logger successfully created! Name = " + Thread.CurrentThread.Name, MsgLevel.Default);
        }

        /// <summary>
        /// Try to create a console for current proc.
        /// </summary>
        /// <returns>false if current proc has already a console.[Like a console app]</returns>
        private static bool CreateCuiConsole(string title)
        {
            try
            {
                ConsoleHelper.CreateConsole(title);
                return true;
            }
            catch (Exception ex)
            {
                preErrMsg.Enqueue(ex.Message);
                return false;
            }
            finally
            {
                Console.Clear();
                Console.WriteLine(UserDefinedStr.sAuthor);
                Console.WriteLine(UserDefinedStr.sHelperCpr);
                Console.WriteLine(UserDefinedStr.sLibCpr);
                Console.WriteLine("");
            }
        }
        private static void CreateGuiConsole(string customPrefix, string title)
        {
            delegateCreateGuiConsole = new CreateGuiConsoleDelegate(_CreateGuiConsole);
            CreateGuiConsoleCallBack = new AsyncCallback(NotifyGuiConsoleTerminated);
            CreateGuiConsoleResult = delegateCreateGuiConsole.BeginInvoke(
                customPrefix: customPrefix,
                title: title,
                callback: CreateGuiConsoleCallBack,
                @object: "GUI Console Disposed.");
        }
        /// <summary>
        /// A method to start a standard message loop of Gui console.
        /// </summary>
        private static void _CreateGuiConsole(string customPrefix, string title)
        {
            try
            {
                guiConsole = new GuiConsole(customPrefix, title);

                Application.EnableVisualStyles();

                Application.Run(guiConsole);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Error!");
            }
        }
        /// <summary>
        /// Called after the Gui console terminated.
        /// </summary>
        /// <param name="asyncResult"></param>
        private static void NotifyGuiConsoleTerminated(IAsyncResult asyncResult)
        {
            //To-dos
            _bReleased = true;

            preErrMsg.Enqueue("A new CUI console is created to keep the app running.");
            CreateConsole(bInitedCMD, Prefix, false, Title);
            return;
        }

        /// <summary>
        /// Make a log to console
        /// </summary>
        /// <param name="str">Contents to be logged</param>
        /// <param name="level">Message level which decides the printing color</param>
        public static void Log(string str, MsgLevel level = MsgLevel.Default)
        {
            string msg = "";

            if (bGuiConsole)
            {
                return;
            }

            msg = Prefix.Trim() + "[" + DateTime.Now.ToString("HH:mm:ss") + "] " + str;
            //Color switch
            switch (level)
            {
                case MsgLevel.Harmless:
                    Console.ForegroundColor = HarmlessColor;
                    break;
                case MsgLevel.Redirected:
                    Console.ForegroundColor = RedirectedColor;
                    break;
                case MsgLevel.Further:
                    Console.ForegroundColor = FurtherColor;
                    break;
                case MsgLevel.Critical:
                    Console.ForegroundColor = CriticalColor;
                    break;
                default:
                    Console.ForegroundColor = DefaultColor;
                    break;
            }

            Console.WriteLine(msg);
        }
        /// <summary>
        /// Only for in-class usage.
        /// Make a non-prefix log according to your willing.
        /// </summary>
        /// <param name="str"></param>
        /// <param name="prefix"></param>
        /// <param name="bPrefix"></param>
        /// <param name="level"></param>
        private static void InnerLog(string str, string prefix = "InnerMsg", bool bPrefix = false, MsgLevel level = MsgLevel.Default)
        {
            string msg = "";
            if (bGuiConsole)
            {
                return;
            }

            msg = bPrefix ? ("[" + prefix + "] " + str) : str;
            //Color switch
            switch (level)
            {
                case MsgLevel.Harmless:
                    Console.ForegroundColor = HarmlessColor;
                    break;
                case MsgLevel.Redirected:
                    Console.ForegroundColor = RedirectedColor;
                    break;
                case MsgLevel.Further:
                    Console.ForegroundColor = FurtherColor;
                    break;
                case MsgLevel.Critical:
                    Console.ForegroundColor = CriticalColor;
                    break;
                default:
                    Console.ForegroundColor = DefaultColor;
                    break;
            }

            Console.WriteLine(msg);
        }
        /// <summary>
        /// Start a new line except GUI console.
        /// </summary>
        public static void CRLF()
        {
            if (bGuiConsole || Released) return;

            Console.WriteLine();
        }
        /// <summary>
        /// Simulate a PAUSE command
        /// </summary>
        private static void Pause()
        {
            if (!bInitedCMD) return;
            if (bGuiConsole) return;

            InnerLog("Press any key to continue...");
            Console.ReadKey();
        }

        /// <summary>
        /// Sync send a cmd command and get the result.
        /// </summary>
        /// <param name="command">A non-interactive CMD command</param>
        public static void ExecuteCMD(string command)
        {
            if(!bInitedCMD)
            {
                Log("You didn't set this logger instance to responds to CMD commands.", MsgLevel.Critical);
                Log("Otherwise this may be caused by errors occured before.", MsgLevel.Critical);
                return;
            }

            string result = "";

            procCMD = new Process();
            procCMD.StartInfo = psiCMD;
            var bStart = procCMD.Start();

            if(!bStart)
            {
                Log("Failed to create CMD process.", MsgLevel.Critical);
                return;
            }

            Log("-Sync Exec- " + command, MsgLevel.Further);

            procCMD.StandardInput.AutoFlush = true;
            procCMD.StandardInput.WriteLine(@"ECHO OFF");
            procCMD.StandardInput.WriteLine(command);
            procCMD.StandardInput.WriteLine(@"EXIT");

            //Parsing result
            result = procCMD.StandardOutput.ReadToEnd().Trim();
            var index = result.IndexOf("ECHO OFF");
            result = result.Substring(index + 8, result.Length - index - 13);

            InnerLog(result, "", false, MsgLevel.Redirected);
            Log("-Sync Exec- Terminated. [" + command + "]", MsgLevel.Further);

            procCMD.Close();
        }
        /// <summary>
        /// Async send a cmd command and get the result.
        /// </summary>
        /// <param name="command">A non-interactive CMD command</param>
        public static void AsyncExecuteCMD(string command)
        {
            if (pendingCount >= 1)
            {
                Log("You can't start a new async command while there is a pending command.", MsgLevel.Critical);
                return;
            }
            //Register async executing event.
            delegateAsyncExecuteCMD = new AsyncExecuteCMDDelegate(_AsyncExecuteCMD);

            //Register async callback.
            AsyncExecuteCMDCallback = new AsyncCallback(NotifyCommandTerminated);
            AsyncExecuteCMDResult = delegateAsyncExecuteCMD.BeginInvoke
                (
                    command: command,
                    callback: AsyncExecuteCMDCallback,
                    @object: "-Async Exec- Terminated."
                );

            PendingAsyncCommands.Enqueue(AsyncExecuteCMDResult);
            Log("[" + command + "] has been added to exec queue.");
            Log("Current pending count = " + PendingAsyncCommands.Count);
            return;
        }
        /// <summary>
        /// Method of AsyncExecuteCMD
        /// </summary>
        /// <param name="command"></param>
        private static void _AsyncExecuteCMD(string command)
        {
            if (!bInitedCMD)
            {
                Log("You didn't set this logger instance to responds to CMD commands.", MsgLevel.Critical);
                Log("Otherwise this may be caused by errors occured before.", MsgLevel.Critical);
                return;
            }

            string result = "";

            var asyncCMD = new Process();
            asyncCMD.StartInfo = psiCMD;
            var bStart = asyncCMD.Start();

            if (!bStart)
            {
                Log("Failed to create CMD process.", MsgLevel.Critical);
                return;
            }

            Log("-Async Exec- " + command, MsgLevel.Further);

            asyncCMD.StandardInput.AutoFlush = true;
            asyncCMD.StandardInput.WriteLine(@"ECHO OFF");
            asyncCMD.StandardInput.WriteLine(command);
            asyncCMD.StandardInput.WriteLine(@"EXIT");

            //Parsing result
            result = asyncCMD.StandardOutput.ReadToEnd().Trim();
            var index = result.IndexOf("ECHO OFF");
            result = result.Substring(index + 8, result.Length - index - 13);

            InnerLog(result, "", false, MsgLevel.Redirected);

            procCMD.Close();
        }
        /// <summary>
        /// Notify that a async-executed command is terminated.
        /// </summary>
        /// <param name="asyncResult"></param>
        private static void NotifyCommandTerminated(IAsyncResult asyncResult)
        {
            string msg = asyncResult.AsyncState.ToString();

            PendingAsyncCommands.Dequeue();

            if (pendingCount == 0)
                msg = msg + "No more task pending.";
            else
                msg = msg + "Waiting for left " + pendingCount + " task(s)";

            Log(msg, MsgLevel.Further);
        }
    }
}
