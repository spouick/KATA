using System;
using System.Collections.Generic;
using System.Text;

using Kata.Interfaces;

namespace Kata
{
    public class WithdrawOperation : BaseOperation
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="amount">must be unsigned</param>
        /// <param name="date">if null, feed with DateTime.Now</param>
        public WithdrawOperation(double amount, DateTime? date = null)
            : base(OperationType.WITHDRAW, amount, date)
        {
        }
    }
}
