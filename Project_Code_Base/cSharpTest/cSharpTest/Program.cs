
 namespace Pravega
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Threading.Tasks;
    using Pravega;
    using Pravega.ClientFactoryModule;
    using Pravega.Config;

    public static class Program
    {
    

        static void Main()
        {
            ClientConfig testConfig = new ClientConfig();
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


            // test creating a thing
            //IntPtr clientFactoryObject = Interop.CreateClientFactoryTest();
            //Console.WriteLine(clientFactoryObject.ToString());
            //IntPtr runtimeObject = Interop.TestGetRuntime(clientFactoryObject);
            //Console.WriteLine(runtimeObject.ToString());

        }


    }
}