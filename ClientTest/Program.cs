using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Kata;

namespace ClientTest
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Clear();

            Console.WriteLine("Familly name:");
            var name = Console.ReadLine();

            Console.WriteLine("First name:");
            var firstName = Console.ReadLine();

            // must be initialized with external sources.
            var accountHistory = new AccountHistory();

            var account = new Account(new Client(firstName, name), accountHistory);

            displayMenu();

            while (true)
            {
                Console.WriteLine();
                Console.Write("Your Choice >");

                var key = Console.ReadKey();
                switch (key.KeyChar)
                {
                    case '1':
                        save(account);
                        break;

                    case '2':
                        withdraw(account);
                        break;

                    case '3':
                        history(account);
                        break;

                    case 'q':
                    case 'Q':
                        Console.WriteLine("Bye...");
                        return;

                    default:
                        displayMenu();
                        break;
                }
            }
        }

        private static void displayMenu()
        {
            Console.Clear();
            Console.WriteLine("1. Save");
            Console.WriteLine("2. Withdraw");
            Console.WriteLine("3. History");
            Console.WriteLine("Q. Quit");
            Console.WriteLine("(any other key to display this Menu)");

        }

        private static void save(Account account)
        {
            Console.WriteLine();
            Console.Write("Amount >");

            var userValue = Console.ReadLine();

            if (!double.TryParse(userValue, out double amount))
            {
                Console.WriteLine("bad enter, retry next time with numerical or decimal value");
                return;
            }

            var newBalance = account.Save(amount);
            Console.WriteLine($"New balance: {newBalance}");
        }

        private static void withdraw(Account account)
        {
            Console.WriteLine();
            Console.Write("Amount >");

            var userValue = Console.ReadLine();

            if (!double.TryParse(userValue, out double amount))
            {
                Console.WriteLine("bad enter, retry next time with numerical or decimal value");
                return;
            }

            if (!account.TryWithdraw(amount, out double newBalance))
            {
                Console.WriteLine("Withdraw exceed your limit, retry next time with lower value or contact your bank.");
                return;
            }

            Console.WriteLine($"New balance: {newBalance}");
        }

        private static void history(Account account)
        {
            Console.Clear();
            Console.WriteLine($"History for {account.Client.Name} {account.Client.FirstName}.");
            account.History.Enumerate(item =>
                {
                    Console.WriteLine($"{item.Date} | {item.Operation.ToString().PadLeft(10)} | {item.Amount}");
                });

            Console.WriteLine($"Balance: {account.Balance}.");
        }
    }
}
