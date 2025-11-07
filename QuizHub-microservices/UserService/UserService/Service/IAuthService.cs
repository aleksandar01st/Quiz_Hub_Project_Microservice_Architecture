using UserService.DTOs;
using UserService.Models;

namespace UserService.Service
{
    public interface IAuthService
    {
        (string token, User user) Authenticate(LoginDto dto);
    }
}
