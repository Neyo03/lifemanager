namespace LifeManager.Services;

using Microsoft.EntityFrameworkCore;
using LifeManager.Data;

public class HouseService
{
    private readonly AppDbContext _context;

    public HouseService(AppDbContext context)
    {
        _context = context;
    }

    // Récupérer les pièces et leurs tâches
    public async Task<List<Room>> GetRoomsAsync()
    {
        
        return await _context.Rooms
            .ToListAsync();
    }
    
    // Récupérer les pièces et leurs tâches non terminées
    public async Task<List<Room>> GetRoomsInprogressTasksAsync()
    {
        
        return await _context.Rooms
            .AsNoTracking()
            .Include(room => room.Tasks.Where(task => !task.IsDone))
            .Where(room => room.Tasks.Any(task => !task.IsDone))
            .ToListAsync();
    }
    
    // Récupérer les pièces et leurs tâches terminées
    public async Task<List<Room>> GetRoomsDoneTasksAsync()
    {
        
        return await _context.Rooms
            .AsNoTracking()
            .Include(room => room.Tasks.Where(task => task.IsDone))
            .Where(room => room.Tasks.Any(task => task.IsDone))
            .ToListAsync();
    }
    
    // Récupérer seulement les pièces qui ont des tâches
    public async Task<List<Room>> GetRoomsWithTasksAsync()
    {
        return await _context.Rooms
            .Include(List<HouseTask> (Room room) => room.Tasks)
            .Where(bool (Room room) => room.Tasks.Any())
            .ToListAsync();
    }
    
    // Récupérer le nombre de tasks
    public async Task<int> GetTotalTasksAsync()
    {
        return await _context.HouseTasks.CountAsync();
    }
    
    // Récupérer le nombre de tasks terminées
    public async Task<int> GetTotalDoneTasksAsync()
    {
        return await _context.HouseTasks.Where(x => x.IsDone).CountAsync();
    }

    // Ajouter une tâche
    public async Task AddTaskAsync(HouseTask task)
    {
        _context.HouseTasks.Add(task);
        await _context.SaveChangesAsync();
    }
    
    // supprimer une tâche
    public async Task RemoveTaskAsync(HouseTask task)
    {
        _context.HouseTasks.Remove(task);
        await _context.SaveChangesAsync();
    }
    
    // Mets à jours la tache en la cherchant de le context et en copiant les valeur de l'objet cloné 
    // car EF Core génère une erreur car il track déjà la tache que je modifie 
    public async Task UpdateTaskAsync(HouseTask taskFromForm)
    {
        
        var existingTask = await _context.HouseTasks
            .Include(t => t.Tags)
            .FirstOrDefaultAsync(t => t.Id == taskFromForm.Id);

        if (existingTask != null)
        {
            _context.Entry(existingTask).CurrentValues.SetValues(taskFromForm);
            
            existingTask.Tags.Clear();
        
            foreach (var tag in taskFromForm.Tags)
            {
                var trackedTag = await _context.Tags.FindAsync(tag.Id);
                if (trackedTag != null)
                {
                    existingTask.Tags.Add(trackedTag);
                }
            }
            await _context.SaveChangesAsync();
        }
    }
    
    public async Task ToggleTaskAsync(HouseTask task)
    {
        task.IsDone = !task.IsDone;
        _context.Entry(task).State = EntityState.Modified;
        await _context.SaveChangesAsync();
    }
}