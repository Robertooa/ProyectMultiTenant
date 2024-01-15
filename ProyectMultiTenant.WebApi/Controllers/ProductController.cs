using System.Threading.Tasks;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using ProyectMultiTenant.Application.Contracts;
using ProyectMultiTenant.Domain.Models;
using ProyectMultiTenant.WebApi.Filters;

namespace ProyectMultiTenant.WebApi.Controllers
{
    [EnableCors("AllowCors"), Route("api/[controller]")]
    [ApiController]
    [TypeFilter(typeof(ValidateToken))]
    public class ProductController : ControllerBase
    {
        private readonly IProductService productService;
        public ProductController(IProductService productService)
        {
            this.productService = productService;
        }

        [HttpPost("add")]

        public async Task<IActionResult> Add(Product product)
        {
            var result = await productService.Add(product);

            return Ok(result);
        }

        [HttpGet("getall")]

        public async Task<IActionResult> GetAll()
        {
            var products = await productService.GetAll();

            return Ok(products);
        }
    }
}