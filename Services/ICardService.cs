using HomeBanking.DTOs;
using HomeBanking.Models;

namespace HomeBanking.Services
{
    public interface ICardService
    {
        int GenerateCVV();
        string GenerateUniqueNumber();
        bool HasReachedCardLimit(Client client);
        bool HasReachedCardTypeLimit(Client client, string cardType);
        bool HasDuplicate(Client client, string cardType, string cardColor);
        void Save(Card card);
        void CreateCardClient(Client client, CardClientDTO cardClientDTO);
        IEnumerable<Card> GetCurrentCards(long id);
    }
}
