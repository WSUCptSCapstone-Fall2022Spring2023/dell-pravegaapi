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

namespace Pravega.Sync
{
    // Continues building the Interop class by adding method signatures found in Sync.
    public static partial class Interop
    {

        // Set path of sync .dll specifically
        public const string SyncDLLPath = @"E:\CptS421\dell-pravegaapi\Project_Code_Base\cSharpTest\PravegaCSharpLibrary\target\debug\deps\byte_wrapper.dll";

        ////////
        /// sync
        ////////
        // ByteReader default constructor (default client config, generated runtime)
        [DllImport(SyncDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CreateByteReaderHelper")]
        internal static extern IntPtr CreateByteReader(IntPtr l);


        ////////
        /// 
        ////////
    }

    /// Contains the class that wraps the Rust sync struct through a pointer and .dll function calls.
    public class Table : RustStructWrapper
    {
        // Override type to return this class's name.
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public virtual string Type()
        {
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
            return "Table";
        }

        public Table(Scope s, string placeholder)

        {

            this.RustStructPointer = IntPtr.Zero;
            
        }

    }

    public class Synchronizer : RustStructWrapper
    {
        // Override type to return this class's name.
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public virtual string Type()
        {
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
            return "Synchronizer";
        }

        public Synchronizer(Scope s, string placeholder)

        {

            this.RustStructPointer = IntPtr.Zero;

        }

    }


}