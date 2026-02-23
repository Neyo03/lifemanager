namespace LifeManager.Data;

public class Home
{
    public int Id { get; set; }
    
    public string Name { get; set; } = "";
    
    public List<User> Users { get; set; } = new();
    
    public List<Room> Rooms { get; set; } = new();
}