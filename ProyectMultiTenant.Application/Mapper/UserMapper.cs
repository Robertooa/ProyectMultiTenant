using ProyectMultiTenant.Application.Dto;
using ProyectMultiTenant.Domain.Models;

namespace ProyectMultiTenant.Application.Mapper
{
    public class UserMapper
    {
        private UserDto userDto;
        public UserMapper(UserDto userDto)
        {
            this.userDto = userDto;
        }
        public User Map()
        {
            return new User
            {
                Email = userDto.Email,
                Password = userDto.Password,
            };
        }
    }
}
