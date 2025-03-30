namespace Bank1.Models
{
    public class Transaction
    {
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public string Type { get; set; } = "";
        public double BalanceAfter { get; set; }

        public override string ToString()
        {
            return $"{Date:G} | {Type,-10} | Amount: {Amount,8:F2} | Balance: {BalanceAfter,8:F2}";
        }
    }
}
