
using System;
using Bank1.Models;
using Spectre.Console;
namespace Bank1
{
    internal partial class Program
    {


        //load sample customers from json
        public static void LoadCustomers()
        {
            try
            {
                string json = File.ReadAllText("customers.json");

                // in order to add balance 
                var rawCustomers = System.Text.Json.JsonSerializer.Deserialize<List<RawCustomer>>(json);

                if (rawCustomers != null)
                {
                    foreach (var rc in rawCustomers)
                    {
                        var customer = new Customer
                        {
                            Id = rc.Id,
                            Name = rc.Name,
                            BirthDay = rc.BirthDay,
                            AccountId = rc.AccountId
                        };

                        if (rc.Balance > 0)
                        {
                            customer.MoneyDeposit(rc.Balance);
                        }

                        bank.AddCustomer(customer);
                    }

                    AnsiConsole.MarkupLine($"[green] Loaded {rawCustomers.Count} customers from file.[/]");
                }
            }
            catch (Exception ex)
            {
                AnsiConsole.MarkupLine($"[red] Failed to load customers: {ex.Message}[/]");
            }
        }



    }

    //we create customer with such DTO method. but not exactly 

    public class RawCustomer
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int AccountId { get; set; }
        public DateTime BirthDay { get; set; }
        public double Balance { get; set; }
    }


}