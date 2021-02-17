using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Services;
using Application.Dtos;
using Domain.Entities;
using ElmahCore;
using Infrastructure.Implemenatations.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Services.JsonResponse;
using Services.StaticResources;
using static Common.Common.CommonUtility;

namespace VisionAppApi.Controllers
{
    //[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class JobsController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IJobsService _jobsService;
        private readonly IAuthService _authService;
        public JobsController(IJobsService JobsService, IWebHostEnvironment env, IAuthService authService)
        {
            this._jobsService = JobsService;
            this._env = env;
            this._authService = authService;
        }

        [Route("SendInvitationEmails")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SendInvitationEmails()
        {

            JsonResponse<IEnumerable<CompanyList>> objResult = new JsonResponse<IEnumerable<CompanyList>>();
            try
            {
                HttpContext context = HttpContext;
                bool response = await this._jobsService.SendInvitationEmails(context);
                if (response == true)
                {
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
                else
                {
                    objResult.Status = StaticResource.NotFoundStatusCode;
                    objResult.Message = StaticResource.NotFoundMessage;
                    return new OkObjectResult(objResult);
                }
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                objResult.Data = null;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = StaticResource.FailMessage;
            }
            return new OkObjectResult(objResult);
        }

        [Route("SendBookedsessionEmailBeforeoneday")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SendBookedsessionEmailBeforeoneday()
        {

            JsonResponse<IEnumerable<CompanyList>> objResult = new JsonResponse<IEnumerable<CompanyList>>();
            try
            {
                HttpContext context = HttpContext;
                bool response = await this._jobsService.SendBookedsessionEmailBeforeoneday(context);
                if (response == true)
                {
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
                else
                {
                    objResult.Status = StaticResource.NotFoundStatusCode;
                    objResult.Message = StaticResource.NotFoundMessage;
                    return new OkObjectResult(objResult);
                }
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                objResult.Data = null;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = StaticResource.FailMessage;
            }
            return new OkObjectResult(objResult);
        }

        [Route("SendBookedsessionEmailBeforeoneHour")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SendBookedsessionEmailBeforeoneHour()
        {

            JsonResponse<IEnumerable<CompanyList>> objResult = new JsonResponse<IEnumerable<CompanyList>>();
            try
            {
                HttpContext context = HttpContext;
                bool response = await this._jobsService.SendBookedsessionEmailBeforeoneHour(context);
                if (response == true)
                {
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
                else
                {
                    objResult.Status = StaticResource.NotFoundStatusCode;
                    objResult.Message = StaticResource.NotFoundMessage;
                    return new OkObjectResult(objResult);
                }
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                objResult.Data = null;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = StaticResource.FailMessage;
            }
            return new OkObjectResult(objResult);
        }

        [Route("SendBookedAvailabilityEmailBeforeHalfHour")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SendBookedAvailabilityEmailBeforeoneHour()
        {

            JsonResponse<bool> objResult = new JsonResponse<bool>();
            try
            {
                HttpContext context = HttpContext;
                bool response = await this._jobsService.SendBookedAvailabilityEmailBeforeHalfHour(context);
                if (response)
                {
                    objResult.Data = response;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                objResult.Data = false;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = StaticResource.FailMessage;
            }
            return new OkObjectResult(objResult);
        }
        
        
        [Route("SendBookedAvailabilityEmailBeforeFiveMinutes")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SendBookedAvailabilityEmailBeforeFiveMinutes()
        {

            JsonResponse<bool> objResult = new JsonResponse<bool>();
            try
            {
                HttpContext context = HttpContext;
                bool response = await this._jobsService.SendBookedAvailabilityEmailBeforeFiveMinutes(context);
                if (response)
                {
                    objResult.Data = response;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                objResult.Data = false;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = StaticResource.FailMessage;
            }
            return new OkObjectResult(objResult);
        }

    }
}