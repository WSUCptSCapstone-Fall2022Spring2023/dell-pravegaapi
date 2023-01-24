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

namespace Pravega.ClientFactory
{
    // Continues building the Interop class by adding method signatures found in Client Factory.
    public static partial class Interop {

        // Client Factory default constructor (default client config, generated runtime)
        [DllImport(NativeLib, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CreateClientFactory")]
        public static extern IntPtr CreateClientFactory();

    }

    /// Contains the class that wraps the Rust client factory struct through a pointer and .dll function calls.
    public class ClientFactory : RustStructWrapper
    {
        // Library for C# Wrapper method calls
        private const string PravegaCSharpWrapperLib = "..\\target\\debug\\PravegaCSharp.dll";

        // Override type to return this class's name.
        public virtual string Type(){
            return "ClientFactory";
        }

        // Default constructor. Initializes with a default ClientConfig
        public ClientFactory(){
            this._rustStructPointer = IntPtr.Zero;
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
        public virtual string Type(){
            return "ClientFactoryAsync";
        }
    }

}