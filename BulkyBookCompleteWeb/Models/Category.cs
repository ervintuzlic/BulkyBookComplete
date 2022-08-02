//** System.ComponentModel.DataAnnotations; for [Key]
using System.ComponentModel.DataAnnotations;

namespace BulkyBookCompleteWeb.Models
{
    public class Category
    {
        //** Primary Key in database
        [Key]
        public int Id { get; set; }

        //** Name is required
        [Required]
        public string Name { get; set; }

        public int DisplayOrder { get; set; }

        //** Automatically assign DateTime.Now to the new CreatedDateTime
        public DateTime CreatedDateTime { get; set; } = DateTime.Now;

    }
}
