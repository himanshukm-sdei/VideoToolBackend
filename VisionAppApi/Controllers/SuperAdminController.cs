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
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class SuperAdminController : Controller
    {
        private readonly IWebHostEnvironment _env;
        private readonly ISuperAdminService _superAdminService;
        private readonly IAuthService _authService;
        public SuperAdminController(ISuperAdminService superAdminService, IWebHostEnvironment env, IAuthService authService)
        {
            this._superAdminService = superAdminService;
            this._env = env;
            this._authService = authService;
        }

        [Route("SaveCompanyDetails")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SaveCompanyDetails([FromBody]CompanyDetails companyDetails)
        {
            JsonResponse<string> objResult = new JsonResponse<string>();
            try
            {
                HttpContext context = HttpContext;
                bool succes = await this._superAdminService.SaveCompanyDetails(companyDetails, context);
                if (succes)
                {
                    objResult.Data = StaticResource.SavedSuccessfulMessage;
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

        [Route("UpdateCompanyDetails")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCompanyDetails([FromBody]CompanyDetails companyDetails)
        {
            JsonResponse<string> objResult = new JsonResponse<string>();
            try
            {
                bool succes = await this._superAdminService.UpdateCompanyDetails(companyDetails);
                if (succes)
                {
                    objResult.Data = StaticResource.SavedSuccessfulMessage;
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

        [Route("GetCompanyList")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCompanyList()
        {

            JsonResponse<IEnumerable<CompanyList>> objResult = new JsonResponse<IEnumerable<CompanyList>>();
            try
            {
                IEnumerable<CompanyList> lists;
                lists = await this._superAdminService.GetCompanyList();
                if (lists != null && lists.Count() > 0)
                {
                    objResult.Data = lists;
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

        [Route("GetCompanyMembersList")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCompanyMembersList(int companyId)
        {

            JsonResponse<IEnumerable<CompanyMembersList>> objResult = new JsonResponse<IEnumerable<CompanyMembersList>>();
            try
            {
                IEnumerable<CompanyMembersList> lists;
                lists = await this._superAdminService.GetCompanyMembersList(companyId);
                if (lists != null && lists.Count() > 0)
                {
                    objResult.Data = lists;
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

        [Route("GetPractitionersList")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetPractitionersList([FromBody]PractitionersRequest practitioners)
        {

            JsonResponse<IEnumerable<PractitionersList>> objResult = new JsonResponse<IEnumerable<PractitionersList>>();
            try
            {
                IEnumerable<PractitionersList> lists;
                lists = await this._superAdminService.GetPractitionersList(practitioners);
                if (lists != null && lists.Count() > 0)
                {
                    objResult.Data = lists;
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

        [Route("GetAllSessionsList")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAllSessionsList([FromBody]CompanySessionsRequest request)
        {
            JsonResponse<IEnumerable<CompanySessionsList>> objResult = new JsonResponse<IEnumerable<CompanySessionsList>>();
            try
            {
                HttpContext context = HttpContext;
                IEnumerable<CompanySessionsList> sessionLists = await this._superAdminService.GetAllSessionsList(request, context);
                if (sessionLists.Count() > 0 && sessionLists != null)
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
                objResult.Message = "Exception occured";
            }
            return new OkObjectResult(objResult);
        }

        [Route("GetActiveSessionTypes")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetActiveSessionTypes()
        {
            JsonResponse<IEnumerable<SessionsTypes>> objResult = new JsonResponse<IEnumerable<SessionsTypes>>();
            try
            {
                IEnumerable<SessionsTypes> sessionTypes = await this._superAdminService.GetActiveSessionTypes();
                if (sessionTypes.Count() > 0 && sessionTypes != null)
                {
                    objResult.Data = sessionTypes;
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
                objResult.Message = "Exception occured";
            }
            return new OkObjectResult(objResult);
        }

        [Route("GetSessionsTypes")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSessionsTypes([FromBody]SessionsTypes request)
        {
            JsonResponse<IEnumerable<SessionsTypes>> objResult = new JsonResponse<IEnumerable<SessionsTypes>>();
            try
            {
                IEnumerable<SessionsTypes> sessionTypes = await this._superAdminService.GetSessionsTypes(request);
                if (sessionTypes.Count() > 0 && sessionTypes != null)
                {
                    objResult.Data = sessionTypes;
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
                objResult.Message = "Exception occured";
            }
            return new OkObjectResult(objResult);
        }

        [Route("AddUpdateSessionType")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddUpdateSessionType([FromBody] SessionTypeData type)
        {
            JsonResponse<bool> objResult = new JsonResponse<bool>();
            try
            {
                bool success = await this._superAdminService.AddUpdateSessionType(type);
                if (success)
                {
                    objResult.Data = success;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SavedSuccessfulMessage;
                }
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                objResult.Data = false;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = "Exception occured";
            }
            return new OkObjectResult(objResult);
        }

        [Route("ChangeSessionsTypesStatus")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangeSessionsTypesStatus([FromBody] SessionsTypes type)
        {
            JsonResponse<bool> objResult = new JsonResponse<bool>();
            try
            {
                bool success = await this._superAdminService.ChangeSessionsTypesStatus(type);
                if (success)
                {
                    objResult.Data = success;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                }
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                objResult.Data = false;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = "Exception occured";
            }
            return new OkObjectResult(objResult);
        }

        [Route("ChangeSessionsIsAcceptedStatus")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangeSessionsIsAcceptedStatus([FromBody] sessionstatusData request)
        {
            JsonResponse<bool> objResult = new JsonResponse<bool>();
            try
            {
                bool success = await this._superAdminService.ChangeSessionsIsAcceptedStatus(request);
                if (success)
                {
                    objResult.Data = success;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                }
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                objResult.Data = false;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = "Exception occured";
            }
            return new OkObjectResult(objResult);
        }

        [Route("ChangeDepartmentStatus")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangeDepartmentStatus([FromBody] DepartmentsList department)
        {
            JsonResponse<bool> objResult = new JsonResponse<bool>();
            try
            {
                bool success = await this._superAdminService.ChangeDepartmentStatus(department);
                if (success)
                {
                    objResult.Data = success;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                }
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                objResult.Data = false;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = "Exception occured";
            }
            return new OkObjectResult(objResult);
        }
        [Route("ChangeTopicStatus")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangeTopicStatus([FromBody] TopicList topic)
        {
            JsonResponse<bool> objResult = new JsonResponse<bool>();
            try
            {
                bool success = await this._superAdminService.ChangeTopicStatus(topic);
                if (success)
                {
                    objResult.Data = success;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
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

        [Route("GetSessionTypeById")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSessionTypeById(int typeId)
        {
            JsonResponse<SessionsTypes> objResult = new JsonResponse<SessionsTypes>();
            try
            {

                SessionsTypes sessionType = await this._superAdminService.GetSessionTypeById(typeId);
                if (sessionType != null)
                {
                    objResult.Data = sessionType;
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
                objResult.Message = "Exception occured";
            }
            return new OkObjectResult(objResult);
        }

        [Route("DeleteTopicById")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteTopicById(int Id)
        {
            JsonResponse<bool> objResult = new JsonResponse<bool>();
            try
            {
                bool success = await this._superAdminService.DeleteTopicById(Id);
                if (success)
                {
                    objResult.Data = success;
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
        [Route("GetTopicById")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetTopicById(int Id)
        {
            JsonResponse<TopicList> objResult = new JsonResponse<TopicList>();
            try
            {
                TopicList list = await this._superAdminService.GetTopicById(Id);
                if (list != null)
                {
                    objResult.Data = list;
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

        [Route("DeleteSessionBySessionId")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteSessionBySessionId(int sessionId)
        {
            JsonResponse<bool> objResult = new JsonResponse<bool>();
            try
            {

                bool successs = await this._superAdminService.DeleteSessionBySessionId(sessionId);
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
                objResult.Message = "Exception occured";
            }
            return new OkObjectResult(objResult);
        }

        [Route("DeleteSessionType")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteSessionType(int typeId)
        {
            JsonResponse<bool> objResult = new JsonResponse<bool>();
            try
            {

                bool successs = await this._superAdminService.DeleteSessionType(typeId);
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
                objResult.Message = "Exception occured";
            }
            return new OkObjectResult(objResult);
        }

        [Route("GetDepartmentsList")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDepartmentsList([FromBody]DepartmentsList request)
        {
            JsonResponse<IEnumerable<DepartmentsList>> objResult = new JsonResponse<IEnumerable<DepartmentsList>>();
            try
            {
                IEnumerable<DepartmentsList> departmentsList = await this._superAdminService.GetDepartmentsList(request);
                if (departmentsList.Count() > 0 && departmentsList != null)
                {
                    objResult.Data = departmentsList;
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
                objResult.Message = "Exception occured";
            }
            return new OkObjectResult(objResult);
        }
        [Route("AddUpdateDepartment")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddUpdateDepartment([FromBody] DepartmentData department)
        {
            JsonResponse<bool> objResult = new JsonResponse<bool>();
            try
            {
                bool success = await this._superAdminService.AddUpdateDepartment(department);
                if (success)
                {
                    objResult.Data = success;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SavedSuccessfulMessage;
                }
                else
                {
                    objResult.Data = false;
                    objResult.Status = StaticResource.DuplicateStatusCode;
                    objResult.Message = StaticResource.DepartmentCodeExist;
                }
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                objResult.Data = false;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = "Exception occured";
            }
            return new OkObjectResult(objResult);
        }
        [Route("GetDepartmentById")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDepartmentById(int departmentId)
        {
            JsonResponse<DepartmentData> objResult = new JsonResponse<DepartmentData>();
            try
            {

                DepartmentData department = await this._superAdminService.GetDepartmentById(departmentId);
                if (department != null)
                {
                    objResult.Data = department;
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
                objResult.Message = "Exception occured";
            }
            return new OkObjectResult(objResult);
        }

        [Route("DeleteDepartmentById")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteDepartmentById(int departmentId)
        {
            JsonResponse<bool> objResult = new JsonResponse<bool>();
            try
            {

                bool successs = await this._superAdminService.DeleteDepartmentById(departmentId);
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
        [Route("AddorUpdateTopics")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> AddorUpdateTopics([FromForm] TopicRequestdata request)
        {
            JsonResponse<bool> objResult = new JsonResponse<bool>();
            try
            {
                bool success = await this._superAdminService.AddorUpdateTopics(request);
                if (success)
                {
                    objResult.Data = success;
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
        [Route("GetTopicList")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetTopicList([FromBody]TopicRequest request)
        {
            JsonResponse<IEnumerable<TopicList>> objResult = new JsonResponse<IEnumerable<TopicList>>();
            try
            {
                IEnumerable<TopicList> lists = new List<TopicList>();
                lists = await this._superAdminService.GetTopicList(request);
                if (lists != null && lists.Count() > 0)
                {
                    objResult.Data = lists;
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


        [AllowAnonymous]
        [Route("GetAttendeeList")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAttendeeList([FromBody] CompanySessionsRequest request)
        {
            JsonResponse<IEnumerable<CompanySessionsList>> objResult = new JsonResponse<IEnumerable<CompanySessionsList>>();
            try
            {
                HttpContext context = HttpContext;
                IEnumerable<CompanySessionsList> sessionLists = await this._superAdminService.GetAttendeeList(request, context);
                if (sessionLists.Count() > 0 && sessionLists != null)
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
                objResult.Message = "Exception occured";
            }
            return new OkObjectResult(objResult);
        }


        [Route("GetvideosList")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetvideosList([FromBody]MasterVideosRequest request)
        {
            JsonResponse<IEnumerable<Application.Dtos.MasterVideos>> objResult = new JsonResponse<IEnumerable<Application.Dtos.MasterVideos>>();
            try
            {
                IEnumerable<Application.Dtos.MasterVideos> list = await this._superAdminService.GetvideosList(request);
                if (list.Count() > 0 && list != null)
                {
                    objResult.Data = list;
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
                objResult.Message = "Exception occured";
            }
            return new OkObjectResult(objResult);
        }
    }
}