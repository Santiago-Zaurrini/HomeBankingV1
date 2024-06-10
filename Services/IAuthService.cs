using HomeBanking.DTOs;
using System.Security.Claims;

namespace HomeBanking.Services
{
    public interface IAuthService
    {
        ClaimsIdentity GetIdentity(LoginDTO login);
    }
}
