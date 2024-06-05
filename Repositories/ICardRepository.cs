using HomeBanking.Models;

namespace HomeBanking.Repositories
{
    public interface ICardRepository
    {
        IEnumerable<Card> GetAllCards();
        Card GetCardById(long id);
        void Save(Card card);
        Card isCvvInUse(int cvv);
        Card isNumberInUse(string number);
    }
}
