///

/// File: Index.cs
/// File Creator: John Sbur
/// Purpose: Contains helper structs under the index module. Implements the C# equivalent of the Rust wrapper structs

/// File: Event.cs
/// File Creator: John Sbur
/// Purpose: Contains helper structs under the event module. Implements the C# equivalent of the Rust wrapper structs

#pragma warning disable 0105
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Pravega.ClientFactoryModule;
using Pravega.Shared;
using Pravega.Utility;
using static Pravega.Interop;
#pragma warning restore 0105

namespace Pravega.Event
{
    public static partial class Interop
    {
        [DllImport(Pravega.Interop.RustDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CreateReaderGroup")]
        internal static extern IntPtr CreateReaderGroup(IntPtr clientFactoryPointer,
            CustomRustString scope,
            CustomRustString stream,
            ulong key,
            [MarshalAs(UnmanagedType.FunctionPtr)] rustCallbackInvoke callback);

        [DllImport(Pravega.Interop.RustDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CreateEventWriter")]
        internal static extern IntPtr CreateEventWriter(IntPtr clientFactoryPointer,
            CustomRustString scope,
            CustomRustString stream,
            ulong key,
            [MarshalAs(UnmanagedType.FunctionPtr)] rustCallbackInvoke callback);
    }

    // ***** Wrapper for EventWriter *****

    public class EventWriter : RustStructWrapper
    {

        internal EventWriter()
        {
            this.RustStructPointer = IntPtr.Zero;
        }

        internal async Task InitializeEventWriter(
            ScopedStream writerScopedStream
        )
        {
            if (ClientFactory.Initialized())
            {
                IntPtr EWPointer = await GenerateEventWriterHelper(writerScopedStream);
                this._rustStructPointer = EWPointer;
                Console.WriteLine("From C#");
                Console.WriteLine(this._rustStructPointer);
            }
            else
            {
                throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
            }
        }
        private Task<IntPtr> GenerateEventWriterHelper(
            ScopedStream writerScopedStream
        )
        {
            TaskCompletionSource<IntPtr> task = new TaskCompletionSource<IntPtr>();
            rustCallback callback = (value) => {
                task.SetResult(value);
            };
            ulong key = CallbackDelegateManager.AddToRustCallbackDict(callback);
            Interop.CreateEventWriter(
                ClientFactory.RustStructPointer,
                writerScopedStream.Scope.RustString,
                writerScopedStream.Stream.RustString,
                key,
                CallbackDelegateManager.OneTimeInvokeFromRustCallbackDict
            );
            return task.Task;
        }
    }


    // ***** Wrapper for EventReader *****

    public class EventReader : RustStructWrapper
    {

        internal EventReader(ScopedStream s)
        {
            this.RustStructPointer = IntPtr.Zero;
        }

    }




    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct SegmentMetadataClientHolder
    {
        byte inner;
    }





    // ***** Wrapper for ReaderGroup *****
    public class ReaderGroup : RustStructWrapper
    {

        internal ReaderGroup()
        {
            this._rustStructPointer = IntPtr.Zero;
        }

        internal async Task InitializeReaderGroup(
            ScopedStream writerScopedStream
        )
        {
            if (ClientFactory.Initialized())
            {
                IntPtr ReaderGroupPointer = await GenerateReaderGroupHelper(writerScopedStream);
                this._rustStructPointer = ReaderGroupPointer;
                Console.WriteLine(this._rustStructPointer);
            }
            else
            {
                throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
            }
        }
        private Task<IntPtr> GenerateReaderGroupHelper(
            ScopedStream writerScopedStream
        )
        {
            TaskCompletionSource<IntPtr> task = new TaskCompletionSource<IntPtr>();
            rustCallback callback = (value) => {
                task.SetResult(value);
            };
            ulong key = CallbackDelegateManager.AddToRustCallbackDict(callback);
            Interop.CreateReaderGroup(
                ClientFactory.RustStructPointer,
                writerScopedStream.Scope.RustString,
                writerScopedStream.Stream.RustString,
                key,
                CallbackDelegateManager.OneTimeInvokeFromRustCallbackDict
            );
            return task.Task;
        }
    }

    // ***** Wrapper for ReaderGroupConfig *****
    public class ReaderGroupConfig : RustStructWrapper
    {

        internal ReaderGroupConfig(ScopedStream s)
        {
            this._rustStructPointer = IntPtr.Zero;
        }
    }

}