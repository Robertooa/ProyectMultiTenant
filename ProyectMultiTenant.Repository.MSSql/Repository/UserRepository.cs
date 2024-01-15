using Microsoft.EntityFrameworkCore;
using ProyectMultiTenant.Domain.Dto;
using ProyectMultiTenant.Domain.IRepository;
using ProyectMultiTenant.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProyectMultiTenant.Repository.MSSql.Repository
{
    public class UserRepository : BaseRepository<User>, IUserRepository
    {
        public UserRepository(ApplicationDbContext context):base(context)
        {
        }
        public async Task<UserLogged> Authenticate(string email, string password)
        {
            var result = await(from u in context.User
                               join o in context.Organization on u.IdOrganization equals o.Id
                               where u.Email.Equals(email)
                               && u.Password.Equals(password)
                               select new UserLogged
                               {
                                   Id = u.Id,
                                   Tenant = o.SlugTenant

                               }).FirstOrDefaultAsync();

            return result;
        }
    }
}
