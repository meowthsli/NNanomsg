﻿using System.Runtime.InteropServices;
using NNanomsg;

namespace Test
{
    class Program
    {
        private static void Main(string[] args)
        {
            Test_Listener.Execute();
            Test_Symbol.Execute();
            Test_Pair.Execute();
            Test_ReqRep.Execute();
            Test_GetSetOptions.Execute();
        }
    }
}
