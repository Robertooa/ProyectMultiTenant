using ProyectMultiTenant.Application.Dto;
using System.Threading.Tasks;

namespace ProyectMultiTenant.Application.Contracts
{
    public interface IAuthenticationUserService
    {
        Task<UserLoginResponse> Authenticate(UserLoginRequest userLoginRequest);
    }
}
