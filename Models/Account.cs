namespace BankingControlPanel.Models
{
    public class Account
    {
        public int Id { get; set; }
        public string? AccountNumber { get; set; }
        public string? AccountType { get; set; }
        public decimal Balance { get; set; }
        public int ClientId { get; set; }
        public Client? Client { get; set; }
    }
}
