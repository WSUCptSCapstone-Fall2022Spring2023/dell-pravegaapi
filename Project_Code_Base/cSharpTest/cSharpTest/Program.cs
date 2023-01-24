
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

    public static class Program
    {
    

        static void Main()
        {

            ClientFactory test = new ClientFactory();
            // test creating a thing
            //IntPtr clientFactoryObject = Interop.CreateClientFactoryTest();
            //Console.WriteLine(clientFactoryObject.ToString());
            //IntPtr runtimeObject = Interop.TestGetRuntime(clientFactoryObject);
            //Console.WriteLine(runtimeObject.ToString());

        }

    
    }
}