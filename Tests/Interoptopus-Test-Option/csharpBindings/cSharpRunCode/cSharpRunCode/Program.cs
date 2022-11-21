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
            Inner i;
            i.x = 10;
            var o = OptionInner.FromNullable(i);
            var option = Interop.pattern_ffi_option_1(o);
            //var inner = Opti
            Console.WriteLine(option.t.x);
            //var b = Interop.pattern_ffi_option_1
            //Console.WriteLine(b);
        }
    }
}

