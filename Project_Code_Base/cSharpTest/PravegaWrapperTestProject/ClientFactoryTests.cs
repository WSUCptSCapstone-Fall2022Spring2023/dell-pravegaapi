///
/// File: PravegaTest.cs
/// File Creator: John Sbur
/// Purpose: Contains the test class for the PravegaCSharp wrapper library. Uses NUnit for testing.
///     Methods tested are in different files under their module's name.
///
namespace PravegaWrapperTestProject
{
    using System;
    using System.Runtime.CompilerServices;
    using Pravega;
    using NUnit.Framework;

    public partial class PravegaCSharpTest
    {
        [Test]
        public void ClientFactoryTest1()
        {
            Console.WriteLine("test");
            Assert.Pass();
        }
    }
}