using Microsoft.EntityFrameworkCore;
using LifeManager.Data;

namespace LifeManager.Services;

public class TagService
{
    private readonly IDbContextFactory<AppDbContext> _factory;
    
    public TagService(IDbContextFactory<AppDbContext> factory)
    {
        _factory = factory;
    }

    public async Task<List<Tag>> GetTagsAsync()
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.Tags.ToListAsync();
    }

    public async Task AddTagAsync(Tag tag)
    {
        await using var context = await _factory.CreateDbContextAsync();
        context.Tags.Add(tag);
        await context.SaveChangesAsync();
    }
}