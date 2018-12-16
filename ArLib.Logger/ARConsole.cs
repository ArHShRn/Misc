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
// Last Update :
//  Dec.15th 2018
//=============================================================================

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;

namespace ArLib.ARConsole
{
    /// <summary>
    /// <para>Log message levels.</para>
    /// <para>This controls the message color.</para>
    /// </summary>
    public enum MsgLevel
    {
        Default = -1,
        Redirected,
        Harmless,
        Further,
        Critical
    };

    /// <summary>
    /// Self-made List extensions
    /// </summary>
    public static class ListExtension
    {
        /// <summary>
        /// [Extension] Take messages of errors occured before console is created one by one.
        /// </summary>
        /// <param name="ErrMsgList">The error messages list</param>
        /// <returns>null if the list is empty</returns>
        public static string DequeueErrorMessage(this List<string> ErrMsgList)
        {
            if (ErrMsgList.Count <= 0) return null;

            string ErrMsg = ErrMsgList.First();
            ErrMsgList.RemoveAt(0);
            return ErrMsg;
        }
    }

    /// <summary>
    /// The log console class
    /// </summary>
    public class LogHelper
    {
        /// <summary>
        /// Process of CMD
        /// </summary>
        private Process procCMD = null;
        /// <summary>
        /// ProcessStartInfo of CMD
        /// </summary>
        private ProcessStartInfo psiCMD = null;

        /// <summary>
        /// <para>Is CMD successfully initialized.</para>
        /// <para>
        /// Related both to if user wants to create CMD
        /// and to if ProcessStartInfo of CMD is successfully initialized.
        /// </para> 
        /// </summary>
        private bool bInitedCMD = false;
        /// <summary>
        /// Is current running proc is a console app?
        /// </summary>
        private bool bConsoleApp = true;
        /// <summary>
        /// Is current console created as a GUI console?
        /// </summary>
        private bool bGuiConsole = false;
        /// <summary>
        /// Is gui console disposed?
        /// </summary>
        private bool bGuiConosleDisposed = false;

        /// <summary>
        /// For get/set usage.
        /// </summary>
        private string _Prefix = "";
        /// <summary>
        /// Default prefix of the log.
        /// </summary>
        private readonly string _Default_Prefix = "[CuiConsole] ";
        /// <summary>
        /// <para>Prefix of the log.</para>
        /// <para>Default prefix is "[Log] "</para>
        /// <para>The log message will like</para>
        /// <para>[Log] This is a sample log.</para>
        /// </summary>
        public string Prefix
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

        /// <summary>
        /// <para>The exception message of errors occured before the console is created.</para>
        /// <para>All errors will be logged according to their own occuring time after initialization.</para>
        /// </summary>
        private List<string> preErrMsg = new List<string>();

        /// <summary>
        /// The form class of Gui console.
        /// </summary>
        private GuiConsole guiConsole = null;
        /// <summary>
        /// <para>A delegate.</para>
        /// <para>Used for async creating Gui console.</para>
        /// </summary>
        private delegate void CreateGuiConsoleDelegate();
        /// <summary>
        /// A callback that would inform us if Gui console is disposed.
        /// </summary>
        private AsyncCallback CreateGuiConsoleCallBack;
        /// <summary>
        /// The instance of the function to create a Gui console.
        /// </summary>
        private CreateGuiConsoleDelegate CreateGuiConsole = null;
        /// <summary>
        /// IAsyncResult of CreateGuiConsoleDelegate's invoke.
        /// </summary>
        private IAsyncResult CreateGuiConsoleResult = null;
        /// <summary>
        /// <para>A delegate.</para>
        /// <para>Used for adding log in Gui console.</para>
        /// </summary>
        /// <param name="msg"></param>
        /// <param name="bAllowTracing"></param>
        private delegate void GuiLogDelegate(string msg, bool bAllowTracing);
        /// <summary>
        /// The method to add log in Gui console.
        /// </summary>
        private GuiLogDelegate LogGuiConsole = null;

