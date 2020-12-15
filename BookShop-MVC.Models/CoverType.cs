using System.ComponentModel.DataAnnotations;

namespace BookShop_MVC.Models
{
    public class CoverType
    {
        [Key]
        public int Id { get; set; }

        [Display(Name="CoverType Name")]
        [Required]
        [MaxLength(50)]
        public string Name { get; set; }
    }
}