using HighInfoVoter_Api.Models.Domain;
using HighInfoVoter_Api.Models.Request;
using HighInfoVoter_Api.Models.Response;
using HighInfoVoter_Api.Services.Interfaces;
using System;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace HighInfoVoter_Api.Web.Controllers.Api
{
    [EnableCors("*", "*", "*")]
    [RoutePrefix("api/account")]
    public class AccountController : ApiController
    {
        private IAccountService _accountService;

        public AccountController(IAccountService accountService)
        {
            _accountService = accountService;
        }

        [AllowAnonymous]
        [Route("create"), HttpPost]
        public HttpResponseMessage Create(AccountAddRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    ItemResponse<int> resp = new ItemResponse<int>();
                    resp.Item = _accountService.Create(model);
                    return Request.CreateResponse(HttpStatusCode.OK, resp);
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

        [AllowAnonymous]
        [Route("login"), HttpPost]
        public HttpResponseMessage Login(AccountAddRequest model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool verified = _accountService.VerifyPassword(model);
                    if (verified)
                    {
                        return Request.CreateResponse(HttpStatusCode.OK, "Login successful.");
                    }
                    else
                    {
                        return Request.CreateErrorResponse(HttpStatusCode.BadRequest, "Invalid Credentials");
                    }
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
    }
}
