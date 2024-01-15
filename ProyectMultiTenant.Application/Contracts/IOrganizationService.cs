using ProyectMultiTenant.Domain.Models;
using ProyectMultiTenant.Domain.Shared;
using ProyectMultiTenant.Domain.Tenant;
using System.Threading.Tasks;

namespace ProyectMultiTenant.Application.Contracts
{
    public interface IOrganizationService
    {
        Task<Result> Add(Organization organization);
        Task<Tenant> GetByTenant(string tenant);
    }
}
