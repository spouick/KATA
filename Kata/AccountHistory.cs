using System;
using System.Collections.Generic;
using System.Text;

using Kata.Interfaces;

namespace Kata
{
    public class AccountHistory : IAccountHistory
    {
        /// <summary>
        /// must be lock before access to _items
        /// TODO: maybe use readlocker or writelocker
        /// </summary>
        private object           _synch = new object();
        private List<IOperation> _items = new List<IOperation>();

        public int Count
        {
            get
            {
                lock(_synch)
                {
                    return _items.Count;
                }
            }
        }

        public void Enumerate(Action<IOperation> callback)
        {
            lock (_synch)
            {
                _items.ForEach(item => callback(item));
            }
        }

        public void AddOperation(IOperation operation)
        {
            lock (_synch)
            {
                // TODO: use Dicotomic search/insert to maximize performance in ordered operation
                _items.Add(operation);
                _items.Sort((lt, rt) => lt.Date.CompareTo(rt.Date));
            }
        }
    }
}
