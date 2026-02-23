using System.ComponentModel.DataAnnotations;

namespace LifeManager.Model;

public class RegisterModel
{
    [Required(ErrorMessage = "Firstname is required")]
    public string Firstname { get; set; } = string.Empty;

    [Required(ErrorMessage = "Lastname is required")]
    public string Lastname { get; set; } = string.Empty;

    [Required(ErrorMessage = "Username is required")]
    public string Username { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email format")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Password is required")]
    [MinLength(6, ErrorMessage = "Password must be at least 6 characters")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Please confirm your password")]
    [Compare(nameof(Password), ErrorMessage = "Passwords do not match")]
    public string ConfirmPassword { get; set; } = string.Empty;
}