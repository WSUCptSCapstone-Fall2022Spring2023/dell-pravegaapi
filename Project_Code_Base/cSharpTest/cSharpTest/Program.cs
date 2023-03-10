
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
            testScope.NativeString = "testScope";
            testClient.CreateScope(testScope).GetAwaiter().GetResult();

            Console.WriteLine("test");
            
            StreamConfiguration streamConfiguration = new StreamConfiguration();
            streamConfiguration.ConfigScopedStream.Scope = testScope;
            streamConfiguration.ConfigScopedStream.Stream = new CustomCSharpString("testStream");

            testClient.CreateStream(streamConfiguration).GetAwaiter().GetResult(); 

            ClientFactory.CreateByteWriter(streamConfiguration.ConfigScopedStream).GetAwaiter().GetResult();
            Console.WriteLine("test3");
            ClientFactory.CreateEventWriter(streamConfiguration.ConfigScopedStream).GetAwaiter().GetResult();
            Console.WriteLine("test3");
            ClientFactory.CreateReaderGroup(streamConfiguration.ConfigScopedStream).GetAwaiter().GetResult();
        }



    }
}