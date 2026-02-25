using Microsoft.EntityFrameworkCore;
using LifeManager.Data;

namespace LifeManager.Services;

public class TagService(IDbContextFactory<AppDbContext> factory)
{
    public async Task<List<Tag>?> GetTagsAsync(User user)
    {
        if (user == null) throw new ArgumentNullException(nameof(user));
        await using var context = await factory.CreateDbContextAsync();
        var home = await context.Homes.Include(home => home.Tags).FirstOrDefaultAsync( home => home.Id == user.Home.Id);
        return home?.Tags.ToList(); 
    }

    public async Task AddTagAsync(Tag tag)
    {
        await using var context = await factory.CreateDbContextAsync();
        context.Tags.Add(tag);
        await context.SaveChangesAsync();
    }
}