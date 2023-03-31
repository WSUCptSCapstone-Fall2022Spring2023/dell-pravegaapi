
 namespace Pravega
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading.Tasks;
    using Pravega;
    using Pravega.ClientFactoryModule;
    using Pravega.Config;
    using Pravega.Shared;
    using Pravega.Event;
    using Pravega.Byte;
    using Pravega.ControllerCli;
    using Pathgen;
    using Pravega.Utility;
    using PravegaWrapperTestProject;
    public static class Program
    {

        static void Main()
        {

            // ********* Sets where to look for DllImport to find the Dll files
            Environment.CurrentDirectory = Pathgen.PathGen.CreateDllPath();




            // ********* Testing variables           
            const string testScopeName = "testScope";
            const string testStreamName = "testStream";
            List<byte> testBytes = new List<byte>();
            ScopedStream testScopedStream= new ScopedStream();
            ulong testResultHolderU64 = 0;



            // ********* Initialize local variables  
            //  -testBytes
            testBytes.Clear();
            testBytes.Add(0);
            testBytes.Add(1);
            testBytes.Add(2);
            testBytes.Add(3);
            testBytes.Add(4);
            //  -testScopedStream
            testScopedStream.Scope = new CustomCSharpString(testScopeName);
            testScopedStream.Stream = new CustomCSharpString(testStreamName);




            // ********* ControllerClient Tests
            Console.WriteLine("Begin ControllerClient Tests:");

            //  -Create Scope
            if (PravegaCSharpTest.ControllerClientCreateScope(testScopeName) != true)
            {
                Console.WriteLine(" Create Scope Fail");
                throw new Exception();
            }
            Console.WriteLine(" Create Scope Pass");

            //  -Create Stream
            if (PravegaCSharpTest.ControllerClientCreateStream(testScopeName, testStreamName) != true){
                Console.WriteLine(" Create Stream Fail");
                throw new Exception();
            }
            Console.WriteLine(" Create Stream Pass");




            // ********* ByteWriter tests
            Console.WriteLine(Environment.NewLine + "Begin ControllerClient Tests:");

            //  -Constructor
            ByteWriter testWriter = PravegaCSharpTest.ByteWriterConstructorTest(testScopedStream);
            Console.WriteLine(" Create ByteWriter Pass");

            //  -Write
            testResultHolderU64 = testWriter.Write(testBytes).GetAwaiter().GetResult();
            Console.WriteLine(" Write Pass");

            //  -  



            ByteReader testReader = ClientFactory.CreateByteReader(testScopedStream).GetAwaiter().GetResult();

            Console.WriteLine("finish");
        }



    }
}