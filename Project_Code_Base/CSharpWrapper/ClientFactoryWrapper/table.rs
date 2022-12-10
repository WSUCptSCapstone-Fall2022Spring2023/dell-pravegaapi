use interoptopus::{ffi_type, extra_type, Inventory, InventoryBuilder};
use interoptopus::patterns::option::FFIOption;

// Holds two int values. Used in place of tuples or two data points
#[ffi_type]
#[repr(C)]
pub struct U16Tuple{
    value1: u16,
    value2: u16,
}



// Custom string used for transferring between C# and Rust
#[ffi_type]
#[repr(C)]
pub struct CustomRustString {
    pub capacity: u32,
    pub string_slice: U8Slice,
}
#[ffi_type]
#[repr(C)]
pub struct CustomCSharpString{
    pub capacity: u32,
    pub string_slice: U16Slice,
}
// Stores a pointer to a slice of data and its length
#[ffi_type]
#[repr(C)]
pub struct U8Slice{
    pub slice_pointer: *mut i32,
    pub length: u64,
}
#[ffi_type]
#[repr(C)]
pub struct U16Slice{
    pub slice_pointer: *mut i32,
    pub length: u64,
}
// Custom string slice
#[ffi_type]
#[repr(C)]
pub struct CustomCSharpStringSlice{
    pub slice_pointer: *mut i32,
    pub length: u64,
}
#[ffi_type]
#[repr(C)]
pub struct CustomRustStringSlice{
    pub slice_pointer: *mut i32,
    pub length: u64,
}



// Imported from previous wrapper. See ScopedStream
// Scope in palatable C#
#[ffi_type]
#[repr(C)]
pub struct ScopeWrapper{
    pub name: CustomCSharpString,
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
    pub name: CustomCSharpString,
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
    value: CustomCSharpString,
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



// DelegationTokenProvider in palatable C#
#[ffi_type]
#[repr(C)]
pub struct DelegationTokenProviderWrapper{
    stream: ScopedStreamWrapper,
    
    // RwLock is a mutex lock based on a delegation token, but RwLock is from tokio and can't be transferred.
    //  For now, a delegation token is going to be in its place
    token: DelegationTokenWrapper,

    // Atomic bool is a bool that can be checked across processes, but is from tokio and can't be transferred.
    //  For now, a bool is going to be in its place.
    signal_expiry: bool, 
}

#[ffi_type]
#[repr(C)]
pub struct PravegaNodeUriWrapper
{
    pub string: CustomCSharpString,
}

#[ffi_type]
#[repr(C)]
pub struct ClientFactoryAsyncWrapper
{

temp: u32,
}

#[ffi_type]
#[repr(C)]
pub struct TableWrapper {
    // name should be unique as it is used to construct the internal stream.
    // different table with same name will share the same state.
    name: CustomCSharpString,
    endpoint: PravegaNodeUriWrapper,
    factory: ClientFactoryAsyncWrapper,
    delegation_token_provider: DelegationTokenProviderWrapper,
}

// This will create a function `my_inventory` which can produce
// an abstract FFI representation (called `Library`) for this crate.
pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new()
            .register(extra_type!(U8Slice))
            .register(extra_type!(U16Slice))
            .register(extra_type!(CustomCSharpString))
            .register(extra_type!(CustomRustString))
            .register(extra_type!(CustomCSharpStringSlice))
            .register(extra_type!(CustomRustStringSlice))
            .register(extra_type!(DelegationTokenProviderWrapper))
            .register(extra_type!(PravegaNodeUriWrapper))
            .register(extra_type!(TableWrapper))
        .inventory()
    }
}