namespace ProyectMultiTenant.Domain.Tenant
{
    public class Tenant
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public Tenant()
        {
        }
        public bool Exist()
        {
            return Id > 0;
        }
    }
}
