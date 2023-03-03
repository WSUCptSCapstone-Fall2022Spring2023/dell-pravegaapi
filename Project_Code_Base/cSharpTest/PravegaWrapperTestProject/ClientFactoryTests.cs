///
/// File: ClientFactoryTests.cs
/// File Creator: John Sbur
/// Purpose: Continues testing in the client factory area.
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
    using Pravega.Utility;
    using System.Diagnostics;

    public partial class PravegaCSharpTest
    {
        /// <summary>
        ///  Client Factory Tests
        /// </summary> 
        // Unit Test. Client Factory default constructor
        [Test]
        public void ClientFactoryDefaultConstructorTest()
        {
            ClientFactory.Initialize();
            Assert.That(ClientFactory.Initialized(), Is.Not.EqualTo(false));
        }

        // Unit Test. Client Factory default constructor time. Aims for C# being at least 85% efficient
        [Test]
        public void ClientFactoryDefaultConstructorTimeTest()
        {

            // C# time
            Stopwatch timer = new Stopwatch();
            timer.Start();
            ClientFactory.Initialize();
            timer.Stop();

            // Rust time
            Assert.IsTrue((ClientFactoryTestMethods.DefaultConstructorTime() * 0.85) < timer.Elapsed.TotalMilliseconds);
        }

        // Unit Test. Client Factory constructor from client config
        [Test]
        public void ClientFactoryClientConfigConstructorTest()
        {
            ClientConfig testConfig = new ClientConfig();
            testConfig.MaxConnectionsInPool = 10;
            ClientFactory.Initialize(testConfig);

            // Make sure client factory was initiated
            Assert.IsTrue(ClientFactory.Initialized());

            // Make sure that the config information was stored
            Assert.IsTrue(ClientFactory.Config.MaxConnectionsInPool == 10);

            // Make sure the config inputted was consumed
            Assert.IsTrue(testConfig.IsNull());
        }

        // Unit Test. Client Factory constructor from client config time. Aims for C# being at least 85% efficient
        [Test]
        public void ClientFactoryClientConfigConstructorTimeTest()
        {
            ClientFactory.Initialize();
            ClientConfig testConfig = ClientFactory.Config;
            ClientFactory.Destroy();

            // C# time
            Stopwatch timer = new Stopwatch();
            timer.Start();
            ClientFactory.Initialize(testConfig); 
            timer.Stop();

            // Rust time
            Assert.IsTrue((ClientFactoryTestMethods.ConstructorConfigTime() * 0.85) < timer.Elapsed.TotalMilliseconds);
        }

        // Unit Test. Client Factory constructor from client config and runtime
        [Test]
        public void ClientFactoryConfigRuntimeConstructorTest()
        {
            // Create a config for testing.
            ClientConfig testConfig = new ClientConfig();
            testConfig.MaxConnectionsInPool = 10;

            // Create a factory and extract a runtime from it
            ClientFactory.Initialize();
            TokioRuntime testRuntime = ClientFactory.Runtime;
            ClientFactory.Destroy();

            // Initialze factory with runtime and config created.
            ClientFactory.Initialize(testConfig, testRuntime);

            // Make sure client factory was initiated
            Assert.IsFalse(ClientFactory.Initialized());

            // Make sure that the config information was stored
            Assert.IsTrue(ClientFactory.Config.MaxConnectionsInPool == 10);

            // Make sure that the client factory's runtime isn't null
            Assert.IsFalse(ClientFactory.Runtime.IsNull());

            // Make sure the config inputted was consumed
            Assert.IsTrue(testConfig.IsNull());

            // Make sure the runtime inputted was consumed
            Assert.IsTrue(testRuntime.IsNull());
        }

        // Unit Test. Client Factory constructor from client config and runtime time. Aims for C# being at least 85% efficient
        [Test]
        public void ClientFactoryConfigRuntimeConstructorTimeTest()
        {
            ClientFactory.Initialize();
            TokioRuntime testRuntime = ClientFactory.Runtime;
            ClientConfig testConfig = ClientFactory.Config;

            // C# time
            Stopwatch timer = new Stopwatch();
            timer.Start();
            ClientFactory.Initialize(testConfig, testRuntime);
            timer.Stop();

            // Rust time
            Assert.IsTrue((ClientFactoryTestMethods.ConstructorConfigAndRuntimeTime() * 0.85) < timer.Elapsed.TotalMilliseconds);
        }

        // Unit Test. Client Factory runtime
        [Test] 
        public void ClientFactoryRuntimeTest()
        {
            ClientFactory.Initialize();
            TokioRuntime testRuntime = ClientFactory.Runtime;
            Assert.IsFalse(testRuntime.IsNull());
        }

        // Unit Test. Client Factory handle time. Aims for C# being at least 85% efficient
        [Test]
        public void ClientFactoryRuntimeTimeTest()
        {
            ClientFactory.Initialize(); 

            // C# time
            Stopwatch timer = new Stopwatch();
            timer.Start();
            TokioRuntime testConfig = ClientFactory.Runtime;
            timer.Stop();

            // Rust time
            Assert.IsTrue((ClientFactoryTestMethods.RuntimeTime() * 0.85) < timer.Elapsed.TotalMilliseconds);
        }

        // Unit Test. Client Factory handle
        [Test]
        public void ClientFactoryHandleTest()
        {
            ClientFactory.Initialize();
            TokioHandle testHandle = ClientFactory.Handle;
            Assert.IsFalse(testHandle.IsNull());
        }

        // Unit Test. Client Factory handle time. Aims for C# being at least 85% efficient
        [Test]
        public void ClientFactoryHandleTimeTest()
        {
            ClientFactory.Initialize();

            // C# time
            Stopwatch timer = new Stopwatch();
            timer.Start();
            TokioHandle testConfig = ClientFactory.Handle;
            timer.Stop();

            // Rust time
            Assert.IsTrue((ClientFactoryTestMethods.HandleTime() * 0.85) < timer.Elapsed.TotalMilliseconds);
        }

        // Unit Test. Client Factory config
        [Test]
        public void ClientFactoryConfigTest()
        {
            ClientFactory.Initialize();
            ClientConfig testConfig = ClientFactory.Config;
            Assert.IsFalse(testConfig.IsNull());
        }

        // Unit Test. Client Factory config time. Aims for C# being at least 85% efficient
        [Test]
        public void ClientFactoryConfigTimeTest()
        {
            ClientFactory.Initialize();

            // C# time
            Stopwatch timer = new Stopwatch();
            timer.Start();
            ClientConfig testConfig = ClientFactory.Config;
            timer.Stop();

            // Rust time
            Assert.IsTrue((ClientFactoryTestMethods.ConfigTime() * 0.85) < timer.Elapsed.TotalMilliseconds);
        }

        // Unit Test. Client Factory to async
        [Test]
        public void ClientFactoryToAsyncTest()
        {
            ClientFactory.Initialize();
            ClientFactoryAsync testAsyncFactory = ClientFactory.ToAsync();
            Assert.IsFalse(testAsyncFactory.IsNull());
        }

        // Unit Test. Client Factory to async time. Aims for C# being at least 85% efficient
        [Test]
        public void ClientFactoryAsyncTimeTest()
        {
            ClientFactory.Initialize();

            // C# time
            Stopwatch timer = new Stopwatch();
            timer.Start();
            ClientFactoryAsync testAsyncFactory = ClientFactory.ToAsync();
            timer.Stop();

            // Rust time
            Assert.IsTrue((ClientFactoryTestMethods.ToAsyncTime() * 0.85) < timer.Elapsed.TotalMilliseconds);
        }
    }
}