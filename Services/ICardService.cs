using HomeBanking.Models;

namespace HomeBanking.Services
{
    public interface ICardService
    {
        int GenerateCVV();
        string GenerateUniqueNumber();
        bool HasReachedCardLimit(Client client);
        bool HasReachedCardTypeLimit(Client client, string cardType);
        void Save(Card card);
        void CreateCardClient(Client client, string cardType, string cardColor);
        bool HasDuplicate(Client client, string cardType, string cardColor);
    }
}
