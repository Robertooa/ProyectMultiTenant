using ProyectMultiTenant.Application.Dto;
using ProyectMultiTenant.Domain.Models;
using ProyectMultiTenant.Domain.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectMultiTenant.Application.Contracts
{
    public interface IProductService
    {
        Task<Result> Add(Product product);
        Task<List<ProductResponse>> GetAll();
    }
}
