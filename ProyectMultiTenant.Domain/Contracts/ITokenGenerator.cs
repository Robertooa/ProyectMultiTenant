namespace ProyectMultiTenant.Domain.Contracts
{
    public interface ITokenGenerator
    {
        string Generate(int IdUser);
        int GetTokenLife();
    }
}
