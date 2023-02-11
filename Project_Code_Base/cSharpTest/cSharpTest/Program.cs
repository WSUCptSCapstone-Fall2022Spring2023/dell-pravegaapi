
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


    using Pathgen;

    public static class Program
    {

        static void Main()
        {
            //Sets where to look for DllImport to find the Dll files
            Environment.CurrentDirectory = Pathgen.PathGen.CreateDllPath();

            ClientFactory testFactory = new ClientFactory();
            ScopedStream testSS = new ScopedStream();
            testSS.Scope.NativeString= "test";
            testSS.Stream.NativeString= "test";

            Task<ByteWriter> newWriter = testFactory.CreateByteWriter(testSS);
            ByteWriter result = newWriter.GetAwaiter().GetResult();

            Console.WriteLine("test");

        }

    }
}