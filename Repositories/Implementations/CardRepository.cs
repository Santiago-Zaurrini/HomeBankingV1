using HomeBanking.Models;

namespace HomeBanking.Repositories.Implementations
{
    public class CardRepository : RepositoryBase<Card>, ICardRepository
    {
        public CardRepository(HomeBankingContext repositoryContext) : base(repositoryContext)
        {
        }

        public IEnumerable<Card> GetAllCards()
        {
            return FindAll()
                .ToList();
        }

        public Card GetCardById(long id)
        {
            return FindByCondition(c => c.Id == id)
                .FirstOrDefault();
        }

        public Card isCvvInUse(int cvv)
        {
            return FindByCondition(c => c.Cvv == cvv)
                .FirstOrDefault();
        }

        public Card isNumberInUse(string number)
        {
            return FindByCondition(c => c.Number == number)
                .FirstOrDefault();
        }

        public void Save(Card card)
        {
            Create(card);
            SaveChanges();
        }
    }
}
