using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models;

public class UserJobInfo
{
    [Key]
    [ForeignKey("User")]
    public int UserId { get; set; }

    [Required]
    [MaxLength(50)]
    public string JobTitle { get; set; } = string.Empty;

    [Required]
    [MaxLength(50)]
    public string Department { get; set; } = string.Empty;

    public User User { get; set; } = null!;
}