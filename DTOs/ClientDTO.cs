using HomeBanking.Models;

namespace HomeBanking.DTOs
{
    public class ClientDTO
    {
        public ClientDTO(Client client)
        {
            Id = client.Id;
            FirstName = client.FirstName;
            LastName = client.LastName;
            Email = client.Email;
            Accounts = client.Accounts.Select(a => new AccountDTO(a)).ToList(); 
        }
        public long Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public ICollection<AccountDTO> Accounts { get; set; }
    }
}
