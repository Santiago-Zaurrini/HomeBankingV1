using HomeBanking.Models;
using HomeBanking.Repositories;

namespace HomeBanking.Services.Implementations
{
    public class CardService : ICardService
    {
        private readonly ICardRepository _cardRepository;
        public CardService(ICardRepository cardRepository)
        {
            _cardRepository = cardRepository;
        }

        private string GenerateNumber()
        {
            Random random = new Random();
            string cardNumber = "";


            for (int i = 0; i < 16; i++)
            {
                if (i > 0 && i % 4 == 0) //Cada 4 números se cumple esta condición
                {
                    cardNumber += "-"; 
                }
                int randomNumber = random.Next(0, 10); // Genera un número aleatorio entre 0 y 9
                cardNumber += randomNumber.ToString(); // Agrega el número aleatorio al string
            }
            return cardNumber;
        }

        public string GenerateUniqueNumber()
        {
            string number;
            do
            {
                number = GenerateNumber();
            } while (_cardRepository.isNumberInUse(number) != null);
            return number;
        }

        public void Save(Card card)
        {
            _cardRepository.Save(card);
        }

        int ICardService.GenerateCVV()
        {
            Random random = new Random();
            int randomNumber = random.Next(100, 1000);
            return randomNumber;
        }
    }
}
