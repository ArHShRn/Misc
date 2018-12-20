//=============================================================================
// ArLIB Library Test Application
// Introduction :
//  Useless, just write to test my lib DLL.
// 
// Code And Concept By ArHShRn
// https://github.com/ArHShRn
//
//=============================================================================

using System;
using System.Threading;
using System.Windows.Forms;
using ArLib.ARConsole;

namespace ArLib.LibTestApp
{
    static class Entrance
    {
        static void Main()
        {
            LogHelper.CreateConsole(true, "Library Test", true, "This is a init title");
            LogHelper.Log("Test Log");
            LogHelper.AsyncExecuteCMD("NSLOOKUP -QT=A WWW.BAIDU.COM 8.8.8.8");

            Thread.Sleep(10000);
        }
    }
}
