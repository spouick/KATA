using System;
using System.Collections.Generic;
using System.Text;

using Kata.Interfaces;

namespace Kata
{
    public class Client : IClient
    {
        public string FirstName { get; }
        public string Name      { get; }

        public Client(string firstName, string name)
        {
            FirstName = firstName;
            Name      = name;
        }
    }
}
