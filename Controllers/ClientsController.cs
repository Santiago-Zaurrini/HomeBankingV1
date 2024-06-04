using HomeBanking.DTOs;
using HomeBanking.Models;
using HomeBanking.Repositories;
using Microsoft.AspNetCore.Authorization;
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
                string email = User.FindFirst("Client") != null ? User.FindFirst("Client").Value : string.Empty;
                if (string.IsNullOrEmpty(email))
                {
                    return StatusCode(403, "User not found");
                }
                Client client = _clientRepository.FindClientByEmail(email);
                if (client == null)
                {
                    return StatusCode(403, "User not found");
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
                return StatusCode(201, new ClientDTO(clientUserDTO));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
