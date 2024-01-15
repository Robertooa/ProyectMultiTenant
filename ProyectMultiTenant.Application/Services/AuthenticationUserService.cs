using ProyectMultiTenant.Application.Contracts;
using ProyectMultiTenant.Application.Dto;
using ProyectMultiTenant.Domain.Contracts;
using ProyectMultiTenant.Domain.IRepository;
using ProyectMultiTenant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectMultiTenant.Application.Services
{
    public class AuthenticationUserService : IAuthenticationUserService
    {
        private readonly IUserRepository userRepository;
        private readonly IBaseRepository<User> userBaseRepository;
        private readonly IBaseRepository<Token> tokenBaseRepository;
        private readonly ITokenGenerator tokenGenerator;
        private readonly ICryptography cryptography;
        public AuthenticationUserService(IUserRepository userRepository,
            IBaseRepository<User> userBaseRepository,
            IBaseRepository<Token> tokenBaseRepository,
            ITokenGenerator tokenGenerator,
            ICryptography cryptography)
        {
            this.userRepository = userRepository;
            this.userBaseRepository = userBaseRepository;
            this.tokenGenerator = tokenGenerator;
            this.tokenBaseRepository = tokenBaseRepository;
            this.cryptography = cryptography;
        }
        public async Task<UserLoginResponse> Authenticate(UserLoginRequest userLoginRequest)
        {
            string encryptedPassword = cryptography.GetMd5Hash(userLoginRequest.Password);
            var user = await userRepository.Authenticate(userLoginRequest.Email, encryptedPassword);
            if (user != null)
            {
                var tokens = await tokenBaseRepository.FindAllPredicate(x => x.IdUser == user.Id && x.Active.Equals("1"));
                tokens.ForEach(t =>
                {
                    t.Active = "0";
                    tokenBaseRepository.UpdatePartial(t, x => x.Active);
                });
                Token tokenEntity = new Token
                {
                    IdUser = user.Id,
                    AccsessToken = tokenGenerator.Generate(user.Id),
                    Duration = tokenGenerator.GetTokenLife(),
                    Active = "1"
                };
                await tokenBaseRepository.Add(tokenEntity);

                return new UserLoginResponse
                {
                    AccessToken = tokenEntity.AccsessToken,
                    Tenants = new List<Tenant>() { new Tenant
                    {
                        SlugTenant = user.Tenant
                    } }
                };
            }

            return null;
        }
    }
}
