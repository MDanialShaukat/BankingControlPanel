using Microsoft.EntityFrameworkCore;
using BankingControlPanel.Data;
using BankingControlPanel.Interfaces;
using BankingControlPanel.Models;

namespace BankingControlPanel.Services
{
    public class ClientService : IClientService
    {
        private readonly ApplicationDbContext _context;

        public ClientService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Client>> GetClients(string search, int page, int pageSize)
        {
            return await _context.Clients
                .Where(c => c.FirstName.Contains(search) || c.LastName.Contains(search) || c.Email.Contains(search))
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();
        }

        public async Task<Client> CreateClient(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<Client> UpdateClient(Client client)
        {
            _context.Entry(client).State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task DeleteClient(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client != null)
            {
                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<Client> GetClientById(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            return client;
        }
    }
}
