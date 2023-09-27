using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Analysis
{
    [Key]
    public int AnalysisId { get; set; }
    [Required]

    public string Description { get; set; }

    [ForeignKey("TrainingTitleId")]
    public int TrainingTitleId { get; set; }

    [ValidateNever]
    public TrainingTitle TrainingTitle { get; set; }

    // Navigation property
    [ValidateNever]
    public List<Syllabus> Syllabuses { get; set; }
}
