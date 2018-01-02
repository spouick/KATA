using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using NSubstitute;

using Kata;
using Kata.Interfaces;

namespace UnitTestKata
{
    [TestClass]
    public class UTAccountHistory
    {
        [TestMethod]
        public void EmptyCheckCount()
        {
            var toTest = new AccountHistory();

            Assert.AreEqual(0, toTest.Count);
        }

        [TestMethod]
        public void EmptyCheckEnumeration()
        {
            var toTest = new AccountHistory();

            var callbackCalled = false;
            toTest.Enumerate(item => callbackCalled = true);

            Assert.IsFalse(callbackCalled);
        }

        [TestMethod]
        public void AddOperation_Single()
        {
            var toTest = new AccountHistory();

            var mockOperation = Substitute.For<IOperation>();
            mockOperation.Amount.Returns(123.456);
            mockOperation.Operation.Returns(OperationType.SAVE);
            mockOperation.Date.Returns(new DateTime(2017, 12, 5, 1, 2, 3));

            toTest.AddOperation(mockOperation);

            Assert.AreEqual(1, toTest.Count);

            var items = new List<IOperation>();
            toTest.Enumerate(item => items.Add(item));

            Assert.AreEqual(1, items.Count);
            Assert.AreEqual(mockOperation.Amount    , items[0].Amount    );
            Assert.AreEqual(mockOperation.Operation , items[0].Operation );
            Assert.AreEqual(mockOperation.Date.Ticks, items[0].Date.Ticks);
        }


        [TestMethod]
        public void AddOperation_Multiple_Unordered()
        {
            var toTest = new AccountHistory();

            var expectedOperations = new List<IOperation>();

            IOperation mockOperation;

            mockOperation = Substitute.For<IOperation>();
            mockOperation.Amount.Returns(123.456);
            mockOperation.Operation.Returns(OperationType.SAVE);
            mockOperation.Date.Returns(new DateTime(2017, 12, 5, 1, 2, 3));
            expectedOperations.Add(mockOperation);

            mockOperation = Substitute.For<IOperation>();
            mockOperation.Amount.Returns(54.87);
            mockOperation.Operation.Returns(OperationType.WITHDRAW);
            mockOperation.Date.Returns(new DateTime(2017, 12, 5, 1, 4, 5));
            expectedOperations.Add(mockOperation);

            mockOperation = Substitute.For<IOperation>();
            mockOperation.Amount.Returns(32.65);
            mockOperation.Operation.Returns(OperationType.WITHDRAW);
            mockOperation.Date.Returns(new DateTime(2017, 12, 5, 1, 3, 4));
            expectedOperations.Add(mockOperation);

            expectedOperations.ForEach(item => toTest.AddOperation(item));

            Assert.AreEqual(3, toTest.Count);

            var items = new List<IOperation>();
            toTest.Enumerate(item => items.Add(item));

            Assert.AreEqual(expectedOperations.Count, items.Count);
            Assert.AreEqual(expectedOperations[0].Amount    , items[0].Amount    );
            Assert.AreEqual(expectedOperations[0].Operation , items[0].Operation );
            Assert.AreEqual(expectedOperations[0].Date.Ticks, items[0].Date.Ticks);

            Assert.AreEqual(expectedOperations[2].Amount    , items[1].Amount    );
            Assert.AreEqual(expectedOperations[2].Operation , items[1].Operation );
            Assert.AreEqual(expectedOperations[2].Date.Ticks, items[1].Date.Ticks);

            Assert.AreEqual(expectedOperations[1].Amount    , items[2].Amount    );
            Assert.AreEqual(expectedOperations[1].Operation , items[2].Operation );
            Assert.AreEqual(expectedOperations[1].Date.Ticks, items[2].Date.Ticks);
        }
    }
}
