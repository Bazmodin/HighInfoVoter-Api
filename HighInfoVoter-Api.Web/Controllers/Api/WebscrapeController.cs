using HighInfoVoter_Api.Models.Domain;
using HighInfoVoter_Api.Models.Response;
using HighInfoVoter_Api.Services.Interfaces;
using System;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace HighInfoVoter_Api.Web.Controllers.Api
{
    [RoutePrefix("api/webscrape")]
    public class WebscrapeController : ApiController
    {
        private IWebscrapeService _webscrapeService;

        public WebscrapeController(IWebscrapeService webscrapeService)
        {
            _webscrapeService = webscrapeService;
        }

        [Route("portrait"), HttpGet]
        public HttpResponseMessage ScrapePortrait()
        {
            try
            {
                var nvp = this.Request.GetQueryNameValuePairs();
                string name = nvp.Where(nv => nv.Key == "name").Select(nv => nv.Value).FirstOrDefault();
                ItemResponse<Portrait> resp = new ItemResponse<Portrait>();
                resp.Item = _webscrapeService.Webscrape(name);
                return Request.CreateResponse(HttpStatusCode.OK, resp);
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

        [Route("scrapeall"), HttpPost]
        public HttpResponseMessage ScrapeAll()
        {
            Debug.WriteLine("ENTER CONTROLLER");
            try
            {
                _webscrapeService.ScrapeAllBPPortraits();
                return Request.CreateResponse(HttpStatusCode.OK, "Yay");
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
