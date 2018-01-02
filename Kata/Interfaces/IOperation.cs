using System;
using System.Collections.Generic;
using System.Text;

namespace Kata.Interfaces
{
    public interface IOperation
    {
        OperationType Operation { get; }
        DateTime      Date      { get; }
        double        Amount    { get; }
    }
}