        private ConsoleColor DefaultColor = ConsoleColor.White;
        private ConsoleColor RedirectedColor = ConsoleColor.Cyan;
        private ConsoleColor HarmlessColor = ConsoleColor.Green;
        private ConsoleColor FurtherColor = ConsoleColor.Yellow;
        private ConsoleColor CriticalColor = ConsoleColor.Red;

        /// <summary>
        /// <para>Create a logger instance.</para>
        /// <para>This instance responds to CMD commands and uses default log prefix</para>
        /// </summary>
        public LogHelper()
        {
            bInitedCMD = false;
            bConsoleApp = !CreateCuiConsole();

            LogPreErrors();
        }

        /// <summary>
        /// <para>Create a logger instance.</para>
        /// <para>The user will decide if this instance responds to CMD commands.</para>
        /// <para>This instance uses default log prefix.</para>
        /// </summary>
        /// <param name="bUseCMD">set true to let the instance responds to CMD commands.</param>
        public LogHelper(bool bUseCMD)
        {
            bInitedCMD = bUseCMD && InitPsiCMD();
            bConsoleApp = !CreateCuiConsole();

            LogPreErrors();
        }

        /// <summary>
        /// <para>Create a logger instance.</para>
        /// <para>The user will decide if this instance responds to CMD commands.</para>
        /// <para>The user will decide if this instance uses customized prefix.</para>
        /// </summary>
        /// <param name="bUseCMD">set true to let the instance responds to CMD commands.</param>
        /// <param name="customPrefix">set your willing prefix.</param>
        public LogHelper(bool bUseCMD, string customPrefix)
        {
            bInitedCMD = bUseCMD && InitPsiCMD();
            bConsoleApp = !CreateCuiConsole();
            Prefix = customPrefix;

            LogPreErrors();
        }

        /// <summary>
        /// <para>Create a logger instance.</para>
        /// <para>The user will decide if this instance responds to CMD commands.</para>
        /// <para>The user will decide if this instance uses customized prefix.</para>
        /// <para>The user will decide if this instance is a GUI console</para>
        /// </summary>
        /// <param name="bUseCMD">set true to let the instance responds to CMD commands.</param>
        /// <param name="customPrefix">set your willing prefix.</param>
        /// <param name="bGUI">set true to create GUI console</param>
        public LogHelper(bool bUseCMD, string customPrefix, bool bGUI)
        {
            bInitedCMD = bUseCMD && InitPsiCMD();
            bGuiConsole = bGUI;
            Prefix = customPrefix;

            if(bGuiConsole)
            {
                guiConsole = new GuiConsole();

                //Register logging method
                LogGuiConsole = new GuiLogDelegate(guiConsole.AddLog);

                //Register async creating event.
                CreateGuiConsole = new CreateGuiConsoleDelegate(CreateGuiThread);
                CreateGuiConsoleCallBack = new AsyncCallback(NotifyGuiConsoleTerminated);
                CreateGuiConsoleResult = CreateGuiConsole.BeginInvoke
                    (
                        callback: CreateGuiConsoleCallBack,
                        @object: "Gui Console Terminiated!"
                    );
            }
            else
                bConsoleApp = !CreateCuiConsole();

            LogPreErrors();
        }

        /// <summary>
        /// A method to start a standard message loop of Gui console.
        /// </summary>
        private void CreateGuiThread()
        {
            Application.Run(guiConsole);
        }

        /// <summary>
        /// Called after the Gui console terminated.
        /// </summary>
        /// <param name="asyncResult"></param>
        private void NotifyGuiConsoleTerminated(IAsyncResult asyncResult)
        {
            //Debug
            MessageBox.Show(asyncResult.AsyncState.ToString(), "Callback Test");

            //To-dos
            return;
        }

        /// <summary>
        /// Log all errors that occured before the console is created.
        /// </summary>
        private void LogPreErrors()
        {
            string errmsg = "";
            Log("Logger successfully created!", MsgLevel.Harmless);
            while(true)
            {
                errmsg = preErrMsg.DequeueErrorMessage();
                if (errmsg == null) break;

                Log(errmsg, MsgLevel.Critical);
            }
        }

