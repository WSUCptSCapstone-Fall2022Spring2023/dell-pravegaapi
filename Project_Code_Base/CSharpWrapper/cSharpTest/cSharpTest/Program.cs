
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
            CustomCSharpString customCSharpTest = new CustomCSharpString(testString);
            Console.WriteLine("test convert from custom c#: " + customCSharpTest.ConvertToString());
            CustomRustString customRustString = customCSharpTest.ConvertToRustString();
            Interop.test(customRustString);
        }
    }
}