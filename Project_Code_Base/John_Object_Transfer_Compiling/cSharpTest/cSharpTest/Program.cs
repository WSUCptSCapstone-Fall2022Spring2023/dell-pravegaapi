
 namespace My.Company
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class Program
    {
        static void Main()
        {
            Console.WriteLine(Environment.CurrentDirectory);
            Console.WriteLine("testing vec2(1,2). returned x = " + Interop.mem_test_function().thing.ToString() + " returned y = " + Interop.mem_test_function().thing2.ToString());
        }
    }
}