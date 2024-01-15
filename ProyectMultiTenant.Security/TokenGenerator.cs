using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using ProyectMultiTenant.Domain.Contracts;
using ProyectMultiTenant.Domain.Security;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ProyectMultiTenant.Security
{
    public class TokenGenerator : ITokenGenerator
    {
        private readonly TokenOption tokenOption;
        public int TokenLife { get; private set; }
        public TokenGenerator(IOptions<TokenOption> tokenOption)
        {
            this.tokenOption = tokenOption.Value;
            TokenLife = this.tokenOption.ExpireToken;
        }
        public string Generate(int IdUser)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(tokenOption.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, IdUser.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(tokenOption.ExpireToken),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }

        public int GetTokenLife()
        {
            return TokenLife;
        }
    }
}
