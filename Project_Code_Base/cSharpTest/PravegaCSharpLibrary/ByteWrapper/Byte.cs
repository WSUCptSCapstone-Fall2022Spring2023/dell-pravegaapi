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
using static Pravega.Interop;
#pragma warning restore 0105

namespace Pravega.ClientFactoryModule
{
    // Continues building the Interop class by adding method signatures found in Byte.
    public static partial class Interop
    {

        // Set path of byte .dll specifically
        public const string ByteDLLPath = @"C:\Users\john_\Desktop\Programming\Senior Project CS421\dell-pravegaapi\dell-pravegaapi\Project_Code_Base\cSharpTest\PravegaCSharpLibrary\target\debug\deps\byte_wrapper.dll";

        ////////
        /// Byte Writer
        ////////
        // ByteWriter default constructor (default client config, generated runtime)
        [DllImport(ByteDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CreateByteWriter")]
        internal static extern IntPtr CreateByteWriter(IntPtr clientFactoryPointer, CustomRustString scope, CustomRustString stream, [MarshalAs(UnmanagedType.FunctionPtr)] rustCallback callback);

        // ByteWriter current offset getter
        [DllImport(ByteDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ByteWriterCurrentOffset")]
        internal static extern ulong ByteWriterCurrentOffset(IntPtr byteWriterPointer);

        ////////
        /// Byte Reader
        ////////
        // ByteReader current offset getter
         [DllImport(ByteDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ByteReaderCurrentOffset")]
        internal static extern ulong ByteReaderCurrentOffset(IntPtr byteWriterPointer);

    }

    /// <summary>
    ///     Allows for writing raw bytes directly to a segment.
    ///     Typically created from a ClientFactory.
    /// </summary>
    public class ByteWriter : RustStructWrapper{

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


    /// <summary>
    ///     A ByteReader enables reading raw bytes from a segment. 
    ///     Typically created from a ClientFactory.
    /// </summary>
    public class ByteReader : RustStructWrapper
    {
        /// <summary>
        ///  Default constructor for ByteReader. Initializes with this object's pointer set to zero.
        /// </summary>
        internal ByteReader()
        {
            this._rustStructPointer = IntPtr.Zero;
        }

        /// <summary>
        ///  Gets this object's current offset.
        /// </summary>
        public ulong CurrentOffset
        {
            get
            {
                if (!this.IsNull())
                {
                    return Interop.ByteReaderCurrentOffset(this.RustStructPointer);
                }
                else
                {
                    throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
                }
            }
        }
    }


}