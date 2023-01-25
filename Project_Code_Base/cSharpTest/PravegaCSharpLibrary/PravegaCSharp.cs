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


    // The static class that manages .dll function call signatures in C#. Built upon in different modules.
    // Contains globals for the C# wrapper as well
    public static partial class Interop
    {
        // String constants
        internal const string RustDllPath = @"C:\Users\john_\Desktop\Programming\Senior Project CS421\dell-pravegaapi\dell-pravegaapi\Project_Code_Base\cSharpTest\PravegaCSharpLibrary\target\debug\PravegaCSharp.dll";

        
        

    }

    // Error class for wrapper exceptions
    public class PravegaException : Exception
    {

        public PravegaException(string error)
            : base($"{error}.")
        {
        }
    }

    // Class containing preset error messages
    internal static class WrapperErrorMessages{

        // For when a rust object called or used cannot be found (consumed, set to null, or could not be dereferenced)
        public static string RustObjectNotFound{
            get
            {
                return "Pravega object not found exception.";
            }
        }
    }
}