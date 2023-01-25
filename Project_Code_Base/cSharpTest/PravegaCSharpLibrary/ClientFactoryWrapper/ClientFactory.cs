///
/// File: ClientFactory.cs
/// File Creator: John Sbur
/// Purpose: Contains helper classes and methods that are used in the ClientFactory module.
///
#pragma warning disable 0105
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Pravega;
using Pravega.Utility;
using Pravega.Config;
#pragma warning restore 0105

namespace Pravega.ClientFactoryModule
{
    // Continues building the Interop class by adding method signatures found in Client Factory.
    public static partial class Interop {
        
        // Set path of ClientFactory .dll specifically
        public const string ClientFactoryDLLPath = @"C:\Users\john_\Desktop\Programming\Senior Project CS421\dell-pravegaapi\dell-pravegaapi\Project_Code_Base\cSharpTest\PravegaCSharpLibrary\target\debug\deps\client_factory_wrapper.dll";

        // Client Factory default constructor (default client config, generated runtime)
        [DllImport(ClientFactoryDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CreateClientFactory")]
        internal static extern IntPtr CreateClientFactory();

        // Client Factory constructor (inputted client config, generated runtime)
        [DllImport(ClientFactoryDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CreateClientFactoryFromConfig")]
        internal static extern IntPtr CreateClientFactoryFromConfig(IntPtr clientConfigPointer);

    }

    /// Contains the class that wraps the Rust client factory struct through a pointer and .dll function calls.
    public class ClientFactory : RustStructWrapper
    {
        // Override type to return this class's name.
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public virtual string Type(){
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
            return "ClientFactory";
        }

        // Default constructor. Initializes with a default ClientConfig
        public ClientFactory(){
            this._rustStructPointer = Interop.CreateClientFactory();
        }

        // Constructor. Initializes with a ClientConfig. Consumes ClientConfig (sets to null after)
        public ClientFactory(ClientConfig factoryConfig){

            // Grab pointer from factoryConfig. If it's null, then throw an exception
            if (factoryConfig.IsNull()){
                throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
            }
            else{
                // Input pointer into constructor
                this._rustStructPointer = Interop.CreateClientFactoryFromConfig(factoryConfig.RustStructPointer);

                // Mark ClientConfig as null
                factoryConfig.MarkAsNull();
            }             
        }

        // Constructor. Initializes with a ClientConfig and Runtime. Consumes ClientConfig and Runtime (sets to null after)
        public ClientFactory(ClientConfig factoryConfig, TokioRuntime factoryRuntime){
            this._rustStructPointer = IntPtr.Zero;
        }
    }

    /// Contains the class that wraps the Rust client factory async struct through a pointer and .dll function calls.
    public class ClientFactoryAsync : RustStructWrapper{
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public virtual string Type(){
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
            return "ClientFactoryAsync";
        }
    }

}