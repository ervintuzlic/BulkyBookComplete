//** System.ComponentModel.DataAnnotations; for [Key]
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace BulkyBookComplete.Models
{
    public class Category
    {
        //** Primary Key in database
        [Key]
        public int Id { get; set; }

        //** Name is required
        [Required]
        public string Name { get; set; }

        [DisplayName("Display Order")]
        [Range(1,100,ErrorMessage ="Display Order needs to be in range of 1 - 100 only!")]
        public int DisplayOrder { get; set; }

        //** Automatically assign DateTime.Now to the new CreatedDateTime
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

    }
}
