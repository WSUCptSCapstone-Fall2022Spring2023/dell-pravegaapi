///
/// File: connection_type_wrapper.rs
/// File Creator: John Sbur
/// Purpose: Contains structs transferred from the PravegaConfig area. Specifically from the original file connection_type.rs. Not all structs are transferred, only necessary ones.
///     Provides definitions on the Rust side.
///
/// 
use interoptopus::{ffi_type};

// ConnectionType in palatable C#
#[ffi_type]
#[repr(C)]
pub enum ConnectionTypeWrapper {
    Happy,
    SegmentIsSealed,
    SegmentIsTruncated,
    WrongHost,
    Tokio,
}
/*
Originally from
pravega-client-rust/config/src/connection_type.rs
as:

#[derive(Debug, PartialEq, Clone, Copy)]
pub enum ConnectionType {
    Mock(MockType),
    Tokio,
}

#[derive(Debug, PartialEq, Clone, Copy)]
pub enum MockType {
    Happy,
    SegmentIsSealed,
    SegmentIsTruncated,
    WrongHost,
}

*/