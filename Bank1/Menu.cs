using System;
using System.Net.Http.Headers;
using System.Xml;
using Bank1.Models;
using Spectre.Console;

namespace Bank1
{
    internal partial class Program
    {
        //  Only ONE bank 
        static Bank bank = new Bank("WEST CITY BANK");
        public static Random rnd = new Random();

        //main method

        public static void BankProgram()
        {

            //create figlet 
           
            var bankTitle=new FigletText(bank.Name)
                .Color(Color.Blue3)
                .Centered();

            //create panel for figle 
            
            var titlePanel=new Panel(bankTitle)
                .Border(BoxBorder.Double)
                .BorderStyle(new Style(Color.DarkSlateGray1))
                .Padding(1,1)
                .Expand();

            
            //write panel 

            AnsiConsole.Write(titlePanel);
            AnsiConsole.Write(new Rule("[grey]Welcome to the system[/]").RuleStyle("dim").Centered());
            AnsiConsole.WriteLine();


            // add example customer 
            /*
            bank.AddCustomer(new Customer
            {
                Id = 1,
                Name = "George West",
                AccountId = rnd.Next(1000000, 9999999),
                BirthDay = new DateTime(1990, 1, 1),
            });
            */

            //later add sample customers from file 
            

            while (true)
            {
                AnsiConsole.Clear();
                //write panel again 
                AnsiConsole.Write(titlePanel);


                //add today to calendar
                var calendar = new Calendar(DateTime.Today);
                calendar.AddCalendarEvent(DateTime.Today);
                calendar.HighlightStyle(Style.Parse("bold green"));
                AnsiConsole.Write(calendar);


                AnsiConsole.Write(new Rule("[yellow] Bank Main Menu[/]").RuleStyle("grey").Centered());
                //panel for menu info
                var menuPanel = new Panel(new Markup("[bold]Use arrow keys to navigate, press [green]Enter[/] to select[/]"))
                    .Border(BoxBorder.Heavy)
                    .BorderStyle(new Style(Color.Blue))
                    .Padding(1, 0)
                    .Expand();

                AnsiConsole.Write(menuPanel);
                //user selection 
                string userSelection = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                        .HighlightStyle("bold white on green")
                        .Title("[bold aqua]Select an operation:[/]")
                        .PageSize(10)
                        .AddChoices(new[]
                            {
                                "1 - View All Customers",
                                "2 - Register New Customer",
                                "3 - View Customer Details",
                                "4 - Make a Deposit",
                                "5 - Make a Withdrawal",
                                "6 - View Transaction History",
                                "0 - Exit Application"
                            })
                            .UseConverter(choice =>
                            {
                                var parts = choice.Split('-');
                                return $"[bold cyan]{parts[0].Trim()}[/] - [white]{parts[1].Trim()}[/]";
                            }));

                string choice = userSelection.Split('-')[0].Trim();

                switch (choice)
                {
                    case "1": bank.ShowAllCustomers(); break;
                    case "2": HandleCreateCustomer(); break;
                    case "3": HandleShowCustomerDetails(); break;
                    case "4": HandleDeposit(); break;
                    case "5": HandleWithdraw(); break;
                    case "6": ShowTransactions(); break;
                    case "0":
                        AnsiConsole.MarkupLine("[green] Thanks for using West-Bank![/]");
                        return;
                    default:
                        AnsiConsole.MarkupLine("[red]! Invalid selection.[/]");
                        break;
                }

                AnsiConsole.MarkupLine("[grey]Press any key to return to the menu...[/]");
                Console.ReadKey(true); //  get key to back
            }
        }


        public static void Menu()
        {

            var menuText = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                .Title("Bank Menu")
                .PageSize(10)
                .AddChoices(new[] {
                    "1 - Show Customers","2- Create New Customer","3- Show Customer Details ","4- Deposit Money","5- Withdraw Money","6-Show Transactions","0- Exit "
                }));

