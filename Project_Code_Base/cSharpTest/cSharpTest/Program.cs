
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
<<<<<<< HEAD
    using Pravega.Event;

=======
    using Pathgen;
>>>>>>> main
    public static class Program
    {

        static void Main()
        {
            //Sets where to look for DllImport to find the Dll files
            Environment.CurrentDirectory = Pathgen.PathGen.CreateDllPath();
            ClientConfig testConfig = new ClientConfig();

            //foreach (string thing in testConfig.TrustCerts)
            //{
            //    Console.WriteLine(thing);
            //}

            Console.WriteLine(testConfig.MaxConnectionsInPool.ToString());
            testConfig.MaxConnectionsInPool = 10;
            Console.WriteLine(testConfig.MaxConnectionsInPool.ToString());
            testConfig.MaxConnectionsInPool = 11;
            Console.WriteLine(testConfig.MaxConnectionsInPool.ToString());

            ClientFactory test = new ClientFactory(testConfig);

            Console.WriteLine(test.Runtime.ToString());
            Console.WriteLine(test.Handle.ToString());

            testConfig = new ClientConfig();
            Console.WriteLine(testConfig.MaxConnectionsInPool.ToString());
            testConfig.MaxConnectionsInPool = 10;
            ClientFactory test2 = new ClientFactory(testConfig, test.Runtime);

            testConfig = test2.Config;
            Console.WriteLine(testConfig.MaxConnectionsInPool.ToString());


            
            ByteReader tb = test.createByteReader(null);
            Console.WriteLine(tb.Type());
        }

    }
}