        /// <summary>
        /// Initialize ProcessStartInfo of CMD.
        /// Will store a exception msg that occurs before the console is created.
        /// </summary>
        /// <returns></returns>
        private bool InitPsiCMD()
        {
            try
            {
                procCMD = new Process();
                psiCMD = new ProcessStartInfo();

                psiCMD.FileName = "cmd.exe";
                //psiCMD.Arguments = "/C "; //Exit right after the command is executed
                psiCMD.UseShellExecute = false; //Don't launch with shell 
                psiCMD.RedirectStandardInput = true;
                psiCMD.RedirectStandardOutput = true;
                psiCMD.RedirectStandardError = true;
                psiCMD.CreateNoWindow = true; //Don't create new window

                return true;
            }
            catch(Exception ex)
            {
                preErrMsg.Add(ex.Message);
                return false;
            }
        }

        /// <summary>
        /// Try to create a console for current proc.
        /// </summary>
        /// <returns>false if current proc has already a console.[Like a console app]</returns>
        private bool CreateCuiConsole()
        {
            try
            {
                ConsoleHelper.CreateConsole();
                return true;
            }
            catch(Exception ex)
            {
                preErrMsg.Add(ex.Message);
                return false;
            }
            finally
            {
                Console.WriteLine(UserDefinedStr.sAuthor);
                Console.WriteLine(UserDefinedStr.sHelperCpr);
                Console.WriteLine(UserDefinedStr.sLibCpr);
                Console.WriteLine("");
            }
        }

        /// <summary>
        /// Release created console.
        /// If the app has already a console then leave it.
        /// </summary>
        public void ReleaseConsole()
        {
            if(bConsoleApp && !bGuiConsole)
            {
                Log("You can't release the console while this process is a console app!", MsgLevel.Critical);
                return;
            }
            if(bGuiConsole && !bGuiConosleDisposed)
            {
                Log("You can't release the console while the Gui console is online!", MsgLevel.Critical);
                return;
            }

            ConsoleHelper.ReleaseConsole();
        }

        /// <summary>
        /// Make a log to console
        /// </summary>
        /// <param name="str"></param>
        public void Log(string str, MsgLevel level = MsgLevel.Default)
        {
            string msg = "";

            if(bGuiConsole)
            {
                LogGuiConsole(str, true);
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
        /// Start a new line.
        /// </summary>
        public void CRLF()
        {
            Console.WriteLine();
        }

        /// <summary>
        /// Debug useage
        /// </summary>
        /// <param name="str"></param>
        /// <param name="prefix"></param>
        /// <param name="bPrefix"></param>
        /// <param name="level"></param>
        private void InnerLog(string str, string prefix = "InnerMsg", bool bPrefix = false, MsgLevel level = MsgLevel.Default)
        {
            string msg = "";
            if (bGuiConsole)
            {
                LogGuiConsole(str, true);
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
        /// Send a cmd command and get the result
        /// </summary>
        /// <param name="command"></param>
        public void ExecuteCMD(string command)
        {
            if(!bInitedCMD)
            {
                Log("You didn't set this logger instance to responds to CMD commands.", MsgLevel.Critical);
                Log("Create this instance with [public LogConsole(bool bUseCMD)].", MsgLevel.Critical);
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

            Log("Waiting For CMD Processing...",MsgLevel.Further);
            procCMD.StandardInput.AutoFlush = true;
            procCMD.StandardInput.WriteLine(@"ECHO OFF");
            procCMD.StandardInput.WriteLine(command);
            procCMD.StandardInput.WriteLine(@"EXIT");

            //Parsing result
            result = procCMD.StandardOutput.ReadToEnd().Trim();
            var index = result.IndexOf("ECHO OFF");
            result = result.Substring(index + 8, result.Length - index - 13);

            InnerLog(result, "", false, MsgLevel.Redirected);
            //Log("Command Successully Executed.", MsgLevel.Harmless);

            procCMD.Close();
        }

        /// <summary>
        /// Simulate a PAUSE command
        /// </summary>
        public void Pause()
        {
            if (!bInitedCMD) return;
            if (bConsoleApp) return;

            Console.Write("Press Any Key To Continue...");
            Console.ReadKey();
        }
    }
}
