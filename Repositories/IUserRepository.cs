using ProfileHub.Models;

namespace ProfileHub.Repositories
{
    public interface IUserRepository
    {
        public interface IUserRepository
        {
            Task<IEnumerable<User>> GetUsersAsync();
            Task<User> GetUserAsync(int id);
            Task AddUserAsync(User user);
            Task UpdateUserAsync(User user);
            Task DeleteUserAsync(int id);
        }
    }
}
