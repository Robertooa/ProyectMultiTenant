namespace ProyectMultiTenant.Domain.Tenant
{
    public interface ITenantSetter
    {
        Tenant CurrentTenant { set; }
    }
}
