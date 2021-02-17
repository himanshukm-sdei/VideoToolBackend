using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
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
using Services.JsonResponse;
using Services.StaticResources;

namespace VisionAppApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class PractitionerController : ControllerBase
    {
        private readonly IPractitionerService practitionerService;
        private readonly IWebHostEnvironment _env;
        public PractitionerController(IPractitionerService practitionerService, IWebHostEnvironment env)
        {
            this.practitionerService = practitionerService;
            this._env = env;
        }
        [Route("GetProfile")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetProfile(int? userId, Guid? guid)
        {
            JsonResponse<ProfileInfo> objResult = new JsonResponse<ProfileInfo>();

            try
            {
                HttpContext request = HttpContext;
                var profile = await this.practitionerService.GetUserProfile(userId, guid);
                if (profile != null && profile.ProfilePhoto != null)
                {
                    string uploadsFolder = Path.Combine(this._env.WebRootPath, "images");
                    var imagefile = Directory.GetFiles(uploadsFolder, profile.ProfilePhoto);
                    if (imagefile.Length > 0)
                        profile.ProfilePhoto = request.Request.Scheme + "://" + request.Request.Host + request.Request.PathBase + "\\images\\" + profile.ProfilePhoto;
                    else
                        profile.ProfilePhoto = string.Empty;

                }
                if (profile == null)
                {
                    objResult.Data = profile;
                    objResult.Status = StaticResource.NotFoundStatusCode;
                    objResult.Message = StaticResource.NotFoundMessage;
                    return new OkObjectResult(objResult);

                }
                else
                {
                    objResult.Data = profile;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
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

        [Route("CreateSession")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> CreateSession([FromBody]SessionRequest sessionRequest)
        {
            JsonResponse<string> objResult = new JsonResponse<string>();
            try
            {
                HttpContext context = HttpContext;
                if (sessionRequest.CreatedBy != null)
                {
                    sessionRequest.CreatedBy = sessionRequest.CreatedBy;
                }
                else
                {
                    sessionRequest.CreatedBy = sessionRequest.UserId;
                }

                long sessionId = await this.practitionerService.CreateSession(sessionRequest);
                if (sessionId > 0)
                {
                    if (sessionRequest.Tags != null && sessionRequest.Tags.Length > 0)
                        await this.practitionerService.SaveTags(sessionId, sessionRequest.UserId, sessionRequest.Tags);
                    objResult.Data = StaticResource.SessionCreated;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                objResult.Data = ex.Message;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = StaticResource.FailMessage;
            }
            return new OkObjectResult(objResult);
        }

        [Route("UpdateSession")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<IActionResult> UpdateSession([FromBody]SessionRequest sessionRequest)
        {
            JsonResponse<string> objResult = new JsonResponse<string>();
            try
            {
                HttpContext context = HttpContext;
                if (sessionRequest.CreatedBy != null)
                {
                    sessionRequest.CreatedBy = sessionRequest.CreatedBy;
                }
                else
                {
                    sessionRequest.CreatedBy = sessionRequest.UserId;
                }

                long sessionId = await this.practitionerService.UpdateSession(sessionRequest);
                if (sessionId > 0)
                {
                    if (sessionRequest.Tags != null && sessionRequest.Tags.Length > 0)
                        await this.practitionerService.UpdateTags(sessionId, sessionRequest.UserId, sessionRequest.Tags);
                    objResult.Data = "Session updated successfully.";
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                objResult.Data = ex.Message;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = StaticResource.FailMessage;
            }
            return new OkObjectResult(objResult);
        }

        [Route("GetTags")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTags()
        {
            JsonResponse<List<ListVm>> objResult = new JsonResponse<List<ListVm>>();
            try
            {
                List<ListVm> listVms = await this.practitionerService.GetTags();
                if (listVms.Count > 0)
                {
                    objResult.Data = listVms;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
                else
                {
                    objResult.Data = null;
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
        [Route("GetTopics")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTopics()
        {
            JsonResponse<List<ListVm>> objResult = new JsonResponse<List<ListVm>>();
            try
            {
                List<ListVm> listVms = await this.practitionerService.GetTopics();
                if (listVms.Count > 0)
                {
                    objResult.Data = listVms;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
                else
                {
                    objResult.Data = null;
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

        [Route("GetTimezones")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTimezones()
        {
            JsonResponse<List<ListVm>> objResult = new JsonResponse<List<ListVm>>();
            try
            {
                List<ListVm> listVms = await this.practitionerService.GetTimezones();
                if (listVms.Count > 0)
                {
                    objResult.Data = listVms;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
                else
                {
                    objResult.Data = null;
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
        [Route("GetSessionList")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSessionList([FromBody]SessionListRequest sessionListRequest)
        {
            JsonResponse<IEnumerable<CategoryList>> objResult = new JsonResponse<IEnumerable<CategoryList>>();

            try
            {
                HttpContext context = HttpContext;
                IEnumerable<CategoryList> sessionLists = await this.practitionerService.GetSessionList(sessionListRequest, context);
                if (sessionLists != null)
                {

                    objResult.Data = sessionLists;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
                else
                {
                    objResult.Data = null;
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
        [Route("PractitionerRelated")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> PractitionerRelated(int PractitionerId, int UserId)
        {
            JsonResponse<IEnumerable<SessionList>> objResult = new JsonResponse<IEnumerable<SessionList>>();

            try
            {
                HttpContext context = HttpContext;
                IEnumerable<SessionList> sessionLists = await this.practitionerService.PractitionerRelated(PractitionerId, UserId, context);
                if (sessionLists != null)
                {

                    objResult.Data = sessionLists;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
                else
                {
                    objResult.Data = null;
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

        [Route("GetSessionListMember")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSessionListMember([FromBody]SessionListRequest sessionListRequest)
        {
            JsonResponse<IEnumerable<CategoryList>> objResult = new JsonResponse<IEnumerable<CategoryList>>();

            try
            {
                HttpContext context = HttpContext;
                IEnumerable<CategoryList> sessionLists = await this.practitionerService.GetSessionListMember(sessionListRequest, context);
                if (sessionLists != null)
                {

                    objResult.Data = sessionLists;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
                else
                {
                    objResult.Data = null;
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

        [Route("GetFilteredSessionList")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetFilteredSessionList([FromBody]FilteredSessionListRequest sessionListRequest)
        {
            JsonResponse<IEnumerable<SessionList>> objResult = new JsonResponse<IEnumerable<SessionList>>();

            try
            {
                HttpContext context = HttpContext;
                IEnumerable<SessionList> sessionLists = await this.practitionerService.GetFilteredSessionList(sessionListRequest, context);
                if (sessionLists != null)
                {

                    objResult.Data = sessionLists;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
                else
                {
                    objResult.Data = null;
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
        [Route("GetAllSession")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllSession([FromBody]SessionListRequest request)
        {
            JsonResponse<IEnumerable<CategoryList>> objResult = new JsonResponse<IEnumerable<CategoryList>>();
            try
            {
                HttpContext context = HttpContext;
                IEnumerable<CategoryList> sessionLists = await this.practitionerService.GetAllSession(request, context);
                if (sessionLists != null)
                {
                    objResult.Data = sessionLists;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
                else
                {
                    objResult.Data = null;
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
        [Route("GetAllSessionMember")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllSessionMember([FromBody]SessionListRequest request)
        {
            JsonResponse<IEnumerable<CategoryList>> objResult = new JsonResponse<IEnumerable<CategoryList>>();
            try
            {
                HttpContext context = HttpContext;
                IEnumerable<CategoryList> sessionLists = await this.practitionerService.GetAllSessionMember(request, context);
                if (sessionLists != null)
                {
                    objResult.Data = sessionLists;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
                else
                {
                    objResult.Data = null;
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
        [Route("GetAllSessionCountMember")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAllSessionCountMember(int memberId)
        {
            JsonResponse<SesionCounts> objResult = new JsonResponse<SesionCounts>();
            try
            {
                HttpContext context = HttpContext;
                SesionCounts counts = await this.practitionerService.GetAllSessionCountMember(memberId);
                if (counts != null)
                {
                    objResult.Data = counts;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
                else
                {
                    objResult.Data = null;
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
        [Route("SessionDetails")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SessionDetails(Guid guid, int? userId)
        {
            JsonResponse<SessionDetails> objResult = new JsonResponse<SessionDetails>();
            try
            {
                HttpContext context = HttpContext;
                SessionDetails session = new SessionDetails();
                session = await this.practitionerService.SessionDetails(guid, userId, context);
                if (session != null)
                {
                    if (session.ProfilePhoto != null)
                        session.ProfilePhoto = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "\\images\\" + session.ProfilePhoto;
                    objResult.Data = session;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
                else
                {
                    objResult.Data = null;
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

        [Route("SessionBookingDetails")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SessionBookingDetails(Guid guid, int userId)
        {
            JsonResponse<SessionBookingData> objResult = new JsonResponse<SessionBookingData>();
            try
            {
                HttpContext context = HttpContext;
                SessionBookingData result = await this.practitionerService.SessionBookingDetails(guid, userId);

                objResult.Data = result;
                objResult.Status = StaticResource.SuccessStatusCode;
                objResult.Message = StaticResource.SuccessMessage;
                return new OkObjectResult(objResult);

            }
            catch (Exception ex)
            {
                var data = new SessionBookingData();
                data.IsBooked = false;
                HttpContext.RiseError(ex);
                objResult.Data = data;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = StaticResource.FailMessage;
            }
            return new OkObjectResult(objResult);
        }

        [Route("BookSession")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> BookSession(Guid guid, int userId)
        {
            JsonResponse<bool> objResult = new JsonResponse<bool>();
            try
            {
                HttpContext context = HttpContext;
                bool result = await this.practitionerService.BookSession(guid, userId, HttpContext);

                objResult.Data = result;
                objResult.Status = StaticResource.SuccessStatusCode;
                objResult.Message = StaticResource.SuccessMessage;
                return new OkObjectResult(objResult);

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

        [Route("EnableNotification")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> EnableNotification([FromBody]NotificationRequest notification)
        {
            HttpContext context = HttpContext;
            JsonResponse<bool> objResult = new JsonResponse<bool>();
            try
            {
                bool result = await this.practitionerService.EnableNotification(notification, context);
                objResult.Data = result;
                objResult.Status = StaticResource.SuccessStatusCode;
                objResult.Message = StaticResource.SuccessMessage;
                return new OkObjectResult(objResult);

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

        [Route("CancelBookedSession")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CancelBookedSession(int bookedId, int sessionId, int userId)
        {
            HttpContext context = HttpContext;
            JsonResponse<bool> objResult = new JsonResponse<bool>();
            try
            {

                bool successs = await this.practitionerService.CancelBookedSession(bookedId, sessionId, userId, context);
                if (successs)
                {
                    objResult.Data = successs;
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


        [Route("GetPractitionerSessionList")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPractitionerSessionList([FromBody] FilteredSessionListRequest sessionListRequest)
        {
            JsonResponse<IEnumerable<SessionList>> objResult = new JsonResponse<IEnumerable<SessionList>>();

            try
            {
                HttpContext context = HttpContext;
                IEnumerable<SessionList> sessionLists = await this.practitionerService.GetPractitionerSessionList(sessionListRequest, context);
                if (sessionLists != null)
                {

                    objResult.Data = sessionLists;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
                else
                {
                    objResult.Data = null;
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

        [Route("SessionvideoAssign")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> SessionvideoAssign(Guid guid, int videoId, int userId)
        {
            JsonResponse<bool> objResult = new JsonResponse<bool>();
            try
            {
                HttpContext context = HttpContext;
                var result = await this.practitionerService.SessionvideoAssign(guid, videoId, userId);

                objResult.Data = result;
                objResult.Status = StaticResource.SuccessStatusCode;
                objResult.Message = StaticResource.SuccessMessage;
                return new OkObjectResult(objResult);

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


        [Route("CancelSessionByPractitioner")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> CancelSessionByPractitioner([FromBody] SessionBookingRequest bookingDetail)
        {
            HttpContext context = HttpContext;
            JsonResponse<bool> objResult = new JsonResponse<bool>();
            try
            {

                bool successs = await this.practitionerService.CancelSessionByPractitioner(bookingDetail, context);
                if (successs)
                {
                    objResult.Data = successs;
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

        //[Route("GetAllSessionCountPractitioner")]
        //[HttpGet]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        //[ProducesResponseType(StatusCodes.Status201Created)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //public async Task<IActionResult> GetAllSessionCountPractitioner(int practitionerID)
        //{
        //    JsonResponse<SesionCounts> objResult = new JsonResponse<SesionCounts>();
        //    try
        //    {
        //        HttpContext context = HttpContext;
        //        SesionCounts counts = await this.practitionerService.GetAllSessionCountPractitioner(practitionerID);
        //        if (counts != null)
        //        {
        //            objResult.Data = counts;
        //            objResult.Status = StaticResource.SuccessStatusCode;
        //            objResult.Message = StaticResource.SuccessMessage;
        //            return new OkObjectResult(objResult);
        //        }
        //        else
        //        {
        //            objResult.Data = null;
        //            objResult.Status = StaticResource.NotFoundStatusCode;
        //            objResult.Message = StaticResource.NotFoundMessage;
        //            return new OkObjectResult(objResult);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        HttpContext.RiseError(ex);
        //        objResult.Data = null;
        //        objResult.Status = StaticResource.FailStatusCode;
        //        objResult.Message = StaticResource.FailMessage;
        //    }
        //    return new OkObjectResult(objResult);
        //}

    }
}