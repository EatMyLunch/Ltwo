using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;

namespace LtwoWeb.Models
{
    public class Department
    {
        [Key]
        public int DepartmentId { get; set; }
        [Required]
        public string Name { get; set; }

        // Navigation property
        [ValidateNever]
        public List<TrainingType> TrainingTypes { get; set; }
    }
}
