using LifeManager.Data;
using LifeManager.Services;

namespace LifeManager.State;

public class TagStateService(TagService tagService)
{
    public List<Tag> Tags { get; private set; } = new();
    
    public event Action? OnChange;
    
    public async Task InitializeAsync(User user)
    {
        if (Tags.Count == 0)
        {
            await RefreshAsync(user);
        }
    }
    
    public async Task AddTagAsync(Tag tag)
    {
        await tagService.AddTagAsync(tag);
    }

    public async Task RefreshAsync(User user)
    {
        var fetchedTags = await tagService.GetTagsAsync(user);
        Tags = fetchedTags ?? new();
        NotifyStateChanged();
    }

    // Deletes a tag, updates memory, and notifies components
    // public async Task DeleteTagAsync(Tag tag)
    // {
    //     await _tagService.DeleteTagAsync(tag);
    //     Tags.Remove(tag);
    //     NotifyStateChanged();
    // }
    
    private void NotifyStateChanged() => OnChange?.Invoke();
}