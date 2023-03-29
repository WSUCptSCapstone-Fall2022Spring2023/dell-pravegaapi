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

    /// <summary>
    ///  Contains all unit and integration tests related to the Byte Module
    /// </summary>
    public partial class PravegaCSharpTest
    {

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