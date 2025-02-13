using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using RestaurantAPI.Domain.DTO.Token;
using RestaurantAPI.Domain.DTO.User;
using RestaurantAPI.Domain.Interface.Token;

namespace RestaurantAPI.Infra.Security.Token
{
    public class TokenService : ITokenService
    {
        private readonly JwtSettings _jwtSettings;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public TokenService(IConfiguration configuration,
            IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
            _jwtSettings = new JwtSettings
            {
                SecretKey = configuration["JwtSettings:SecretKey"],
                ExpirationInMinutes = int.Parse(configuration["JwtSettings:ExpirationInMinutes"] ?? "240")
            };
        }
        public string Generate(UserLoginResponseDTO user)
        {
            JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
            if (string.IsNullOrEmpty(_jwtSettings?.SecretKey))
                throw new ArgumentNullException("Token secret não configurado");

            byte[] key = Encoding.ASCII.GetBytes(_jwtSettings.SecretKey);

            SecurityTokenDescriptor tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new System.Security.Claims.ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Email, user.Email)
                }),
                Expires = DateTime.Now.AddMinutes(_jwtSettings.ExpirationInMinutes),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public UserDTO GetUser()
        {
            var user = _httpContextAccessor.HttpContext.User;

            return new UserDTO
            {
                Id = int.Parse(user.FindFirst(ClaimTypes.NameIdentifier)?.Value),
                Email = user.FindFirst(ClaimTypes.Email)?.Value
            };
        }
    }
}
