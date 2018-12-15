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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ArLib.Logger
{
    public class LoggerIns
    {
        private string outputStr = "";

        public LoggerIns()
        {
            GuiConsole.CreateConsole();
        }

        public void ReleaseConsole()
        {
            GuiConsole.ReleaseConsole();
        }

        public void Log(string str)
        {
            Console.WriteLine(str);
        }

        public void Pause()
        {
            Console.WriteLine("PRESS ANY KEY TO CONTINUE.");
            Console.ReadKey();
        }
    }
}
