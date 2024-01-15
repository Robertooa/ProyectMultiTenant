using ProyectMultiTenant.Application.Contracts;
using ProyectMultiTenant.Application.Dto;
using ProyectMultiTenant.Application.Mapper;
using ProyectMultiTenant.CrossCutting;
using ProyectMultiTenant.Domain.Contracts;
using ProyectMultiTenant.Domain.IRepository;
using ProyectMultiTenant.Domain.Models;
using ProyectMultiTenant.Domain.Shared;
using System.Threading.Tasks;

namespace ProyectMultiTenant.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IBaseRepository<User> baseRepository;
        private readonly IBaseRepository<Organization> organizationBaseRepository;
        private readonly ICryptography cryptography;
        public UserService(IBaseRepository<User> baseRepository,
            IBaseRepository<Organization> organizationBaseRepository,
            ICryptography cryptography)
        {
            this.baseRepository = baseRepository;
            this.organizationBaseRepository = organizationBaseRepository;
            this.cryptography = cryptography;
        }

        public async Task<Result> Add(UserDto userDto)
        {
            var organization = await organizationBaseRepository.FindPredicate(x => x.Name == userDto.OrganizationName);
            var user = new User
            {
                Email = userDto.Email,
                Password = cryptography.GetMd5Hash(userDto.Password),
                IdOrganization = organization.Id
            };
            await baseRepository.Add(user);

            return new Result
            {
                Message = Constants.ProcessMessage.MSG_PROCESS_SUCCESSFULLY_COMPLETED
            };
        }
    }
}
