using HighInfoVoter_Api.Models.Response;
using HighInfoVoter_Api.Models.View;
using HighInfoVoter_Api.Services.Interfaces;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HighInfoVoter_Api.Web.Controllers.Api
{
    [RoutePrefix("api/fileupload")]
    public class AwsS3Controller : ApiController
    {
        private IAwsS3Service _awsS3Service;

        public AwsS3Controller(IAwsS3Service awsS3Service)
        {
            _awsS3Service = awsS3Service;
        }

        [Route("all"), HttpPost]
        public HttpResponseMessage uploadAll()
        {
            try
            {
                _awsS3Service.UploadAll();
                return Request.CreateResponse(HttpStatusCode.OK, "Files uploaded.");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("select"), HttpGet]
        public HttpResponseMessage SelectByKey()
        {
            try
            {
                var nvp = this.Request.GetQueryNameValuePairs();
                string key = nvp.Where(nv => nv.Key == "key").Select(nv => nv.Value).FirstOrDefault();
                ItemResponse<SignedUrlView> resp = new ItemResponse<SignedUrlView>();
                resp.Item = _awsS3Service.SelectByKey(key);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
