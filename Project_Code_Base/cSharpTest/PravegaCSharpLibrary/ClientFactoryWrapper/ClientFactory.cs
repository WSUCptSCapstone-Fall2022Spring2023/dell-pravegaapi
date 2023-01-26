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
using Pravega.Index;
using Pravega.Shared;
using Pravega.Event;
#pragma warning restore 0105

namespace Pravega.ClientFactoryModule
{
    // Continues building the Interop class by adding method signatures found in Client Factory.
    public static partial class Interop {
        
        // Set path of ClientFactory .dll specifically
        public const string ClientFactoryDLLPath = @"E:\CptS421\dell-pravegaapi\Project_Code_Base\cSharpTest\PravegaCSharpLibrary\target\debug\deps\client_factory_wrapper.dll";

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

        ////////
        /// 
        ////////
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
                    Console.WriteLine("debug: runtime pointer = " + runtimeObject.RustStructPointer.ToString());
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
        //Spawns an IndexReader with a ScopedStream as input
        //Full Functionality not yet implemented
        public IndexReader creatIndexReader(ScopedStream s)
        {
            return new IndexReader(s);
        }
        //Spawns an IndexWriter with a ScopedStream as input
        //Full Functionality not yet implemented
        public IndexWriter createIndexWriter(ScopedStream s)
        {
            return new IndexWriter(s);
        }
        //Spawns an EventReader with a ScopedStream as input
        //Full Functionality not yet implemented
        public EventReader creatEventReader(ScopedStream s)
        {
            return new EventReader(s);
        }
        //Spawns an EventWRiter with a ScopedStream as input
        //Full Functionality not yet implemented
        public EventWriter createEventWriter(ScopedStream s)
        {
            return new EventWriter(s);
        }

        public ByteReader createByteReader(ScopedStream s)
        {
            return new ByteReader(s,this.RustStructPointer);
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