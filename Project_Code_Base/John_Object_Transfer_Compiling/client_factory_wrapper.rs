use core::ffi;

use interoptopus::{callback, ffi_type, extra_type, Inventory, InventoryBuilder};
use interoptopus::patterns::slice::{FFISlice};
use interoptopus::patterns::option::FFIOption;

// Represents FFISlice of custom string
callback!(CallbackCustomStringFFISlice(slice: FFISlice<CustomString>) -> CustomString);
// Represents FFISlice of u32, which is within custom string representing a sequence of characters
//  Rust uses uses utf-8 which has a range from 0 to 127, effectively making the range 1 byte. As 
//  such, using u8 to represent a char should not be a problem.
callback!(CallbackCharFFISlice(slice: FFISlice<u8>) -> u8);



// Holds two int values. Used in place of tuples or two data points
#[ffi_type]
#[repr(C)]
pub struct U16Tuple{
    value1: u16,
    value2: u16,
}


// Custom string used for transferring
#[ffi_type]
#[repr(C)]
pub struct CustomString {
    pub capacity: u32,
    pub length: u32,
    pub string_slice: CallbackCharFFISlice,
}



// Cred is a trait and is meant for Credentials to contain the trait and its functions to returning.
//  Because of this, Cred needs to replicate the behavior of having two functions within it.
#[ffi_type]
#[repr(C)]
pub struct CredWrapper {
    pub is_expired_result: bool,
    pub get_request_metadata_result: CustomString,
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
 


// RetryWithBackoff in palatable C#
#[ffi_type]
#[repr(C)]
pub struct RetryWithBackoffWrapper {

    // usize -> u64
    attempt: u64,

    // duraction tuple -> u16 tuple struct
    initial_delay: U16Tuple,
    backoff_coefficient: u32,

    // usize -> u64
    pub max_attempt: FFIOption<u64>,
    pub max_delay: FFIOption<U16Tuple>,

    // Note: Needs to have a number passed in to represent the start of an instant when wrapped.
    //  i.e. a clock needs to be on the C# side and pass in it's time into this wrapper
    pub expiration_time: FFIOption<f64>,
}
/*
Originally from pravega-client-rust/retry/src/retry_policy.rs
as:
pub struct RetryWithBackoff {
    attempt: usize,

    initial_delay: Duration,
    backoff_coefficient: u32,
    max_attempt: Option<usize>,
    max_delay: Option<Duration>,
    expiration_time: Option<Instant>,
}
*/



// PravegaNodeUri in palatable C#
#[ffi_type]
#[repr(C)]
pub struct PravegaNodeUriWrapper {
    pub inner: CustomString,
}
/* 
    Originally from pravega-client-rust/shared/src/lib.rs
    as:
    #[derive(From, Shrinkwrap, Debug, Clone, Hash, PartialEq, Eq)]
    pub struct PravegaNodeUri(pub String);
*/



// Client Config Representation 
#[ffi_type]
#[repr(C)]
pub struct ClientConfigWrapper{

    // No issue transferring
    pub max_controller_connections: u32,

    // Connection Type -> Mock(MockType) + Tokio
    // Mock() imported library X. Instead, "Happy, SegmentIsSealed, SegmentIsTruncated, WrongHost," which is inside MockType
    pub connection_type: ConnectionTypeWrapper,

    // See above
    pub retry_policy: RetryWithBackoffWrapper,

    // See above
    pub controller_uri: PravegaNodeUriWrapper,

    // No issue transferring
    pub transaction_timeout_time: u64,

    // No issue transferring
    pub mock: bool,

    // No issue transferring
    pub is_tls_enabled: bool,

    // No issue transferring
    pub disable_cert_verification: bool,    

    // See above
    pub credentials: CredentialsWrapper,

    // No issue transferring
    pub is_auth_enabled: bool,

    // usize -> u64
    pub reader_wrapper_buffer_size: u64,

    // duration tuple -> array
    pub request_timeout: [u16; 2],

    // At end for dynamic allocation
    pub trustcerts: CallbackCustomStringFFISlice,
}
/*
 Originally as: 

pub struct ClientConfig{

    // No issue transferring
    #[get_copy = "pub"]
    #[builder(default = "u32::max_value()")]
    pub max_controller_connections: u32,

    // Connection Type -> Mock(MockType) + Tokio
    // Mock() imported library X. Instead, "Happy, SegmentIsSealed, SegmentIsTruncated, WrongHost," which is inside MockType
    #[get_copy = "pub"]
    #[builder(default = "ConnectionType::Tokio")]
    pub connection_type: ConnectionType,

    // See above
    #[get_copy = "pub"]
    #[builder(default = "RetryWithBackoff::default()")]
    pub retry_policy: RetryWithBackoff,

    // See above
    #[get]
    pub controller_uri: PravegaNodeUri,

    #[get_copy = "pub"]
    #[builder(default = "90 * 1000")]
    pub transaction_timeout_time: u64,

    #[get_copy = "pub"]
    #[builder(default = "false")]
    pub mock: bool,

    #[get_copy = "pub"]
    #[builder(default = "self.default_is_tls_enabled()")]
    pub is_tls_enabled: bool,

    #[builder(default = "false")]
    pub disable_cert_verification: bool,

    #[builder(default = "self.extract_trustcerts()")]
    pub trustcerts: Vec<String>,

    #[builder(default = "self.extract_credentials()")]
    pub credentials: Credentials,

    #[get_copy = "pub"]
    #[builder(default = "false")]
    pub is_auth_enabled: bool,

    #[get_copy = "pub"]
    #[builder(default = "1024 * 1024")]
    pub reader_wrapper_buffer_size: usize,

    #[get_copy = "pub"]
    #[builder(default = "self.default_timeout()")]
    pub request_timeout: Duration,
}
*/



// This will create a function `my_inventory` which can produce
// an abstract FFI representation (called `Library`) for this crate.
pub fn my_inventory() -> Inventory {
    {
        InventoryBuilder::new()
            .register(extra_type!(CustomString))
            .register(extra_type!(CredWrapper))
            .register(extra_type!(CredentialsWrapper))
            .register(extra_type!(RetryWithBackoffWrapper))
            .register(extra_type!(PravegaNodeUriWrapper))
            .register(extra_type!(ClientConfigWrapper))
        .inventory()
    }
}