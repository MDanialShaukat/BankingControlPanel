using Microsoft.EntityFrameworkCore;
using BankingControlPanel.Data;
using BankingControlPanel.Interfaces;
using BankingControlPanel.Models;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace BankingControlPanel.Services
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _context;

        /// <summary>
        /// Initializes a new instance of the ClientService class with the specified DbContext.
        /// </summary>
        /// <param name="context">The ApplicationDbContext instance.</param>
        public ClientService(ApplicationDbContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Retrieves a paginated list of clients based on search criteria.
        /// </summary>
        /// <param name="search">The search term for filtering clients.</param>
        /// <param name="page">The page number for pagination.</param>
        /// <param name="pageSize">The number of clients per page.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains an enumerable of clients.</returns>
        public async Task<IEnumerable<Client>> GetClients(string search, int page, int pageSize)
        {
            return await _context.Clients
                .Where(c => c.FirstName.Contains(search) || c.LastName.Contains(search) || c.Email.Contains(search))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        /// <summary>
        /// Creates a new client asynchronously.
        /// </summary>
        /// <param name="client">The client to create.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the created client.</returns>
        public async Task<Client> CreateClient(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        /// <summary>
        /// Updates an existing client asynchronously.
        /// </summary>
        /// <param name="client">The client data to update.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the updated client.</returns>
        public async Task<Client> UpdateClient(Client client)
        {
            //update the client
            _context.Entry(client).State = EntityState.Modified;
            // Update the address
            _context.Entry(client.Address).State = EntityState.Modified;
            // Update each account
            foreach (var account in client.Accounts)
            {
                _context.Entry(account).State = EntityState.Modified;
            }
            await _context.SaveChangesAsync();
            return client;
        }

        /// <summary>
        /// Deletes a client by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the client to delete.</param>
        /// <returns>A task representing the asynchronous operation.</returns>
        public async Task DeleteClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
        }

        /// <summary>
        /// Retrieves a client by its ID asynchronously.
        /// </summary>
        /// <param name="id">The ID of the client.</param>
        /// <returns>A task representing the asynchronous operation. The task result contains the client if found; otherwise, null.</returns>
        public async Task<Client> GetClientById(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            return client;
        }
    }
}
