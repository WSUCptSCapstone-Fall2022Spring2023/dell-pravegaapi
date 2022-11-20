using System;
using My.Company;

namespace cSharpRunCode
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
            var arr = Interop.test_arrays();
            Console.WriteLine(arr);
            //Console.WriteLine(ptr);
            //Increment ptr
            //ptr += 8;
            //Console.WriteLine(ptr.ToInt32());
        }
    }
}
