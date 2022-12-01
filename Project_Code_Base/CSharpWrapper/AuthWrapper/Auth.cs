///
/// File: Auth.cs
/// File Creator: John Sbur
/// Purpose: Contains helper structs under the shared area. Implements the C# equivalent of the Rust wrapper structs
///
#pragma warning disable 0105
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Pravega;
using Pravega.Shared;
#pragma warning restore 0105

namespace Pravega.Auth
{
    //  ***** Wrapper for DelegationTokenProvider *****
    [Serializable]
    [StructLayout(LayoutKind.Sequential)]
    public partial struct DelegationTokenProviderWrapper
    {
        ScopedStreamWrapper stream;
        DelegationTokenWrapper token;
        bool signal_expiry;
    }

}