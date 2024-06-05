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

        public int GenerateCVV()
        {
            Random random = new Random();
            int randomNumber = random.Next(100, 1000);
            return randomNumber;
        }

        public bool HasReachedCardLimit(Client client)
        {
            return client.Cards.Count >= 6;
        }

        public bool HasReachedCardTypeLimit(Client client, string cardType)
        {
            int cardCount = client.Cards.Count(c => c.Type == cardType);
            return cardCount >= 3;
        }

        public void CreateCardClient(Client client, string cardType, string cardColor)
        {
            if (HasReachedCardLimit(client))
            {
                throw new Exception("Alcanzado el límite (6) de tarjetas totales.");
            }

            if (HasReachedCardTypeLimit(client, cardType))
            {
                if (cardType == CardType.DEBIT.ToString())
                {
                    throw new Exception("Alcanzado el límite (3) de tarjetas de débito.");
                }
                else if (cardType == CardType.CREDIT.ToString())
                {
                    throw new Exception("Alcanzado el límite (3) de tarjetas de crédito.");
                }
            }

            if (HasDuplicate(client, cardType, cardColor))
            {
                throw new Exception("Ya existe una tarjeta con el mismo tipo y color.");
            }

            var newCard = new Card
            {
                CardHolder = client.FirstName + " " + client.LastName,
                ClientId = client.Id,
                Type = cardType,
                Color = cardColor,
                FromDate = DateTime.Now,
                ThruDate = DateTime.Now.AddYears(5),
                Number = GenerateUniqueNumber(),
                Cvv = GenerateCVV(),
            };

            Save(newCard);
        }

        public bool HasDuplicate(Client client, string cardType, string cardColor)
        {
            return client.Cards.Any(c => c.Type == cardType && c.Color == cardColor);
        }
    }
}
