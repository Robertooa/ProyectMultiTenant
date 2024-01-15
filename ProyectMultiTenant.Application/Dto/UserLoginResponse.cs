using System.Collections.Generic;

namespace ProyectMultiTenant.Application.Dto
{
    public class UserLoginResponse
    {
        public string AccessToken { get; set; }
        public List<Tenant> Tenants { get; set; }
    }
    public class Tenant
    {
        public string SlugTenant { get; set; }
    }
}
