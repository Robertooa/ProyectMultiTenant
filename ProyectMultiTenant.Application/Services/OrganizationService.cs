using ProyectMultiTenant.Application.Contracts;
using ProyectMultiTenant.CrossCutting;
using ProyectMultiTenant.Domain.IRepository;
using ProyectMultiTenant.Domain.Models;
using ProyectMultiTenant.Domain.Shared;
using ProyectMultiTenant.Domain.Tenant;
using ProyectMultiTenant.Repository.MSSql;
using System.Text;
using System.Threading.Tasks;

namespace ProyectMultiTenant.Application.Services
{
    public class OrganizationService : IOrganizationService
    {
        private readonly IBaseRepository<Organization> baseRepository;
        private readonly IGeneratorTenantRepository generatorTenantRepository;
        public OrganizationService(IBaseRepository<Organization> baseRepository,
                                    IGeneratorTenantRepository generatorTenantRepository)
        {
            this.baseRepository = baseRepository;
            this.generatorTenantRepository = generatorTenantRepository;
        }

        public async Task<Result> Add(Organization organization)
        {
            await baseRepository.Add(organization);
            generatorTenantRepository.GenerateDataBaseProduct(organization.SlugTenant);

            return new Result
            {
                Message = Constants.ProcessMessage.MSG_PROCESS_SUCCESSFULLY_COMPLETED
            };
        }

        public async Task<Tenant> GetByTenant(string tenant)
        {
            var organization = await baseRepository.FindPredicate(x => x.SlugTenant.Equals(tenant));

            if (organization == null)
            {
                return new Tenant
                {
                    Id = 0,
                    Name = string.Empty
                };
            }
            else
            {
                return new Tenant
                {
                    Id = organization.Id,
                    Name = organization.SlugTenant
                };
            }
            
        }
    }
}
