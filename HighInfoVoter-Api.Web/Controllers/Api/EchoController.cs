using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HighInfoVoter_Api.Web.Controllers.Api
{
    [RoutePrefix("api/echo")]
    public class EchoController : ApiController
    {
        [Route("hello"), HttpGet]
        public HttpResponseMessage Hello()
        {
            return Request.CreateResponse(HttpStatusCode.OK, "Hello");
        }
    }
}
