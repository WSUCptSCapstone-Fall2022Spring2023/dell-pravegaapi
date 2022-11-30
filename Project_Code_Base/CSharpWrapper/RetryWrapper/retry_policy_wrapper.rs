///
/// File: retry_policy_wrapper.rs
/// File Creator: John Sbur
/// Purpose: Contains structs transferred from the Retry area. 
///     Provides definitions on the Rust side.
///
use interoptopus::{ffi_type};
use interoptopus::patterns::option::FFIOption;
use utility::U16Tuple;



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