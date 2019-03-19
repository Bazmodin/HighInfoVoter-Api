using System.ComponentModel.DataAnnotations;

namespace HighInfoVoter_Api.Models.Request
{
    public class ConfigAddRequest
    {
        [Required]
        [MaxLength(128)]
        [Display(Name = "Config Key")]
        public string ConfigKey { get; set; }

        [Required]
        [MaxLength(256)]
        [Display(Name = "Config Value")]
        public string ConfigValue { get; set; }

        [MaxLength(64)]
        [Display(Name = "Modified By")]
        public string ModifiedBy { get; set; }
    }
}
