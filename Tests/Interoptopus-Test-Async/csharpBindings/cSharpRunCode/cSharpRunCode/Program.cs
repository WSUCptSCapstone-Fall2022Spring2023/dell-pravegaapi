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
            Interop.test_async(600851475143);
            Interop.test_async(1001);
            Console.WriteLine("Hello World");
        }
    }
}

