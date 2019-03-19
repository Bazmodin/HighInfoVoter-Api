using HighInfoVoter_Api.Models.Domain;
using HighInfoVoter_Api.Models.Request;
using HighInfoVoter_Api.Models.Response;
using HighInfoVoter_Api.Services.Interfaces;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HighInfoVoter_Api.Web.Controllers.Api
{
    [RoutePrefix("api/config")]
    public class ConfigController : ApiController
    {
        private IConfigService _configService;

        public ConfigController(IConfigService configService)
        {
            _configService = configService;
        }

        [Route("create"), HttpPost]
        public HttpResponseMessage Create(ConfigAddRequest model)
        {
            try
            {
                if(ModelState.IsValid)
                {
                    ItemResponse<int> response = new ItemResponse<int>();
                    response.Item = _configService.Create(model);
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("getall"), HttpGet]
        public HttpResponseMessage GetAll()
        {
            ItemsResponse<Config> response = new ItemsResponse<Config>();
            response.Items = _configService.GetAll();
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }

        
        [Route("{id:int}"), HttpGet]
        public HttpResponseMessage GetById(int id)
        {
            try
            {
                ItemResponse<Config> response = new ItemResponse<Config>();
                response.Item = _configService.GetById(id);
                return Request.CreateResponse(HttpStatusCode.OK, response);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("getbykey"), HttpGet]
        public HttpResponseMessage GetByKey()
        {
            try
            {
                var nvp = this.Request.GetQueryNameValuePairs();
                string key = nvp.Where(nv => nv.Key == "key").Select(nv => nv.Value).FirstOrDefault();
                ItemResponse<Config> resp = new ItemResponse<Config>();
                resp.Item = _configService.GetByKey(key);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("{id:int}"), HttpPut]
        public HttpResponseMessage Update(ConfigUpdateRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    SuccessResponse response = new SuccessResponse();
                    _configService.Update(model);
                    return Request.CreateResponse(HttpStatusCode.OK, response);
                }
                else
                {
                    return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("{id:int}"), HttpDelete]
        public HttpResponseMessage Delete(int id)
        {
            _configService.Delete(id);
            SuccessResponse response = new SuccessResponse();
            return Request.CreateResponse(HttpStatusCode.OK, response);
        }
    }
}
