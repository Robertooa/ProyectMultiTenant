namespace ProyectMultiTenant.Domain.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int IdOrganization { get; set; }
        public virtual Organization Organization { get; set; }
    }
}
