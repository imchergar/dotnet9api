using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebApplication2.Models;

public class UserSalary
{
    [Key]
    [ForeignKey("User")]
    public int UserId { get; set; }

    [Column(TypeName = "decimal(18,4)")]
    public decimal Salary { get; set; }

    public User User { get; set; } = null!;
}