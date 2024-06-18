using HomeBanking.Models;

namespace HomeBanking.Repositories
{
    public interface ICardRepository
    {
        IEnumerable<Card> GetAllCards();
        IEnumerable<Card> GetClientCards(long id);
        Card GetCardById(long id);
        Card isCvvInUse(int cvv);
        Card isNumberInUse(string number);
        void Save(Card card);
    }
}
