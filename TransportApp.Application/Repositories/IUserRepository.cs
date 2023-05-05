using TransportApp.Domain;

namespace TransportApp.Application.Repositories;

public interface IUserRepository
{
    public Task AddUserAsync(User user);
    public Task UpdateUserAsync(User user);
    public Task DeleteUserAsync(User user);
    public Task<User> GetUserByIdAsync(int id);
    public Task<User> LoginAsync(string username, string password);
}
