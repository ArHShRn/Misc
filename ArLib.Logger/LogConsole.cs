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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArLib.Logger
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
    public class LogConsole
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
        /// and to if ProcessStartInfo of CMD is successfully initialized
        /// </para> 
        /// </summary>
        private bool bInitedCMD = false;
        /// <summary>
        /// Is current running proc is a console app
        /// </summary>
        private bool bConsoleApp = true;

        private ConsoleColor DefaultColor = ConsoleColor.White;
        private ConsoleColor RedirectedColor = ConsoleColor.Cyan;
        private ConsoleColor HarmlessColor = ConsoleColor.Green;
        private ConsoleColor FurtherColor = ConsoleColor.Yellow;
        private ConsoleColor CriticalColor = ConsoleColor.Red;

        /// <summary>
        /// For get/set usage.
        /// </summary>
        private string _Prefix = "";
        /// <summary>
        /// Default prefix of the log.
        /// </summary>
        private readonly string _Default_Prefix = "[Log] ";
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
                _Prefix =  "[" + value + "] ";
            }
        }

        /// <summary>
        /// <para>The exception message of errors occured before the console is created.</para>
        /// <para>All errors will be logged according to their own occuring time after initialization.</para>
        /// </summary>
        private List<string> preErrMsg = new List<string>();

        /// <summary>
        /// <para>Create a logger instance.</para>
        /// <para>This instance responds to CMD commands and uses default log prefix</para>
        /// </summary>
        public LogConsole()
        {
            bInitedCMD = false;
            bConsoleApp = !CreateGUIConsole();
        }

        /// <summary>
        /// <para>Create a logger instance.</para>
        /// <para>The user will decide if this instance responds to CMD commands.</para>
        /// <para>This instance uses default log prefix.</para>
        /// </summary>
        /// <param name="bUseCMD">set true to let the instance responds to CMD commands.</param>
        public LogConsole(bool bUseCMD)
        {
            bInitedCMD = bUseCMD && InitPsiCMD();
            bConsoleApp = !CreateGUIConsole();
        }

        /// <summary>
        /// <para>Create a logger instance.</para>
        /// <para>The user will decide if this instance responds to CMD commands.</para>
        /// <para>The user will decide if this instance uses customized prefix.</para>
        /// </summary>
        /// <param name="bUseCMD">set true to let the instance responds to CMD commands.</param>
        /// <param name="customPrefix">ste your willing prefix.</param>
        public LogConsole(bool bUseCMD, string customPrefix)
        {
            bInitedCMD = bUseCMD && InitPsiCMD();
            bConsoleApp = !CreateGUIConsole();
            Prefix = customPrefix;
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
        private bool CreateGUIConsole()
        {
            try
            {
                GuiConsole.CreateConsole();
                return true;
            }
            catch(Exception ex)
            {
                preErrMsg.Add(ex.Message);
                return false;
            }
            finally
            {
                Console.WriteLine("ArHShRn Library Logger Console [Version Release]");
                Console.WriteLine("  (c) 2018 Eusth [GuiConsole] All rights reserved under MIT license.");
                Console.WriteLine("  (c) 2018 ArHShRn  [Library] All rights reserved under MIT license.");
                Console.WriteLine("");
            }
        }

        /// <summary>
        /// Release created console.
        /// If the app has already a console then leave it.
        /// </summary>
        public void ReleaseConsole()
        {
            if (bConsoleApp) return;
            GuiConsole.ReleaseConsole();
        }

        /// <summary>
        /// Make a log to console
        /// </summary>
        /// <param name="str"></param>
        public void Log(string str, MsgLevel level = MsgLevel.Default)
        {
            string msg = Prefix.Trim() + "[" + DateTime.Now.ToString("HH:mm:ss") + "] " + str;
            //Color switch
            switch (level)
            {
                case MsgLevel.Harmless: Console.ForegroundColor = HarmlessColor; break;
                case MsgLevel.Redirected: Console.ForegroundColor = RedirectedColor; break;
                case MsgLevel.Further: Console.ForegroundColor = FurtherColor; break;
                case MsgLevel.Critical: Console.ForegroundColor = CriticalColor; break;
                default: Console.ForegroundColor = DefaultColor; break;
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
            string msg = bPrefix ? ("[" + prefix + "] " + str) : str;
            //Color switch
            switch (level)
            {
                case MsgLevel.Harmless: Console.ForegroundColor = HarmlessColor; break;
                case MsgLevel.Redirected: Console.ForegroundColor = RedirectedColor; break;
                case MsgLevel.Further: Console.ForegroundColor = FurtherColor; break;
                case MsgLevel.Critical: Console.ForegroundColor = CriticalColor; break;
                default: Console.ForegroundColor = DefaultColor; break;
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

            Console.ForegroundColor = DefaultColor;
            Console.Write("Press Any Key To Continue...");
            Console.ReadKey();
        }
    }
}
