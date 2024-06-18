using HomeBanking.DTOs;
using HomeBanking.Models;

namespace HomeBanking.Services
{
    public interface IClientService
    {
        Client GetCurrent(string email);
        Client FindClientById(long id);
        Client FindClientByEmail(string email);
        Client CreateClient(ClientUserDTO clientUserDTO);
        IEnumerable<Client> GetAllClients();
        void Save(Client client);
    }
}
