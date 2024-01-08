using TaskMgmt.DataAccess.Models;
namespace TaskMgmt.DataAccess.Repositories
{
    public interface IUserRepository
    {
        public Task<IEnumerable<User>> Get();
        public Task<User> GetByEmail(string email);
        public Task Add(User user);
    }
}