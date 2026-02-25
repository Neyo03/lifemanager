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
    
    public Room Room { get; set; } = new();
    
    public List<Tag> Tags { get; set; } = new();
    
    public int? UserAssignedId { get; set; }
    
    public int? CompletedById { get; set; }
}