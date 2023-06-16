///
/// File: ClientFactory.cs
/// File Creator: John Sbur
/// Purpose: Contains helper classes and methods that are used in the ClientFactory module.
///
#pragma warning disable 0105
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;
using Pravega;
using Pravega.Utility;
using Pravega.Config;
using Pravega.Shared;
using Pravega.ClientFactoryModule;
using static Pravega.Interop;
using System.Threading.Tasks;
#pragma warning restore 0105

namespace Pravega.ControllerCli
{
    // Continues building the Interop class by adding method signatures found in Client Factory.
    public static partial class Interop
    {
        ////////
        /// Controller Client
        ////////
        
        // ControllerClient.create_scope()
        [DllImport(Pravega.Interop.RustDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ControllerClientImplCreateScope")]
        internal static extern void ControllerClientImplCreateScope(
            IntPtr controllerClientPointer,
            CustomRustString newScope,
            ulong key,
            [MarshalAs(UnmanagedType.FunctionPtr)] rustCallbackU64Invoke callback);

        // ControllerClient.check_scope_exists()
        [DllImport(Pravega.Interop.RustDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ControllerClientImplCheckScopeExists")]
        internal static extern void ControllerClientImplCheckScopeExists (
            IntPtr controllerClientPointer,
            CustomRustString checkScope,
            ulong key,
            [MarshalAs(UnmanagedType.FunctionPtr)] rustCallbackU64Invoke callback
        );

        // ControllerClient.list_scopes
        [DllImport(Pravega.Interop.RustDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ControllerClientImplListScopes")]
        internal static extern void ControllerClientImplListScopes(
            IntPtr controllerClientPointer,
            ulong key,
            [MarshalAs(UnmanagedType.FunctionPtr)] rustCallbackArrayInvoke callback
        );

        // ControllerClient.delete_scope()
        [DllImport(Pravega.Interop.RustDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ControllerClientImplDeleteScope")]
        internal static extern void ControllerClientImplDeleteScope(
            IntPtr controllerClientPointer,
            CustomRustString targetScope,
            ulong key,
            [MarshalAs(UnmanagedType.FunctionPtr)] rustCallbackU64Invoke callback
        );


        // ControllerClient.create_stream()
        [DllImport(Pravega.Interop.RustDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ControllerClientImplCreateStream")]
                internal static extern void ControllerClientImplCreateStream(
                    IntPtr controllerClientPointer,
                    CustomRustString newStream,
                    CustomRustString targetScope,
                    int scalingType,
                    int scalingTargetRate,
                    int scalingFactor,
                    int scalingMinNumSegments,
                    int retentionType,
                    int retentionParam,
                    ulong key,
                    [MarshalAs(UnmanagedType.FunctionPtr)] rustCallbackU64Invoke callback);

        // ControllerClient.check_stream_exists()
        [DllImport(Pravega.Interop.RustDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ControllerClientImplCheckStreamExists")]
        internal static extern void ControllerClientImplCheckStreamExists(
                    IntPtr controllerClientPointer,
                    CustomRustString targetStream,
                    CustomRustString targetScope,
                    ulong key,
                    [MarshalAs(UnmanagedType.FunctionPtr)] rustCallbackU64Invoke callback);

        // ControllerClient.delete_stream()
        [DllImport(Pravega.Interop.RustDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ControllerClientImplDeleteStream")]
        internal static extern void ControllerClientImplDeleteStream(
                    IntPtr controllerClientPointer,
                    CustomRustString targetStream,
                    CustomRustString targetScope,
                    ulong key,
                    [MarshalAs(UnmanagedType.FunctionPtr)] rustCallbackU64Invoke callback);

        // ControllerClient.seal_stream()
        [DllImport(Pravega.Interop.RustDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ControllerClientImplSealStream")]
        internal static extern void ControllerClientImplSealStream(
                    IntPtr controllerClientPointer,
                    CustomRustString targetStream,
                    CustomRustString targetScope,
                    ulong key,
                    [MarshalAs(UnmanagedType.FunctionPtr)] rustCallbackU64Invoke callback);
    }

    /// <summary>
    ///  Controller APIs for administrative action for streams
    /// </summary>
    public class ControllerClient : RustStructWrapper
    {
        
        /// <summary>
        ///  Default constructor for controller client. Creates with a null pointer
        /// </summary>
        internal ControllerClient(){
            this._rustStructPointer = IntPtr.Zero;
        }
        
        /// <summary>
        ///  Creates a scope within the controller client's handle with the newScope's name.
        /// </summary>
        /// <param name="newScope">
        ///  New scope to be created
        /// </param>
        /// <returns>
        ///  A task that shows when the scope has been successfully created.
        ///  Set to true when done.
        /// </returns>
        public Task<bool> CreateScope(Scope newScope)
        {
            // Verify ClientFactory is initialized. If not, throw.
            if (!ClientFactory.Initialized()){
                throw new PravegaException(WrapperErrorMessages.ClientFactoryNotInitialized);
            }

            // Create and pin the callback so it isn't garbage collected.
            TaskCompletionSource<bool> task = new TaskCompletionSource<bool>();
            rustCallbackU64 callback = (value) => {
                task.SetResult(true);
            };
            ulong key = CallbackDelegateManager.AddToRustCallbackU64Dictionary(callback);
            Interop.ControllerClientImplCreateScope(
                this._rustStructPointer,
                newScope.RustString,
                key,
                CallbackDelegateManager.OneTimeInvokeFromRustCallbackU64Dict
            );
            
            return task.Task;
        }

        /// <summary>
        ///  Given the name of a scope, this function checks if the scope exists on the ClientFactory's server.
        /// </summary>
        /// <param name="checkingScope">
        ///  Scope being checked
        /// </param>
        /// <returns>
        ///  A task that will be completed in the future. The value will be
        ///  -true if it does exist
        ///  -false if it doesn't exist
        /// </returns>
        public Task<bool> CheckScopeExists(Scope checkingScope)
        {
            // Verify ClientFactory is initialized. If not, throw.
            if (!ClientFactory.Initialized())
            {
                throw new PravegaException(WrapperErrorMessages.ClientFactoryNotInitialized);
            }

            // Create and pin the callback so it isn't garbage collected.
            TaskCompletionSource<bool> task = new TaskCompletionSource<bool>();
            rustCallbackU64 callback = (value) => {
                if (value == 1)
                {
                    task.SetResult(true);
                }
                else
                {
                    task.SetResult(false);
                }
            };
            ulong key = CallbackDelegateManager.AddToRustCallbackU64Dictionary(callback);
            Interop.ControllerClientImplCheckScopeExists(
                this._rustStructPointer,
                checkingScope.RustString,
                key,
                CallbackDelegateManager.OneTimeInvokeFromRustCallbackU64Dict
            );

            return task.Task;
        }

        /// <summary>
        ///  Lists all scopes on the controller client's server
        /// </summary>
        /// <returns>
        ///  A task that once completed will contain either:
        ///  - A list of strings with the names of the scopes on the server
        ///  - Null if there are no scopes on the server
        /// </returns>
        public Task<List<string>?> ListScopes()
        {
            // Verify ClientFactory is initialized. If not, throw.
            if (!ClientFactory.Initialized())
            {
                throw new PravegaException(WrapperErrorMessages.ClientFactoryNotInitialized);
            }

            // Create and pin the callback so it isn't garbage collected.
            TaskCompletionSource<List<string>?> task = new TaskCompletionSource<List<string>?>();
            rustCallbackArray callback = (arrayArray, size) => {

                // Safety checks. If the pointers are null, return an empty list.
                if (arrayArray == IntPtr.Zero)
                {
                    task.SetResult(null);
                }

                // Local variables
                CustomRustString rustStringHolder;
                CustomCSharpString cSharpStringHolder;

                // Turn the array pointer into an array
                CustomRustStringSlice stringArraySlice = Marshal.PtrToStructure<CustomRustStringSlice>(arrayArray);

                // Extract the strings from each array, storing them as CustomRustStrings
                List<string> stringList = new List<string>();
                stringList.Clear();
                for (int i = 0; i < size; i++)
                {
                    // Transfer the array value into a CustomRustString
                    rustStringHolder = stringArraySlice[i];

                    // Transfer the CustomRustString into a CustomCSharpString
                    cSharpStringHolder = new CustomCSharpString(rustStringHolder);

                    // Move the CustomCSharpString into the list as a string.
                    stringList.Add(cSharpStringHolder.NativeString);
                }

                // Set the result of the task to be the derived list
                task.SetResult(stringList);

                // Exit
                return;
            };
            ulong key = CallbackDelegateManager.AddToRustCallbackArrayDictionary(callback);
            Interop.ControllerClientImplListScopes(
                this._rustStructPointer,
                key,
                CallbackDelegateManager.OneTimeInvokeFromRustCallbackArrayDict
            );

            return task.Task;

        }

        /// <summary>
        ///  Given the name of a scope, this function tries to delete the scope from the ClientFactory's server.
        ///  
        ///  Exception cases:
        ///  -Stream/Scope doesn't exist
        ///  -Invalid Stream/Scope name.
        /// </summary>
        /// <param name="targetScope">
        ///  Scope to be deleted
        /// </param>
        /// <returns>
        ///  A task that will be completed in the future. The value will be
        ///  -true when successfully deleted
        ///  -false when unsuccessfully deleted
        /// </returns>
        public Task<bool> DeleteScope(Scope targetScope)
        {
            // Verify ClientFactory is initialized. If not, throw.
            if (!ClientFactory.Initialized())
            {
                throw new PravegaException(WrapperErrorMessages.ClientFactoryNotInitialized);
            }

            // Create and pin the callback so it isn't garbage collected.
            TaskCompletionSource<bool> task = new TaskCompletionSource<bool>();
            rustCallbackU64 callback = (value) => {
                if (value == 1)
                {
                    task.SetResult(true);
                }
                else
                {
                    task.SetResult(false);
                }
            };
            ulong key = CallbackDelegateManager.AddToRustCallbackU64Dictionary(callback);
            Interop.ControllerClientImplCheckScopeExists(
                this._rustStructPointer,
                targetScope.RustString,
                key,
                CallbackDelegateManager.OneTimeInvokeFromRustCallbackU64Dict
            );

            return task.Task;

        }

        /// <summary>
        ///  Creates a stream within the controller client's handle and a scope with the newStream's name.
        /// </summary>
        /// <param name="streamConfiguration">
        ///  configuration to base the stream on
        /// </param>
        /// <returns>
        ///  A task that shows when the stream has been successfully created.
        ///  Set to true when done.
        /// </returns>
        public Task<bool> CreateStream(StreamConfiguration streamConfiguration){

            // Verify ClientFactory is initialized. If not, throw.
            if (!ClientFactory.Initialized()){
                throw new PravegaException(WrapperErrorMessages.ClientFactoryNotInitialized);
            }

            TaskCompletionSource<bool> task = new TaskCompletionSource<bool>();
            rustCallbackU64 callback = (value) => {
                if (value == 1)
                {
                    task.SetResult(true);
                }
                else
                {
                    task.SetResult(false);
                }
            };
            ulong key = CallbackDelegateManager.AddToRustCallbackU64Dictionary(callback);
            
            Interop.ControllerClientImplCreateStream(
                this.RustStructPointer,
                streamConfiguration.ConfigScopedStream.Stream.RustString,
                streamConfiguration.ConfigScopedStream.Scope.RustString,
                (int)streamConfiguration.ConfigScaling.Type,
                streamConfiguration.ConfigScaling.TargetRate,
                streamConfiguration.ConfigScaling.ScaleFactor,
                streamConfiguration.ConfigScaling.MinimumNumberOfSegments,
                (int)streamConfiguration.ConfigRetention.Policy,
                streamConfiguration.ConfigRetention.RetentionParameter,
                key,
                CallbackDelegateManager.OneTimeInvokeFromRustCallbackU64Dict
            );
            
            return task.Task;
        }

        /// <summary>
        /// Checks to see whether a ScopedStream exists in the controller runtime.
        /// 
        ///  Exception cases:
        ///  -Invalid Stream/Scope name.
        /// </summary>
        /// <param name="checkingScopedStream">
        ///  ScopedStream used for checking
        /// </param>
        /// <returns>
        ///  A task that shows when the stream has been successfully created.
        ///  Set to true when done.
        /// </returns>
        public Task<bool> CheckStreamExists(ScopedStream checkingScopedStream)
        {
            // Verify ClientFactory is initialized. If not, throw.
            if (!ClientFactory.Initialized())
            {
                throw new PravegaException(WrapperErrorMessages.ClientFactoryNotInitialized);
            }

            TaskCompletionSource<bool> task = new TaskCompletionSource<bool>();
            rustCallbackU64 callback = (value) => {
                if (value == 1)
                {
                    task.SetResult(true);
                }
                else
                {
                    task.SetResult(false);
                }
            };
            ulong key = CallbackDelegateManager.AddToRustCallbackU64Dictionary(callback);

            Interop.ControllerClientImplCheckStreamExists(
                this.RustStructPointer,
                checkingScopedStream.Stream.RustString,
                checkingScopedStream.Scope.RustString,
                key,
                CallbackDelegateManager.OneTimeInvokeFromRustCallbackU64Dict
            );

            return task.Task;
        }

        /// <summary>
        ///  Given the name of a scopedstream, this function tries to delete said stream in the background.
        ///  Requires that the stream is sealled before deletion.
        /// </summary>
        /// <param name="targetScopedStream">
        ///  ScopedStream to be deleted
        /// </param>
        /// <returns>
        ///  A task that will be completed in the future. The value will be
        ///  -true when successfully deleted
        ///  -false when unsuccessfully deleted
        /// </returns>
        public Task<bool> DeleteStream(ScopedStream targetScopedStream)
        {
            // Verify ClientFactory is initialized. If not, throw.
            if (!ClientFactory.Initialized())
            {
                throw new PravegaException(WrapperErrorMessages.ClientFactoryNotInitialized);
            }

            // Create and pin the callback so it isn't garbage collected.
            TaskCompletionSource<bool> task = new TaskCompletionSource<bool>();
            rustCallbackU64 callback = (value) => {
                if (value == 1)
                {
                    task.SetResult(true);
                }
                else
                {
                    task.SetResult(false);
                }
            };
            ulong key = CallbackDelegateManager.AddToRustCallbackU64Dictionary(callback);
            Interop.ControllerClientImplDeleteStream(
                this._rustStructPointer,
                targetScopedStream.Stream.RustString,
                targetScopedStream.Scope.RustString,
                key,
                CallbackDelegateManager.OneTimeInvokeFromRustCallbackU64Dict
            );

            return task.Task;
        }

        /// <summary>
        ///  Given the name of a scopedstream, this function tries to seal said stream in the background, preventing further writes to it.
        ///  
        ///  Exception cases:
        ///  -Stream/Scope doesn't exist
        ///  -Invalid Stream/Scope name.
        /// </summary>
        /// <param name="targetScopedStream">
        ///  ScopedStream to be sealed
        /// </param>
        /// <returns>
        ///  A task that will be completed in the future. The value will be
        ///  -true when successfully deleted
        ///  -false when unsuccessfully deleted
        /// </returns>
        public Task<bool> SealStream(ScopedStream targetScopedStream)
        {
            // Verify ClientFactory is initialized. If not, throw.
            if (!ClientFactory.Initialized())
            {
                throw new PravegaException(WrapperErrorMessages.ClientFactoryNotInitialized);
            }

            // Create and pin the callback so it isn't garbage collected.
            TaskCompletionSource<bool> task = new TaskCompletionSource<bool>();
            rustCallbackU64 callback = (value) => {
                if (value == 1)
                {
                    task.SetResult(true);
                }
                else
                {
                    task.SetResult(false);
                }
            };
            ulong key = CallbackDelegateManager.AddToRustCallbackU64Dictionary(callback);
            Interop.ControllerClientImplSealStream(
                this._rustStructPointer,
                targetScopedStream.Stream.RustString,
                targetScopedStream.Scope.RustString,
                key,
                CallbackDelegateManager.OneTimeInvokeFromRustCallbackU64Dict
            );

            return task.Task;
        }
    }

}