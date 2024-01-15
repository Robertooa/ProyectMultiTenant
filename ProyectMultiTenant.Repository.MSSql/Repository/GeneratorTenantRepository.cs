using Microsoft.EntityFrameworkCore;
using ProyectMultiTenant.Domain.IRepository;
using ProyectMultiTenant.Domain.Tenant;

namespace ProyectMultiTenant.Repository.MSSql.Repository
{
    public class GeneratorTenantRepository : IGeneratorTenantRepository
    {
        private MultiTenantDbContext contextProduct;
        private ITenantContext tenant;
        public GeneratorTenantRepository(MultiTenantDbContext contextProduct, ITenantContext tenant)
        {
            this.contextProduct = contextProduct;
            this.tenant = tenant;
        }

        public void GenerateDataBaseProduct(string tenantName)
        {
            tenant.CurrentTenant = new Tenant
            {
                Name = tenantName
            };
            contextProduct.Database.Migrate();
        }
    }
}
