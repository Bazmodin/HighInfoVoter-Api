using HighInfoVoter_Api.Models.Request;

namespace HighInfoVoter_Api.Models.Domain
{
    public class Account : AccountUpdateRequest
    {
        public string Salt { get; set; }
    }
}
