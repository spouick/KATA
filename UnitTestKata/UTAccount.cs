using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using NSubstitute;

using Kata;
using Kata.Interfaces;

namespace UnitTestKata
{
    [TestClass]
    public class UTAccount
    {
        [TestMethod]
        public void EmptyCheck()
        {
            var mockClient  = Substitute.For<IClient>();
            var mockHistory = Substitute.For<IAccountHistory>();
            mockHistory.Count.Returns(0);
            //mockHistory.Enumerate(Arg.Any<Action<IOperation>>())
            //mockHistory.AddOperation(Arg.Any<IOperation>())

            var toTest = new Account(mockClient, mockHistory);

            Assert.AreEqual(mockClient , toTest.Client );
            Assert.AreEqual(mockHistory, toTest.History);
            Assert.AreEqual(0, toTest.Balance);
        }

        [TestMethod]
        public void CheckAlreadyFeed()
        {
            var expectedOperations = new List<IOperation>();

            IOperation mockOperation;

            mockOperation = Substitute.For<IOperation>();
            mockOperation.Amount.Returns(123.456);
            mockOperation.Operation.Returns(OperationType.SAVE);
            mockOperation.Date.Returns(new DateTime(2017, 12, 5, 1, 2, 3));
            expectedOperations.Add(mockOperation);

            mockOperation = Substitute.For<IOperation>();
            mockOperation.Amount.Returns(32.65);
            mockOperation.Operation.Returns(OperationType.WITHDRAW);
            mockOperation.Date.Returns(new DateTime(2017, 12, 5, 1, 3, 4));
            expectedOperations.Add(mockOperation);

            mockOperation = Substitute.For<IOperation>();
            mockOperation.Amount.Returns(54.87);
            mockOperation.Operation.Returns(OperationType.WITHDRAW);
            mockOperation.Date.Returns(new DateTime(2017, 12, 5, 1, 4, 5));
            expectedOperations.Add(mockOperation);

            var mockClient  = Substitute.For<IClient>();

            var mockHistory = Substitute.For<IAccountHistory>();
            mockHistory.Count.Returns(expectedOperations.Count);
            mockHistory.When(instance => instance.Enumerate(Arg.Any<Action<IOperation>>()))
                       .Do(args => expectedOperations.ForEach(item =>
                        {
                            var callback = (Action<IOperation>)args[0];
                            callback(item);
                        }));

            mockHistory.DidNotReceive()
                       .AddOperation(Arg.Any<IOperation>());

            var toTest = new Account(mockClient, mockHistory);

            Assert.AreEqual(mockClient , toTest.Client );
            Assert.AreEqual(mockHistory, toTest.History);
            Assert.AreEqual(123.456 - 32.65 - 54.87, toTest.Balance);
        }
    }
}
