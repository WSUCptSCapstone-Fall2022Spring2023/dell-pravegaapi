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
            var b = Interop.test_box();
            Console.WriteLine(b);
        }
    }
}
