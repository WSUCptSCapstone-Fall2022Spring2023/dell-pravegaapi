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
        //public const string ControllerclientDLLPath = @"C:\Users\john_\Desktop\Programming\Senior Project CS421\dell-pravegaapi\dell-pravegaapi\Project_Code_Base\cSharpTest\PravegaCSharpLibrary\target\debug\deps\controller_client_wrapper.dll";
        //public const string ControllerclientDLLPath = @"E:\CptS421\dell-pravegaapi\Project_Code_Base\cSharpTest\PravegaCSharpLibrary\target\debug\deps\controller_client_wrapper.dll";
        public const string ControllerclientDLLPath = @"C:\Users\brand\Documents\Capstone\dell-pravegaapi\Project_Code_Base\cSharpTest\PravegaCSharpLibrary\target\debug\deps\controller_client_wrapper.dll";
        ////////
        /// Controller Client
        ////////
        // ControllerClient default constructor (inputted client config)
        [DllImport(ControllerclientDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CreateControllerCliDefault")]
        internal static extern IntPtr CreateControllerCliDefault(IntPtr clientFactoryPointer, IntPtr clientConfigPointer);
        
        // ControllerClient.create_scope()
        [DllImport(ControllerclientDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ControllerClientImplCreateScope")]
        internal static extern void ControllerClientImplCreateScope(IntPtr clientFactoryPointer, IntPtr controllerClientPointer, CustomRustString newScope, [MarshalAs(UnmanagedType.FunctionPtr)] rustCallback callback);

        // ControllerClient.create_stream()
        [DllImport(ControllerclientDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "ControllerClientImplCreateStream")]
        internal static extern void ControllerClientImplCreateStream(
            IntPtr clientFactoryPointer,
            IntPtr controllerClientPointer,
            CustomRustString newStream,
            CustomRustString targetScope,
            int scalingType,
            int scalingTargetRate,
            int scalingFactor,
            int scalingMinNumSegments,
            int retentionType,
            int retentionParam,
            [MarshalAs(UnmanagedType.FunctionPtr)] rustCallback callback);
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
        ///  Constructor for controller client. Creates using a client config and runtime handle inputted.
        /// </summary>
        public ControllerClient(ClientConfig sourceConfig)
        {
            // Verify ClientFactory is initialized. If not, throw.
            if (!ClientFactory.Initialized()){
                throw new PravegaException(WrapperErrorMessages.ClientFactoryNotInitialized);
            }

            // Verify both objects aren't null
            if (sourceConfig.IsNull())
            {
                throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
            }
            else
            {
                this._rustStructPointer = Interop.CreateControllerCliDefault(ClientFactory.RustStructPointer, sourceConfig.RustStructPointer);

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
            // Verify ClientFactory is initialized. If not, throw.
            if (!ClientFactory.Initialized()){
                throw new PravegaException(WrapperErrorMessages.ClientFactoryNotInitialized);
            }

            TaskCompletionSource<bool> task = new TaskCompletionSource<bool>();
            Interop.ControllerClientImplCreateScope(
                ClientFactory.RustStructPointer,
                this.RustStructPointer,
                newScope.RustString,
                (value) => {
                    task.SetResult(true);
                }
            );
            return task.Task;
        }
        
        /// <summary>
        ///  Creates a scope within the controller client's handle with the newScope's name.
        /// </summary>
        /// <param name="newScope">
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
            Interop.ControllerClientImplCreateStream(
                ClientFactory.RustStructPointer,
                this.RustStructPointer,
                streamConfiguration.ConfigScopedStream.Stream.RustString,
                streamConfiguration.ConfigScopedStream.Scope.RustString,
                (int)streamConfiguration.ConfigScaling.Type,
                streamConfiguration.ConfigScaling.TargetRate,
                streamConfiguration.ConfigScaling.ScaleFactor,
                streamConfiguration.ConfigScaling.MinimumNumberOfSegments,
                (int)streamConfiguration.ConfigRetention.Policy,
                streamConfiguration.ConfigRetention.RetentionParameter,
                (value) => {
                    task.SetResult(true);
                }
            );
            return task.Task;
        }
    }

}