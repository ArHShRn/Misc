//=============================================================================
// Arguments Displayer
// Introduction :
//  Why are you here?
//  I just wrote this for myself...
// 
// Code And Concept By ArHShRn
// https://github.com/ArHShRn
//
// Release Log :
//  Add comments.
//
// Last Update :
//  Dec.15th 2018
//=============================================================================
using System;

namespace ARGDisplayer
{
    class ARGDisplayer
    {
        static void Main(string[] args)
        {
            int count = 0;

            Console.WriteLine("//---------------------------------------------------------------");
            Console.WriteLine("//              Arguments Displayer X64 Platform");
            Console.WriteLine("// ");
            Console.WriteLine("//           Run This With Arguments To Display Them");
            Console.WriteLine("//         Author:ArHShRn @ https://github.com/ArHShRn");
            Console.WriteLine("// ");
            Console.WriteLine("//---------------------------------------------------------------\n");
            Console.WriteLine("--INDEX--    --ARGS--");
            foreach (var arg in args)
            {
                Console.WriteLine("    {0}          {1}", count, arg);
                count++;
            }
            if (count == 0)
            {

                Console.WriteLine("   NIL         NULL\n");
                Console.WriteLine("//---------------------------------------------------------------\n");
                Console.Write("RUN THIS WITH ARGUMENTS TO DISPLAY THEM.\nPRESS ANY KEY TO EXIT.");
                Console.ReadKey();
                return;
            }
            Console.WriteLine("//---------------------------------------------------------------");
            Console.Write("PRESS ANY KEY TO EXIT.");
            Console.ReadKey();
            return;
        }
    }
}
