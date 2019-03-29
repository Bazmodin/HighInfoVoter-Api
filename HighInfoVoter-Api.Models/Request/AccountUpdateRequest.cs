using System.ComponentModel.DataAnnotations;

namespace HighInfoVoter_Api.Models.Request
{
    public class AccountUpdateRequest : AccountAddRequest
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
    }
}
