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
        //The number means how many times slower can the C# library be
        const int ConversionLossLimit = 8;

        const int TestingAmount = 10;

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
            double totalTime = 0;
            for (int i = 0; i < TestingAmount; i++)
            {
                // C# time
                Stopwatch timer = new Stopwatch();
                timer.Start();
                ClientFactory testFactory = new ClientFactory();
                timer.Stop();
                double ticks = timer.ElapsedTicks;
                double timerNanoseconds = (ticks / Stopwatch.Frequency) * 1000000000;
                totalTime += timerNanoseconds;
            }
            totalTime = totalTime / TestingAmount; 
            Console.WriteLine("Rust Time: " + ClientFactoryTestMethods.DefaultConstructorTime().ToString());
            Console.WriteLine("C# Time: " + totalTime.ToString());

            Assert.IsTrue(ClientFactoryTestMethods.DefaultConstructorTime() > totalTime / ConversionLossLimit);
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

            double totalTime = 0;
            for (int i = 0; i < TestingAmount; i++)
            {
                ClientFactory testFactory = new ClientFactory();
                ClientConfig testConfig = testFactory.Config;

                // C# time
                Stopwatch timer = new Stopwatch();
                timer.Start();
                ClientFactory testFactory2 = new ClientFactory(testConfig);
                timer.Stop();
                double ticks = timer.ElapsedTicks;
                double timerNanoseconds = (ticks / Stopwatch.Frequency) * 1000000000;
                totalTime += timerNanoseconds;
            }
            totalTime = totalTime / TestingAmount;
            Console.WriteLine("Rust Time: " + ClientFactoryTestMethods.ConstructorConfigTime().ToString());
            Console.WriteLine("C# Time: " + totalTime.ToString());

            Assert.IsTrue((ClientFactoryTestMethods.ConstructorConfigTime() > totalTime / ConversionLossLimit));
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


            // C# time
            double totalTime = 0;
            for (int i = 0; i < TestingAmount; i++)
            {

                ClientFactory testFactory = new ClientFactory();
                TokioRuntime testRuntime = testFactory.Runtime;
                ClientConfig testConfig = testFactory.Config;

                Stopwatch timer = new Stopwatch();
                timer.Start();
                ClientFactory testFactory2 = new ClientFactory(testConfig, testRuntime);
                timer.Stop();
                double ticks = timer.ElapsedTicks;
                double timerNanoseconds = (ticks / Stopwatch.Frequency) * 1000000000;
                totalTime += timerNanoseconds;
            }
            totalTime = totalTime / TestingAmount;
            Console.WriteLine("Rust Time: " + ClientFactoryTestMethods.ConstructorConfigAndRuntimeTime().ToString());
            Console.WriteLine("C# Time: " + totalTime.ToString());

            Assert.IsTrue((ClientFactoryTestMethods.ConstructorConfigAndRuntimeTime() > totalTime / ConversionLossLimit));
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
            double totalTime = 0;
            for (int i = 0; i < TestingAmount; i++)
            {
                ClientFactory testFactory = new ClientFactory();

                // C# time
                Stopwatch timer = new Stopwatch();
                timer.Start();
                TokioRuntime testConfig = testFactory.Runtime;
                timer.Stop();
                double ticks = timer.ElapsedTicks;
                double timerNanoseconds = (ticks / Stopwatch.Frequency) * 1000000000;
                totalTime += timerNanoseconds;
            }
            totalTime = totalTime / TestingAmount;
            Console.WriteLine("Rust Time: " + ClientFactoryTestMethods.RuntimeTime().ToString());
            Console.WriteLine("C# Time: " + totalTime.ToString());

            Assert.IsTrue((ClientFactoryTestMethods.RuntimeTime() > totalTime / ConversionLossLimit));
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
            double totalTime = 0;
            for (int i = 0; i < TestingAmount; i++)
            {
                ClientFactory testFactory = new ClientFactory();

                // C# time
                Stopwatch timer = new Stopwatch();
                timer.Start();
                TokioHandle testConfig = testFactory.Handle;
                timer.Stop();
                double ticks = timer.ElapsedTicks;
                double timerNanoseconds = (ticks / Stopwatch.Frequency) * 1000000000;
                totalTime += timerNanoseconds;
            }
            totalTime = totalTime / TestingAmount;
            Console.WriteLine("Rust Time: " + ClientFactoryTestMethods.HandleTime().ToString());
            Console.WriteLine("C# Time: " + totalTime.ToString());

            Assert.IsTrue((ClientFactoryTestMethods.HandleTime() > totalTime / ConversionLossLimit));
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

            double totalTime = 0;
            for (int i = 0; i < TestingAmount; i++)
            {
                ClientFactory testFactory = new ClientFactory();

                // C# time
                Stopwatch timer = new Stopwatch();
                timer.Start();
                ClientConfig testConfig = testFactory.Config;
                timer.Stop();
                double ticks = timer.ElapsedTicks;
                double timerNanoseconds = (ticks / Stopwatch.Frequency) * 1000000000;
                totalTime += timerNanoseconds;
            }
            totalTime = totalTime / TestingAmount;
            Console.WriteLine("Rust Time: " + ClientFactoryTestMethods.ConfigTime().ToString());
            Console.WriteLine("C# Time: " + totalTime.ToString());

            Assert.IsTrue((ClientFactoryTestMethods.ConfigTime() > totalTime / ConversionLossLimit));
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
            double totalTime = 0;
            for (int i = 0; i < TestingAmount; i++)
            {
                ClientFactory testFactory = new ClientFactory();

                // C# time
                Stopwatch timer = new Stopwatch();
                timer.Start();
                ClientFactoryAsync testAsyncFactory = testFactory.ToAsync();
                timer.Stop();
                double ticks = timer.ElapsedTicks;
                double timerNanoseconds = (ticks / Stopwatch.Frequency) * 1000000000;
                totalTime += timerNanoseconds;
            }
            totalTime = totalTime / TestingAmount;
            Console.WriteLine("Rust Time: " + ClientFactoryTestMethods.HandleTime().ToString());
            Console.WriteLine("C# Time: " + totalTime.ToString());

            Assert.IsTrue((ClientFactoryTestMethods.ToAsyncTime() > totalTime / ConversionLossLimit));
        }
    }
}