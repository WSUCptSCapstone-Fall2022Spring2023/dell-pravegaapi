#pragma warning disable 0105
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
#pragma warning restore 0105

// Overarching namespace for the C# Wrapper. Contains definitions that apply to all wrappers.
namespace Pravega {

    // Contains globals for the C# wrapper
    public static class WrapperConstants
    {
        internal const string RustDllPath = "PravegaCSharp.dll";
    }

    // The static class that manages .dll function call signatures in C#. Built upon in different modules.
    public static partial class Interop
    {
        

    }
}