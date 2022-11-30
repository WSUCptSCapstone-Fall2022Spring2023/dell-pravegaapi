///
/// File: lib.rs
/// File Creator: John Sbur
/// Purpose: Contains structs transferred from the Auth area. Not all structs are transferred, only necessary ones.
///     Provides definitions on the Rust side.
///
use interoptopus::{ffi_type};
use interoptopus::patterns::option::FFIOption;
use shared_wrapper::{DelegationTokenWrapper, ScopedStreamWrapper};


// DelegationTokenProvider in palatable C#
#[ffi_type]
#[repr(C)]
pub struct DelegationTokenProviderWrapper{
    stream: ScopedStreamWrapper,
    
    // RwLock is a mutex lock based on a delegation token, but RwLock is from tokio and can't be transferred.
    //  For now, a delegation token is going to be in its place
    token: FFIOption<DelegationTokenWrapper>,

    // Atomic bool is a bool that can be checked across processes, but is from tokio and can't be transferred.
    //  For now, a bool is going to be in its place.
    signal_expiry: bool, 
}
/*
    Originally from pravega-client-rust/auth/src/lib.rs
    as:
    pub struct DelegationTokenProvider {
        stream: ScopedStream,
        token: RwLock<Option<DelegationToken>>,
        signal_expiry: AtomicBool,
    }
*/
