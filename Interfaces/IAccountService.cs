using BankingControlPanel.Models;

namespace BankingControlPanel.Interfaces
{
    public interface IAccountService
    {
        Task<IEnumerable<Account>> GetAllAccountsAsync();
        Task<Account> GetAccountByIdAsync(int id);
        Task<Account> CreateAccountAsync(Account account);
        Task<Account> UpdateAccountAsync(int id, Account account);
        Task<bool> DeleteAccountAsync(int id);
    }
}
