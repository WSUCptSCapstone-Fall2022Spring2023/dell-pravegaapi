
 namespace Pravega
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading.Tasks;

    public static class Program
    {
        static void Main()
        {
            // Showing taking a string from C# and turning it into a CustomString for sending into Rust
            string testString = "test";
            U16Slice test;
            test.slice_pointer = Marshal.StringToHGlobalAnsi(testString);
            test.length = (ulong)testString.Length;
            CustomCSharpString testCustomString = new CustomCSharpString();
            testCustomString.string_slice = test;
            for (ulong i = 0; i < testCustomString.string_slice.length; i++)
            {
                Console.WriteLine((char)testCustomString.string_slice[(int)i]);
            }
        }
    }
}