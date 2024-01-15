using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProyectMultiTenant.Application.Contracts;
using ProyectMultiTenant.Domain.Models;

namespace ProyectMultiTenant.WebApi.Controllers
{
    [EnableCors("AllowCors"), Route("api/[controller]")]
    [ApiController]
    public class OrganizationController : ControllerBase
    {
        private readonly IOrganizationService organizationService;
        public OrganizationController(IOrganizationService organizationService)
        {
            this.organizationService = organizationService;
        }

        [HttpPost("add")]
        public async Task<IActionResult> Add(Organization organization)
        {
            var result = await organizationService.Add(organization);

            return Ok(result);
        }
    }
}