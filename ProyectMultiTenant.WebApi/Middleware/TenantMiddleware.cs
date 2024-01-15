using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Extensions;
using ProyectMultiTenant.Application.Contracts;
using ProyectMultiTenant.Domain.Tenant;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectMultiTenant.WebApi.Middleware
{
    public class TenantMiddleware
    {
        private readonly RequestDelegate next;

        public TenantMiddleware(RequestDelegate next)
        {
            this.next = next;
        }

        public async Task Invoke(
            HttpContext context, ITenantSetter tenantSetter, IOrganizationService organizationService)
        {
            (string tenantName, string realPath) = GetTenantAndPathFrom(context.Request);

            Tenant tenant = await organizationService.GetByTenant(tenantName);

            if (tenant.Exist())
            {
                context.Request.PathBase = $"/{tenant.Name}";
                context.Request.Path = realPath;
            }
            tenantSetter.CurrentTenant = tenant;
            await next(context);
        }

        private static (string tenantName, string realPath)
            GetTenantAndPathFrom(HttpRequest httpRequest)
        {
            var tenantName = new Uri(httpRequest.GetDisplayUrl())
                .Segments
                .FirstOrDefault(x => x != "/")
                ?.TrimEnd('/');

            if (!string.IsNullOrWhiteSpace(tenantName) &&
                httpRequest.Path.StartsWithSegments($"/{tenantName}",
                    out PathString realPath))
            {
                return (tenantName, realPath);
            }

            return (null, null);
        }
    }
}
