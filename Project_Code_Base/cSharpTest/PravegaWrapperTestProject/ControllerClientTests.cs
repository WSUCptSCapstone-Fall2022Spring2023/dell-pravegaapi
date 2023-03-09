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
    }
}