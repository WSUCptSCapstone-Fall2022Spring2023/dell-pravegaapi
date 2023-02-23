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

        public static String CreateDllPath()
        {
           var cwd = System.IO.Directory.GetCurrentDirectory();
           String code_base = "Project_Code_Base";
           int indexTo = cwd.IndexOf(code_base);
           String return_string;
           // If IndexOf could not find code_base String
           if (indexTo == -1)
           {
               return "";
           }
           return_string = cwd.Substring(0, indexTo + code_base.Length);
           return_string += @"\cSharpTest\PravegaCSharpLibrary\target\debug\deps\";

           return return_string;
        }
        [SetUp]
        public void SetupEnvironment()
        {
           Environment.CurrentDirectory = CreateDllPath();
        }
        // / <summary>
        // /  Client Factory Tests
        // / </summary> 
        // Unit Test. Client Factory default constructor
        [Test]
        public void ClientFactoryDefaultConstructorTest()
        {
            ClientFactory testFactory = new ClientFactory();
            Assert.That(testFactory.IsNull(), Is.Not.EqualTo(true));
        }

        // Unit Test. Client Factory default constructor time. Aims for C# being at least 85% efficient
        [Test]
        public void ClientFactoryDefaultConstructorTimeTest()
        {

            // C# time
            Stopwatch timer = new Stopwatch();
            timer.Start();
            ClientFactory testFactory = new ClientFactory();
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
            ClientFactory testFactory = new ClientFactory(testConfig);

            // Make sure client factory was initiated
            Assert.IsFalse(testFactory.IsNull());

            // Make sure that the config information was stored

            Assert.IsTrue(testFactory.Config.MaxConnectionsInPool == 10);

            // Make sure the config inputted was consumed
            Assert.IsTrue(testConfig.IsNull());
        }

        // Unit Test. Client Factory constructor from client config time. Aims for C# being at least 85% efficient
        [Test]
        public void ClientFactoryClientConfigConstructorTimeTest()
        {
            ClientFactory testFactory = new ClientFactory();
            ClientConfig testConfig = testFactory.Config;

            // C# time
            Stopwatch timer = new Stopwatch();
            timer.Start();
            ClientFactory testFactory2 = new ClientFactory(testConfig);
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
            ClientFactory testFactory = new ClientFactory();
            TokioRuntime testRuntime = testFactory.Runtime;

            // Initialze factory with runtime and config created.
            ClientFactory testFactory2 = new ClientFactory(testConfig, testRuntime);

            // Make sure client factory was initiated
            Assert.IsFalse(testFactory2.IsNull());

            // Make sure that the config information was stored
            Assert.IsTrue(testFactory2.Config.MaxConnectionsInPool == 10);

            // Make sure that the client factory's runtime isn't null
            Assert.IsFalse(testFactory2.Runtime.IsNull());

            // Make sure the config inputted was consumed
            Assert.IsTrue(testConfig.IsNull());

            // Make sure the runtime inputted was consumed
            Assert.IsTrue(testRuntime.IsNull());
        }

        // Unit Test. Client Factory constructor from client config and runtime time. Aims for C# being at least 85% efficient
        [Test]
        public void ClientFactoryConfigRuntimeConstructorTimeTest()
        {
            ClientFactory testFactory = new ClientFactory();
            TokioRuntime testRuntime = testFactory.Runtime;
            ClientConfig testConfig = testFactory.Config;

            // C# time
            Stopwatch timer = new Stopwatch();
            timer.Start();
            ClientFactory testFactory2 = new ClientFactory(testConfig, testRuntime);
            timer.Stop();

            // Rust time
            Assert.IsTrue((ClientFactoryTestMethods.ConstructorConfigAndRuntimeTime() * 0.85) < timer.Elapsed.TotalMilliseconds);
        }

        // Unit Test. Client Factory runtime
        [Test] 
        public void ClientFactoryRuntimeTest()
        {
            ClientFactory testFactory = new ClientFactory();
            TokioRuntime testRuntime = testFactory.Runtime;
            Assert.IsFalse(testRuntime.IsNull());
        }

        // Unit Test. Client Factory handle time. Aims for C# being at least 85% efficient
        [Test]
        public void ClientFactoryRuntimeTimeTest()
        {
            ClientFactory testFactory = new ClientFactory();

            // C# time
            Stopwatch timer = new Stopwatch();
            timer.Start();
            TokioRuntime testConfig = testFactory.Runtime;
            timer.Stop();

            // Rust time
            Assert.IsTrue((ClientFactoryTestMethods.RuntimeTime() * 0.85) < timer.Elapsed.TotalMilliseconds);
        }

        // Unit Test. Client Factory handle
        [Test]
        public void ClientFactoryHandleTest()
        {
            ClientFactory testFactory = new ClientFactory();
            TokioHandle testHandle = testFactory.Handle;
            Assert.IsFalse(testHandle.IsNull());
        }

        // Unit Test. Client Factory handle time. Aims for C# being at least 85% efficient
        [Test]
        public void ClientFactoryHandleTimeTest()
        {
            ClientFactory testFactory = new ClientFactory();

            // C# time
            Stopwatch timer = new Stopwatch();
            timer.Start();
            TokioHandle testConfig = testFactory.Handle;
            timer.Stop();

            // Rust time
            Assert.IsTrue((ClientFactoryTestMethods.HandleTime() * 0.85) < timer.Elapsed.TotalMilliseconds);
        }

        // Unit Test. Client Factory config
        [Test]
        public void ClientFactoryConfigTest()
        {
            ClientFactory testFactory = new ClientFactory();
            ClientConfig testConfig = testFactory.Config;
            Assert.IsFalse(testConfig.IsNull());
        }

        // Unit Test. Client Factory config time. Aims for C# being at least 85% efficient
        [Test]
        public void ClientFactoryConfigTimeTest()
        {
            ClientFactory testFactory = new ClientFactory();

            // C# time
            Stopwatch timer = new Stopwatch();
            timer.Start();
            ClientConfig testConfig = testFactory.Config;
            timer.Stop();

            // Rust time
            Assert.IsTrue((ClientFactoryTestMethods.ConfigTime() * 0.85) < timer.Elapsed.TotalMilliseconds);
        }

        // Unit Test. Client Factory to async
        [Test]
        public void ClientFactoryToAsyncTest()
        {
            ClientFactory testFactory = new ClientFactory();
            ClientFactoryAsync testAsyncFactory = testFactory.ToAsync();
            Assert.IsFalse(testAsyncFactory.IsNull());
        }

        // Unit Test. Client Factory to async time. Aims for C# being at least 85% efficient
        [Test]
        public void ClientFactoryAsyncTimeTest()
        {
            ClientFactory testFactory = new ClientFactory();

            // C# time
            Stopwatch timer = new Stopwatch();
            timer.Start();
            ClientFactoryAsync testAsyncFactory = testFactory.ToAsync();
            timer.Stop();

            // Rust time
            Assert.IsTrue((ClientFactoryTestMethods.ToAsyncTime() * 0.85) < timer.Elapsed.TotalMilliseconds);
        }
    }
}