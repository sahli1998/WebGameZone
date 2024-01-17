using Microsoft.AspNetCore.Identity;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace GameZone.Models
{
    public class ApplicationUser:IdentityUser
    {
        [DisplayName( "Phone Office")]
        public string? PhoneNumber2 { get; set; }


        [NotMapped]
        public bool IsAdmin { get; set; }
    }
}
