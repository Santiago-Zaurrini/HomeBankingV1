using HomeBanking.DTOs;
using HomeBanking.Exceptions;
using HomeBanking.Models;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using static BCrypt.Net.BCrypt;

namespace HomeBanking.Services.Implementations
{
    public class AuthService : IAuthService
    {
        private readonly IClientService _clientService;
        private readonly IConfiguration _config;

        public AuthService(IClientService clientService, IConfiguration config)
        {
            _clientService = clientService;
            _config = config;
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

        //Generador de token
        public string GenerateToken(LoginDTO loginDTO)
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
                    claims.Add(new Claim("Role", "Admin"));
                }

                // Crear la clave de seguridad y las credenciales de firma
                var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:SecretKey"]));
                var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                // Crear el token JWT
                var token = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.Now.AddMinutes(10),
                    signingCredentials: creds);

                // Devolver el token JWT como una cadena
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
            catch (CustomException ex)
            {
                throw new CustomException(ex.Message, ex.StatusCode);
            }
        }
    }
}