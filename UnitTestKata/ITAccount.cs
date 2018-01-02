using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using NSubstitute;

using Kata;
using Kata.Interfaces;

namespace UnitTestKata
{
    [TestClass]
    public class ITAccount
    {
        [TestMethod]
        public void CheckSequence_HappyPath()
        {
            var internalOperations = new List<IOperation>();

            var mockClient  = Substitute.For<IClient>();

            var mockHistory = Substitute.For<IAccountHistory>();
            mockHistory.Count.Returns(internalOperations.Count);
            mockHistory.When(instance => instance.Enumerate(Arg.Any<Action<IOperation>>()))
                       .Do(args => internalOperations.ForEach(item =>
                        {
                            var callback = (Action<IOperation>)args[0];
                            callback(item);
                        }));

            mockHistory.When(instance => instance.AddOperation(Arg.Any<IOperation>()))
                       .Do(args => internalOperations.Add((IOperation)args[0]));

            var toTest = new Account(mockClient, mockHistory);

            double newBalance;
            double amount;

            amount = 123.456;
            newBalance = toTest.Save(amount);
            Assert.AreEqual(amount    , newBalance    );
            Assert.AreEqual(newBalance, toTest.Balance);

            var withDraw = amount / 2;
            var isSuccess = toTest.TryWithdraw(withDraw, out newBalance);
            Assert.IsTrue(isSuccess);
            Assert.AreEqual(amount - withDraw, newBalance    );
            Assert.AreEqual(newBalance       , toTest.Balance);
        }

        [TestMethod]
        public void CheckSequence_WithDrawEmptyFailed()
        {
            var internalOperations = new List<IOperation>();

            var mockClient  = Substitute.For<IClient>();

            var mockHistory = Substitute.For<IAccountHistory>();
            mockHistory.Count.Returns(internalOperations.Count);
            mockHistory.When(instance => instance.Enumerate(Arg.Any<Action<IOperation>>()))
                       .Do(args => internalOperations.ForEach(item =>
                        {
                            var callback = (Action<IOperation>)args[0];
                            callback(item);
                        }));

            mockHistory.When(instance => instance.AddOperation(Arg.Any<IOperation>()))
                       .Do(args => internalOperations.Add((IOperation)args[0]));

            var toTest = new Account(mockClient, mockHistory);

            double newBalance;
            var isSuccess = toTest.TryWithdraw(1.0, out newBalance);
            Assert.IsFalse(isSuccess);
            Assert.AreEqual(newBalance, toTest.Balance);
        }

        [TestMethod]
        public void CheckSequence_WithdrawNotEmptyFailed()
        {
            var internalOperations = new List<IOperation>();

            var mockClient  = Substitute.For<IClient>();

            var mockHistory = Substitute.For<IAccountHistory>();
            mockHistory.Count.Returns(internalOperations.Count);
            mockHistory.When(instance => instance.Enumerate(Arg.Any<Action<IOperation>>()))
                       .Do(args => internalOperations.ForEach(item =>
                        {
                            var callback = (Action<IOperation>)args[0];
                            callback(item);
                        }));

            mockHistory.When(instance => instance.AddOperation(Arg.Any<IOperation>()))
                       .Do(args => internalOperations.Add((IOperation)args[0]));

            var toTest = new Account(mockClient, mockHistory);

            double newBalance;
            double amount;

            amount = 123.456;
            newBalance = toTest.Save(amount);
            Assert.AreEqual(amount    , newBalance    );
            Assert.AreEqual(newBalance, toTest.Balance);

            var withDraw = amount * 2;
            var isSuccess = toTest.TryWithdraw(withDraw, out newBalance);
            Assert.IsFalse(isSuccess);
            Assert.AreEqual(amount    , newBalance    );
            Assert.AreEqual(newBalance, toTest.Balance);
        }
    }
}
