using UserService.Data;
using UserService.Models;

namespace UserService.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly UserServiceContext _context;

        public UserRepository(UserServiceContext context)
        {
            _context = context;
        }

        public IEnumerable<User> GetAll() => _context.Users.ToList();

        public User? GetById(long id) => _context.Users.Find(id);

        public void Add(User user) => _context.Users.Add(user);

        public void SaveChanges() => _context.SaveChanges();
    }
}
