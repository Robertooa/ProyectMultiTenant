using System.Threading.Tasks;

namespace ProyectMultiTenant.Domain.IRepository
{
    public interface ITokenRepository
    {
        Task<bool> IsInValid(string token, string tenant);
    }
}
