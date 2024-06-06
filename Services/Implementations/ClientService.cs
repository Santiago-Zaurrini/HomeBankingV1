using HomeBanking.DTOs;
using HomeBanking.Models;
using HomeBanking.Repositories;

namespace HomeBanking.Services.Implementations
{
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public Client CreateClient(ClientUserDTO clientUserDTO)
        {
            if (String.IsNullOrEmpty(clientUserDTO.Email) || String.IsNullOrEmpty(clientUserDTO.Password) 
                || String.IsNullOrEmpty(clientUserDTO.FirstName) || String.IsNullOrEmpty(clientUserDTO.LastName))
            {
                throw new Exception("Datos faltantes");
            }
            var newClient = new Client
            {
                Email = clientUserDTO.Email,
                Password = clientUserDTO.Password,
                FirstName = clientUserDTO.FirstName,
                LastName = clientUserDTO.LastName,
            };

            // Guardar el cliente en la base de datos
            _clientRepository.Save(newClient);

            // Devolver el cliente creado
            return newClient;
        }

        public Client FindClientByEmail(string email)
        {
            return _clientRepository.FindClientByEmail(email);
        }

        public Client FindClientById(long id)
        {
            return _clientRepository.FindClientById(id);
        }

        public IEnumerable<Client> GetAllClients()
        {
            return _clientRepository.GetAllClients();
        }

        public Client GetCurrent(string email)
        {
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

        public void Save(Client client)
        {
            _clientRepository.Save(client);
        }
    }
}
