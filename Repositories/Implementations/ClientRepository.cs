using HomeBanking.Models;
using Microsoft.EntityFrameworkCore;

namespace HomeBanking.Repositories.Implementations
{
    public class ClientRepository : RepositoryBase<Client>, IClientRepository
    {
        public ClientRepository(HomeBankingContext repositoryContext) : base(repositoryContext)
        {
        }

        public Client FindClientByEmail(string email)
        {
            return FindByCondition(client => client.Email.ToUpper() == email.ToUpper())
                .Include(client => client.Accounts)
                .Include(cl => cl.ClientLoans)
                    .ThenInclude(l => l.Loan)
                .Include(c => c.Cards)
                .FirstOrDefault();
        }

        public Client FindClientById(long id)
        {
            return FindByCondition(client => client.Id == id)
                .Include(client => client.Accounts)
                .Include(cl => cl.ClientLoans)
                    .ThenInclude(l => l.Loan)
                .Include(c => c.Cards)
                .FirstOrDefault();
        }

        public IEnumerable<Client> GetAllClients()
        {
            return FindAll()
                .Include(client => client.Accounts)
                .Include(cl => cl.ClientLoans)
                    .ThenInclude(l => l.Loan)
                .Include(c => c.Cards)
                .ToList();
        }

        public void Save(Client client)
        {
            Create(client);
            SaveChanges();
        }
    }
}
