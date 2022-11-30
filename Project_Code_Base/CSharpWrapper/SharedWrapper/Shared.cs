///
/// File: Shared.cs
/// File Creator: John Sbur
/// Purpose: Contains helper structs under the shared area. Implements the C# equivalent of the Rust wrapper structs
///
#pragma warning disable 0105
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Pravega;
#pragma warning restore 0105

namespace Pravega.Shared
{
    //  ***** Wrapper for TxId *****
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct TxIdWrapper
    {
        public CustomU128 inner;
    }



    //  ***** Wrapper for WriterID *****
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct WriterIdWrapper
    {
        public CustomU128 inner;
    }
    /* Originally from pravega-client-rust/shared/src/lib.rs 
        as:
        #[derive(From, Shrinkwrap, Copy, Clone, Hash, PartialEq, Eq)]
        pub struct WriterId(pub u128);
    */



    //  ***** Wrapper for DelegationToken *****
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct DelegationTokenWrapper
    {
        CustomCSharpString value;
        Optionu64 expiry_time;
    }



    //  ***** Wrapper for Scope *****
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ScopeWrapper
    {
        public CustomCSharpString name;
    }



    //  ***** Wrapper for ScopedStream *****
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ScopedStreamWrapper
    {
        public ScopeWrapper scope;
        public StreamWrapper stream;
    }



    //  ***** Wrapper for Stream *****
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct StreamWrapper
    {
        public CustomCSharpString name;
    }



    //  ***** Wrapper for ScopedSegment *****
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct ScopedSegmentWrapper
    {
        public ScopeWrapper scope;
        public StreamWrapper stream;
        public SegmentWrapper segment;
    }



    //  ***** Wrapper for Segment *****
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct SegmentWrapper
    {
        public long number;
        public OptionTxIdWrapper tx_id;
    }



    // ***** Wrapper for Credentials *****
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct PravegaNodeUriWrapper
    {
        public CustomCSharpString inner;
    }
    /* 
        Originally from pravega-client-rust/shared/src/lib.rs
        as:
        #[derive(From, Shrinkwrap, Debug, Clone, Hash, PartialEq, Eq)]
        pub struct PravegaNodeUri(pub String);
    */
}
