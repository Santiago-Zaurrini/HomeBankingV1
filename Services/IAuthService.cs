using HomeBanking.DTOs;
using HomeBanking.Models;
using System.Security.Claims;

namespace HomeBanking.Services
{
    public interface IAuthService
    {
        ClaimsIdentity GetIdentity(LoginDTO login);
        string GenerateToken(LoginDTO login);
    }
}
