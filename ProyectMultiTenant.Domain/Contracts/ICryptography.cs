namespace ProyectMultiTenant.Domain.Contracts
{
    public interface ICryptography
    {
        string GetMd5Hash(string input);

        bool VerifyMd5Hash(string input, string hash);
    }
}
