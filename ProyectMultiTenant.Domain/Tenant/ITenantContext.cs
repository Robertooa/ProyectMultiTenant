namespace ProyectMultiTenant.Domain.Tenant
{
    public interface ITenantContext
    {
        Tenant CurrentTenant { get; set; }
    }
}
