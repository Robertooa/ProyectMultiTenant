namespace ProyectMultiTenant.Application.Dto
{
    public class UserDto
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string RepeatPassword { get; set; }
        public string OrganizationName { get; set; }
    }
}
