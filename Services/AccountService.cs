using BankingControlPanel.Data;
using BankingControlPanel.Interfaces;
using BankingControlPanel.Models;
using Microsoft.EntityFrameworkCore;

namespace BankingControlPanel.Services
{
    public class AccountService : IAccountService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the AccountService class with the specified DbContext.
        /// </summary>
        /// <param name="context">The ApplicationDbContext instance.</param>
        public AccountService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves all accounts asynchronously.
        /// </summary>
        /// <returns>A task representing the asynchronous operation. The task result contains an enumerable of accounts.</returns>
        public async Task<IEnumerable<Account>> GetAllAccountsAsync()
        {
            return await _context.Accounts.ToListAsync();
        }

        /// <summary>
        /// Retrieves an account by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the account.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the account if found; otherwise, null.</returns>
        public async Task<Account> GetAccountByIdAsync(int id)
        {
            return await _context.Accounts.FindAsync(id);
        }

        /// <summary>
        /// Creates a new account asynchronously.
        /// </summary>
        /// <param name="account">The account to create.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the created account.</returns>
        public async Task<Account> CreateAccountAsync(Account account)
        {
            _context.Accounts.Add(account);
            await _context.SaveChangesAsync();
            return account;
        }

        /// <summary>
        /// Updates an existing account asynchronously.
        /// </summary>
        /// <param name="id">The ID of the account to update.</param>
        /// <param name="account">The account data to update.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the updated account if found; otherwise, null.</returns>
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

        /// <summary>
        /// Deletes an account by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the account to delete.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains true if the account was deleted; otherwise, false.</returns>
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
