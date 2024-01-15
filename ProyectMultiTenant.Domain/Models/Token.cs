namespace ProyectMultiTenant.Domain.Models
{
    public class Token
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public string AccsessToken { get; set; }
        public int Duration { get; set; }
        public string Active { get; set; }
        public virtual User User { get; set; }
    }
}
