//=============================================================================
// ArLIB Library Test Application
// Introduction :
//  Useless, just write to test my lib DLL.
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
using System.Threading.Tasks;
using System.Windows.Forms;
using ArLib.Logger;
using System.Runtime.InteropServices;

namespace ArLib.LibTestApp
{
    static class Entrance
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var logger = new LogConsole(true, "LIB Test");

            logger.Log("Default Message Color. Using default parameter.");
            logger.Log("Default Message Color. Assigned parameter.", MsgLevel.Default);
            logger.Log("Harmless Message Color.", MsgLevel.Harmless);
            logger.Log("Further Message Color.", MsgLevel.Further);
            logger.Log("Critical Message Color.", MsgLevel.Critical);
            logger.CRLF();
            logger.Log("Time Stamp: " + DateTime.Now.ToString("yyyyMMddHHmmssffff"));

            logger.ExecuteCMD(@"NSLOOKUP -QT=A JP1.AR-DISTRIBUTED.COM 8.8.8.8");
            logger.ExecuteCMD(@"TASKLIST | FINDSTR AWCC");
            logger.Pause();

            logger.ReleaseConsole();

            return;
        }
    }
}
