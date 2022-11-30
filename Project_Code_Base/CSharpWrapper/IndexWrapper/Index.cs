///
/// File: Index.cs
/// File Creator: John Sbur
/// Purpose: Contains helper structs under the index module. Implements the C# equivalent of the Rust wrapper structs
///
#pragma warning disable 0105
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Pravega;
#pragma warning restore 0105

namespace Pravega.Index
{
    // Note: These are left intentionally unfinished 
    //  these will be completed once our team works on
    //  the Index module.
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct AsyncSegmentReaderImplHolder
    {
        byte inner;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ByteWriterHolde
    {
        byte inner;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ClientFactoryAsyncHolder
    {
        byte inner;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ScopedStreamHolder
    {
        byte inner;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct SegmentMetadataClientHolder
    {
        byte inner;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct IndexReaderWrapper
    {
        ScopedStreamHolder stream;
        ClientFactoryAsyncHolder factory;
        SegmentMetadataClientHolder meta;
        AsyncSegmentReaderImplHolder segment_reader;
    }

    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct IndexWriterWrapperu8
    {
        ByteWriterHolder byte_writer;
        OptionU128U64TupleSlice hashed_fields;
        Optionu8 fields;
    }
}