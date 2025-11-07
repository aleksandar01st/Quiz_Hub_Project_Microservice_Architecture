using UserService.Models;

namespace UserService.Repository
{
    public interface IAuthRepository
    {
        User? GetUserByUsernameOrEmail(string usernameOrEmail);
    }
}
