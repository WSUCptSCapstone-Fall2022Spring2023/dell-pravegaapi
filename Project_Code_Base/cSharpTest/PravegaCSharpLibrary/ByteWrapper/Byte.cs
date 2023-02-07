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
    // Continues building the Interop class by adding method signatures found in Byte.
    public static partial class Interop
    {

        // Set path of byte .dll specifically
        public const string ByteDLLPath = @"E:\CptS421\dell-pravegaapi\Project_Code_Base\cSharpTest\PravegaCSharpLibrary\target\debug\deps\byte_wrapper.dll";

        ////////
        /// Byte
        ////////
        // ByteReader default constructor (default client config, generated runtime)
        [DllImport(ByteDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CreateByteReaderHelper")]
        internal static extern IntPtr CreateByteReader(IntPtr l);


        ////////
        /// 
        ////////
    }

    /// Contains the class that wraps the Rust client factory struct through a pointer and .dll function calls.
    public class ByteReader : RustStructWrapper
    {
        // Override type to return this class's name.
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public virtual string Type()
        {
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
            return "ByteReader";
        }

        public ByteReader(ScopedStream s, IntPtr clientFactory)

        {
 
                this.RustStructPointer = Interop.CreateByteReader(clientFactory);
            
        }

    }


}