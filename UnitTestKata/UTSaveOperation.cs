using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Kata;
using Kata.Interfaces;

namespace UnitTestKata
{
    [TestClass]
    public class UTSaveOperation
    {
        [TestMethod]
        public void HappyPath_NoDate()
        {
            var expectedValue = 123.456;

            var dateTime1 = DateTime.Now;
            var toTest = new SaveOperation(expectedValue);
            var dateTime2 = DateTime.Now;

            Assert.AreEqual(OperationType.SAVE, toTest.Operation);
            Assert.AreEqual(expectedValue     , toTest.Amount   );

            Assert.IsTrue(dateTime1.Ticks   <= toTest.Date.Ticks);
            Assert.IsTrue(toTest.Date.Ticks <= dateTime2.Ticks  );
        }

        [TestMethod]
        public void HappyPath_WithDate()
        {
            var expectedValue = 123.456;
            var expectedDate  = new DateTime(2017, 12, 5, 1, 3, 4);

            var toTest = new SaveOperation(expectedValue, expectedDate);

            Assert.AreEqual(OperationType.SAVE, toTest.Operation );
            Assert.AreEqual(expectedValue     , toTest.Amount    );
            Assert.AreEqual(expectedDate.Ticks, toTest.Date.Ticks);
        }
    }
}
