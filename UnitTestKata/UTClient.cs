using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Kata;

namespace UnitTestKata
{
    [TestClass]
    public class UTClient
    {
        [TestMethod]
        public void SetGet()
        {
            var expectedFirstName = "TestFirstName";
            var expectedName      = "TestName";

            var toTest = new Client(expectedFirstName, expectedName);

            Assert.AreEqual(expectedFirstName, toTest.FirstName);
            Assert.AreEqual(expectedName     , toTest.Name     );
        }
    }
}
