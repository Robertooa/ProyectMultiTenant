namespace ProyectMultiTenant.Domain.Tenant
{
    public class TenantContext : ITenantContext, ITenantSetter
    {
        public Tenant CurrentTenant { get; set; }
    }
}
