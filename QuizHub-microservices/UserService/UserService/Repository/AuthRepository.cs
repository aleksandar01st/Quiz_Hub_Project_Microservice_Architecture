using UserService.Data;
using UserService.Models;

namespace UserService.Repository
{
    public class AuthRepository : IAuthRepository
    {
        private readonly UserServiceContext _context;

        public AuthRepository(UserServiceContext context)
        {
            _context = context;
        }

        public User? GetUserByUsernameOrEmail(string usernameOrEmail)
        {
            return _context.Users.FirstOrDefault(u =>
                u.Username == usernameOrEmail || u.Email == usernameOrEmail);
        }
    }
}
