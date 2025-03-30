using Bank1.Models;
using Spectre.Console;


namespace Bank1
{
    public class Bank
    {
        public string Name { get; set; }
        public List<Customer> Customers { get; set; } = new();

        public Bank(string name)
        {
            Name = name;
        }

        public void AddCustomer(Customer customer)
        {
            if (customer == null)
                throw new ArgumentNullException(nameof(customer));

            Customers.Add(customer);
        }

        public void ShowAllCustomers()
        {
            if (!Customers.Any())
            {
                AnsiConsole.MarkupLine("[grey]No customers found.[/]");
                return;
            }

            var table = new Table()
                .Border(TableBorder.Rounded)
                .AddColumn("[yellow]ID[/]")
                .AddColumn("[yellow]Name[/]")
                .AddColumn("[yellow]Account ID[/]")
                .AddColumn("[yellow]Birth Day[/]")
                .AddColumn("[yellow]Balance[/]");

            foreach (var customer in Customers)
            {
                table.AddRow(
                    customer.Id.ToString(),
                    customer.Name,
                    customer.AccountId.ToString(),
                    customer.BirthDay.ToShortDateString(),
                    customer.Balance.ToString("F2")
                );
            }

            AnsiConsole.Write(table);
        }

        public void ShowCustomerInfo(int customerId)
        {
            var customer = Customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
            {
                AnsiConsole.MarkupLine("[Red] Customer not found [/]");
                return;
            }

            //Console.WriteLine(customer.CustomerInfo());
            AnsiConsole.Write(customer.CustomerInfo());
        }

        public Customer? FindCustomer(int customerId)
        {
            return Customers.FirstOrDefault(c => c.Id == customerId);
        }

        public void ShowCustomerTransaction(int customerId)
        {
            var customer = Customers.FirstOrDefault(c => c.Id == customerId);
            if (customer == null)
            {
                //Console.WriteLine("Customer not found.");
                AnsiConsole.MarkupLine("[Red] Customer not found [/]");
                return;

            }
            customer.ShowTransaction();
        }
    }
}
