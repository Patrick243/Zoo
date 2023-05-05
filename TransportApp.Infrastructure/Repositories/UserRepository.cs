

using Microsoft.EntityFrameworkCore;
using TransportApp.Application.Repositories;
using TransportApp.Domain;

namespace TransportApp.Infrastructure.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationDbContext _context;

    public UserRepository(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task AddUserAsync(User user)
    {
        user.RoleId = -2;

        _context.Users.Add(user);

        await _context.SaveChangesAsync();
    }

    public async Task DeleteUserAsync(User user)
    {
        _context.Users.Remove(user);
        await _context.SaveChangesAsync();
    }

    public async Task<User?> GetUserByIdAsync(int id)
    {
        return  await _context.Users.Where(user => user.Id == id).FirstOrDefaultAsync();
    }

    public async Task<User?> LoginAsync(string username, string password)
    {
        return await _context.Users.Include(user => user.Role).Where(user => user.Username == username && user.Password == password).FirstOrDefaultAsync();
    }

    public async Task UpdateUserAsync(User user)
    {
        _context.Users.Update(user);
        await _context.SaveChangesAsync();
    }
}
