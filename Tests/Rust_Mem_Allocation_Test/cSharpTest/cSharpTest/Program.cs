
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
            Memtest testStruct = Interop.mem_test_function();
            Console.WriteLine(testStruct.thing.ToString() + " " + testStruct.thing2.ToString());
        }
    }
}