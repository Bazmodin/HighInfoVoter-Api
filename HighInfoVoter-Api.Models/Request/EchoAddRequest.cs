using System.ComponentModel.DataAnnotations;

namespace HighInfoVoter_Api.Models.Request
{
    public class EchoAddRequest
    {
        [Required]
        public string Echo { get; set; }
    }
}
