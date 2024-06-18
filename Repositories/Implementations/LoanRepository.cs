using HomeBanking.Models;

namespace HomeBanking.Repositories.Implementations
{
    public class LoanRepository : RepositoryBase<Loan>, ILoanRepository
    {
        public LoanRepository(HomeBankingContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Loan> GetAllLoans()
        {
            return FindAll().ToList();
        }

        public Loan GetLoanById(long id)
        {
            return FindByCondition(l => l.Id == id)
                .FirstOrDefault();
        }

        public void Save(Loan loan)
        {
            Create(loan);
            SaveChanges();
        }
    }
}
