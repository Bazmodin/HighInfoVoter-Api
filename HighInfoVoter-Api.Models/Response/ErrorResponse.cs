using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighInfoVoter_Api.Models.Response
{
    public class ErrorResponse : Response
    {
        public ErrorResponse()
        {
            this.IsSuccessful = false;
        }
    }
}
