using ProyectMultiTenant.Application.Dto;
using ProyectMultiTenant.Domain.Shared;
using System.Threading.Tasks;

namespace ProyectMultiTenant.Application.Contracts
{
    public interface IUserService
    {
        Task<Result> Add(UserDto userDto);
    }
}
