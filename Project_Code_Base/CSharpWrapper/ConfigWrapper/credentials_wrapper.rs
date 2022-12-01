///
/// File: credentials_wrapper.rs
/// File Creator: John Sbur
/// Purpose: Contains structs transferred from the PravegaConfig area. Not all structs are transferred, only necessary ones.
///     Provides definitions on the Rust side.
///
use interoptopus::{ffi_type};
use utility::CustomRustString;



// Cred is a trait and is meant for Credentials to contain the trait and its functions to returning.
//  Because of this, Cred needs to replicate the behavior of having two functions within it.
#[ffi_type]
#[repr(C)]
pub struct CredWrapper {
    pub is_expired_result: bool,
    pub get_request_metadata_result: CustomRustString,
}
// Credentials in palatable C#
#[ffi_type]
#[repr(C)]
pub struct CredentialsWrapper {
    // Needs to get allocated into a box when wrapped 
    inner: CredWrapper,
}
/*
Originally from pravega-client-rust/config/src/credentials.rs 
as:
    #[derive(Debug)]
    pub struct Credentials {
        inner: Box<dyn Cred>,
    }
*/