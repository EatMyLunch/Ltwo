using LtwoWeb.Models;
using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TrainingType
{
    [Key]
    public int TrainingTypeId { get; set; }
    [Required]

    public string Name { get; set; }

    [ForeignKey("DepartmentId")]
    public int DepartmentId { get; set; }

    [ValidateNever]
    public Department Department { get; set; }

    // Navigation property
    [ValidateNever]
    public List<Category> Categories { get; set; }
}
