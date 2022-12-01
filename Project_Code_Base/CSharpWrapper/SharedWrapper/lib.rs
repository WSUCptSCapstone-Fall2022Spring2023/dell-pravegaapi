///
/// File: lib.rs
/// File Creator: John Sbur
/// Purpose: Contains structs transferred from the Shared area. Not all structs are transferred, only necessary ones.
///     Provides definitions on the Rust side.
///
use interoptopus::{ffi_type};
use interoptopus::patterns::option::FFIOption;
use utility::{CustomU128, CustomRustString};



#[ffi_type]
#[repr(C)]
// Wrapper for WriterID
pub struct WriterIdWrapper{
    pub inner: CustomU128,
}
/* Originally from pravega-client-rust/shared/src/lib.rs 
    as:
    #[derive(From, Shrinkwrap, Copy, Clone, Hash, PartialEq, Eq)]
    pub struct WriterId(pub u128);
*/



// PravegaNodeUri in palatable C#
#[ffi_type]
#[repr(C)]
pub struct PravegaNodeUriWrapper {
    pub inner: CustomRustString,
}
/* 
    Originally from pravega-client-rust/shared/src/lib.rs
    as:
    #[derive(From, Shrinkwrap, Debug, Clone, Hash, PartialEq, Eq)]
    pub struct PravegaNodeUri(pub String);
*/



// Imported from previous wrapper. See ScopedStream
// Scope in palatable C#
#[ffi_type]
#[repr(C)]
pub struct ScopeWrapper{
    pub name: CustomRustString,
}
/*
    Originally from pravega-client-rust/shared/src/lib.rs 
    as:
    #[derive(From, Shrinkwrap, Debug, Display, Clone, Hash, PartialEq, Eq, Serialize, Deserialize)]
    pub struct Scope {
        pub name: String,
    }
*/



// Imported from previous wrapper. See ScopedStream
// Stream in palatable C#
#[ffi_type]
#[repr(C)]
pub struct StreamWrapper {
    pub name: CustomRustString,
}
/*
    Originally from 
    as: 
    #[derive(From, Shrinkwrap, Debug, Display, Clone, Hash, PartialEq, Eq, Serialize, Deserialize)]
    pub struct Stream {
        pub name: String,
    }
*/



// Imported from previous wrapper. See ScopedStream
// ScopedStream in palatable C#
#[ffi_type]
#[repr(C)]
pub struct ScopedStreamWrapper{
    pub scope: ScopeWrapper,
    pub stream: StreamWrapper,
}
/*
    Originally from pravega-client-rust/shared/src/lib.rs 
    as: 
    #[derive(new, Debug, Clone, Hash, PartialEq, Eq, Serialize, Deserialize)]
    pub struct ScopedStream {
        pub scope: Scope,
        pub stream: Stream,
    }   
*/



// DelegationToken in palatable C#
#[ffi_type]
#[repr(C)]
pub struct DelegationTokenWrapper{
    value: CustomRustString,
    expiry_time: FFIOption<u64>,
}
/*
    Orignally from pravega-client-rust/shared/src/lib.rs
    as:
    #[derive(new, Debug, Clone, Hash, PartialEq, Eq)]
    pub struct DelegationToken {
        value: String,
        expiry_time: Option<u64>,
    }

*/




#[ffi_type]
#[repr(C)]
pub struct TxIdWrapper{
    pub inner: CustomU128,
}
/*
    Originally from pravega-client-rust/shared/src/lib.rs
    as: 
    #[derive(From, Shrinkwrap, Copy, Clone, Hash, PartialEq, Eq, Serialize, Deserialize)]
    pub struct TxId(pub u128);
*/



// Segment in palatable C#
#[ffi_type]
#[repr(C)]
pub struct SegmentWrapper{
    pub number: i64,
    pub tx_id: FFIOption<TxIdWrapper>,
}
/*
    Originally from pravega-client-rust/shared/src/lib.rs
    as: 
    #[derive(Clone, Hash, PartialEq, Eq, Serialize, Deserialize)]
    pub struct Segment {
        pub number: i64,
        pub tx_id: Option<TxId>,
    }
*/




// ScopedSegment in palatable C#
#[ffi_type]
#[repr(C)]
pub struct ScopedSegmentWrapper{
    pub scope: ScopeWrapper,
    pub stream: StreamWrapper,
    pub segment: SegmentWrapper,
}
/*
    Originally from
    as: 
    #[derive(new, Debug, Clone, Hash, PartialEq, Eq, Serialize, Deserialize)]
    pub struct ScopedSegment {
        pub scope: Scope,
        pub stream: Stream,
        pub segment: Segment,
    }
*/
