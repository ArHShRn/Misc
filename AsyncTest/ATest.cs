using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using ArLib.ARConsole;
using System.Reflection;

namespace AsyncTest
{

    class Program
    {
        static void Main(string[] args)
        {
            LogHelper.CreateConsole(false, "AR", false);
            LogHelper.ReleaseConsole();
            Thread.Sleep(2000);
        }
    }
}
