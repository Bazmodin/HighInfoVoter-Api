using System.ComponentModel.DataAnnotations;

namespace HighInfoVoter_Api.Models.Request
{
    public class EchoUpdateRequest : EchoAddRequest
    {
        [Required]
        public int Id { get; set; }
    }
}
