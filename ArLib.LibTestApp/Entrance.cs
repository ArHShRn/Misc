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
using System.Threading;
using System.Windows.Forms;
using ArLib.ARConsole;

namespace ArLib.LibTestApp
{
    static class Entrance
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //Application.Run(new MyForm());

            var logger = new LogHelper(true, "MyGuiConsole", true);
            logger.ExecuteCMD("NSLOOKUP -QT=A JP1.AR-DISTRIBUTED.COM 8.8.8.8");

            //logger.AsyncExecuteCMD("DIR");
            //logger.AsyncExecuteCMD("TASKLIST | FINSTR AWCC");
            //logger.AsyncExecuteCMD("NSLOOKUP -QT=A JP1.AR-DISTRIBUTED.COM 8.8.8.8");
            //logger.AsyncExecuteCMD("NSLOOKUP -QT=A HK1.AR-DISTRIBUTED.COM 8.8.8.8");
            //logger.AsyncExecuteCMD("NSLOOKUP -QT=A RU1.AR-DISTRIBUTED.COM 8.8.8.8");
            //logger.AsyncExecuteCMD("NSLOOKUP -QT=A CN1.AR-DISTRIBUTED.COM 8.8.8.8");
            //logger.AsyncExecuteCMD("NSLOOKUP -QT=A CN2.AR-DISTRIBUTED.COM 8.8.8.8");
            //logger.AsyncExecuteCMD("NSLOOKUP -QT=A CN3.AR-DISTRIBUTED.COM 8.8.8.8");
            //logger.AsyncExecuteCMD("NSLOOKUP -QT=A CN3KF1.AR-DISTRIBUTED.COM 8.8.8.8");
            //logger.AsyncExecuteCMD("TIMEOUT -T 3");

            //logger.Log("Current thread sleeps for 1 sec...", MsgLevel.Further);

            //logger.ReleaseConsole();
            logger.Pause();
        }
    }
}
