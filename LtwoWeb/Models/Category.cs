using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace LtwoWeb.Models
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        [DisplayName("Display Order")]
        [Range(1,100, ErrorMessage ="1-100 only bro")]
        public int DisplayOrder { get; set; }
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;
    }
}
