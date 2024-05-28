using HomeBanking.Models;

namespace HomeBanking.Repositories
{
    public interface IClientRepository
    {
        IEnumerable<Client> GetAllClients();
        Client FindById(long id);
        void Save(Client client);

    }
}
