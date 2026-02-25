using System.ComponentModel.DataAnnotations;

namespace LifeManager.Model;

public class TagCreateModel
{
    [Required(ErrorMessage = "Le libellé du tag est obligatoire.")]
    [MinLength(2, ErrorMessage = "Le libellé doit faire au moins 2 caractères.")]
    public string Label { get; set; } = "";
    
    [Required(ErrorMessage = "La couleur du tag est obligatoire.")]
    public string ColorHex { get; set; } = "#3b82f6";
}