using HomeBanking.DTOs;
using HomeBanking.Exceptions;
using HomeBanking.Models;
using HomeBanking.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace HomeBanking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly IAccountService _accountService;
        private readonly ICardService _cardService;
        public ClientsController(IAccountService service, ICardService cardService, 
            IClientService clientService)
        {           
            _accountService = service;
            _cardService = cardService;
            _clientService = clientService;
        }

        [HttpGet]
        [Authorize(Policy = "AdminOnly")]
        public IActionResult GetAllClients()
        {
            try
            {
                var clients = _clientService.GetAllClients();
                return Ok(clients.Select(c => new ClientDTO(c)).ToList());
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetClientsById(long id)
        {
            try
            {
                var client = _clientService.FindClientById(id);
                return Ok(new ClientDTO(client));
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpGet("current")]
        [Authorize(Policy = "ClientOnly")]
        public IActionResult GetCurrentUser()
        {
            try
            {
                string email = User.FindFirst("Client")?.Value ?? string.Empty;
                Client client = _clientService.GetCurrent(email);
                return Ok(new ClientDTO(client));
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult PostClient([FromBody] ClientUserDTO clientUserDTO)
        {
            try
            {
                Client newClient = _clientService.CreateClient(clientUserDTO);
                Account newAccount = _accountService.CreateAccount(newClient);
                return StatusCode(201, new ClientDTO(clientUserDTO));
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpPost("current/accounts")]
        [Authorize(Policy = "ClientOnly")]
        public IActionResult PostAccount()
        {
            try
            {
                string email = User.FindFirst("Client")?.Value ?? string.Empty;
                Client client = _clientService.GetCurrent(email);
                Account newAccount = _accountService.CreateAccForExistingClient(client);
                return StatusCode(201, "Cuenta creada");
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpGet("current/accounts")]
        [Authorize(Policy = "ClientOnly")]
        public IActionResult GetCurrentAccounts()
        {
            try
            {
                string email = User.FindFirst("Client")?.Value ?? string.Empty;
                Client client = _clientService.GetCurrent(email);
                var accounts = _accountService.FindAccountsByClient(client.Id);
                return Ok(accounts.Select(a => new AccountDTO(a)).ToList());
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }


        [HttpPost("current/cards")]
        [Authorize(Policy = "ClientOnly")]
        public IActionResult PostCard(CardClientDTO cardClientDTO)
        {
            try
            {
                string email = User.FindFirst("Client")?.Value ?? string.Empty;
                Client client = _clientService.GetCurrent(email);
                _cardService.CreateCardClient(client, cardClientDTO);
                return StatusCode(201, "Tarjeta creada");
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }

        [HttpGet("current/cards")]
        public IActionResult GetCurrentCards()
        {
            try
            {
                string email = User.FindFirst("Client")?.Value ?? string.Empty;
                Client client = _clientService.GetCurrent(email);
                var cards = _cardService.GetCurrentCards(client.Id);
                return Ok(cards.Select(c => new CardDTO(c)).ToList());
            }
            catch (CustomException ex)
            {
                return StatusCode(ex.StatusCode, ex.Message);
            }
        }
    }
}
