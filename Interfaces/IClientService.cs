using BankingControlPanel.Models;

namespace BankingControlPanel.Interfaces
{
    public interface IClientService
    {
        Task<IEnumerable<Client>> GetClients(string search, int page, int pageSize);
        Task<Client> CreateClient(Client client);
        Task<Client> UpdateClient(Client client);
        Task DeleteClient(int id);
        Task<Client> GetClientById(int id);
    }
}
