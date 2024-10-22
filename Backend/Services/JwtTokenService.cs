using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using EStore.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using EStore.StartupConfigurations;

namespace Estore.Services
{
    public interface IJwtTokenService
    {
        RefreshToken GenerateRefreshToken();
        string? GenerateSymmetricJwtToken(string username);
        long? ExtractIdFromExpiredToken(string token);

    }

    public class JwtTokenService : IJwtTokenService
    {
        private readonly JwtOptions _jwtOptions;

        public JwtTokenService(IOptions<JwtOptions> options)
        {
            _jwtOptions = options.Value;
        }

        

        public string? GenerateSymmetricJwtToken(string username)
        {
            
            if (username != "admin@example.com") // edit username email
                return null;
                
            

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);


            var claims = new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Sub, username),
                new Claim("IsAdmin", "true"),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
            };
           

            var token = new JwtSecurityToken(
                issuer: _jwtOptions.Issuer,
                audience: _jwtOptions.Audience,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(double.Parse(_jwtOptions.AccessTokenValidity)),
                signingCredentials: credentials); // privatekey

            var strToken = new JwtSecurityTokenHandler().WriteToken(token);

            return strToken;
        }

        public RefreshToken GenerateRefreshToken()
        {
             var refreshToken = new RefreshToken()
             {
                 ExpiryDate = DateTime.UtcNow.AddDays(double.Parse(_jwtOptions.RefreshTokenValidity)),
                 Value = Guid.NewGuid().ToString("N"),
                 Name = "local",
                 LoginProvider = "localhost",
             };

            return refreshToken;
             
        }


       
        public  ClaimsPrincipal GetSymmetricPrincipalFromExpiredToken(string token)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtOptions.Key));

            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = true,
                ValidateAudience = true,
                ValidateIssuerSigningKey = true,
                ValidateLifetime = false, // Ignore token expiration
                ValidIssuer = _jwtOptions.Issuer,
                ValidAudience = _jwtOptions.Audience,
                IssuerSigningKey = key
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            SecurityToken securityToken;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out securityToken);

            var jwtToken = securityToken as JwtSecurityToken;
            if (jwtToken == null)
            {
                throw new SecurityTokenException("Invalid token");
            }

            return principal;
        }

        public long? ExtractIdFromExpiredToken(string token)
        {
            var principal =  GetSymmetricPrincipalFromExpiredToken(token);
            var idClaim = principal.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);
            long id;
            if (idClaim?.Value != null)
            {
              var res = long.TryParse(idClaim.Value, out id);
                if (res)
                    return id;
            }
            return null;
        }


    }
}
