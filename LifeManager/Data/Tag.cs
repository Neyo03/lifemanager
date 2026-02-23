using System.ComponentModel.DataAnnotations;

namespace LifeManager.Data;

public class Tag
{
    public int Id { get; set; }
    public string Label { get; set; } = "";
    public string ColorHex { get; set; } = "#3b82f6";
    
    public List<HouseTask> Tasks { get; set; } = new();
    
    [Required(ErrorMessage = "Une maison est obligatoire.")]
    public int? HomeId { get; set; }
}