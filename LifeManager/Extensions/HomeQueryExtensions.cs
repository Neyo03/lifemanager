using Microsoft.EntityFrameworkCore;
using LifeManager.Data;

namespace LifeManager.Extensions;

public static class HomeQueryExtensions
{
    public static IQueryable<Room> GetRoomsByHome(this IQueryable<Room> query, User user)
    {
        if (user.Home is null)
        {
            return query.Where(r => false);
        }
        
        return query.Where(room => room.Home!.Id == user.Home.Id);
    }
    
    public static IQueryable<HouseTask>? GetTasksByHome(this IQueryable<HouseTask> query, User user)
    {
        if (user.Home is null)
        {
            return query.Where(r => false);
        }
        
        return query.Where(task => task.Room.Home!.Id == user.Home.Id);
    }
}