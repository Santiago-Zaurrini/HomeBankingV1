using HomeBanking.Models;

namespace HomeBanking.Services
{
    public interface ICardService
    {
        int GenerateCVV();
        string GenerateUniqueNumber();
        void Save(Card card);
    }
}
