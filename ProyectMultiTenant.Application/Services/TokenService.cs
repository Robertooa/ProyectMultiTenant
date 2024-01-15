using ProyectMultiTenant.Application.Contracts;
using ProyectMultiTenant.Domain.IRepository;
using ProyectMultiTenant.Domain.Models;
using ProyectMultiTenant.Domain.Tenant;
using System.Threading.Tasks;

namespace ProyectMultiTenant.Application.Services
{
    public class TokenService : ITokenService
    {
        private readonly ITokenRepository tokenRepository;
        private ITenantContext tenant;
        public TokenService(ITokenRepository tokenRepository, ITenantContext tenant)
        {
            this.tokenRepository = tokenRepository;
            this.tenant = tenant;
        }

        public async Task<bool> IsInValid(string token)
        {
            var existToken = await tokenRepository.IsInValid(token, tenant.CurrentTenant.Name);

            return existToken;
        }
    }
}
