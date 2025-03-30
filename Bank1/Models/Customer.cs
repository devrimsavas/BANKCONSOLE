
using System.Transactions;
using  Spectre.Console;

namespace Bank1.Models
{
    public class Customer
    {
        public int Id { get; set; }
        public int AccountId { get; set; }
        public string Name { get; set; }
        public DateTime BirthDay { get; set; }
        public double Balance { get; private set; } = 0;

        public List<Transaction> Transactions { get; set; } = new List<Transaction>();

        //constructor 
        public Customer()
        {

        }
        public Panel  CustomerInfo()
        {
            var content = new Markup($@"
            [green]Customer ID:[/] [bold]{Id}[/]
            [green]Name:[/] [bold]{Name}[/]
            [green]Account ID:[/] [bold]{AccountId}[/]
            [green]Birthday:[/] [bold]{BirthDay:yyyy-MM-dd}[/]
            [green]Current Balance:[/] [bold yellow]{Balance:F2} ₺[/]");

            return new Panel(content)
                .Header("[bold blue]👤 Customer Info[/]")
                .Border(BoxBorder.Rounded)
                .Padding(1, 1)
                .Expand();

        }



        public void MoneyDeposit(double amount)
        {
            if (double.IsNaN(amount) || double.IsInfinity(amount) || amount <= 0)
            {
                throw new ArgumentException("Invalid deposit amount");
            }
            Balance += amount;
            //transactions 
            Transactions.Add(new Transaction
            {
                Date = DateTime.Now,
                Amount = amount,
                Type = "Deposit",
                BalanceAfter = Balance
            });
        }
        public void MoneyWithDrawal(double amount)
        {
            if (double.IsNaN(amount) || double.IsInfinity(amount) || amount <= 0 || amount > Balance)
            {
                throw new ArgumentException("Invalid withdrawal amount");
            }
            Balance -= amount;
            Transactions.Add(new Transaction
            {
                Date = DateTime.Now,
                Amount = amount,
                Type = "WithDrawal",
                BalanceAfter = Balance


            });
        }

        public void ShowTransaction()
        {
            foreach (var transaction in Transactions)
            {
                Console.WriteLine(transaction);
            }
        }
    }
}