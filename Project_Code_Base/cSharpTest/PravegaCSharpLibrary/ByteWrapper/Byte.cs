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
using System.Runtime.CompilerServices;
#pragma warning restore 0105

namespace Pravega.ClientFactoryModule
{
    // Continues building the Interop class by adding method signatures found in Byte.
    public static partial class Interop
    {

        // Set path of byte .dll specifically
        public const string ByteDLLPath = "byte_wrapper.dll";//@"C:\Users\john_\Desktop\Programming\Senior Project CS421\dell-pravegaapi\dell-pravegaapi\Project_Code_Base\cSharpTest\PravegaCSharpLibrary\target\debug\deps\byte_wrapper.dll";

        ////////
        /// Byte
        ////////
        public delegate void rustCallback(IntPtr arg);
        // ByteReader default constructor (default client config, generated runtime)
        [DllImport(ByteDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CreateByteWriter")]
        internal static extern IntPtr CreateByteWriter(IntPtr clientFactoryPointer, CustomRustString scope, CustomRustString stream, rustCallback callback);

        [DllImport(ByteDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ByteWriterCurrentOffset")]
        internal static extern ulong ByteWriterCurrentOffset(IntPtr byteWriterPointer);


        ////////
        /// 
        ////////
    }

    public class ByteWriter : RustStructWrapper{

        // Override type to return this class's name.
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public virtual string Type()
        {
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
            return "ByteWriter";
        }

        // Default constructor for byte writer. Initializes with no pointer
        internal ByteWriter()
        {
            this._rustStructPointer = IntPtr.Zero;
        }

        internal async Task InitializeByteWriter(
            ClientFactoryAsync spawnFactory,
            ScopedStream writerScopedStream
        )
        {
            if (!spawnFactory.IsNull())
            {
                IntPtr byteWriterPointer = await GenerateByteWriterHelper(spawnFactory, writerScopedStream);
                this._rustStructPointer = byteWriterPointer;
            }
            else
            {
                throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
            }
        }
        private Task<IntPtr> GenerateByteWriterHelper(
            ClientFactoryAsync spawnFactory,
            ScopedStream writerScopedStream
        )
        {
            TaskCompletionSource<IntPtr> task = new TaskCompletionSource<IntPtr>();
            Interop.CreateByteWriter(
                spawnFactory.RustStructPointer,
                writerScopedStream.Scope.RustString,
                writerScopedStream.Stream.RustString,
                (value) => {
                    task.SetResult(value);
                }
            );
            return task.Task;
        }

        public ulong CurrentOffset{
            get{
                if (!this.IsNull()){
                    return Interop.ByteWriterCurrentOffset(this.RustStructPointer);
                }
                else{
                    throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
                }
            }
        }
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


    }


}