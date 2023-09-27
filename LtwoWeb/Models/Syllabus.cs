using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Syllabus
{
    [Key]
    public int SyllabusId { get; set; }
    [Required]

    public string Content { get; set; }

    [ForeignKey("AnalysisId")]
    public int AnalysisId { get; set; }

    [ValidateNever]
    public Analysis Analysis { get; set; }
}
