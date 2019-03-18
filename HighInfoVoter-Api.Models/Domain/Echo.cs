using HighInfoVoter_Api.Models.Request;
using System;

namespace HighInfoVoter_Api.Models.Domain
{
    public class Echo : EchoUpdateRequest
    {
        public DateTime CreatedDate { get; set; }
        public DateTime ModifiedDate { get; set; }
    }
}
