using System.ComponentModel.DataAnnotations;

namespace HighInfoVoter_Api.Models.Request
{
    public class AccountAddRequest
    {
        [Required]
        [MaxLength(128)]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required]
        [MaxLength(256)]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
