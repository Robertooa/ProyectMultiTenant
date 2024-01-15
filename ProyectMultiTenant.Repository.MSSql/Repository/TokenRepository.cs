using Microsoft.EntityFrameworkCore;
using ProyectMultiTenant.Domain.Dto;
using ProyectMultiTenant.Domain.IRepository;
using ProyectMultiTenant.Domain.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectMultiTenant.Repository.MSSql.Repository
{
    public class TokenRepository : BaseRepository<Token>, ITokenRepository
    {
        public TokenRepository(ApplicationDbContext context):base(context)
        {

        }
        public async Task<bool> IsInValid(string token, string tenant)
        {
            var tokenEntity = await (from t in context.Token
                                    join u in context.User on t.IdUser equals u.Id
                                    join org in context.Organization on u.IdOrganization equals org.Id
                                    where t.AccsessToken.Equals(token)
                                    && org.SlugTenant.Equals(tenant)
                                    && t.Active.Equals("1")
                                    select new TokenDto
                                    {
                                        Id = t.Id
                                    }
                                    ).FirstOrDefaultAsync();

            return tokenEntity == null;
        }

    }
}
