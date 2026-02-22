namespace LifeManager.Data;

public class Room
{
    public int Id { get; set; }
    public string Name { get; set; } = "";
    public List<HouseTask> Tasks { get; set; } = new();
}