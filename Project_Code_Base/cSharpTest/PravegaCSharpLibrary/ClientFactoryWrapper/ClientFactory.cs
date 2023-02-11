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
using Pravega.Shared;
#pragma warning restore 0105

namespace Pravega.ClientFactoryModule
{
    // Continues building the Interop class by adding method signatures found in Client Factory.
    public static partial class Interop {

        // Set path of ClientFactory .dll specifically
        public const string ClientFactoryDLLPath = @"C:\Users\john_\Desktop\Programming\Senior Project CS421\dell-pravegaapi\dell-pravegaapi\Project_Code_Base\cSharpTest\PravegaCSharpLibrary\target\debug\deps\client_factory_wrapper.dll";
        //public const string ClientFactoryDLLPath = @"C:\Users\brand\Documents\Capstone\dell-pravegaapi\Project_Code_Base\cSharpTest\PravegaCSharpLibrary\target\debug\deps\client_factory_wrapper.dll";
        //public const string ClientFactoryDLLPath = "client_factory_wrapper.dll";
        ////////
        /// Client Factory
        ////////
        // Client Factory default constructor (default client config, generated runtime)
        [DllImport(ClientFactoryDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CreateClientFactory")]
        internal static extern IntPtr CreateClientFactory();

        // Client Factory constructor (inputted client config, generated runtime)
        [DllImport(ClientFactoryDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CreateClientFactoryFromConfig")]
        internal static extern IntPtr CreateClientFactoryFromConfig(IntPtr clientConfigPointer);

        // Client Factory constructor (inputted client config, inputted runtime)
        [DllImport(ClientFactoryDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CreateClientFactoryFromConfigAndRuntime")]
        internal static extern IntPtr CreateClientFactoryFromConfigAndRuntime(IntPtr clientConfigPointer, IntPtr runtimePointer);

        // Getters and Setters
        // ClientFactory.runtime
        [DllImport(ClientFactoryDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetClientFactoryRuntime")]
        internal static extern IntPtr GetClientFactoryRuntime(IntPtr sourceClientFactory);

        // ClientFactory.runtime_handle
        [DllImport(ClientFactoryDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetClientFactoryRuntimeHandle")]
        internal static extern IntPtr GetClientFactoryRuntimeHandle(IntPtr sourceClientFactory);

        // ClientFactory.runtime_handle
        [DllImport(ClientFactoryDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetClientFactoryConfig")]
        internal static extern IntPtr GetClientFactoryConfig(IntPtr sourceClientFactory);

        // ClientFactory.to_async()
        [DllImport(ClientFactoryDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ClientFactoryToAsync")]
        internal static extern IntPtr ClientFactoryToAsync(IntPtr sourceClientFactory);

        // ClientFactory testing functions
        // ClientFactory default constructor time take to complete in milliseconds in rust
        [DllImport(ClientFactoryDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CreateClientFactoryTime")]
        internal static extern ulong CreateClientFactoryTime();

        // ClientFactory.new with config time take to complete in milliseconds in rust
        [DllImport(ClientFactoryDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CreateClientFactoryFromConfigTime")]
        internal static extern ulong CreateClientFactoryFromConfigTime();

        // ClientFactory.new_with_runtime time take to complete in milliseconds in rust
        [DllImport(ClientFactoryDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CreateClientFactoryFromConfigAndRuntimeTime")]
        internal static extern ulong CreateClientFactoryFromConfigAndRuntimeTime();
        
        // ClientFactory.runtime time take to complete in milliseconds in rust
        [DllImport(ClientFactoryDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetClientFactoryRuntimeTime")]
        internal static extern ulong GetClientFactoryRuntimeTime();
        
        // ClientFactory.handle time take to complete in milliseconds in rust
        [DllImport(ClientFactoryDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetClientFactoryRuntimeHandleTime")]
        internal static extern ulong GetClientFactoryRuntimeHandleTime();
        
        // ClientFactory.config time take to complete in milliseconds in rust
        [DllImport(ClientFactoryDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetClientFactoryConfigTime")]
        internal static extern ulong GetClientFactoryConfigTime();
        
        // to_async time take to complete in milliseconds in rust
        [DllImport(ClientFactoryDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ClientFactoryToAsyncTime")]
        internal static extern ulong ClientFactoryToAsyncTime();


        ////////
        /// 
        ////////
    }

    // Static class with helper functions used for testing ClientFactory
    public static class ClientFactoryTestMethods
    {
        // Return time it takes in Rust to run "to_async"
        public static float ToAsyncTime(){
            return Interop.ClientFactoryToAsyncTime();
        }
        // Return time it takes in Rust to run "config"
        public static float ConfigTime()
        {
            return Interop.ClientFactoryToAsyncTime();
        }
        // Return time it takes in Rust to run "runtime_handle"
        public static float HandleTime(){
            return Interop.GetClientFactoryRuntimeHandleTime();
        }
        // Return time it takes in Rust to run "runtime"
        public static float RuntimeTime(){
            return Interop.GetClientFactoryRuntimeTime();
        }
        // Return time it takes in Rust to run the client factory constructor that takes a runtime and config
        public static float ConstructorConfigAndRuntimeTime(){
            return Interop.CreateClientFactoryFromConfigAndRuntimeTime();
        }
        // Return time it takes in Rust to run the client factory constructor that takes a config
        public static float ConstructorConfigTime(){
            return Interop.CreateClientFactoryFromConfigTime();
        }
        // Return time it takes in Rust to the the client factory default constructor
        public static float DefaultConstructorTime(){
            return Interop.CreateClientFactoryTime();
        }
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

            // Grab pointer from factoryConfig. If it's null, then throw an exception
            if (factoryConfig.IsNull() || factoryRuntime.IsNull()){
                throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
            }
            else{
                // Input pointer into constructor
                this._rustStructPointer = Interop.CreateClientFactoryFromConfigAndRuntime(
                    factoryConfig.RustStructPointer,
                    factoryRuntime.RustStructPointer
                );

                // Mark ClientConfig and Runtime as null
                factoryConfig.MarkAsNull();
                factoryRuntime.MarkAsNull();
            }
        }


        // Setters and Getters
        public TokioRuntime Runtime{
            get
            {
                if (this.IsNull()){
                    throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
                }
                else{
                    IntPtr runtimePointer = Interop.GetClientFactoryRuntime(this._rustStructPointer);
                    TokioRuntime runtimeObject = new TokioRuntime();
                    runtimeObject.RustStructPointer = runtimePointer;

                    // debug
                    //Console.WriteLine("debug: runtime pointer = " + runtimeObject.RustStructPointer.ToString());
                    return runtimeObject;
                }
            }
        }
        public TokioHandle Handle{
            get
            {
                if (this.IsNull()){
                    throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
                }
                else{
                    IntPtr runtimePointer = Interop.GetClientFactoryRuntimeHandle(this._rustStructPointer);
                    TokioHandle runtimeObject = new TokioHandle();
                    runtimeObject.RustStructPointer = runtimePointer;

                    // debug
                    //Console.WriteLine("debug: handle pointer = " + runtimeObject.RustStructPointer.ToString());
                    return runtimeObject;
                }
            }
        }
        public ClientConfig Config{
            get
            {
                if (this.IsNull()){
                    throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
                }
                else{
                    IntPtr runtimePointer = Interop.GetClientFactoryConfig(this._rustStructPointer);
                    ClientConfig runtimeObject = new ClientConfig();
                    runtimeObject.RustStructPointer = runtimePointer;

                    // debug
                    //Console.WriteLine("debug: config pointer = " + runtimeObject.RustStructPointer.ToString());
                    return runtimeObject;
                }
            }     
        }
        

        // Methods
        // Clones and returns a copy of this client factory's client factory async.
        public ClientFactoryAsync ToAsync()
        {
            if (this.IsNull()){
                throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
            }
            else{
                IntPtr clientFactoryAsyncClone = Interop.ClientFactoryToAsync(this._rustStructPointer);
                ClientFactoryAsync newClientFactoryAsync = new ClientFactoryAsync();
                newClientFactoryAsync.RustStructPointer = clientFactoryAsyncClone;

                return newClientFactoryAsync;
            }
        }

        // Creates a Byte Writer using this object's client factory async.
        public async Task<ByteWriter> CreateByteWriter(ScopedStream writerScopedStream){

            ByteWriter returnWriter = new ByteWriter();
            await returnWriter.InitializeByteWriter(this.ToAsync(), writerScopedStream);
            return returnWriter;
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