///
/// File: Config.cs
/// File Creator: John Sbur
/// Purpose: Contains helper structs under the config area. Implements the C# equivalent of the Rust wrapper structs
///
#pragma warning disable 0105
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Numerics;
using System.Runtime.InteropServices;
using Pravega;
using Pravega.Auth;
using Pravega.Retry;
using Pravega.Shared;
using Pravega.Utility;
using Pravega.Retry;

#pragma warning restore 0105

namespace Pravega.Config
{
     // Continues building the Interop class by adding method signatures found in the Config para-module.
    public static partial class Interop {

        // Set path of ClientFactory .dll specifically

        public const string ConfigDLLPath = "config_wrapper.dll";//@"C:\Users\john_\Desktop\Programming\Senior Project CS421\dell-pravegaapi\dell-pravegaapi\Project_Code_Base\cSharpTest\PravegaCSharpLibrary\target\debug\deps\config_wrapper.dll";
        //public const string ConfigDLLPath = @"C:\Users\brand\Documents\Capstone\dell-pravegaapi\Project_Code_Base\cSharpTest\PravegaCSharpLibrary\target\debug\deps\config_wrapper.dll";
        //public const string ConfigDLLPath = "config_wrapper.dll";

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

        // RetryPolicy
        [DllImport(ConfigDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetClientConfigRetryPolicy")]
        internal static extern IntPtr GetClientConfigRetryPolicy(IntPtr sourceClientConfig);
        [DllImport(ConfigDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetClientConfigRetryPolicy")]
        internal static extern void SetClientConfigRetryPolicy(IntPtr sourceClientConfig, IntPtr newPolicy);

        // ControllerUri
        [DllImport(ConfigDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetClientConfigControllerUri")]
        internal static extern IntPtr GetClientConfigControllerUri(IntPtr sourceClientConfig);
        [DllImport(ConfigDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetClientConfigControllerUri")]
        internal static extern void SetClientConfigControllerUri(IntPtr sourceClientConfig, IntPtr newUri);

        // TransactionTimeoutTime
        [DllImport(ConfigDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetClientConfigTransactionTimeoutTime")]
        internal static extern ulong GetClientConfigTransactionTimeoutTime(IntPtr sourceClientConfig);
        [DllImport(ConfigDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetClientConfigTransactionTimeoutTime")]
        internal static extern void SetClientConfigTransactionTimeoutTime(IntPtr sourceClientConfig, ulong newValue);
        
        // Mock
        [DllImport(ConfigDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetClientConfigMock")]
        internal static extern uint GetClientConfigMock(IntPtr sourceClientConfig);
        [DllImport(ConfigDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetClientConfigMock")]
        internal static extern void SetClientConfigMock(IntPtr sourceClientConfig, uint newValue);

        // IsTlsEnabled
        [DllImport(ConfigDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetClientConfigIsTlsEnabled")]
        internal static extern uint GetClientConfigIsTlsEnabled(IntPtr sourceClientConfig);
        [DllImport(ConfigDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "SetClientConfigIsTlsEnabled")]
        internal static extern void SetClientConfigIsTlsEnabled(IntPtr sourceClientConfig, uint newValue);

        // TrustCerts
        [DllImport(ConfigDLLPath, CallingConvention = CallingConvention.Cdecl, EntryPoint = "GetClientExtractTrustCert")]
        internal static extern CustomRustString GetClientExtractTrustCert(IntPtr sourceClientConfig, uint index);

        
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
                if (this.IsNull()){
                    throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
                }
                else{
                    Interop.SetClientConfigMaxConnectionsInPool(this._rustStructPointer, value);
                }
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
                if (this.IsNull()){
                    throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
                }
                else{
                    Interop.SetClientConfigMaxControllerConnections(this._rustStructPointer, value);
                }
            }
        } 
        public RetryWithBackoff RetryPolicy{
            get{
                if (this.IsNull()){
                    throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
                }
                else{
                    RetryWithBackoff newPolicy = new RetryWithBackoff();
                    newPolicy.RustStructPointer = Interop.GetClientConfigRetryPolicy(this._rustStructPointer);
                    return newPolicy;
                }
            }
            set{
                if (this.IsNull() || value.IsNull()){
                    throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
                }
                else{
                    Interop.SetClientConfigRetryPolicy(this._rustStructPointer, value.RustStructPointer);
                }
            }
        }
        public PravegaNodeUri ControllerUri{
            get{
                if (this.IsNull()){
                    throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
                }
                else{
                    PravegaNodeUri newUri = new PravegaNodeUri();
                    newUri.RustStructPointer = Interop.GetClientConfigRetryPolicy(this._rustStructPointer);
                    return newUri;
                }
            }
            set{
                if (this.IsNull() || value.IsNull()){
                    throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
                }
                else{
                    Interop.SetClientConfigRetryPolicy(this._rustStructPointer, value.RustStructPointer);
                }
            }
        }
        public ulong TransactionTimeoutTime{
            get{
                if (this.IsNull()){
                    throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
                }
                else{
                    return Interop.GetClientConfigTransactionTimeoutTime(this._rustStructPointer);
                }
            }
            set{
                if (this.IsNull()){
                    throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
                }
                else{
                    Interop.SetClientConfigTransactionTimeoutTime(this._rustStructPointer, value);
                }
            }
        }
        public bool Mock{
            get{
                if (this.IsNull()){
                    throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
                }
                else{
                    if( Interop.GetClientConfigMock(this._rustStructPointer) == 0)
                    {
                        return false;
                    }
                    else{
                        return true;
                    }
                }
            }
            set{
                if (this.IsNull()){
                    throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
                }
                else{
                    if (value == true){
                        Interop.SetClientConfigMock(this._rustStructPointer, 1);
                    }
                    else{
                        Interop.SetClientConfigMock(this._rustStructPointer, 0);
                    }
                }
            }
        }
        public bool IsTlsEnabled{
            get{
                if (this.IsNull()){
                    throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
                }
                else{
                    if( Interop.GetClientConfigIsTlsEnabled(this._rustStructPointer) == 0)
                    {
                        return false;
                    }
                    else{
                        return true;
                    }
                }
            }
            set{
                if (this.IsNull()){
                    throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
                }
                else{
                    if (value == true){
                        Interop.SetClientConfigIsTlsEnabled(this._rustStructPointer, 1);
                    }
                    else{
                        Interop.SetClientConfigIsTlsEnabled(this._rustStructPointer, 0);
                    }
                }
            }
        }
    }

    //    public List<string> TrustCerts
    //    {
    //        get
    //        {
    //            if (this.IsNull())
    //            {
    //                throw new PravegaException(WrapperErrorMessages.RustObjectNotFound);
    //            }
    //            else
    //            {
    //                List<string> returnList = new List<string>();

    //                // Iterate through the returnedVector and add values to the returnList. Stop once this hits the sentinel
    //                uint i = 0;
    //                CustomCSharpString convertedStringCSharp = new CustomCSharpString(Interop.GetClientExtractTrustCert(this._rustStructPointer, i));
    //                string convertedString = convertedStringCSharp.NativeString;

    //                /*

    //                    Sentinel in lib.rs in Rust

    //                    // Put sentinel for vector so C# knows when to shop iterating
    //                    let sentinel: String = String::from("null string");
    //                    return_vec.push(CustomRustString::from_string(sentinel.clone()));

    //                */
    //                while (convertedString != "null string")
    //                {
    //                    i++;
    //                    returnList.Add(convertedString);
    //                    convertedStringCSharp = new CustomCSharpString(Interop.GetClientExtractTrustCert(this._rustStructPointer, i));
    //                    convertedString = convertedStringCSharp.NativeString;
    //                }

    //                // return when done iterating.
    //                return returnList;
    //            }
    //        }
    //    }


    //}
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