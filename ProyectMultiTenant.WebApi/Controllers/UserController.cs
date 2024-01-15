using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProyectMultiTenant.Application.Contracts;
using ProyectMultiTenant.Application.Dto;

namespace ProyectMultiTenant.WebApi.Controllers
{
    [EnableCors("AllowCors"), Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService userService;
        public UserController(IUserService userService)
        {
            this.userService = userService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(UserDto userDto)
        {
            var result = await userService.Add(userDto);

            return Ok(result);
        }
    }
}