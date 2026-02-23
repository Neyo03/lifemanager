using LifeManager.Data;
using Microsoft.EntityFrameworkCore;

namespace LifeManager.Services;

public class UserService(IDbContextFactory<AppDbContext> factory)
{
    public async Task<User?> AuthenticateAsync(string email, string password)
    {
        await using var context = await factory.CreateDbContextAsync();
        var user = await context.Users.FirstOrDefaultAsync(u => u.Email == email);
        
        if (user == null || !BCrypt.Net.BCrypt.Verify(password, user.Password))
            return null;

        return user;
    }

    public async Task<bool> RegisterAsync(User user)
    {
        await using var context = await factory.CreateDbContextAsync();
        
        if (await context.Users.AnyAsync(u => u.Email == user.Email))
            return false;

        user.Password = BCrypt.Net.BCrypt.HashPassword(user.Password);
        context.Users.Add(user);
        await context.SaveChangesAsync();
        return true;
    }
}