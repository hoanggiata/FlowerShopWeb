using System.ComponentModel.DataAnnotations;

namespace AeFLOWER.Models
{
    public class RegVM
    {
        [Required]
        public string UserName { get; set; } = null!;

        [Required]
        [DataType(DataType.Password)]
        public string PassWord { get; set; } = null!;

        [Compare("PassWord")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; } = null!;

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Email { get; set; } = null!;

        public string returnUrl { get; set; } = null!;
    }
}
