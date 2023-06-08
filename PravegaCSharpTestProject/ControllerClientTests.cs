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
    using Pravega.Byte;
    using Pravega.ControllerCli;
    using Pravega.Shared;
    using Pravega.Utility;

    public partial class PravegaCSharpTest
    {
        /// <summary>
        ///  Controller Client Tests
        /// </summary>

        /// <summary>
        ///  ControllerClient Create Scope Test. Tests the ability to create scopes on a Pravega server from C#
        ///  
        ///  Prereq. 
        ///  -Pravega Standalone Server is running locally.
        /// </summary>
        /// <param name="scopeName">
        ///  Scope being tested
        /// </param>
        /// <param name="testCase">
        ///  Type of test being done. 
        ///  1 = normal
        ///  2 = boarder
        ///  3 = exception
        /// </param>
        [Test]
        [TestCase(" ", 3)]
        [TestCase("testScope")]
        public void ControllerClientCreateScope(string scopeName, int testCase=1)
        {
            ClientFactory.Initialize();
            ControllerClient testController = ClientFactory.FactoryControllerClient;

            // Normal case
            if (testCase == 1)
            {
                // Create a scope and verify signs of life.
                Scope testScope = new Scope();
                testScope.NativeString = scopeName;
                Assert.IsTrue(testController.CreateScope(testScope).GetAwaiter().GetResult());
            }

            // Exception case
            else if (testCase == 3)
            {
                try
                {
                    // Create a scope and verify signs of life.
                    Scope testScope = new Scope();
                    testScope.NativeString = scopeName;
                    Assert.IsTrue(testController.CreateScope(testScope).GetAwaiter().GetResult());
                }
                catch
                {
                    Assert.Pass();
                }
                Assert.Fail();
            }
        }

        /// <summary>
        ///  ControllerClient Check Scope Exists Test. Tests the ability of the controller client to tell whether a scope exists on the server or not.
        ///  
        ///  Prereq. 
        ///  -Pravega Server is running.
        /// </summary>
        /// <param name="testCase">
        ///  Type of test being done. 
        ///  1 = normal
        ///  2 = boarder
        ///  3 = exception
        /// </param>
        [Test]
        [TestCase(1)]
        [TestCase(3)]
        public void ControllerClientCheckScopeExists(int testCase = 1)
        {
            // Initialize factory
            ClientFactory.Initialize();

            // Make a controller client
            ControllerClient testController = ClientFactory.FactoryControllerClient;

            switch (testCase) {

                // Normal Case
                case 1:

                    // Generate a randomly named scope
                    string randomScopeString = RandomString(8);

                    // Verify the scope doesn't exist
                    Scope randomScope = new Scope();
                    randomScope.NativeString = randomScopeString;
                    Assert.IsFalse(testController.CheckScopeExists(randomScope).GetAwaiter().GetResult());

                    // Create the Scope, then check that it does exist.
                    // 1) Create Scope
                    Assert.IsTrue(testController.CreateScope(randomScope).GetAwaiter().GetResult());
                    // 2) Verify Existance
                    Assert.IsTrue(testController.CheckScopeExists(randomScope).GetAwaiter().GetResult());

                    break;

                // Boundary Case
                case 2:

                    // Not implemented
                    Assert.Fail();

                    break;

                // Exception Case
                case 3:

                    // Similar procedure to normal case, though we expect that when inputting " ", the code crashes
                    string exceptionScopeString = " ";

                    // Verify the scope doesn't exist and catch when it throws
                    Scope exceptionScope = new Scope();
                    exceptionScope.NativeString = exceptionScopeString;
                    try
                    {
                        testController.CheckScopeExists(exceptionScope).GetAwaiter().GetResult();
                        Assert.Fail();
                    }
                    catch
                    {
                        Assert.Pass();
                    }

                    break;

                
                // Default to fail
                default:
                    Assert.Fail();
                    break;

            }
        }

        /// <summary>
        ///  ControllerClient Delete Scope Test. Tests the ability of the controller client to delete existing scopes
        ///  
        ///  Prereq. 
        ///  -Pravega Standalone Server is running locally.
        /// </summary>
        /// <param name="testCase">
        ///  Type of test being done. 
        ///  1 = normal
        ///  2 = boarder
        ///  3 = exception
        /// </param>
        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void ControllerClientDeleteScope(int testCase = 1)
        {
            // Initialize factory
            ClientFactory.Initialize();

            // Make a controller client
            ControllerClient testController = ClientFactory.FactoryControllerClient;

            switch (testCase)
            {

                // Normal Case
                case 1:

                    // Generate a randomly named scope
                    string randomScopeString = RandomString(8);
                    int checks = 100;

                    // Verify the scope doesn't exist
                    Scope randomScope = new Scope();
                    randomScope.NativeString = randomScopeString;
                    Assert.IsFalse(testController.CheckScopeExists(randomScope).GetAwaiter().GetResult());

                    // Create the Scope, then delete it. It doesn't exists after.
                    // 1) Create Scope
                    Assert.IsTrue(testController.CreateScope(randomScope).GetAwaiter().GetResult());
                    // 2) Verify Existance
                    Assert.IsTrue(testController.CheckScopeExists(randomScope).GetAwaiter().GetResult());
                    // 3) Delete Scope
                    Assert.IsTrue(testController.DeleteScope(randomScope).GetAwaiter().GetResult());
                    // 4) Verify it was Deleted through a loop (deleting a scope isn't instantaneous)
                    // If in the loop it does pass, then that means it was deleted in the background. If not, then
                    //  it's considered timed out and therefore fails.
                    while (checks > 0)
                    {
                        if (testController.CheckScopeExists(randomScope).GetAwaiter().GetResult() == true){
                            Assert.Pass();
                            break;
                        }
                        checks--;
                    }
                    Assert.Fail();

                    break;

                // Boundary Case
                case 2:

                    // Generate a randomly named scope
                    string boundaryScopeString = RandomString(8);
                    Scope boundaryScope = new Scope();
                    boundaryScope.NativeString = boundaryScopeString;

                    // Try deleting the Scope, even though it doesn't exist. Verify it was unsuccessfully deleted.
                    Assert.IsFalse(testController.DeleteScope(boundaryScope).GetAwaiter().GetResult());

                    break;

                // Exception Case
                case 3:

                    // Similar procedure to normal case, though we expect that when inputting " ", the code crashes
                    string exceptionScopeString = " ";

                    // Verify the scope doesn't exist and catch when it throws
                    Scope exceptionScope = new Scope();
                    exceptionScope.NativeString = exceptionScopeString;
                    try
                    {
                        testController.DeleteScope(exceptionScope).GetAwaiter().GetResult();
                        Assert.Fail();
                    }
                    catch
                    {
                        Assert.Pass();
                    }

                    break;


                // Default to fail
                default:
                    Assert.Fail();
                    break;

            }
        }

        /// <summary>
        ///  ControllerClient Create Stream Test. Tests the ability to create streams on a Pravega server from C#
        ///  
        ///  Prereq. 
        ///  -Pravega Standalone Server is running locally.
        /// </summary>
        /// <param name="scopeToBaseOn">
        ///  Scope the stream is to be based on
        /// </param>
        /// <param name="streamName">
        ///  Stream being tested
        /// </param>
        /// <param name="testCase">
        ///  Type of test being done. 
        ///  1 = normal
        ///  2 = boarder
        ///  3 = exception
        /// </param>
        [Test]
        [TestCase("testScope", " ", 3)]
        [TestCase("testScope", "testStream")]
        public void ControllerClientCreateStream(string scopeToBaseOn, string streamName, int testCase = 1)
        {
            ClientFactory.Initialize();
            ControllerClient testController = ClientFactory.FactoryControllerClient;

            // Create a scope to base the stream on.
            Scope testScope = new Scope();
            testScope.NativeString = scopeToBaseOn;
            testController.CreateScope(testScope).GetAwaiter().GetResult();

            // Normal case
            if (testCase == 1)
            {
                // Create a stream config to control the stream
                StreamConfiguration streamConfiguration = new StreamConfiguration();
                streamConfiguration.ConfigScopedStream.Scope = testScope;
                streamConfiguration.ConfigScopedStream.Stream = new CustomCSharpString(streamName);

                // Create the stream
                Assert.IsTrue(testController.CreateStream(streamConfiguration).GetAwaiter().GetResult());

            }

            // Exception Case
            else if (testCase == 3)
            {
                try
                {
                    // Create a stream config to control the stream
                    StreamConfiguration streamConfiguration = new StreamConfiguration();
                    streamConfiguration.ConfigScopedStream.Scope = testScope;
                    streamConfiguration.ConfigScopedStream.Stream = new CustomCSharpString(streamName);

                    // Create the stream
                    Assert.IsTrue(testController.CreateStream(streamConfiguration).GetAwaiter().GetResult());
                }
                catch
                {
                    Assert.Pass();
                }
                Assert.Fail();
            }

        }

        /// <summary>
        ///  ControllerClient Check Stream Exists Test. Tests the ability of the controller client to tell whether a stream exists on a scope on the server or not.
        ///  
        ///  Prereq. 
        ///  -Pravega Standalone Server is running locally.
        /// </summary>
        /// <param name="testCase">
        ///  Type of test being done. 
        ///  1 = normal
        ///  2 = boarder
        ///  3 = exception
        /// </param>
        [Test]
        [TestCase(1)]
        [TestCase(3)]
        public void ControllerClientCheckStreamExists(int testCase = 1)
        {
            // Initialize factory
            ClientFactory.Initialize();

            // Make a controller client
            ControllerClient testController = ClientFactory.FactoryControllerClient;

            switch (testCase)
            {

                // Normal Case
                case 1:

                    // Generate a randomly named scope
                    string randomScopeString = RandomString(8);

                    // Verify the scope doesn't exist
                    Scope randomScope = new Scope();
                    randomScope.NativeString = randomScopeString;
                    Assert.IsFalse(testController.CheckScopeExists(randomScope).GetAwaiter().GetResult());

                    // Generate a randomly named stream
                    string randomStreamString = RandomString(8);

                    // Create the scoped stream
                    Stream randomStream = new Stream();
                    randomStream.NativeString = randomStreamString;
                    ScopedStream randomScopedStream = new ScopedStream();
                    randomScopedStream.Scope = randomScope;
                    randomScopedStream.Stream = randomStream;

                    // Verify the stream doesn't exist
                    Assert.IsFalse(testController.CheckStreamExists(randomScopedStream).GetAwaiter().GetResult());

                    // Create the Stream, then check that it does exist.
                    // 1) Create Scope
                    Assert.IsTrue(testController.CreateScope(randomScope).GetAwaiter().GetResult());
                    // 2) Create the Stream 
                    StreamConfiguration randomStreamConfig = new StreamConfiguration();
                    randomStreamConfig.ConfigScopedStream = randomScopedStream;
                    Assert.IsTrue(testController.CreateStream(randomStreamConfig).GetAwaiter().GetResult());
                    // 3) Verify Existance
                    Assert.IsTrue(testController.CheckStreamExists(randomScopedStream).GetAwaiter().GetResult());

                    break;

                // Boundary Case
                case 2:

                    // Not implemented
                    Assert.Fail();

                    break;

                // Exception Case
                case 3:

                    // Generate a randomly named scope 
                    string exceptionScopeString = RandomString(8);
                    Scope exceptionScope = new Scope();
                    exceptionScope.NativeString = exceptionScopeString;
                    testController.CreateScope(exceptionScope).GetAwaiter().GetResult();

                    // Similar procedure to normal case, though we expect that when inputting " " for the stream, the code crashes
                    string exceptionStreamString = " ";

                    // Verify the stream doesn't exist and catch when it throws
                    Stream exceptionStream = new Stream();
                    exceptionScope.NativeString = exceptionStreamString;
                    ScopedStream exceptionScopedStream = new ScopedStream();
                    exceptionScopedStream.Scope = exceptionScope;
                    exceptionScopedStream.Stream = exceptionStream;
                    try
                    {
                        testController.CheckStreamExists(exceptionScopedStream).GetAwaiter().GetResult();
                        Assert.Fail();
                    }
                    catch
                    {
                        Assert.Pass();
                    }

                    break;


                // Default to fail
                default:
                    Assert.Fail();
                    break;

            }
        }

        /// <summary>
        ///  ControllerClient Delete Stream Test. Tests the ability of the controller client to delete existing streams
        ///  
        ///  Prereq. 
        ///  -Pravega Standalone Server is running locally.
        /// </summary>
        /// <param name="testCase">
        ///  Type of test being done. 
        ///  1 = normal
        ///  2 = boarder
        ///  3 = exception
        /// </param>
        [Test]
        [TestCase(1)]
        [TestCase(2)]
        [TestCase(3)]
        public void ControllerClientDeleteStream(int testCase = 1)
        {
            // Initialize factory
            ClientFactory.Initialize();

            // Make a controller client
            ControllerClient testController = ClientFactory.FactoryControllerClient;

            switch (testCase)
            {

                // Normal Case
                case 1:

                    // Generate a randomly named scope and stream
                    string randomScopeString = RandomString(8);
                    int checks = 100;

                    // Verify the scopedstream doesn't exist
                    Scope randomScope = new Scope();
                    randomScope.NativeString = randomScopeString;
                    Assert.IsFalse(testController.CheckScopeExists(randomScope).GetAwaiter().GetResult());

                    // Generate a randomly named stream
                    string randomStreamString = RandomString(8);

                    // Create the scoped stream
                    Stream randomStream = new Stream();
                    randomStream.NativeString = randomStreamString;
                    ScopedStream randomScopedStream = new ScopedStream();
                    randomScopedStream.Scope = randomScope;
                    randomScopedStream.Stream = randomStream;

                    // Verify the stream doesn't exist
                    Assert.IsFalse(testController.CheckStreamExists(randomScopedStream).GetAwaiter().GetResult());

                    // Create the Stream, then check that it does exist.
                    // 1) Create Scope
                    Assert.IsTrue(testController.CreateScope(randomScope).GetAwaiter().GetResult());
                    // 2) Create the Stream 
                    StreamConfiguration randomStreamConfig = new StreamConfiguration();
                    randomStreamConfig.ConfigScopedStream = randomScopedStream;
                    Assert.IsTrue(testController.CreateStream(randomStreamConfig).GetAwaiter().GetResult());
                    // 3) Seal the stream
                    Assert.IsTrue(testController.SealStream(randomScopedStream).GetAwaiter().GetResult());
                    // 3) Delete the Stream
                    Assert.IsTrue(testController.DeleteStream(randomScopedStream).GetAwaiter().GetResult());
                    // 4) Verify it was Deleted through a loop (deleting a scope isn't instantaneous)
                    // If in the loop it does pass, then that means it was deleted in the background. If not, then
                    //  it's considered timed out and therefore fails.
                    while (checks > 0)
                    {
                        if (testController.CheckStreamExists(randomScopedStream).GetAwaiter().GetResult() == true)
                        {
                            Assert.Pass();
                            break;
                        }
                        checks--;
                    }
                    Assert.Fail();

                    break;

                // Boundary Case
                case 2:

                    // Generate a randomly named scope and stream
                    string boundaryScopeString = RandomString(8);
                    Scope boundaryScope = new Scope();
                    boundaryScope.NativeString = boundaryScopeString;
                    Stream boundaryStream = new Stream();
                    boundaryStream.NativeString = boundaryScopeString;
                    ScopedStream boundaryScopedStream = new ScopedStream();
                    boundaryScopedStream.Scope = boundaryScope;
                    boundaryScopedStream.Stream = boundaryStream;

                    // Try deleting the Stream, even though it doesn't exist. Verify it was unsuccessfully deleted.
                    Assert.IsFalse(testController.DeleteStream(boundaryScopedStream).GetAwaiter().GetResult());

                    break;

                // Exception Case
                case 3:

                    // Generate a randomly named scope 
                    string exceptionScopeString = RandomString(8);
                    Scope exceptionScope = new Scope();
                    exceptionScope.NativeString = exceptionScopeString;
                    testController.CreateScope(exceptionScope).GetAwaiter().GetResult();

                    // Similar procedure to normal case, though we expect that when inputting " " for the stream, the code crashes
                    string exceptionStreamString = " ";

                    // Verify the stream doesn't exist and catch when it throws
                    Stream exceptionStream = new Stream();
                    exceptionScope.NativeString = exceptionStreamString;
                    ScopedStream exceptionScopedStream = new ScopedStream();
                    exceptionScopedStream.Scope = exceptionScope;
                    exceptionScopedStream.Stream = exceptionStream;
                    try
                    {
                        testController.DeleteStream(exceptionScopedStream).GetAwaiter().GetResult();
                        Assert.Fail();
                    }
                    catch
                    {
                        Assert.Pass();
                    }

                    break;


                // Default to fail
                default:
                    Assert.Fail();
                    break;

            }
        }

        [Test]
        [TestCase(1)]
        [TestCase(3)]
        public void ControllerClientSealStream(int testCase = 1)
        {
            // Initialize factory
            ClientFactory.Initialize();

            // Make a controller client
            ControllerClient testController = ClientFactory.FactoryControllerClient;

            switch (testCase)
            {

                // Normal Case
                case 1:

                    // Generate a randomly named scope and stream
                    string randomScopeString = RandomString(8);

                    // Verify the scopedstream doesn't exist
                    Scope randomScope = new Scope();
                    randomScope.NativeString = randomScopeString;
                    Assert.IsFalse(testController.CheckScopeExists(randomScope).GetAwaiter().GetResult());

                    // Generate a randomly named stream
                    string randomStreamString = RandomString(8);

                    // Create the scoped stream
                    Stream randomStream = new Stream();
                    randomStream.NativeString = randomStreamString;
                    ScopedStream randomScopedStream = new ScopedStream();
                    randomScopedStream.Scope = randomScope;
                    randomScopedStream.Stream = randomStream;

                    // Verify the stream doesn't exist
                    Assert.IsFalse(testController.CheckStreamExists(randomScopedStream).GetAwaiter().GetResult());

                    // Create the Stream, then check that it does exist.
                    // 1) Create Scope
                    Assert.IsTrue(testController.CreateScope(randomScope).GetAwaiter().GetResult());
                    // 2) Create the Stream 
                    StreamConfiguration randomStreamConfig = new StreamConfiguration();
                    randomStreamConfig.ConfigScopedStream = randomScopedStream;
                    Assert.IsTrue(testController.CreateStream(randomStreamConfig).GetAwaiter().GetResult());
                    // 3) Seal the Stream
                    Assert.IsTrue(testController.SealStream(randomScopedStream).GetAwaiter().GetResult());
                    // 4) Verify it was Sealed by trying to write to it. 
                    ByteWriter testWriter = ClientFactory.CreateByteWriter(randomScopedStream).GetAwaiter().GetResult();
                    try
                    {
                        List<byte> testList = new List<byte>();
                        testList.Add(1);
                        testWriter.Write(testList);
                        Assert.Fail();
                    }
                    catch
                    {
                        Assert.Pass();
                    }

                    break;

                // Boundary Case
                case 2:

                    // Not implemented
                    Assert.Fail();

                    break;

                // Exception Case
                case 3:

                    // Exception case 1, invalid stream name.
                    // Generate a randomly named scope 
                    string exceptionScopeString = RandomString(8);
                    Scope exceptionScope = new Scope();
                    exceptionScope.NativeString = exceptionScopeString;
                    testController.CreateScope(exceptionScope).GetAwaiter().GetResult();

                    // Similar procedure to normal case, though we expect that when inputting " " for the stream, the code crashes
                    string exceptionStreamString = " ";

                    // Try to seal the stream with the invalid name, causing it to throw.
                    Stream exceptionStream = new Stream();
                    exceptionScope.NativeString = exceptionStreamString;
                    ScopedStream exceptionScopedStream = new ScopedStream();
                    exceptionScopedStream.Scope = exceptionScope;
                    exceptionScopedStream.Stream = exceptionStream;
                    try
                    {
                        testController.SealStream(exceptionScopedStream).GetAwaiter().GetResult();
                        Assert.Fail();
                    }
                    catch
                    {
                        Assert.Pass();
                    }

                    // Exception case 2, stream doesn't exist

                    // Generate a randomly named scope and stream
                    exceptionScopeString = RandomString(8);
                    exceptionScope = new Scope();
                    exceptionScope.NativeString = exceptionScopeString;
                    exceptionStream = new Stream();
                    exceptionStream.NativeString = exceptionStreamString;
                    exceptionScopedStream = new ScopedStream();
                    exceptionScopedStream.Scope = exceptionScope;
                    exceptionScopedStream.Stream = exceptionStream;

                    // Try sealing the Stream, even though it doesn't exist. Verify it was unsuccessfully deleted.
                    try
                    {
                        Assert.IsFalse(testController.SealStream(exceptionScopedStream).GetAwaiter().GetResult());
                        Assert.Fail();
                    }
                    catch
                    {
                        Assert.Pass();
                    }

                    break;


                // Default to fail
                default:
                    Assert.Fail();
                    break;

            }
        }
    }
}