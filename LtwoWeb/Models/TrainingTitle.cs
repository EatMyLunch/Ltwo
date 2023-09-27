using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class TrainingTitle
{
    [Key]
    public int TrainingTitleId { get; set; }
    [Required]

    public string Name { get; set; }

    [ForeignKey("CategoryId")]
    public int CategoryId { get; set; }

    [ValidateNever]
    public Category Category { get; set; }

    // Navigation property
    [ValidateNever]
    public List<Analysis> Analyses { get; set; }
}
