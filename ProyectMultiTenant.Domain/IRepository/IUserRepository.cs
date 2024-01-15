using ProyectMultiTenant.Domain.Dto;
using System.Threading.Tasks;

namespace ProyectMultiTenant.Domain.IRepository
{
    public interface IUserRepository
    {
        Task<UserLogged> Authenticate(string user, string password);
    }
}
