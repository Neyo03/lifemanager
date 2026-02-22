using LifeManager.Data;
using LifeManager.Services;

namespace LifeManager.State;

public class TagStateService
{
    private readonly TagService _tagService;
    
    // The shared list of tags in memory
    public List<Tag> Tags { get; private set; } = new();
    
    // The event triggered whenever the tag list changes
    public event Action? OnChange;

    public TagStateService(TagService tagService)
    {
        _tagService = tagService;
    }

    // Loads tags from the DB ONLY if the list is currently empty
    public async Task InitializeAsync()
    {
        if (!Tags.Any())
        {
            Tags = await _tagService.GetTagsAsync();
            NotifyStateChanged();
        }
    }

    // Adds a tag to the DB, updates the memory list, and notifies all components
    public async Task AddTagAsync(Tag tag)
    {
        await _tagService.AddTagAsync(tag);
        Tags = await _tagService.GetTagsAsync(); // Refresh from DB to get the new ID
        NotifyStateChanged();
    }

    // Deletes a tag, updates memory, and notifies components
    // public async Task DeleteTagAsync(Tag tag)
    // {
    //     await _tagService.DeleteTagAsync(tag);
    //     Tags.Remove(tag);
    //     NotifyStateChanged();
    // }

    // Triggers the UI refresh for all subscribed components
    private void NotifyStateChanged() => OnChange?.Invoke();
}