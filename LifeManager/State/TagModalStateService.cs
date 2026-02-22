namespace LifeManager.State;
using System;



public class TagModalStateService
{
    public bool IsOpen { get; private set; }
    
    public event Action? OnChange;

    public void Show()
    {
        IsOpen = true;
        NotifyStateChanged();
    }

    public void Close()
    {
        IsOpen = false;
        NotifyStateChanged();
    }

    private void NotifyStateChanged() => OnChange?.Invoke();
}