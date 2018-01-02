using System;
using System.Collections.Generic;
using System.Text;

namespace Kata.Interfaces
{
    public interface IAccountHistory
    {
        int Count { get; }
        void Enumerate(Action<IOperation> callback);
        void AddOperation(IOperation operation);
    }
}
