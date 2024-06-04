using HomeBanking.DTOs;
using HomeBanking.Models;
using HomeBanking.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace HomeBanking.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientsController : ControllerBase
    {
        private readonly IClientRepository _clientRepository;
        public ClientsController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        [HttpGet]
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
        public IActionResult GetCurrentUser()
        {
            try
            {
                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
                if (string.IsNullOrEmpty(email))
                {
                    return Forbid();
                }
                Client client = _clientRepository.FindClientByEmail(email);
                if (client == null)
                {
                    return Forbid();
                }
                var clientUserDTO = new ClientDTO(client);
                return Ok(clientUserDTO);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        public IActionResult PostClient([FromBody] ClientUserDTO client)
        {
            try
            {
                if (String.IsNullOrEmpty(client.Email) || String.IsNullOrEmpty(client.Password) || String.IsNullOrEmpty(client.FirstName)
                    || String.IsNullOrEmpty(client.LastName))
                {
                    return StatusCode(403, "Datos Inválidos");
                }
                  
                Client clientExists = _clientRepository.FindClientByEmail(client.Email);
                if (clientExists != null)
                {
                    return StatusCode(403, "Email está en uso");
                }

                var newClient = new Client
                {
                    Email = client.Email,
                    Password = client.Password,
                    FirstName = client.FirstName,
                    LastName = client.LastName,
                };

                _clientRepository.Save(newClient);
                return Created("", client);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
