namespace ProyectMultiTenant.Domain.IRepository
{
    public interface IGeneratorTenantRepository
    {
        void GenerateDataBaseProduct(string tenantName);
    }
}
