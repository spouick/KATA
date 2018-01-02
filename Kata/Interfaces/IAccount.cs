using System;

namespace Kata.Interfaces
{
    public interface IAccount
    {
        IClient         Client  { get; }
        IAccountHistory History { get; }
        double          Balance { get; }
        //TODO: Add limite off account: overdraft, limite per week/month by withrdaw/payment/internet, ...

        /// <summary>
        /// 
        /// </summary>
        /// <param name="amout">must be unsigned</param>
        /// <returns>new balance</returns>
        double Save(double amout);

        //TODO: add reasone type if return FALSE: overdraft limit, max withrdraw per week/month, ...
        /// <summary>
        /// 
        /// </summary>
        /// <param name="amout">must be unsigned</param>
        /// <param name="balance">new balance is succeed, else curent balance</param>
        /// <returns>true if operation is authorized, else false</returns>
        bool TryWithdraw(double amout, out double balance);
    }
}