            //Console.WriteLine(menuText);
            AnsiConsole.Markup(menuText);
        }

        static void HandleDeposit()
        {
            Console.Write("Customer ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var customer = bank.FindCustomer(id);
                if (customer == null)
                {
                    //Console.WriteLine("Customer not found.");
                    AnsiConsole.MarkupLine("[Red] Customer not found [/]");
                    return;
                }

                Console.Write("Amount to deposit: ");
                if (double.TryParse(Console.ReadLine(), out double amount))
                {
                    customer.MoneyDeposit(amount);
                    //Console.WriteLine("Deposit successful.");
                    AnsiConsole.MarkupLine("[Green] Deposit successful. [/]");
                }
                else
                {
                    //Console.WriteLine("Invalid amount.");
                    AnsiConsole.MarkupLine("[Red] Invalid amount. [/]");
                }
            }
        }

        static void HandleWithdraw()
        {
            Console.Write("Customer ID: ");
            if (int.TryParse(Console.ReadLine(), out int id))
            {
                var customer = bank.FindCustomer(id);
                if (customer == null)
                {
                    //Console.WriteLine("Customer not found.");
                    AnsiConsole.MarkupLine("[Red] Customer Not found [/]");
                    return;
                }

                Console.Write("Amount to withdraw: ");
                if (double.TryParse(Console.ReadLine(), out double amount))
                {
                    try
                    {
                        customer.MoneyWithDrawal(amount);
                        //Console.WriteLine("Withdrawal successful.");
                        AnsiConsole.MarkupLine("[Green] Withdrawal successful. [/]");
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error: {ex.Message}");
                    }
                }
                else
                {
                    AnsiConsole.MarkupLine("[Red] Invalid amount. [/]");
                }
            }
        }

        static void HandleCreateCustomer()
        {
            var name = AnsiConsole.Ask<string>("Enter [green]Customer Name[/]:");

            if (string.IsNullOrWhiteSpace(name))
            {
                AnsiConsole.MarkupLine("[red] ! Name cannot be empty.[/]");
                return;
            }

            var birthdayInput = AnsiConsole.Ask<string>("Enter [green]Birthday[/] (yyyy-MM-dd):");
            if (!DateTime.TryParse(birthdayInput, out DateTime birthday))
            {
                AnsiConsole.MarkupLine("[red]! Invalid birthday format.[/]");
                return;
            }

            int newId = bank.Customers.Any() ? bank.Customers.Max(c => c.Id) + 1 : 1;
            int accountId = rnd.Next(1000000, 9999999);

            var customer = new Customer
            {
                Id = newId,
                Name = name,
                BirthDay = birthday,
                AccountId = accountId
            };

            bank.AddCustomer(customer);
            AnsiConsole.MarkupLine($"[green] Customer created:[/] ID = {customer.Id}, Account ID = {accountId}");
        }


        static void HandleShowCustomerDetails()
        {
            int id = AnsiConsole.Ask<int>("Enter [green]Customer ID[/]:");

            var customer = bank.FindCustomer(id);
            if (customer == null)
            {
                AnsiConsole.MarkupLine("[yellow]! Customer not found.[/]");
                return;
            }

            AnsiConsole.MarkupLine("[blue]📄 Customer Details:[/]");
            AnsiConsole.Write(customer.CustomerInfo());
        }


        static void ShowTransactions()
        {
            int id = AnsiConsole.Ask<int>("Enter [green]Customer ID[/]:");

            var customer = bank.FindCustomer(id);
            if (customer == null)
            {
                AnsiConsole.MarkupLine("[yellow]! Customer not found.[/]");
                return;
            }

            if (!customer.Transactions.Any())
            {
                AnsiConsole.MarkupLine("[grey]No transactions found.[/]");
                return;
            }

            var table = new Table()
                .Border(TableBorder.Rounded)
                .AddColumn("[blue]Date[/]")
                .AddColumn("[blue]Type[/]")
                .AddColumn("[blue]Amount[/]")
                .AddColumn("[blue]Balance After[/]");

            foreach (var t in customer.Transactions)
            {
                table.AddRow(
                    t.Date.ToString("g"),
                    t.Type,
                    t.Amount.ToString("F2"),
                    t.BalanceAfter.ToString("F2")
                );
            }

            AnsiConsole.Write(table);
        }



    }
}
