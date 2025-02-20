using System.ComponentModel.DataAnnotations;

namespace WebApplication2.DTO;

public class UserDto
{
    [Required]
    [MaxLength(50)]
    public string FirstName { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string LastName { get; set; } = string.Empty;

    [Required]
    [EmailAddress]
    [MaxLength(50)]
    public string Email { get; set; } = string.Empty;

    [MaxLength(50)]
    public string? Gender { get; set; }

    public bool Active { get; set; }
}