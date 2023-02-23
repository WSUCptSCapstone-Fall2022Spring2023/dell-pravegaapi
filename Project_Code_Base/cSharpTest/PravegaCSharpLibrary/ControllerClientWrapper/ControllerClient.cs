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
using static Pravega.Interop;
using System.Threading.Tasks;
#pragma warning restore 0105

namespace Pravega.ControllerCli
{
    // Continues building the Interop class by adding method signatures found in Client Factory.
    public static partial class Interop
    {
        public const string ClientFactoryDLLPath = @"C:\Users\john_\Desktop\Programming\Senior Project CS421\dell-pravegaapi\dell-pravegaapi\Project_Code_Base\cSharpTest\PravegaCSharpLibrary\target\debug\deps\controller_client_wrapper.dll";
        
        ////////
        /// Controller Client
        ////////
        // ControllerClient default constructor (inputted client config, inputted handle)
        [DllImport(ClientFactoryDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CreateControllerCliDefault")]
        internal static extern IntPtr CreateControllerCliDefault(IntPtr clientConfigPointer, IntPtr handlePointer);

        // ControllerClient.create_scope()
        [DllImport(ClientFactoryDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ControllerClientImplCreateScope")]
        internal static extern void ControllerClientImplCreateScope(IntPtr controllerClientPointer, CustomRustString newScope, [MarshalAs(UnmanagedType.FunctionPtr)] rustCallback callback);
    }

    /// <summary>
    ///  Controller APIs for administrative action for streams
    /// </summary>
    public class ControllerClient : RustStructWrapper
    {
        
        /// <summary>
        ///  Default constructor for controller client. Creates with a null pointer
        /// </summary>
        public ControllerClient(){
            this._rustStructPointer = IntPtr.Zero;
        }

        /// <summary>
        ///  Constructor for controller client. Creates using a client config and runtime handle inputted.
        /// </summary>
        public ControllerClient(ClientConfig sourceConfig, TokioHandle sourceHandle)
        {
            // Verify both objects aren't null
            if (sourceConfig.IsNull() || sourceHandle.IsNull())
            {
                throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
            }
            else
            {
                this._rustStructPointer = Interop.CreateControllerCliDefault(sourceConfig.RustStructPointer, sourceHandle.RustStructPointer);

                // Mark client config as null since it was consumed.
                sourceConfig.MarkAsNull();
            }
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
            TaskCompletionSource<bool> task = new TaskCompletionSource<bool>();
            Interop.ControllerClientImplCreateScope(
                this.RustStructPointer,
                newScope.RustString,
                (value) => {
                    task.SetResult(true);
                }
            );
            return task.Task;
        }
        
        
    }

}