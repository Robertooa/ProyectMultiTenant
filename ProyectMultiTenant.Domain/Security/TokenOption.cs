namespace ProyectMultiTenant.Domain.Security
{
    public class TokenOption
    {
        public string Secret { get; set; }
        public string SecretPublic { get; set; }
        public int ExpireToken { get; set; }
    }
}
