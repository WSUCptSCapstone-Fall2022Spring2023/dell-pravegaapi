using System.Runtime.InteropServices;

namespace CSharpTestAllOneCall
{
    internal class Program
    {
        public const string NativeLib = @"E:\CptS421\dell-pravegaapi\Project_Code_Base\cSharpTest\PravegaCSharpLibrary\target\debug\deps\byte_wrapper.dll";
        [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "TestEverything")]
        public static extern void TestEverything();
        static void Main(string[] args)
        {
            Console.WriteLine("Beginning Rust Test");
            TestEverything();
            Console.WriteLine("Test Concluded");
        }
    }
}