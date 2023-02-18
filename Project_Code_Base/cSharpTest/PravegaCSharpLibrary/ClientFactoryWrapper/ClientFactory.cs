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

    /// <summary>
    ///  Testing class. Measures time it takes to execute an equivalent function in Rust.
    ///     Time returned represents execution time in nanoseconds.
    ///  
    ///  Ex. C# contains ClientFactory.ToAsync()
    ///   Rust also contains a method called to_async() that is ran from
    ///   a clientfactory. The function ToAsyncTime() runs that function 
    ///   in a Rust .dll call and returns how long it took to execute.
    /// </summary>
    public static class ClientFactoryTestMethods
    {
        /// <summary>
        ///   Return time it takes in Rust to run "to_async"
        /// </summary>
        /// <returns>
        ///  Execution time in nanoseconds
        /// </returns>
        public static float ToAsyncTime(){
            return Interop.ClientFactoryToAsyncTime();
        }

        /// <summary>
        ///  Return time it takes in Rust to run "config"
        /// </summary>
        /// <returns>
        ///  Execution time in nanoseconds
        /// </returns> 
        public static float ConfigTime()
        {
            return Interop.ClientFactoryToAsyncTime();
        }

        /// <summary>
        ///   Return time it takes in Rust to run "runtime_handle"
        /// </summary>
        /// <returns>
        ///  Execution time in nanoseconds
        /// </returns>
        public static float HandleTime(){
            return Interop.GetClientFactoryRuntimeHandleTime();
        }

        /// <summary>
        ///   Return time it takes in Rust to run "runtime"
        /// </summary>
        /// <returns>
        ///  Execution time in nanoseconds
        /// </returns>
        public static float RuntimeTime(){
            return Interop.GetClientFactoryRuntimeTime();
        }
         
        /// <summary>
        ///   Return time it takes in Rust to run the client factory constructor that takes a runtime and config
        /// </summary>
        /// <returns>
        ///  Execution time in nanoseconds
        /// </returns>
        public static float ConstructorConfigAndRuntimeTime(){
            return Interop.CreateClientFactoryFromConfigAndRuntimeTime();
        }

        /// <summary>
        ///   Return time it takes in Rust to run the client factory constructor that takes a config
        /// </summary>
        /// <returns>
        ///  Execution time in nanoseconds
        /// </returns>
        public static float ConstructorConfigTime(){
            return Interop.CreateClientFactoryFromConfigTime();
        }

        /// <summary>
        ///   Return time it takes in Rust to the the client factory default constructor
        /// </summary>
        /// <returns>
        ///  Execution time in nanoseconds
        /// </returns>
        public static float DefaultConstructorTime(){
            return Interop.CreateClientFactoryTime();
        }
    }

    /// <summary>
    ///  Applications should use ClientFactory to create resources they need.
    ///
    ///  ClientFactory contains a connection pool that is shared by all the 
    ///    readers and writers it creates. It also contains a tokio runtime that is 
    ///    used to drive async tasks. Spawned tasks in readers and writers are tied 
    ///    to this runtime. Tokio runtime represents the asynchronous execution
    ///    environement that client factory will execute asynchronous functions from.
    ///     
    /// </summary>
    public class ClientFactory : RustStructWrapper
    {
        /// <summary>
        ///  Default Constructor. Initializes ClientFactory with a default ClientConfig and
        ///  a generated config.
        ///  LocalHost = 9090
        /// 
        ///  Default configuration:
        ///   MaxConnectionsInPool = uint.MaxValue
        ///   MaxControllerConnections = 3
        ///   ConnectionType = Tokio
        ///   RetryPolicy = new RetryWithBackoff(); // Default constructor
        ///   TransactionTimeoutTime = 9000
        ///   Mock = false
        ///   IsTlsEnabled = (determined based on application)
        ///   IsAuthEnabled = false
        ///   RequestTimeout = (determined based on application)
        /// </summary>
        public ClientFactory(){
            this._rustStructPointer = Interop.CreateClientFactory();
        }

        /// <summary>
        ///  Constructor. Initializes with a ClientConfig.
        ///     Consumes ClientConfig (sets to null after)
        /// </summary>
        /// <param name="factoryConfig">
        ///  Config to base the client factory on.
        /// </param>
        /// <exception cref="PravegaException">
        ///  This error is thrown when an object has a bad
        ///     reference or no reference. In this case, the
        ///     inputted clientconfig was set to null.
        /// </exception>
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

        /// <summary>
        ///  Constructor. Initializes with a ClientConfig.
        ///     Consumes ClientConfig (sets to null after)
        ///     Consumes Runtime (sets to null after)
        /// </summary>
        /// <param name="factoryConfig">
        ///     Config to base the client factory on.
        /// </param>
        /// <param name="factoryRuntime">
        ///     Runtime this factory will execute in.
        /// </param>
        /// <exception cref="PravegaException">
        ///  This error is thrown when an object has a bad
        ///     reference or no reference. In this case, the
        ///     either one or more inputs were set to null.
        /// </exception>        
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
        /// <summary>
        ///  Gets the runtime object that this clientfactory's asynchronous operations 
        ///  execute on.
        /// </summary>
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
        /// <summary>
        ///  Gets the handle of the runtime object that this clientfactory's asynchronous
        ///  operations execute on.
        /// </summary>
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
        /// <summary>
        /// Gets the configuration settings of this client factory.
        /// </summary>
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
        /// <summary>
        ///   Gets the asynchronous factory this object is tied to, clones it, and returns
        ///   it.
        /// </summary>
        /// <returns>
        ///   A clone of this object's ClientFactoryAsync.
        /// </returns>
        /// <exception cref="PravegaException">
        ///   This error is thrown if this object is set to null or uninitialized.
        /// </exception>
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

        /// <summary>
        ///  Creates a new byte writer from a ScopedStream input and uses this object's
        ///  ClientFactoryAsync.
        /// </summary>
        /// <param name="writerScopedStream">
        ///  ScopedStream to base the bytewriter on.
        /// </param>
        /// <returns>
        ///  Newly created bytewriter running on this clientfactory's runtime.
        /// </returns>
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