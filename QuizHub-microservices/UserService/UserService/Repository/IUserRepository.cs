using UserService.Models;

namespace UserService.Repository
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAll();
        User? GetById(long id);
        void Add(User user);
        void SaveChanges();
    }
}
