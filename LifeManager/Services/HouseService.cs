namespace LifeManager.Services;

using Microsoft.EntityFrameworkCore;
using LifeManager.Data;

public class HouseService
{
    private readonly IDbContextFactory<AppDbContext> _factory;

    public HouseService(IDbContextFactory<AppDbContext> factory)
    {
        _factory = factory;
    }

    // Récupérer les pièces et leurs tâches
    public async Task<List<Room>> GetRoomsAsync()
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.Rooms
            .ToListAsync();
    }
    
    // Récupérer les pièces et leurs tâches non terminées
    public async Task<List<Room>> GetRoomsInprogressTasksAsync()
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.Rooms
            .AsNoTracking()
            .Include(room => room.Tasks.Where(task => !task.IsDone))
            .Where(room => room.Tasks.Any(task => !task.IsDone))
            .ToListAsync();
    }
    
    // Récupérer les pièces et leurs tâches terminées
    public async Task<List<Room>> GetRoomsDoneTasksAsync()
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.Rooms
            .AsNoTracking()
            .Include(room => room.Tasks.Where(task => task.IsDone))
            .Where(room => room.Tasks.Any(task => task.IsDone))
            .ToListAsync();
    }
    
    // Récupérer seulement les pièces qui ont des tâches
    public async Task<List<Room>> GetRoomsWithTasksAsync()
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.Rooms
            .Include(List<HouseTask> (Room room) => room.Tasks)
            .Where(bool (Room room) => room.Tasks.Any())
            .ToListAsync();
    }
    
    // Récupérer le nombre de tasks
    public async Task<int> GetTotalTasksAsync()
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.HouseTasks.CountAsync();
    }
    
    // Récupérer le nombre de tasks terminées
    public async Task<int> GetTotalDoneTasksAsync()
    {
        await using var context = await _factory.CreateDbContextAsync();
        return await context.HouseTasks.Where(x => x.IsDone).CountAsync();
    }

    // Ajouter une tâche
    public async Task AddTaskAsync(HouseTask task)
    {
        await using var context = await _factory.CreateDbContextAsync();
        context.HouseTasks.Add(task);
        await context.SaveChangesAsync();
    }
    
    // supprimer une tâche
    public async Task RemoveTaskAsync(HouseTask task)
    {
        await using var context = await _factory.CreateDbContextAsync();
        context.HouseTasks.Remove(task);
        await context.SaveChangesAsync();
    }
    
    // Mets à jours la tache en la cherchant de le context et en copiant les valeur de l'objet cloné 
    // car EF Core génère une erreur car il track déjà la tache que je modifie 
    public async Task UpdateTaskAsync(HouseTask taskFromForm)
    {
        await using var context = await _factory.CreateDbContextAsync();
        var existingTask = await context.HouseTasks
            .Include(t => t.Tags)
            .FirstOrDefaultAsync(t => t.Id == taskFromForm.Id);

        if (existingTask != null)
        {
            context.Entry(existingTask).CurrentValues.SetValues(taskFromForm);
            
            existingTask.Tags.Clear();
        
            foreach (var tag in taskFromForm.Tags)
            {
                var trackedTag = await context.Tags.FindAsync(tag.Id);
                if (trackedTag != null)
                {
                    existingTask.Tags.Add(trackedTag);
                }
            }
            await context.SaveChangesAsync();
        }
    }
    
    public async Task ToggleTaskAsync(HouseTask task)
    {
        await using var context = await _factory.CreateDbContextAsync();
        
        await context.HouseTasks
            .Where(t => t.Id == task.Id)
            .ExecuteUpdateAsync(setters => setters.SetProperty(t => t.IsDone, !task.IsDone));
    }
}