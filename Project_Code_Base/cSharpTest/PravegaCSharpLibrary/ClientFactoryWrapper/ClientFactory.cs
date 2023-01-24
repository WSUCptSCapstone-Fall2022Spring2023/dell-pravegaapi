///
/// File: ClientFactory.cs
/// File Creator: John Sbur
/// Purpose: Contains helper structs that are used in the ClientFactory module.
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
        
        // Client Factory default constructor (default client config, generated runtime)
        [DllImport(WrapperConstants.RustDllPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CreateClientFactory")]
        internal static extern IntPtr CreateClientFactory();

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
            this._rustStructPointer = IntPtr.Zero;
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