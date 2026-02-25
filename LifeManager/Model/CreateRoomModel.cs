using System.ComponentModel.DataAnnotations;

namespace LifeManager.Model;

public class CreateRoomModel
{
    [Required(ErrorMessage = "Le nom de la pièce est obligatoire.")]
    [MinLength(2, ErrorMessage = "Le nom doit faire au moins 2 caractères.")]
    public string Name { get; set; } = "";
}