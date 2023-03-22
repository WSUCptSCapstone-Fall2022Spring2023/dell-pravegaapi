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

        /// <summary>
        /// Delegate functions used for async callbacks from rust.
        /// </summary>
        internal delegate void rustCallback(IntPtr arg);
        internal delegate void rustCallbackU64(ulong arg);
        internal delegate void rustCallbackArray(IntPtr arrayPointer, uint size);

        /// <summary>
        ///  Utility delegates used with the callback delegate manager for invoking delegates from rust safely.
        /// </summary>
        /// <param name="key">
        ///  Key of delegate to be invoked
        /// </param>    
        internal delegate void rustCallbackInvoke(ulong key, IntPtr arg);
        internal delegate void rustCallbackU64Invoke(ulong key, ulong arg);
        internal delegate void rustCallbackArrayInvoke(ulong key, IntPtr arrayPointer, uint size);

        /// <summary>
        ///  Manages delegates allocated, keeping them from being collected from GC until they are removed.
        ///  Used as a way of pinning delegates and ensuring they aren't freed after a dll call until told specifically.
        /// </summary>
        internal static class CallbackDelegateManager
        {
            // Private lists used to keep track of delegates in GC memory.
            // Dictionaries are populated with keys starting from 0 and filling in counting up.
            private static Dictionary<ulong, rustCallback> rustCallbackDict = new Dictionary<ulong, rustCallback>();
            private static Dictionary<ulong, rustCallbackU64> rustCallbackU64Dict = new Dictionary<ulong, rustCallbackU64>();
            private static Dictionary<ulong, rustCallbackArray> rustCallbackArrayDict = new Dictionary<ulong, rustCallbackArray>();

            /// <summary>
            /// Adds a member to the rustCallbackList, returning the key of the callback
            /// </summary>
            /// <param name="arg">
            /// Callback to be added
            /// </param>
            internal static ulong AddToRustCallbackDict(rustCallback arg)
            {
                ulong i = 0;
                while (rustCallbackDict.ContainsKey(i))
                {
                    i++;
                }
                rustCallbackDict[i] = arg;
                return i;
            }

            /// <summary>
            /// Adds a member to the rustCallbackU64List, returning the key of the callback
            /// </summary>
            /// <param name="arg">
            /// Callback to be added
            /// </param>
            internal static ulong AddToRustCallbackU64Dictionary(rustCallbackU64 arg)
            {
                ulong i = 0;
                while (rustCallbackU64Dict.ContainsKey(i))
                {
                    i++;
                }
                rustCallbackU64Dict[i] = arg;
                return i;
            }

            /// <summary>
            /// Adds a member to the rustCallbackArray, returning the key of the callback
            /// </summary>
            /// <param name="arg">
            /// Callback to be added
            /// </param>
            internal static ulong AddToRustCallbackUArrayDictionary(rustCallbackArray arg)
            {
                ulong i = 0;
                while (rustCallbackArrayDict.ContainsKey(i))
                {
                    i++;
                }
                rustCallbackArrayDict[i] = arg;
                return i;
            }

            /// <summary>
            ///  Given a key and an argument, this function tries to invoke a delegate matching the dictionary key
            ///  inputting "arg" as its argument.
            /// </summary>
            /// <param name="key">
            ///  Key of delegate to be invoked.
            /// </param>
            /// <param name="arg">
            ///  Argument of delegate
            /// </param>
            internal static void InvokeFromRustCallbackDict(ulong key, IntPtr arg)
            {
                if (rustCallbackDict.ContainsKey(key))
                {
                    rustCallbackDict[key].Invoke(arg);
                }
            }

            /// <summary>
            ///  Given a key and an argument, this function tries to invoke a delegate matching the dictionary key
            ///  inputting "arg" as its argument.
            /// </summary>
            /// <param name="key">
            ///  Key of delegate to be invoked.
            /// </param>
            /// <param name="arg">
            ///  Argument of delegate
            /// </param>
            internal static void InvokeFromRustCallbackU64Dict(ulong key, ulong arg)
            {
                if (rustCallbackU64Dict.ContainsKey(key))
                {
                    rustCallbackU64Dict[key].Invoke(arg);
                }
            }

            /// <summary>
            ///  Given a key and an argument, this function tries to invoke a delegate matching the dictionary key
            ///  inputting "arg" as its argument.
            /// </summary>
            /// <param name="key">
            ///  Key of delegate to be invoked.
            /// </param>
            /// <param name="arrayPtr">
            ///  Pointer to relevant array
            /// </param>
            /// <param name="size">
            ///  Size of relevant array
            /// </param>
            internal static void InvokeFromRustCallbackArrayDict(ulong key, IntPtr arrayPtr, uint size)
            {
                if (rustCallbackArrayDict.ContainsKey(key))
                {
                    rustCallbackArrayDict[key].Invoke(arrayPtr, size);
                }
            }

            /// <summary>
            ///  Given a key and an argument, this function tries to invoke a delegate matching the dictionary key
            ///  inputting "arg" as its argument.
            ///  The only difference between this and the non OneTimeMethod is that this also tries to delete the
            ///  delegate after using it.
            /// </summary>
            /// <param name="key">
            ///  Key of delegate to be invoked.
            /// </param>
            /// <param name="arg">
            ///  Argument of delegate
            /// </param>
            internal static void OneTimeInvokeFromRustCallbackDict(ulong key, IntPtr arg)
            {
                if (rustCallbackDict.ContainsKey(key))
                {
                    rustCallbackDict[key].Invoke(arg);
                    rustCallbackDict.Remove(key);
                }
            }

            /// <summary>
            ///  Given a key and an argument, this function tries to invoke a delegate matching the dictionary key
            ///  inputting "arg" as its argument.
            /// </summary>
            /// <param name="key">
            ///  Key of delegate to be invoked.
            /// </param>
            /// <param name="arg">
            ///  Argument of delegate
            /// </param>
            internal static void OneTimeInvokeFromRustCallbackU64Dict(ulong key, ulong arg)
            {
                if (rustCallbackU64Dict.ContainsKey(key))
                {
                    rustCallbackU64Dict[key].Invoke(arg);
                    rustCallbackU64Dict.Remove(key);
                }
            }

            /// <summary>
            ///  Given a key and an argument, this function tries to invoke a delegate matching the dictionary key
            ///  inputting "arg" as its argument.
            /// </summary>
            /// <param name="key">
            ///  Key of delegate to be invoked.
            /// </param>
            /// <param name="arrayPtr">
            ///  Pointer to relevant array
            /// </param>
            /// <param name="size">
            ///  Size of relevant array
            /// </param>
            internal static void OneTimeInvokeFromRustCallbackArrayDict(ulong key, IntPtr arrayPtr, uint size)
            {
                if (rustCallbackArrayDict.ContainsKey(key))
                {
                    rustCallbackArrayDict[key].Invoke(arrayPtr, size);
                    rustCallbackArrayDict.Remove(key);
                }
            }

            /// <summary>
            ///  Removes a member from the RustCallBackList according to the key if it exists.
            /// </summary>
            /// <param name="key">
            ///  Key of value to be removed
            /// </param>
            internal static void RemoveFromRustCallbackDict(ulong key)
            {
                if (rustCallbackDict.ContainsKey(key))
                {
                    rustCallbackDict.Remove(key);
                }
            }

            /// <summary>
            ///  Removes a member from the RustCallBackList according to the key if it exists.
            /// </summary>
            /// <param name="key">
            ///  Key of value to be removed
            /// </param>
            internal static void RemoveFromRustCallbackU64Dict(ulong key)
            {
                if (rustCallbackU64Dict.ContainsKey(key))
                {
                    rustCallbackU64Dict.Remove(key);
                }
            }

            /// <summary>
            ///  Removes a member from the RustCallbackArrayList according to the key if it exists.
            /// </summary>
            /// <param name="key">
            ///  Key of value to be removed
            /// </param>
            internal static void RemoveFromRustCallbackArrayDict(ulong key)
            {
                if (rustCallbackArrayDict.ContainsKey(key))
                {
                    rustCallbackArrayDict.Remove(key);
                }
            }

            internal static rustCallbackInvoke CallbackDictDelegateInvoke = (key, arg) => { CallbackDelegateManager.InvokeFromRustCallbackDict(key, arg); };
            internal static rustCallbackU64Invoke CallbackU64DictDelegateInvoke = (key, arg) => { CallbackDelegateManager.InvokeFromRustCallbackU64Dict(key, arg); };
            internal static rustCallbackArrayInvoke CallbackArrayDictDelegateInvoke = (key, arrayPtr, size) => { CallbackDelegateManager.InvokeFromRustCallbackArrayDict(key, arrayPtr, size); };

            /// <summary>
            ///  Delegates that can be marshalled to rust and called in order to remove a delegate from memory from rust.
            /// </summary>
            internal static rustCallbackU64 RemoveFromCallbackDictDelegate = (key) => { CallbackDelegateManager.RemoveFromRustCallbackDict(key); };
            internal static rustCallbackU64 RemoveFromCallbackU64DictDelegate = (key) => { CallbackDelegateManager.RemoveFromRustCallbackU64Dict(key); };
            internal static rustCallbackU64 RemoveFromCallbackArrayDictDelegate = (key) => { CallbackDelegateManager.RemoveFromRustCallbackArrayDict(key); };

        }
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

        // For when a function is called with Client Factory, but client factory is not initialized
        public static string ClientFactoryNotInitialized
        {
            get
            {
                return "Client Factory was not initialized, but a function requiring Client Factory to be initialized was called.";
            }
        }
    }
}