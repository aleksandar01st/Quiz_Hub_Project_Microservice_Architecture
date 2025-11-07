using UserService.DTOs;

namespace UserService.Service
{
    public interface IUserService
    {
        IEnumerable<UserDto> GetUsers();
        UserDto? GetUser(long id);
        UserDto CreateUser(CreateUserDto dto);
    }
}
