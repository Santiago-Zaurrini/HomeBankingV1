using HomeBanking.DTOs;
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
        public bool HasDuplicate(Client client, string cardType, string cardColor)
        {
            return client.Cards.Any(c => c.Type == cardType && c.Color == cardColor);
        }
        public void CreateCardClient(Client client, CardClientDTO cardClientDTO)
        {
            if (string.IsNullOrEmpty(cardClientDTO.Type) || string.IsNullOrEmpty(cardClientDTO.Color))
            {
                throw new Exception("Datos faltantes");
            }

            if (HasReachedCardLimit(client))
            {
                throw new Exception("Alcanzado el límite (6) de tarjetas totales.");
            }

            if (HasReachedCardTypeLimit(client, cardClientDTO.Type))
            {
                if (cardClientDTO.Type == CardType.DEBIT.ToString())
                {
                    throw new Exception("Alcanzado el límite (3) de tarjetas de débito.");
                }
                else if (cardClientDTO.Type == CardType.CREDIT.ToString())
                {
                    throw new Exception("Alcanzado el límite (3) de tarjetas de crédito.");
                }
            }

            if (HasDuplicate(client, cardClientDTO.Type, cardClientDTO.Color))
            {
                throw new Exception("Ya existe una tarjeta con el mismo tipo y color.");
            }

            var newCard = new Card
            {
                CardHolder = $"{client.FirstName} {client.LastName}",
                ClientId = client.Id,
                Type = cardClientDTO.Type,
                Color = cardClientDTO.Color,
                FromDate = DateTime.Now,
                ThruDate = DateTime.Now.AddYears(5),
                Number = GenerateUniqueNumber(),
                Cvv = GenerateCVV(),
            };

            Save(newCard);
        }
        public void Save(Card card)
        {
            _cardRepository.Save(card);
        }

        public IEnumerable<Card> GetCurrentCards(long clientId)
        {
            return _cardRepository.GetClientCards(clientId);
        }
    }
}
