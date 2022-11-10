using System;
using System.Collections;
using System.Runtime.InteropServices;
using My.Company;

namespace cSharpRunCode
{
    class Program
    {
        static void Main(string[] args)
        {

            var x = Interop.test_closures(5);
            Console.WriteLine(x);
        }
    }
}

