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
using Pravega.Utility;
#pragma warning restore 0105

namespace Pravega.Retry
{
    // ***** Wrapper for RetryWithBackoff *****
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct RetryWithBackoffWrapper
    {
        ulong attempt;
        U16Tuple initial_delay;
        uint backoff_coefficient;
        public Optionu64 max_attempt;
        public OptionU16Tuple max_delay;
        public Optionf64 expiration_time;
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
}

    