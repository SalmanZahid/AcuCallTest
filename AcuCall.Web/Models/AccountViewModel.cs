using System.ComponentModel.DataAnnotations;

namespace AcuCall.Web.Models
{
    public class LoginViewModel
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
