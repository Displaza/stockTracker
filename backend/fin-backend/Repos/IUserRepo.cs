using fin_backend.Models;

namespace fin_backend.Repos
{
    public interface IUserRepo
    {
        Task<User> GetUserAsync(string username);
        Task AddAsync(User user);
        Task SaveChangesAsync();
    }
}
