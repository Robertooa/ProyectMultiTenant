using ProyectMultiTenant.Application.Dto;
using ProyectMultiTenant.Domain.Models;
using System.Collections.Generic;

namespace ProyectMultiTenant.Application.Mapper
{
    public static class ProductMapper
    {
        public static List<ProductResponse> Map(List<Product> products)
        {
            var list = new List<ProductResponse>();
            products.ForEach(p=>{
                list.Add(new ProductResponse
                {
                    Name = p.Name,
                    Description = p.Description,
                    Duration = p.Duration
                });
            });

            return list;
        }
    }
}
