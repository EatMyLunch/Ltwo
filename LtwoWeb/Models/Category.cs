﻿using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

public class Category
{
    [Key]
    public int CategoryId { get; set; }
    [Required]

    public string Name { get; set; }

    [ForeignKey("TrainingTypeId")]
    public int TrainingTypeId { get; set; }

    [ValidateNever]
    public TrainingType TrainingType { get; set; }

    // Navigation property
    [ValidateNever]
    public List<TrainingTitle> TrainingTitles { get; set; }
}
