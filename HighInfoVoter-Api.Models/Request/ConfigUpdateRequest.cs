using System.ComponentModel.DataAnnotations;

namespace HighInfoVoter_Api.Models.Request
{
    public class ConfigUpdateRequest : ConfigAddRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
    }
}
