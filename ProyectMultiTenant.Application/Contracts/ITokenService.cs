using System.Threading.Tasks;

namespace ProyectMultiTenant.Application.Contracts
{
    public interface ITokenService
    {
        Task<bool> IsInValid(string token);
    }
}
