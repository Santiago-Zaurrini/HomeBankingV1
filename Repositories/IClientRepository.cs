using HomeBanking.Models;

namespace HomeBanking.Repositories
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAllClients();
        Client FindClientById(long id);
        void Save(Client client);

    }
}
