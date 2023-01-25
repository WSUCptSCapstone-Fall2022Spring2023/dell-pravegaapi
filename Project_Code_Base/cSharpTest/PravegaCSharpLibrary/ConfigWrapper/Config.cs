///
/// File: Config.cs
/// File Creator: John Sbur
/// Purpose: Contains helper structs under the config area. Implements the C# equivalent of the Rust wrapper structs
///
#pragma warning disable 0105
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using Pravega;
using Pravega.Auth;
using Pravega.Retry;
using Pravega.Shared;
using Pravega.Utility;

#pragma warning restore 0105

namespace Pravega.Config
{
     // Continues building the Interop class by adding method signatures found in the Config para-module.
    public static partial class Interop {
        
        // Set path of ClientFactory .dll specifically
        public const string ConfigDLLPath = @"C:\Users\john_\Desktop\Programming\Senior Project CS421\dell-pravegaapi\dell-pravegaapi\Project_Code_Base\cSharpTest\PravegaCSharpLibrary\target\debug\deps\config_wrapper.dll";

        ////////
        /// Client Config
        ////////
        // Default constructor
        [DllImport(ConfigDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "CreateClientConfig")]
        internal static extern IntPtr CreateClientConfig();

        // Getters and Setters
        // MaxConnectionsInPool
        [DllImport(ConfigDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetClientConfigMaxConnectionsInPool")]
        internal static extern uint GetClientConfigMaxConnectionsInPool(IntPtr sourceClientConfig);
        [DllImport(ConfigDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetClientConfigMaxConnectionsInPool")]
        internal static extern void SetClientConfigMaxConnectionsInPool(IntPtr sourceClientConfig, uint newValue);

        // MaxControllerConnections
        [DllImport(ConfigDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetClientConfigMaxControllerConnections")]
        internal static extern uint GetClientConfigMaxControllerConnections(IntPtr sourceClientConfig);
        [DllImport(ConfigDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetClientConfigMaxControllerConnections")]
        internal static extern void SetClientConfigMaxControllerConnections(IntPtr sourceClientConfig, uint newValue);
        ////////
        ///
        ////////
    }

    // ***** Wrapper for ClientConfig *****
    public class ClientConfig : RustStructWrapper{
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public virtual string Type(){
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
            return "ClientConfig";
        }

        // Default Constructor. Initializes with default Pravega Client Config
        // -https://github.com/pravega/pravega-client-rust/blob/master/config/src/lib.rs
        public ClientConfig(){
            this._rustStructPointer = Interop.CreateClientConfig();

            // debug
            //Console.WriteLine("client config pointer: " + this._rustStructPointer.ToString());
        }

        // Setters and Getters
        public uint MaxConnectionsInPool{
            get{
                if (this.IsNull()){
                    throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
                }
                else{
                    return Interop.GetClientConfigMaxConnectionsInPool(this._rustStructPointer);
                }
            }
            set{
                Interop.SetClientConfigMaxConnectionsInPool(this._rustStructPointer, value);
            }
        }
        public uint MaxControllerConnections{
            get{
                if (this.IsNull()){
                    throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
                }
                else{
                    return Interop.GetClientConfigMaxControllerConnections(this._rustStructPointer);
                }
            }
            set{
                Interop.SetClientConfigMaxControllerConnections(this._rustStructPointer, value);
            }
        } 

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



    //  ***** Wrapper for Connection Type *****
    public enum ConnectionTypeWrapper
    {
        Happy = 0,
        SegmentIsSealed = 1,
        SegmentIsTruncated = 2,
        WrongHost = 3,
        Tokio = 4,
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

 

    /// Credentials Wrapper Class
    public class Credentials : RustStructWrapper {
#pragma warning disable CS0114 // Member hides inherited member; missing override keyword
        public virtual string Type(){
#pragma warning restore CS0114 // Member hides inherited member; missing override keyword
            return "Config.Credentials";
        }
    }
    /*
    Originally from pravega-client-rust/config/src/credentials.rs
    as:
        #[derive(Debug)]
        pub struct Credentials {
            inner: Box<dyn Cred>,
        }
    */
}