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

    public partial class PravegaCSharpTest
    {
        /// <summary>
        ///  ByteWriter tests
        ///  Tests done on ByteReader require a running Pravega Server in order to
        ///  socket into and run.
        /// </summary>
        /// 


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
        /// <returns>
        ///  ByteWriter generated. If a byteWriter is generated, it means that
        ///  the test passed.
        /// </returns>
        public static ByteWriter ByteWriterConstructorTest(ScopedStream testScopedStream)
        {
            return ClientFactory.CreateByteWriter(testScopedStream).GetAwaiter().GetResult();
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
        /// <returns>
        ///  True if it passes and False if it fails the test.
        ///  Passes if bytes written is >= 0 and <= testList.Count - 1
        ///  Failes otherwise
        /// </returns>
        public static bool ByteWriterWriteTest(ByteWriter testWriter, List<byte> testList)
        {
            ulong result = testWriter.Write(testList).GetAwaiter().GetResult();
            if (result <= (ulong)testList.Count - 1 && result > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        ///  ByteReader tests
        ///  
        ///  Tests done on ByteReader require a running Pravega Server in order to
        ///  socket into and run.
        /// </summary>
        [Test]
        public void ByteReaderConstructorTest()
        {
            // Initialize ClientFactory before constructing.
            ClientFactory.Initialize();

            // Create a Scoped Stream for Bytereader to be based on.


        }
    }
}