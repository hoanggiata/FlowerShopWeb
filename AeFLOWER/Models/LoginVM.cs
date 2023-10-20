using System.ComponentModel.DataAnnotations;

namespace AeFLOWER.Models
{
    public class LoginVM
    {
        [Required]
        public string Username { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Required]
        public bool RememberLogin { get; set; }

        public string ReturnUrl { get; set; }
    }
}
