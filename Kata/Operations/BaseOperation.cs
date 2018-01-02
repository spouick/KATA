using System;
using System.Collections.Generic;
using System.Text;

using Kata.Interfaces;

namespace Kata
{
    // TODO: Use heritage to decline in OperationType specifications
    public class BaseOperation : IOperation
    {
        public OperationType Operation { get; private set; }
        public DateTime      Date      { get; private set; }
        public double        Amount    { get; private set; }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="operation"></param>
        /// <param name="amount">must be unsigned</param>
        /// <param name="date">if null, feed with DateTime.Now</param>
        protected BaseOperation(OperationType operation, double amount, DateTime? date = null)
        {
            Date = date.HasValue
                ? date.Value
                : DateTime.Now;

            Operation = operation;
            Amount     = amount;
        }
    }
}
