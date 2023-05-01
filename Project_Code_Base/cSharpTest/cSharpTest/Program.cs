
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
    using Pravega.ControllerCli;


    using Pathgen;
    using Pravega.Utility;

    public static class Program
    {

        static void Main()
        {
            //Sets where to look for DllImport to find the Dll files
            Environment.CurrentDirectory = Pathgen.PathGen.CreateDllPath();

            ClientFactory.Initialize();

            ControllerClient testClient = ClientFactory.FactoryControllerClient;

            Scope testScope = new Scope();
            testScope.NativeString = "testScope2";
            testClient.CreateScope(testScope).GetAwaiter().GetResult();

            Console.WriteLine("test");
            
            StreamConfiguration streamConfiguration = new StreamConfiguration();
            streamConfiguration.ConfigScopedStream.Scope = testScope;
            streamConfiguration.ConfigScopedStream.Stream = new CustomCSharpString("testStream2");

            testClient.CreateStream(streamConfiguration).GetAwaiter().GetResult();

            ByteWriter testWriter = ClientFactory.CreateByteWriter(streamConfiguration.ConfigScopedStream).GetAwaiter().GetResult();

            List<byte> testBytes = new List<byte>();
            testBytes.Add(0);
            testBytes.Add(1);
            testBytes.Add(2);
            testBytes.Add(3);
            Console.WriteLine(testWriter.Write(testBytes).GetAwaiter().GetResult().ToString());
            Console.WriteLine("Testing EventWriter");
            EventWriter e = ClientFactory.CreateEventWriter(streamConfiguration.ConfigScopedStream).GetAwaiter().GetResult();
            Console.WriteLine("Testing EventWriter:WriteRoutingKey");
            e.WriteRoutingKey(testBytes, "testKey");
            Console.WriteLine("Testing ReaderGroup");
            ReaderGroup r = ClientFactory.CreateReaderGroup(streamConfiguration.ConfigScopedStream).GetAwaiter().GetResult();
            Console.WriteLine("Testing ByteReader");
            ByteReader testReader = ClientFactory.CreateByteReader(streamConfiguration.ConfigScopedStream).GetAwaiter().GetResult();

            Console.WriteLine("finish");
        }



    }
}