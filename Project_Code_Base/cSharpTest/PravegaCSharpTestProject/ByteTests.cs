///
/// File: ByteTests.cs
/// File Creator: John Sbur
/// Purpose: Continues testing in the byte area.
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
    using Pravega.Shared;
    using Pravega.Byte;
    using Pravega.ControllerCli;
    using Pravega.Utility;
    using System.Xml.Schema;

    public partial class PravegaCSharpTest
    {
        /// <summary>
        ///  ByteWriter tests
        ///  Tests done on ByteReader require a running Pravega Server in order to
        ///  socket into and run.
        /// </summary>

        /// <summary
        ///  Helper method for testing. Creates a ByteWriter with default values
        ///  under the scope "testScope" and the stream "testStream"
        /// ></summary>
        internal static ByteWriter CreateTestByteWriter()
        {
            ClientFactory.Initialize();
            ControllerClient testController = ClientFactory.FactoryControllerClient;

            // Create a scope to base the stream on.
            Scope testScope = new Scope();
            testScope.NativeString = "testScope";
            testController.CreateScope(testScope).GetAwaiter().GetResult();

            // Create a stream config to control the stream
            StreamConfiguration streamConfiguration = new StreamConfiguration();
            streamConfiguration.ConfigScopedStream.Scope = testScope;
            streamConfiguration.ConfigScopedStream.Stream = new CustomCSharpString("testStream");

            // Create the stream
            testController.CreateStream(streamConfiguration).GetAwaiter().GetResult();

            // Create the ByteWriter
            return ClientFactory.CreateByteWriter(streamConfiguration.ConfigScopedStream).GetAwaiter().GetResult();
        }

        /// <summary>
        ///  ByteWriter Constructor Test. Create ByteWriter from inputted ScopedStream
        ///  
        ///  Prereq. 
        ///  -Scope is initialized and Stream is initialized on that scope
        ///  -Pravega Server is running.
        ///  -Client Factory is initialized.
        /// </summary>
        /// <param name="testScopedStream">
        ///  ScopedStream to test on.
        /// </param>
        [Test]
        [TestCase("testScope", "testStream")]
        [TestCase(" ", " ")]
        public void ByteWriterConstructorTest(string scopeToBaseOn, string streamName)
        {
            ClientFactory.Initialize();
            ControllerClient testController = ClientFactory.FactoryControllerClient;

            // Create a scope to base the stream on.
            Scope testScope = new Scope();
            testScope.NativeString = scopeToBaseOn;
            Assert.IsTrue(testController.CreateScope(testScope).GetAwaiter().GetResult());

            // Create a stream config to control the stream
            StreamConfiguration streamConfiguration = new StreamConfiguration();
            streamConfiguration.ConfigScopedStream.Scope = testScope;
            streamConfiguration.ConfigScopedStream.Stream = new CustomCSharpString(streamName);

            // Create the stream
            Assert.IsTrue(testController.CreateStream(streamConfiguration).GetAwaiter().GetResult());

            // Create the ByteWriter
            ClientFactory.CreateByteWriter(streamConfiguration.ConfigScopedStream).GetAwaiter().GetResult();
            Assert.Pass();
        }

        /// <summary>
        ///     Byte Writer Current Offset Test. Verify that offset is changed when writing froma  bytewriter
        /// </summary>
        [Test]
        public void ByteWriterCurrentOffsetTest()
        {
            // Testing variables
            List<byte> testList = new List<byte>();
            testList.Clear();
            testList.Add(0);
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);

            // Create a byte writer
            ByteWriter testWriter = CreateTestByteWriter();

            // Get current offset
            ulong currentOffset = testWriter.CurrentOffset;

            // Attempt to write to byte writer
            ulong result = testWriter.Write(testList).GetAwaiter().GetResult();

            // Pass if 0 or more variables were written, but not more than the size of the list. Fail otherwise
            if (result > (ulong)testList.Count - 1 || result < 0)
            {
                Assert.Fail();
            }

            // Verify that current offset changed by the amount written or more if the tail is longer than the amount written (in case streams are writing in parallel to it when it gets the current offset)
            Assert.IsTrue(currentOffset + result >= testWriter.CurrentOffset);
        }
 
        /// <summary>
        ///  ByteWriter Write Test. Write bytes inputted in test list to byte writer inputted
        ///  
        ///  Prereq. 
        ///  -ByteWriter is initialized
        ///  -Pravega Server is running.
        ///  -Client Factory is initialized.
        /// </summary>
        /// <param name="testWriter"></param>
        /// <param name="testList"></param>
        [Test]
        public void ByteWriterWriteTest()
        {
            // Testing variables
            List<byte> testList = new List<byte>();
            testList.Clear();
            testList.Add(0);
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);

            // Create a byte writer
            ByteWriter testWriter = CreateTestByteWriter();
                
            // Attempt to write to byte writer
            ulong result =  testWriter.Write(testList).GetAwaiter().GetResult();

            // Pass if 0 or more variables were written, but not more than the size of the list. Fail otherwise
            if (result <= (ulong)testList.Count - 1 && result > 0) {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        ///  Byte Writer Flush Test. Tests the ability to flush from a byte writer object
        /// </summary>
        [Test]
        public void ByteWriterFlushTest()
        {
            // Testing variables
            List<byte> testList = new List<byte>();
            testList.Clear();
            testList.Add(0);
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);

            // Create a byte writer
            ByteWriter testWriter = CreateTestByteWriter();

            // Attempt to write to byte writer
            ulong result = testWriter.Write(testList).GetAwaiter().GetResult();

            // Attempt to flush the byte writer data to permanent data. Pass when done
            testWriter.Flush().GetAwaiter().GetResult();
            Assert.Pass();
        }

        /// <summary>
        ///  Byte Writer Seal Test. Tests the ability to seal a stream
        /// </summary>
        [Test]
        public void ByteWriterSealTest()
        {
            // Local variables
            int failedSuccessfully = 0;

            // Client Factory for testing
            ClientFactory.Initialize();
            ControllerClient testController = ClientFactory.FactoryControllerClient;

            // Create a scope to base the stream on.
            Scope testScope = new Scope();
            testScope.NativeString = "uniqueByteTestScope";
            testController.CreateScope(testScope).GetAwaiter().GetResult();

            // Create a stream config to control the stream
            StreamConfiguration streamConfiguration = new StreamConfiguration();
            streamConfiguration.ConfigScopedStream.Scope = testScope;
            streamConfiguration.ConfigScopedStream.Stream = new CustomCSharpString("uniqueByteTestStream");

            // Create the stream
            testController.CreateStream(streamConfiguration).GetAwaiter().GetResult();

            // Create the ByteWriter
            ByteWriter testWriter =  ClientFactory.CreateByteWriter(streamConfiguration.ConfigScopedStream).GetAwaiter().GetResult();

            // Seal the testWriter
            testWriter.Seal().GetAwaiter().GetResult();

            // Attempt to flush the data, which should error and show that the stream is sealled
            try
            {
                testWriter.Flush().GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
                failedSuccessfully = 1;
            }

            if (failedSuccessfully == 1)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }

        }

        /// <summary>
        ///  Byte Writer TruncateBefore Test. 
        /// </summary>
        [Test]
        public void ByteWriterTruncateBeforeTest()
        {
            // Testing variables
            int failedSuccessfully = 0;
            List<byte> testList = new List<byte>();
            testList.Clear();
            testList.Add(0);
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);

            // Create a byte writer
            ByteWriter testWriter = CreateTestByteWriter();

            // Create a reader with an offset of 0. Trying to read a byte should fail.
            ByteReader testReader = CreateTestByteReader();

            // Make sure the offset is currently 0
            Assert.IsTrue(testReader.CurrentOffset == 0);

            // Attempt to write to byte writer
            ulong result = testWriter.Write(testList).GetAwaiter().GetResult();

            // Truncate data before "1"
            testWriter.TruncateDataBefore(1).GetAwaiter().GetResult();


            // Attempt to flush the data, which should error and show that the stream is sealled
            try
            {
                testReader.Read(1);
            }
            catch (Exception ex)
            {
                failedSuccessfully = 1;
            }

            if (failedSuccessfully == 1)
            {
                Assert.Pass();
            }
            else
            {
                Assert.Fail();
            }
        }

        /// <summary>
        ///  Byte Writer Seek To Tail Test. Verifies that seek to tail seeks to the end of the segment.
        /// </summary>
        [Test]
        public void ByteWriterSeekToTailTest()
        {
            // Testing variables
            List<byte> testList = new List<byte>();
            testList.Clear();
            testList.Add(0);
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);

            // Create two byte writers
            ByteWriter testWriter = CreateTestByteWriter();
            ByteWriter testWriter2 = CreateTestByteWriter();

            // Write data through one bytewriter and seektotail on the other. Check to see if the offset isn't 0
            ulong result = testWriter.Write(testList).GetAwaiter().GetResult();
            testWriter2.SeekToTail();
            Assert.IsTrue(testWriter2.CurrentOffset != 0);
        }

        /// <summary>
        ///   Byte Writer Reset Test.
        /// </summary>
        [Test]
        public void ByteWriterResetTest()
        {
            // Create two byte writers
            ByteWriter testWriter = CreateTestByteWriter();

            // Resets the internal reactor. Makes sure nothing freezes up.
            testWriter.Reset();
            Assert.Pass();
        }

        /// <summary>
        ///  ByteReader tests
        ///  
        ///  Tests done on ByteReader require a running Pravega Server in order to
        ///  socket into and run.
        /// </summary>


        /// <summary
        ///  Helper method for testing. Creates a ByteReader with default values
        ///  under the scope "testScope" and the stream "testStream"
        /// ></summary>
        internal static ByteReader CreateTestByteReader()
        {
            ClientFactory.Initialize();
            ControllerClient testController = ClientFactory.FactoryControllerClient;

            // Create a scope to base the stream on.
            Scope testScope = new Scope();
            testScope.NativeString = "testScope";
            testController.CreateScope(testScope).GetAwaiter().GetResult();

            // Create a stream config to control the stream
            StreamConfiguration streamConfiguration = new StreamConfiguration();
            streamConfiguration.ConfigScopedStream.Scope = testScope;
            streamConfiguration.ConfigScopedStream.Stream = new CustomCSharpString("testStream");

            // Create the stream
            testController.CreateStream(streamConfiguration).GetAwaiter().GetResult();

            // Create the ByteReader
            return ClientFactory.CreateByteReader(streamConfiguration.ConfigScopedStream).GetAwaiter().GetResult();
        }

        /// <summary>
        ///  Test Byte Reader Constructor. 
        /// </summary>
        [Test]
        [TestCase("testScope", "testStream")]
        [TestCase(" ", " ")]
        public void ByteReaderConstructorTest(string scopeToBaseOn, string streamName)
        {
            ClientFactory.Initialize();
            ControllerClient testController = ClientFactory.FactoryControllerClient;

            // Create a scope to base the stream on.
            Scope testScope = new Scope();
            testScope.NativeString = scopeToBaseOn;
            testController.CreateScope(testScope).GetAwaiter().GetResult();

            // Create a stream config to control the stream
            StreamConfiguration streamConfiguration = new StreamConfiguration();
            streamConfiguration.ConfigScopedStream.Scope = testScope;
            streamConfiguration.ConfigScopedStream.Stream = new CustomCSharpString(streamName);

            // Create the stream
            testController.CreateStream(streamConfiguration).GetAwaiter().GetResult();

            // Create the ByteWriter
            ClientFactory.CreateByteReader(streamConfiguration.ConfigScopedStream).GetAwaiter().GetResult();

            Assert.Pass();

        }

        /// <summary>
        ///  Byte Reader Read Test. Tests byte reader's ability to read a payload
        /// </summary>
        [Test]
        public void ByteReaderReadTest()
        {
            // Testing variables
            List<byte> testList = new List<byte>();
            testList.Clear();
            testList.Add(0);
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);

            // Create a byte writer
            ByteWriter testWriter = CreateTestByteWriter();

            // Create a byte reader
            ByteReader testReader = CreateTestByteReader();

            // Attempt to write to byte writer
            ulong result = testWriter.Write(testList).GetAwaiter().GetResult();

            // Read the amount written to the stream
            byte[] resultingBytes = testReader.Read(result).GetAwaiter().GetResult();

            // Assert that the contents of the read array is the same as the bytes written. If all are the same, pass the test
            for (int i = 0; i < resultingBytes.Length; i++)
            {
                if (resultingBytes[i] != testList[i])
                {
                    Assert.Fail();
                }
            }

            Assert.Pass();
        }

        /// <summary>
        ///  Byte Reader Current Offset Test. Tests byte reader's current offset, making sure it's 0 to start and non-zero after reading.
        /// </summary>
        [Test]
        public void ByteReaderCurrentOffsetTest()
        {
            // Testing variables
            List<byte> testList = new List<byte>();
            testList.Clear();
            testList.Add(0);
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);

            // Create a byte writer
            ByteWriter testWriter = CreateTestByteWriter();

            // Create a byte reader
            ByteReader testReader = CreateTestByteReader();

            // Make sure current offset is 0 to start
            Assert.IsTrue(testReader.CurrentOffset == 0);

            // Attempt to write to byte writer
            ulong result = testWriter.Write(testList).GetAwaiter().GetResult();

            // Read the amount written to the stream
            byte[] resultingBytes = testReader.Read(result).GetAwaiter().GetResult();

            // Make sure current offset was changed after reading
            Assert.IsTrue(testReader.CurrentOffset >= (ulong)resultingBytes.Length);

        }

        /// <summary>
        ///  Byte Reader Available Test. Tests byte reader's available field after having content written to its segment and before.
        /// </summary>
        [Test]
        public void ByteReaderAvailableTest()
        {
            // Testing variables
            List<byte> testList = new List<byte>();
            testList.Clear();
            testList.Add(0);
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);

            // Client Factory for testing
            ClientFactory.Initialize();
            ControllerClient testController = ClientFactory.FactoryControllerClient;

            // Create a scope to base the stream on.
            Scope testScope = new Scope();
            testScope.NativeString = "uniqueByteTestScope2";
            testController.CreateScope(testScope).GetAwaiter().GetResult();

            // Create a stream config to control the stream
            StreamConfiguration streamConfiguration = new StreamConfiguration();
            streamConfiguration.ConfigScopedStream.Scope = testScope;
            streamConfiguration.ConfigScopedStream.Stream = new CustomCSharpString("uniqueByteTestStream2");

            // Create the stream
            testController.CreateStream(streamConfiguration).GetAwaiter().GetResult();

            // Create the ByteWriter
            ByteWriter testWriter = ClientFactory.CreateByteWriter(streamConfiguration.ConfigScopedStream).GetAwaiter().GetResult();
            ByteReader testReader = ClientFactory.CreateByteReader(streamConfiguration.ConfigScopedStream).GetAwaiter().GetResult();

            // Check reader.available, which should be 0 if nothing has been written.
            Assert.IsTrue(testReader.Available == 0);

            // Write data to stream
            ulong result = testWriter.Write(testList).GetAwaiter().GetResult();

            // Check reader.available, which should be equal to or less than the amount written.
            Assert.IsTrue(testReader.Available <= result);
        }

        /// <summary>
        ///  Byte Reader Seek Test. Tests seeking rom different points on a stream with a constant stream size of 5.
        /// </summary>
        /// <param name="mode"></param>
        /// <param name="amount"></param>
        /// <param name="expectedOffset"></param>
        [Test]
        [TestCase((ulong)0, (ulong)1)]
        [TestCase((ulong)0, (ulong)2)]
        [TestCase((ulong)0, (ulong)3)]
        [TestCase((ulong)0, (ulong)4)]
        [TestCase((ulong)0, (ulong)5)]
        [TestCase((ulong)1, (ulong)1)]
        [TestCase((ulong)1, (ulong)2)]
        [TestCase((ulong)1, (ulong)3)]
        [TestCase((ulong)1, (ulong)4)]
        [TestCase((ulong)1, (ulong)5)]
        [TestCase((ulong)2, (ulong)1)]
        [TestCase((ulong)2, (ulong)2)]
        [TestCase((ulong)2, (ulong)3)]
        [TestCase((ulong)2, (ulong)4)]
        [TestCase((ulong)2, (ulong)5)]
        public void ByteReaderSeekTest(ulong mode, ulong amount)
        {
            // Testing variables
            List<byte> testList = new List<byte>();
            testList.Clear();
            testList.Add(0);
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);

            // Client Factory for testing
            ClientFactory.Initialize();
            ControllerClient testController = ClientFactory.FactoryControllerClient;

            // Create a scope to base the stream on.
            Scope testScope = new Scope();
            testScope.NativeString = "uniqueByteTestScope3";
            testController.CreateScope(testScope).GetAwaiter().GetResult();

            // Create a stream config to control the stream
            StreamConfiguration streamConfiguration = new StreamConfiguration();
            streamConfiguration.ConfigScopedStream.Scope = testScope;
            streamConfiguration.ConfigScopedStream.Stream = new CustomCSharpString("uniqueByteTestStream3");

            // Create the stream
            testController.CreateStream(streamConfiguration).GetAwaiter().GetResult();

            // Create the ByteWriter
            ByteWriter testWriter = ClientFactory.CreateByteWriter(streamConfiguration.ConfigScopedStream).GetAwaiter().GetResult();
            ByteReader testReader = ClientFactory.CreateByteReader(streamConfiguration.ConfigScopedStream).GetAwaiter().GetResult();

            // Write data to stream. Seal afterwards to keep the size constant for testing.
            ulong result = 0;
            try
            {
                ulong totalBytesWritten = 0;
                while (totalBytesWritten < 5)
                {
                    result = testWriter.Write(testList).GetAwaiter().GetResult();
                    totalBytesWritten += result;
                }
                testWriter.Seal();
            }
            catch
            {
            }

            // Test seeking based on the mode and amount
            switch (mode)
            {
                // Seek from beginning
                case 0:
                    testReader.Seek(mode, amount);
                    Assert.IsTrue(testReader.CurrentOffset <= amount);
                    break;

                // Seek from current
                case 1:
                    testReader.Seek(mode, 1);
                    testReader.Seek(mode, amount);
                    Assert.IsTrue(testReader.CurrentOffset <= amount + 1);
                    break;

                // Seek from end
                case 2:
                    testReader.Seek(mode, amount);
                    Assert.IsTrue(testReader.CurrentOffset >= 0);
                    break;

                default:
                    Assert.Fail();
                    break;
            }
        }

        /// <summary>
        ///  Byte Reader Current Head Test. Tests reading current head before and after the stream is truncated
        /// </summary>
        [Test]
        public void ByteReaderCurrentHeadTest()
        {
            // Testing variables
            List<byte> testList = new List<byte>();
            testList.Clear();
            testList.Add(0);
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);

            // Create a byte writer
            ByteWriter testWriter = CreateTestByteWriter();

            // Create a byte reader
            ByteReader testReader = CreateTestByteReader();

            // Make sure current head is 0 to start
            Assert.IsTrue(testReader.CurrentHead().GetAwaiter().GetResult() == 0);

            // Attempt to write to byte writer
            ulong result = testWriter.Write(testList).GetAwaiter().GetResult();

            // Read the amount written to the stream
            byte[] resultingBytes = testReader.Read(result).GetAwaiter().GetResult();

            // Make sure current head was changed after reading
            Assert.IsTrue(testReader.CurrentHead().GetAwaiter().GetResult() >= (ulong)resultingBytes.Length);

            // Attempt to write to byte writer again
            result = testWriter.Write(testList).GetAwaiter().GetResult();

            // Truncate
            testWriter.TruncateDataBefore((long)testWriter.CurrentOffset).GetAwaiter().GetResult();

            // Verify current head is no longer valid
            try
            {
                // Should fail here.
                testReader.CurrentHead().GetAwaiter().GetResult();

                // If it doesn't fail, don't pass
                Assert.Fail();
            }
            catch
            {
                Assert.Pass();
            }
        }

        /// <summary>
        ///  Byte Reader Current Head Test. Tests reading current head before and after the stream is truncated
        /// </summary>
        [Test]
        public void ByteReaderCurrentTailTest()
        {
            // Testing variables
            List<byte> testList = new List<byte>();
            testList.Clear();
            testList.Add(0);
            testList.Add(1);
            testList.Add(2);
            testList.Add(3);
            testList.Add(4);

            // Create a byte writer
            ByteWriter testWriter = CreateTestByteWriter();

            // Create a byte reader
            ByteReader testReader = CreateTestByteReader();

            // Attempt to write to byte writer
            ulong result = testWriter.Write(testList).GetAwaiter().GetResult();

            // Read the amount written to the stream
            byte[] resultingBytes = testReader.Read(result).GetAwaiter().GetResult();
            ulong currentTail = testReader.CurrentTail().GetAwaiter().GetResult();

            // Make sure current offset was changed after reading
            Assert.IsTrue(testReader.CurrentHead().GetAwaiter().GetResult() >= (ulong)resultingBytes.Length);

            // Verify current tail changed
            Assert.IsTrue(currentTail != testReader.CurrentTail().GetAwaiter().GetResult());
        }
    }
}