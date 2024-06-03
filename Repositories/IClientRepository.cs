using HomeBanking.Models;

namespace HomeBanking.Repositories
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAllClients();
        Client FindClientById(long id);
        Client FindClientByEmail(string email);
        void Save(Client client);

    }
}
