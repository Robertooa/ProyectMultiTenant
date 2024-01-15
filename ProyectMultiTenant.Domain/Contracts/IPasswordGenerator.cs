namespace ProyectMultiTenant.Domain.Contracts
{
    public interface IPasswordGenerator
    {
        string Generate(int length);
    }
}
