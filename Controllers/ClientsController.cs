using HomeBanking.DTOs;
using HomeBanking.Models;
using HomeBanking.Repositories;
using HomeBanking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeBanking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        private readonly IAccountService _accountService;
        private readonly ICardService _cardService;
        public ClientsController(IClientRepository clientRepository, IAccountService service, 
            ICardService cardService)
        {
            _clientRepository = clientRepository;
            _accountService = service;
            _cardService = cardService;
        }

        private Client GetCurrentClient()
        {
            string email = User.FindFirst("Client")?.Value ?? string.Empty;
            if (string.IsNullOrEmpty(email))
            {
                throw new Exception("User not found");
            }
            Client client = _clientRepository.FindClientByEmail(email);
            if (client == null)
            {
                throw new Exception("User not found");
            }
            return client;
        }


        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult GetAllClients()
        {
            try
            {
                var clients = _clientRepository.GetAllClients();
                //Maps Client to ClientDTO
                var clientsDTO = clients.Select(c => new ClientDTO(c)).ToList();
                //Returns status code and client
                return Ok(clientsDTO);
            }
            catch (Exception ex)
            {
                //Returns status code and error message
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetClientsById(long id)
        {
            try
            {
                var client = _clientRepository.FindClientById(id);
                var clientDTO = new ClientDTO(client);
                return Ok(clientDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("current")]
        [Authorize(Policy = "ClientOnly")]
        public IActionResult GetCurrentUser()
        {
            try
            {
                Client client = GetCurrentClient();
                var clientUserDTO = new ClientDTO(client);
                return Ok(clientUserDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult PostClient([FromBody] ClientUserDTO clientUserDTO)
        {
            try
            {
                if (String.IsNullOrEmpty(clientUserDTO.Email) || String.IsNullOrEmpty(clientUserDTO.Password) || String.IsNullOrEmpty(clientUserDTO.FirstName)
                    || String.IsNullOrEmpty(clientUserDTO.LastName))
                {
                    return StatusCode(400, "Datos faltantes");
                }
                  
                Client clientExists = _clientRepository.FindClientByEmail(clientUserDTO.Email);
                if (clientExists != null)
                {
                    return StatusCode(400, "Email está en uso");
                }

                var newClient = new Client
                {
                    Email = clientUserDTO.Email,
                    Password = clientUserDTO.Password,
                    FirstName = clientUserDTO.FirstName,
                    LastName = clientUserDTO.LastName,
                };

                
                _clientRepository.Save(newClient);

                // Buscamos el cliente recién creado para acceder a su id
                Client savedClient = _clientRepository.FindClientByEmail(newClient.Email);
                var newAccount = new Account
                {
                    CreationDate = DateTime.Now,
                    Balance = 0,
                    ClientId = savedClient.Id, 
                    Transactions = new List<Transaction>(),
                    Number = _accountService.GenerateUniqueAccountNumber()
                };

                _accountService.Save(newAccount);

                return StatusCode(201, new ClientDTO(clientUserDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("current/accounts")]
        [Authorize(Policy = "ClientOnly")]
        public IActionResult PostAccount()
        {
            try
            {              
                Client client = GetCurrentClient();
                if (client.Accounts.Count >= 3)
                {
                    return StatusCode(403, "Alcanzado el límite de cuentas.");
                }
                else
                {
                    var newAccount = new Account
                    {
                        CreationDate = DateTime.Now,
                        Balance = 0,
                        ClientId = client.Id,
                        Transactions = new List<Transaction>(),
                        Number = _accountService.GenerateUniqueAccountNumber()
                    };

                    _accountService.Save(newAccount);
                    return StatusCode(201, "Cuenta creada");
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost("current/cards")]
        [Authorize(Policy = "ClientOnly")]
        public IActionResult PostCard(CardClientDTO cardClientDTO)
        {
            try
            {
                if (String.IsNullOrEmpty(cardClientDTO.Type) || String.IsNullOrEmpty(cardClientDTO.Color))
                {
                    return StatusCode(400, "Datos faltantes");
                }

                Client client = GetCurrentClient();

                int debitCards = client.Cards.Count(c => c.Type == CardType.DEBIT.ToString());
                int creditCards = client.Cards.Count(c => c.Type == CardType.CREDIT.ToString());

                if (client.Cards.Count >= 6)
                {
                    return StatusCode(403, "Alcanzado el límite (6) de tarjetas totales.");
                }
                else if (cardClientDTO.Type == CardType.DEBIT.ToString() && debitCards >= 3)
                {
                    return StatusCode(403, "Alcanzado el límite (3) de tarjetas de débito.");
                }
                else if (cardClientDTO.Type == CardType.CREDIT.ToString() && creditCards >= 3)
                {
                    return StatusCode(403, "Alcanzado el límite (3) de tarjetas de crédito.");
                }


                var newCard = new Card
                {
                    CardHolder = client.FirstName + " " + client.LastName,
                    ClientId = client.Id,
                    Type = cardClientDTO.Type,
                    Color = cardClientDTO.Color,
                    FromDate = DateTime.Now,
                    ThruDate = DateTime.Now.AddYears(5),
                    Number = _cardService.GenerateUniqueNumber(),
                    Cvv = _cardService.GenerateCVV(),
                };
                _cardService.Save(newCard);
                return StatusCode(201, "Tarjeta creada");
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
