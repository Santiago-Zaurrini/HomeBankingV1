using HomeBanking.Models;

namespace HomeBanking.DTOs
{
    public class ClientUserDTO
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public ClientUserDTO(Client client)
        {
            FirstName = client.FirstName;
            LastName = client.LastName;
            Email = client.Email;
            Password = client.Password;
        }
    }
}
