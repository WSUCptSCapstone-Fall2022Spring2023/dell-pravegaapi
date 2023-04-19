
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
    using NUnit.Framework;
    using System.Security.Cryptography.X509Certificates;

    public static class Program
    {

        static void Main()
        {

            // ********* Sets where to look for DllImport to find the Dll files
            Environment.CurrentDirectory = Pathgen.PathGen.CreateDllPath();
            Console.WriteLine("finish");
            ClientFactory.Initialize();
            ControllerClient testController = ClientFactory.FactoryControllerClient;

            // Create a scope to base the stream on.
            Scope testScope = new Scope();
            testScope.NativeString = "testScope";
            testController.CreateScope(testScope).GetAwaiter().GetResult();

            // Create a stream config to control the stream
            StreamConfiguration streamConfiguration = new StreamConfiguration();
            streamConfiguration.ConfigScopedStream.Scope = testScope;
            streamConfiguration.ConfigScopedStream.Stream = new CustomCSharpString("testStream");

            // Create the stream
            testController.CreateStream(streamConfiguration).GetAwaiter().GetResult();

            // Testing variables
            List<byte> testList = new List<byte>();
            testList.Clear();
            testList.Add(0);
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);

            // Create a byte writer
            ByteWriter testWriter = ClientFactory.CreateByteWriter(streamConfiguration.ConfigScopedStream).GetAwaiter().GetResult();

            // Attempt to write to byte writer
            ulong result = testWriter.Write(testList).GetAwaiter().GetResult();

            // Create the ByteWriter
            ByteReader testReader = ClientFactory.CreateByteReader(streamConfiguration.ConfigScopedStream).GetAwaiter().GetResult();

            byte[] bytes = testReader.Read(4).GetAwaiter().GetResult();

            Console.WriteLine("finish");
        }



    }
}