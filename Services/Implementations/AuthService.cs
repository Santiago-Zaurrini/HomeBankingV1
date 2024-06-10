using HomeBanking.DTOs;
using HomeBanking.Exceptions;
using HomeBanking.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Net;
using System.Security.Claims;
using static BCrypt.Net.BCrypt;

namespace HomeBanking.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IClientService _clientService;

        public AuthService(IClientService clientService)
        {
            _clientService = clientService;
        }
        public ClaimsIdentity GetIdentity(LoginDTO loginDTO)
        {
            try
            {
                Client user = _clientService.FindClientByEmail(loginDTO.Email);
                if (user == null)
                {
                    throw new CustomException("User not found", 401);
                }
                if (!Verify(loginDTO.Password, user.Password))
                {
                    throw new CustomException("Invalid user info", 401);
                }

                var claims = new List<Claim>
                {
                    new Claim("Client", user.Email)
                };

                if (user.Email.Equals("szaurrini@gmail.com"))
                {
                    claims.Add(new Claim("Admin", "true"));
                }

                var claimsIdentity = new ClaimsIdentity(claims,
                    CookieAuthenticationDefaults.AuthenticationScheme);
                return claimsIdentity;
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex.Message, ex.StatusCode);
            }
        }
    }
}