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
    public class VideoController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly IVideoService videoService;
        private readonly IAuthService _authService;
        public VideoController(IVideoService Videoservice, IWebHostEnvironment env, IAuthService authService)
        {
            this.videoService = Videoservice;
            this._env = env;
            this._authService = authService;
        }

        [Route("GetTwilioToken")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTwilioToken()
        {

            JsonResponse<string> objResult = new JsonResponse<string>();
            try
            {
                HttpContext context = HttpContext;
                var response = await this.videoService.GetTwilioToken(context);
                     objResult.Data = response;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
               
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

       

    }
}