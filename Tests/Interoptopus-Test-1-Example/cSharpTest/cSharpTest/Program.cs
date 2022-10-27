
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
            Vec2 test;
            test.x = 1;
            test.y = 2;
            Console.WriteLine("testing vec2(1,2). returned x = " + Interop.my_function(test).x.ToString() + " returned y = " + Interop.my_function(test).y.ToString());
        }
    }
}