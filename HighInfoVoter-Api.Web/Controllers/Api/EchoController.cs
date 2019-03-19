using HighInfoVoter_Api.Models.Request;
using HighInfoVoter_Api.Models.Response;
using HighInfoVoter_Api.Services;
using System;
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

        public string Get(int id)
        {
            return "value";
        }

        public HttpResponseMessage Post([FromBody]EchoAddRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    EchoService svc = new EchoService();
                    ItemResponse<int> response = new ItemResponse<int>();
                    response.Item = svc.Insert(model);
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    return Request.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest, ex.Message);
            }
        }
    }
}
