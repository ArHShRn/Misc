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
            LogHelper.CreateConsole(true, "Library Test", true, "Just A Test QAQ");

            Thread.Sleep(10000);
        }
    }
}
