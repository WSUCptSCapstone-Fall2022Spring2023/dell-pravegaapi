﻿///
/// File: ClientFactory.cs
/// File Creator: John Sbur
/// Purpose: Contains helper classes and methods that are used in the ClientFactory module.
///
#pragma warning disable 0105
using System;
using System.Buffers;
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
using System.Drawing;
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
        internal static extern IntPtr CreateByteWriter(
            IntPtr clientFactoryPointer,
            CustomRustString scope, 
            CustomRustString stream, 
            ulong key,
            [MarshalAs(UnmanagedType.FunctionPtr)] rustCallbackInvoke callback);

        // ByteWriter current offset getter
        [DllImport(ByteDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ByteWriterCurrentOffset")]
        internal static extern ulong ByteWriterCurrentOffset(IntPtr byteWriterPointer);

        // ByteWriter.write()
        [DllImport(ByteDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ByteWriterWrite")]
        internal static extern ulong ByteWriterWrite(IntPtr clientFactroyPointer, IntPtr byteWriterPointer, IntPtr bufferPointer, int bufferSize, [MarshalAs(UnmanagedType.FunctionPtr)] rustCallbackU64 callback);

        ////////
        /// Byte Reader
        ////////
        // ByteReader default constructor
        [DllImport(ByteDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CreateByteReader")]
        internal static extern IntPtr CreateByteReader(
            IntPtr clientFactoryPointer,
            CustomRustString scope,
            CustomRustString stream,
            ulong callbackDictKey,
            [MarshalAs(UnmanagedType.FunctionPtr)] rustCallbackInvoke callback
        );

        // ByteReader current offset getter
        [DllImport(ByteDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ByteReaderCurrentOffset")]
        internal static extern ulong ByteReaderCurrentOffset(IntPtr byteReaderPointer);

        // ByteReader available getter
        [DllImport(ByteDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ByteReaderAvailable")]
        internal static extern ulong ByteReaderAvailable(IntPtr byteReaderPointer);

        // ByteReader.seek()
        [DllImport(ByteDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ByteReaderSeek")]
        internal static extern void ByteReaderSeek(IntPtr clientFactoryPointer, IntPtr byteReaderPointer, ulong mode, ulong nBytes, [MarshalAs(UnmanagedType.FunctionPtr)] rustCallbackU64 callback);

        // ByteReader.read()
        [DllImport(ByteDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ByteReaderRead")]
        internal static extern void ByteReaderRead(IntPtr clientFactoryPointer, IntPtr byteReaderPointer, ulong bytesRequested, [MarshalAs(UnmanagedType.FunctionPtr)] rustCallbackArray callback);

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

        /// <summary>
        ///   ByteWriter initializer. Creates a ByteWriter based on an inputted scopedstream.
        /// </summary>
        /// <param name="writerScopedStream">
        ///     ScopedStream to base the ByteWriter on
        /// </param>
        /// <returns>
        ///     A task that marks when this object is successfully initialized. 
        /// </returns>
        /// <exception cref="PravegaException">
        ///     Occurs if the ClientFactory isn't initialized when called.
        /// </exception>
        internal async Task InitializeByteWriter(
            ScopedStream writerScopedStream
        )
        {
            if (ClientFactory.Initialized())
            {
                IntPtr byteWriterPointer = await GenerateByteWriterHelper(writerScopedStream);
                this._rustStructPointer = byteWriterPointer;
            }
            else
            {
                throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
            }
        }

        /// <summary>
        ///  Internal method that generates a byte writer using a dll call. Sets this object's pointer when initialized successfully.
        /// </summary>
        /// <param name="writerScopedStream"></param>
        /// <returns></returns>
        private Task<IntPtr> GenerateByteWriterHelper(
            ScopedStream writerScopedStream
        )
        {
            TaskCompletionSource<IntPtr> task = new TaskCompletionSource<IntPtr>();

            // Create and pin the callback so it isn't garbage collected.
            rustCallback callback = (value) => {
                task.SetResult(value);
            };
            ulong key = CallbackDelegateManager.AddToRustCallbackDict(callback);

            Interop.CreateByteWriter(
                ClientFactory.RustStructPointer,
                writerScopedStream.Scope.RustString,
                writerScopedStream.Stream.RustString,
                key,
                CallbackDelegateManager.OneTimeInvokeFromRustCallbackDict
            );
            return task.Task;
        }

        /// <summary>
        ///  Gets this object's current offset.
        /// </summary>
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

        public Task<ulong> Write(
            List<byte> buffer
        )
        {
            // If ClientFactory isn't initialized, throw an exception.
            if (!ClientFactory.Initialized())
            {
                throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
            }

            // Split the list into local variables. 
            byte[] bufferArray = buffer.ToArray();
            int bufferSize = bufferArray.Length;

            // Marshal the array to unmanaged memory.
            IntPtr unmanagedBufferArray = Marshal.AllocCoTaskMem(Marshal.SizeOf(typeof(byte))
                       * bufferSize);
            Marshal.Copy(bufferArray, 0, unmanagedBufferArray, bufferSize);

            // Write to stream the unmanaged buffer
            TaskCompletionSource<ulong> task = new TaskCompletionSource<ulong>();
            Interop.ByteWriterWrite(
                ClientFactory.RustStructPointer,
                this._rustStructPointer,
                unmanagedBufferArray,
                bufferSize,
                (value) => {
                    task.SetResult(value);
                }
            );
            return task.Task;
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
        ///   ByteReader initializer. Creates a bytereader based on an inputted scopedstream.
        /// </summary>
        /// <param name="writerScopedStream">
        ///     ScopedStream to base the ByteReader on
        /// </param>
        /// <returns>
        ///     A task that marks when this object is successfully initialized. 
        /// </returns>
        /// <exception cref="PravegaException">
        ///     Occurs if the ClientFactory isn't initialized when called.
        /// </exception>
        internal async Task InitializeByteReader(
            ScopedStream writerScopedStream
        )
        {
            if (ClientFactory.Initialized())
            {
                IntPtr byteWriterPointer = await GenerateByteReaderHelper(writerScopedStream);
                this._rustStructPointer = byteWriterPointer;
            }
            else
            {
                throw new PravegaException(WrapperErrorMessages.ClientFactoryNotInitialized);
            }
        }

        /// <summary>
        ///  Internal method that generates a byte reader using a dll call. Sets this object's pointer when initialized successfully.
        /// </summary>
        /// <param name="writerScopedStream"></param>
        /// <returns></returns>
        private Task<IntPtr> GenerateByteReaderHelper(
            ScopedStream writerScopedStream
        )
        {
            TaskCompletionSource<IntPtr> task = new TaskCompletionSource<IntPtr>();

            // Create and pin the callback so it isn't garbage collected.
            rustCallback callback = (value) => {
                task.SetResult(value);
            };
            ulong key = CallbackDelegateManager.AddToRustCallbackDict(callback);

            Interop.CreateByteReader(
                ClientFactory.RustStructPointer,
                writerScopedStream.Scope.RustString,
                writerScopedStream.Stream.RustString,
                key,
                CallbackDelegateManager.OneTimeInvokeFromRustCallbackDict
            );
            return task.Task;


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

        /// <summary>
        /// Return the bytes that are available to read instantly without fetching from server.
        /// ByteReader has a buffer internally.This method returns the size of remaining data in that buffer.
        /// </summary>
        public ulong Available
        {
            get
            {
                if (!this.IsNull())
                {
                    return Interop.ByteReaderAvailable(this.RustStructPointer);
                }
                else
                {
                    throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
                }
            }
        }

        /// <summary>
        ///  The seek method for ByteReader allows seeking to a byte offset from the beginning
        ///     of the stream or a byte offset relative to the current position in the stream.
        ///     If the stream has been truncated, the byte offset will be relative to the original beginning of the stream.
        /// </summary>
        /// <param name="mode">
        ///     Determines which direction to seek from
        ///     0=from beginning stream
        ///     1=from current position
        ///     2=from end of stream
        ///     
        ///     *Inputting other modes not listed will result in the mode being set to 0
        /// </param>
        /// <param name="numberOfBytes">
        ///     Amount to seek
        /// </param>
        /// <returns>
        ///     A task that will eventually complete and contains the newoffset of this byte reader.
        /// </returns>
        public Task<ulong> Seek(ulong mode=0, ulong numberOfBytes=0)
        {
            // If Client Factory isn't initialized, throw
            if (!ClientFactory.Initialized())
            {
                throw new PravegaException(WrapperErrorMessages.ClientFactoryNotInitialized);
            }

            // If mode isn't 0, 1, or 2. Set to 0
            if (mode < 0 || mode > 2)
            {
                mode = 0;
            }

            TaskCompletionSource<ulong> task = new TaskCompletionSource<ulong>();
            Interop.ByteReaderSeek(
                ClientFactory.RustStructPointer,
                this._rustStructPointer,
                mode,
                numberOfBytes,
                (value) => {
                    task.SetResult(value);
                }
            );
            return task.Task;
        }
        
        /// <summary>
        ///  Reads data from the ByteReader on the current offset, filling in as much as possible up to the amount requested. Returns the buffer read afterwards
        /// </summary>
        /// <param name="numberOfBytesRequested">
        ///  Amount of bytes requested by the user. May not read the full amount, only the amount immediately available.
        /// </param>
        /// <returns>
        ///  Buffer containing bytes read
        /// </returns>
        public Task<byte[]> Read(uint numberOfBytesRequested)
        {
            // If Client Factory isn't initialized, throw
            if (!ClientFactory.Initialized())
            {
                throw new PravegaException(WrapperErrorMessages.ClientFactoryNotInitialized);
            }

            TaskCompletionSource<byte[]> task = new TaskCompletionSource<byte[]>();

            // Create and pin the callback so it isn't garbage collected.
            rustCallbackArray callback = (arrPointer, numberOfBytesRead)
            =>
            {
                U8Slice buffer = new U8Slice();
                buffer.slice_pointer = arrPointer;
                buffer.length = numberOfBytesRead;
                byte[] bufferManaged = buffer.Copied;
                task.SetResult(bufferManaged);
            };
            GCHandle callbackAlloc = GCHandle.Alloc(callback, GCHandleType.Pinned);
            IntPtr callbackAllocPtr = GCHandle.ToIntPtr(callbackAlloc);

            // Create a task and call ByteReaderRead. Await array pointer passback
            Interop.ByteReaderRead(
                ClientFactory.RustStructPointer,
                this._rustStructPointer,
                numberOfBytesRequested,
                callback
            );

            return task.Task;
        }
    
    }


}