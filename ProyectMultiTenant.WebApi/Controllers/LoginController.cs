using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProyectMultiTenant.Application.Contracts;
using ProyectMultiTenant.Application.Dto;
using System.Threading.Tasks;

namespace ProyectMultiTenant.WebApi.Controllers
{
    [EnableCors("AllowCors"), Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IAuthenticationUserService userService;
        public LoginController(IAuthenticationUserService userService)
        {
            this.userService = userService;
        }
        [HttpPost("autenticate")]
        public async Task<IActionResult> Autenticate(UserLoginRequest userLoginRequest)
        {
            var result = await userService.Authenticate(userLoginRequest);

            return Ok(result);
        }
    }
}