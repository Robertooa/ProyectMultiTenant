using ProyectMultiTenant.Application.Contracts;
using ProyectMultiTenant.Application.Dto;
using ProyectMultiTenant.Application.Mapper;
using ProyectMultiTenant.CrossCutting;
using ProyectMultiTenant.Domain.IRepository;
using ProyectMultiTenant.Domain.Models;
using ProyectMultiTenant.Domain.Shared;
using ProyectMultiTenant.Repository.MSSql.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProyectMultiTenant.Application.Services
{
    public class ProductService : IProductService
    {
        private readonly IBaseRepositoryTenant2<Product> productBaseRepository;
        public ProductService(IBaseRepositoryTenant2<Product> productBaseRepository)
        {
            this.productBaseRepository = productBaseRepository;
        }

        public async Task<Result> Add(Product product)
        {
            await productBaseRepository.Add(product);

            return new Result
            {
                Message = Constants.ProcessMessage.MSG_PROCESS_SUCCESSFULLY_COMPLETED
            };
        }

        public async Task<List<ProductResponse>> GetAll()
        {
            var products = await productBaseRepository.GetAll();

            return ProductMapper.Map(products);
        }
    }
}
