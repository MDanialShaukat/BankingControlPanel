using BankingControlPanel.Data;
using BankingControlPanel.Interfaces;
using BankingControlPanel.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingControlPanel.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;

        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        public async Task<Account> GetAccountByIdAsync(int id)
        {
            return await _context.Accounts.FindAsync(id);
        }

        public async Task<Account> CreateAccountAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        public async Task<Account> UpdateAccountAsync(int id, Account account)
        {
            var existingAccount = await _context.Accounts.FindAsync(id);
            if (existingAccount is null)
            {
                return null;
            }

            existingAccount.AccountNumber = account.AccountNumber;
            existingAccount.AccountType = account.AccountType;
            existingAccount.Balance = account.Balance;
            // Update other fields as necessary

            _context.Accounts.Update(existingAccount);
            await _context.SaveChangesAsync();
            return existingAccount;
        }

        public async Task<bool> DeleteAccountAsync(int id)
        {
            var account = await _context.Accounts.FindAsync(id);
            if (account is null)
            {
                return false;
            }

            _context.Accounts.Remove(account);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
