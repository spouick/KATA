using System;

using Kata.Interfaces;

namespace Kata
{
    public class Account : IAccount
    {
        public IClient         Client  { get; private set; }
        public IAccountHistory History { get; private set; }
        public double          Balance { get; private set; }

        public Account(IClient client, IAccountHistory history)
        {
            Client  = client;
            History = history;
            Balance = computeBalance();
        }

        private double computeBalance()
        {
            double result = 0.0;
            History.Enumerate(item =>
                {
                    switch (item.Operation)
                    {
                        case OperationType.SAVE:
                            result += item.Amount;
                            break;

                        case OperationType.WITHDRAW:
                            result -= item.Amount;
                            break;

                        default:
                            throw new ApplicationException($"DEV Error: {item.Operation} not already supported.");
                    }
                });

            return result;
        }

        public double Save(double amout)
        {
            History.AddOperation(new SaveOperation(amout));
            Balance += amout;
            return Balance;
        }

        public bool TryWithdraw(double amout, out double balance)
        {
            if (Balance - amout < 0)
            {
                balance = Balance;
                return false;
            }

            History.AddOperation(new WithdrawOperation(amout));
            balance = (Balance -= amout);
            return true;
        }
    }
}
