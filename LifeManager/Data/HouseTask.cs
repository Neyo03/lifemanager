using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace LifeManager.Data;

public class HouseTask
{
    public int Id { get; set; }
    
    [Required(ErrorMessage = "Un titre est obligatoire.")]
    public string Title { get; set; } = "";
    
    public string Description { get; set; } = "";
    
    public DateTime? DueDate { get; set; }
    
    public bool IsDone { get; set; }
    
    [Required(ErrorMessage = "Une pièce est obligatoire.")]
    public int? RoomId { get; set; }
    
    public List<Tag> Tags { get; set; } = new();
}