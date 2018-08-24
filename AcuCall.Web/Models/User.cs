using System.ComponentModel.DataAnnotations;

namespace AcuCall.Web.Models
{
    public class User
    {
        public int UserId { get; set; }
        [Required]
        [Display(Name ="First Name")]
        [MaxLength(50)]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        [MaxLength(50)]
        public string LastName { get; set; }
        [Required]
        [MaxLength(50)]
        public string Username { get; set; }
        [Required]
        [MaxLength(50)]
        public string Password { get; set; }
    }
}
