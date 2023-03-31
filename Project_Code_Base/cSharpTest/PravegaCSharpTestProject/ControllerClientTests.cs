///
/// File: ControllerClientTests.cs
/// File Creator: John Sbur
/// Purpose: Continues testing in the controller client area.
/// 
namespace PravegaWrapperTestProject
{
    using System;
    using System.Runtime.CompilerServices;
    using Pravega;
    using NUnit.Framework;
    using Pravega.ClientFactoryModule;
    using Pravega.Config;
    using System.Threading.Tasks;
    using Pravega.ControllerCli;
    using Pravega.Shared;
    using Pravega.Utility;

    public partial class PravegaCSharpTest
    {
        /// <summary>
        ///  Controller Client Tests
        /// </summary>
        // Unit Test. Controller Client Constructor
        [Test]
        public void ControllerClientConstructor()
        {
            // Initialize Client Factory and grab its configuration
            ClientFactory.Initialize();
            ClientConfig testConfig = ClientFactory.Config;

            // Attempt to create a new controller
            ControllerClient testController = new ControllerClient(testConfig);

            // Verify the controller was initialized
            Assert.IsTrue(testController.IsNull() == false);
        }

        /// <summary>
        /// Unit Test. Create Stream
        /// Requires a running Pravega server.
        /// Cannot be ran in NUnit 
        /// </summary>
        /// <param name="streamName">
        ///  Testing name of the stream
        /// </param>
        /// <returns>
        ///  A bool signifying if the test was successful.
        /// </returns>
        public static bool ControllerClientCreateStream(string scopeToBaseOn, string streamName)
        {
            ClientFactory.Initialize();
            ControllerClient testController = new ControllerClient(ClientFactory.Config);

            // Create a scope to base the stream on.
            Scope testScope = new Scope();
            testScope.NativeString = scopeToBaseOn;
            testController.CreateScope(testScope).GetAwaiter().GetResult();

            // Create a stream config to control the stream
            StreamConfiguration streamConfiguration = new StreamConfiguration();
            streamConfiguration.ConfigScopedStream.Scope = testScope;
            streamConfiguration.ConfigScopedStream.Stream = new CustomCSharpString(streamName);

            // Create the stream
            return testController.CreateStream(streamConfiguration).GetAwaiter().GetResult();

        }

        /// <summary>
        /// Unit Test. Create Scope
        /// Requires a running Pravega server.
        /// Cannot be ran in NUnit 
        /// </summary>
        /// <param name="streamName">
        ///  Testing name of the scope
        /// </param>
        /// <returns>
        ///  A bool signifying if the test was successful.
        /// </returns>
        public static bool ControllerClientCreateScope(string scopeName)
        {
            ClientFactory.Initialize();
            ControllerClient testController = new ControllerClient(ClientFactory.Config);

            // Create a scope and verify signs of life.
            Scope testScope = new Scope();
            testScope.NativeString = scopeName;
            return testController.CreateScope(testScope).GetAwaiter().GetResult();
        }
        
    }
}