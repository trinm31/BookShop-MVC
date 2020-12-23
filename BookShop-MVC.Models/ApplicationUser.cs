using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Identity;

namespace BookShop_MVC.Models
{
    public class ApplicationUser: IdentityUser
    {
        [Required]
        public string Name { get; set; }
        public string StreetAddress { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string PostalCode { get; set; }
        public int? CompanyId { get; set; }
        [ForeignKey("CompanyId")]
        public Company company { get; set; }
        [NotMapped] 
        public string Role { get; set; }
    }
}