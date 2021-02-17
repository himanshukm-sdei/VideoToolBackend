using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.AccessControl;
using System.Security.Claims;
using System.Threading.Tasks;
using Application.Abstractions.Services;
using Application.Dtos;
using Common.Common;
using Domain.Entities;
using ElmahCore;
using Infrastructure.Implemenatations.Context;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Services.JsonResponse;
using Services.StaticResources;
using static Common.Common.CommonUtility;
using static Common.Common.CommonUtility.Enums;

namespace VisionApp.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService authService;
        private readonly IWebHostEnvironment _env;
        public AuthController(IAuthService authService, IWebHostEnvironment env)
        {
            this.authService = authService;
            this._env = env;
        }

        [Route("LogIn")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> LogIn([FromBody] User user)
        {
            JsonResponse<User> objResult = new JsonResponse<User>();
            if (user != null)
            {

                try
                {
                    var _user = await this.authService.Authenticate(user.UserName, user.UserPassword);

                    switch (_user.Status)
                    {
                        case 1:
                            objResult.Data = _user;
                            objResult.Status = StaticResource.SuccessStatusCode;
                            objResult.Message = StaticResource.SuccessMessage;
                            break;
                        case 0:
                            objResult.Data = null;
                            objResult.Status = StaticResource.NotFoundStatusCode;
                            objResult.Message = StaticResource.InvalidUserNamePass;
                            break;
                        case 2:
                            objResult.Data = null;
                            objResult.Status = StaticResource.SuccessStatusCode;
                            objResult.Message = StaticResource.UserInactive;
                            break;
                        case 3:
                            objResult.Data = null;
                            objResult.Status = StaticResource.SuccessStatusCode;
                            objResult.Message = StaticResource.DeactivateMessage;
                            break;
                        case 4:
                            objResult.Data = null;
                            objResult.Status = StaticResource.SuccessStatusCode;
                            objResult.Message = StaticResource.PasswordExpired;
                            break;
                        case 10:
                            objResult.Data = null;
                            objResult.Status = StaticResource.NotFoundStatusCode;
                            objResult.Message = StaticResource.UserNotExist;
                            break;
                        default:
                            objResult.Data = null;
                            objResult.Status = StaticResource.NotFoundStatusCode;
                            objResult.Message = StaticResource.UserNotExist;
                            break;
                    }
                    return new OkObjectResult(objResult);
                }
                catch (Exception ex)
                {
                    HttpContext.RiseError(ex);
                    objResult.Data = null;
                    objResult.Status = StaticResource.FailStatusCode;
                    objResult.Message = StaticResource.FailMessage;
                }
            }
            else
            {
                objResult.Data = null;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = StaticResource.InvalidMessage;
            }
            return new OkObjectResult(objResult);
        }

        [Route("SignUp")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> SignUp([FromForm] SignupRequest signup)
        {
            JsonResponse<User> objResult = new JsonResponse<User>();
            if (signup != null)
            {


                try
                {
                    HttpContext context = HttpContext;
                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    string fileName = null;
                    bool matched = await this.authService.MatchSecretKey(signup.SecretKey);
                    if (!matched)
                    {
                        objResult.Data = null;
                        objResult.Status = StaticResource.UnauthorizedStatusCode;
                        objResult.Message = StaticResource.UnauthorizedMessage;
                        return new OkObjectResult(objResult);
                    }

                    if (signup.ProfileImage != null)
                    {
                        if (string.IsNullOrWhiteSpace(this._env.WebRootPath))
                        {
                            this._env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                        }
                        string uploadsFolder = Path.Combine(this._env.WebRootPath, "images");
                        fileName = await this.authService.UploadedFile(signup.ProfileImage, uploadsFolder);
                    }
                    var user = await this.authService.CreateUser(signup, fileName, Convert.ToInt32(RolesEnum.Practitioner), context);

                    switch (user.Status)
                    {
                        case 1:
                            objResult.Data = null;
                            objResult.Status = StaticResource.SuccessStatusCode;
                            objResult.Message = StaticResource.PractionerSuccessMessage;
                            break;
                        case 5:
                            objResult.Data = null;
                            objResult.Status = StaticResource.DuplicateStatusCode;
                            objResult.Message = StaticResource.EmailExist;
                            break;
                        case 6:
                            objResult.Data = null;
                            objResult.Status = StaticResource.DuplicateStatusCode;
                            objResult.Message = StaticResource.UsernameExist;
                            break;
                        default:
                            objResult.Data = null;
                            objResult.Status = StaticResource.NotFoundStatusCode;
                            objResult.Message = StaticResource.NotFoundMessage;
                            break;

                    }
                }
                catch (Exception ex)
                {
                    HttpContext.RiseError(ex);
                    objResult.Data = null;
                    objResult.Status = StaticResource.FailStatusCode;
                    objResult.Message = StaticResource.FailMessage;
                }
            }
            else
            {
                objResult.Data = null;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = StaticResource.InvalidMessage;
            }
            return new OkObjectResult(objResult);

        }

        [Route("CompanyMemberSignup")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> CompanyMemberSignup([FromForm] CompanyMemberSignup signup)
        {
            JsonResponse<User> objResult = new JsonResponse<User>();
            try
            {
                HttpContext context = HttpContext;
                if (!ModelState.IsValid)
                {
                    return BadRequest(ModelState);
                }
                bool matched = await this.authService.MatchSecretKey(signup.SecretKey);
                if (!matched)
                {
                    objResult.Data = null;
                    objResult.Status = StaticResource.UnauthorizedStatusCode;
                    objResult.Message = StaticResource.UnauthorizedMessage;
                    return new OkObjectResult(objResult);
                }
                var user = await this.authService.CompanyMemberSignup(signup, Convert.ToInt32(RolesEnum.Member), context);

                switch (user.Status)
                {
                    case 1:
                        objResult.Data = null;
                        objResult.Status = StaticResource.SuccessStatusCode;
                        objResult.Message = StaticResource.SuccessMessage;
                        break;
                    case 5:
                        objResult.Data = null;
                        objResult.Status = StaticResource.DuplicateStatusCode;
                        objResult.Message = StaticResource.EmailExist;
                        break;
                    case 6:
                        objResult.Data = null;
                        objResult.Status = StaticResource.DuplicateStatusCode;
                        objResult.Message = StaticResource.UsernameExist;
                        break;
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

        [Route("MemberSignUp")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> MemberSignUp([FromForm] MemberSignupRequest signup)
        {
            JsonResponse<User> objResult = new JsonResponse<User>();
            if (signup != null)
            {
                try
                {
                    HttpContext context = HttpContext;

                    if (!ModelState.IsValid)
                    {
                        return BadRequest(ModelState);
                    }
                    string fileName = null;
                    bool matched = await this.authService.MatchSecretKey(signup.SecretKey);
                    if (!matched)
                    {
                        objResult.Data = null;
                        objResult.Status = StaticResource.UnauthorizedStatusCode;
                        objResult.Message = StaticResource.UnauthorizedMessage;
                        return new OkObjectResult(objResult);
                    }

                    if (signup.ProfileImage != null)
                    {
                        if (string.IsNullOrWhiteSpace(this._env.WebRootPath))
                        {
                            this._env.WebRootPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot");
                        }
                        string uploadsFolder = Path.Combine(this._env.WebRootPath, "images");
                        fileName = await this.authService.UploadedFile(signup.ProfileImage, uploadsFolder);
                    }
                    var user = await this.authService.CreateMemberUser(signup, fileName, Convert.ToInt32(RolesEnum.Member), context);

                    switch (user.Status)
                    {
                        case 1:
                            if (signup.PlanId != null)
                            {
                                bool saved = await this.authService.SaveMemberShipPlan(signup.PlanId, user.UserId);
                            }
                            objResult.Data = null;
                            objResult.Status = StaticResource.SuccessStatusCode;
                            objResult.Message = StaticResource.SuccessMessage;
                            break;
                        case 5:
                            objResult.Data = null;
                            objResult.Status = StaticResource.DuplicateStatusCode;
                            objResult.Message = StaticResource.EmailExist;
                            break;
                        case 6:
                            objResult.Data = null;
                            objResult.Status = StaticResource.DuplicateStatusCode;
                            objResult.Message = StaticResource.UsernameExist;
                            break;
                        default:
                            objResult.Data = null;
                            objResult.Status = StaticResource.NotFoundStatusCode;
                            objResult.Message = StaticResource.NotFoundMessage;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    HttpContext.RiseError(ex);
                    objResult.Data = null;
                    objResult.Status = StaticResource.FailStatusCode;
                    objResult.Message = StaticResource.FailMessage;
                }
            }
            else
            {
                objResult.Data = null;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = StaticResource.InvalidMessage;
            }
            return new OkObjectResult(objResult);
        }

        [Route("EmailExist")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> EmailExist(string email, bool isEmail)
        {
            JsonResponse<string> objResult = new JsonResponse<string>();
            try
            {
                if (isEmail)
                {
                    bool exist = await this.authService.CheckEmailExist(email);
                    if (exist)
                    {
                        objResult.Data = "Email Already Exist";
                        objResult.Status = StaticResource.duplicateNameCode;
                        objResult.Message = StaticResource.duplicateNameMessage;
                    }
                    else
                    {
                        objResult.Data = "Email Not Exist";
                        objResult.Status = StaticResource.SuccessStatusCode;
                        objResult.Message = StaticResource.SuccessMessage;
                    }
                    return new OkObjectResult(objResult);
                }
                else
                {
                    bool exist = await this.authService.CheckUserNameExist(email);
                    if (exist)
                    {
                        objResult.Data = "Username Already Exist";
                        objResult.Status = StaticResource.duplicateNameCode;
                        objResult.Message = StaticResource.duplicateNameMessage;
                    }
                    else
                    {
                        objResult.Data = "Username Not Exist";
                        objResult.Status = StaticResource.SuccessStatusCode;
                        objResult.Message = StaticResource.SuccessMessage;
                    }
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
        [Route("GetDepartmentDetails")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDepartmentDetails(int userId, int sessionId)
        {
            JsonResponse<IEnumerable<DepartmentDetails>> objResult = new JsonResponse<IEnumerable<DepartmentDetails>>();
            try
            {
                IEnumerable<DepartmentDetails> list = await this.authService.GetDepartmentDetails(userId, sessionId);
                if (list.Count() > 0 || list != null)
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
                objResult.Message = StaticResource.FailMessage;
            }
            return new OkObjectResult(objResult);
        }
        [Authorize]
        [Route("AuthorizeRole")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult AuthorizeRole(int UserId, int RoleId)
        {
            try
            {
                var RequestedRole = Enum.GetName(typeof(RolesEnum), RoleId);
                var userIdentity = (ClaimsIdentity)User.Identity;
                var claims = userIdentity.Claims;
                var roles = claims.Where(c => c.Type == ClaimTypes.Role).FirstOrDefault();
                var id = claims.Where(c => c.Type == "UserId").FirstOrDefault();
                if (roles.Value == RequestedRole && UserId == Convert.ToInt32(id.Value)) 
                    return Ok(true);
                else
                    return Ok(false);
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }


        [Route("GetMemberDetails")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMemberDetails(Guid TokenGuid)
        {
            JsonResponse<BulkCreatedUsers> objResult = new JsonResponse<BulkCreatedUsers>();
            try
            {
                BulkCreatedUsers bulkCreated = await this.authService.GetMemberDetails(TokenGuid);
                if (bulkCreated != null)
                {
                    objResult.Data = bulkCreated;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
                else
                {
                    objResult.Data = null;
                    objResult.Status = StaticResource.FailStatusCode;
                    objResult.Message = StaticResource.FailMessage;
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

        [Route("GetPlans")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetPlans(int? role)
        {
            JsonResponse<List<MasterPlan>> objResult = new JsonResponse<List<MasterPlan>>();
            try
            {
                List<MasterPlan> masterPlans = await this.authService.GetPlans(role);
                if (masterPlans.Count > 0)
                {
                    objResult.Data = masterPlans;
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


        #region Reset Password
        /// <summary>
        /// API to send reset password email link
        /// </summary>
        /// <param name="objModel"></param>
        /// <returns></returns>
        [Route("SendResetPasswordEmail")]
        [HttpPost]
        public async Task<IActionResult> SendResetPasswordEmail([FromBody] ForgetPasswordRequestModel objModel)
        {
            JsonResponse<User> objResultList = new JsonResponse<User>();
            if (objModel != null)
            {
                try
                {
                    bool matched = await this.authService.MatchSecretKey(objModel.SecretKey);
                    if (!matched)
                    {
                        objResultList.Data = null;
                        objResultList.Status = StaticResource.UnauthorizedStatusCode;
                        objResultList.Message = StaticResource.UnauthorizedMessage;
                        return new OkObjectResult(objResultList);
                    }

                    User response = await this.authService.ResetPassword(objModel);

                    switch (response.Status)
                    {
                        case 1:
                            objResultList.Data = null;
                            objResultList.Status = StaticResource.SuccessStatusCode;
                            objResultList.Message = StaticResource.EmailSent;
                            break;
                        case 0:
                            objResultList.Data = null;
                            objResultList.Status = StaticResource.NotFoundStatusCode;
                            objResultList.Message = StaticResource.EmailNotExist;
                            break;
                        default:
                            objResultList.Data = null;
                            objResultList.Status = StaticResource.NotFoundStatusCode;
                            objResultList.Message = StaticResource.NotFoundMessage;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    HttpContext.RiseError(ex);
                    objResultList.Data = null;
                    objResultList.Status = StaticResource.FailStatusCode;
                    objResultList.Message = StaticResource.FailMessage;
                    throw;
                }
            }
            else
            {
                objResultList.Data = null;
                objResultList.Status = StaticResource.FailStatusCode;
                objResultList.Message = StaticResource.FailMessage;
            }

            return new OkObjectResult(objResultList);
        }




        /// <summary>
        /// Reset password from reset link sent to Email Address
        /// </summary>
        /// <param name="objModel"></param>
        /// <returns></returns>
        [Route("GenerateNewPassword")]
        [HttpPost]
        public async Task<IActionResult> GenerateNewPassword([FromBody] ResetPasswordRequestModel objModel)
        {
            JsonResponse<User> objResultList = new JsonResponse<User>();
            if (objModel != null)
            {
                try
                {
                    User response = await this.authService.ResetPasswordFromLink(objModel);
                    switch (response.Status)
                    {
                        case 1:
                            objResultList.Data = null;
                            objResultList.Status = StaticResource.SuccessStatusCode;
                            objResultList.Message = StaticResource.PasswordUpdated;
                            break;
                        case 0:
                            objResultList.Data = null;
                            objResultList.Status = StaticResource.NotFoundStatusCode;
                            objResultList.Message = StaticResource.LinkExpired;
                            break;
                        case 5:
                            objResultList.Data = null;
                            objResultList.Status = StaticResource.NotFoundStatusCode;
                            objResultList.Message = StaticResource.EmailNotExist;
                            break;
                        default:
                            objResultList.Data = null;
                            objResultList.Status = StaticResource.NotFoundStatusCode;
                            objResultList.Message = StaticResource.LinkExpired;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    HttpContext.RiseError(ex);
                    objResultList.Data = null;
                    objResultList.Status = StaticResource.FailStatusCode;
                    objResultList.Message = StaticResource.FailMessage;
                    throw;
                }
            }
            else
            {
                objResultList.Data = null;
                objResultList.Status = StaticResource.FailStatusCode;
                objResultList.Message = StaticResource.FailMessage;
            }

            return new OkObjectResult(objResultList);
        }



        /// <summary>
        /// Reset password from reset link sent to Email Address
        /// </summary>
        /// <param name="objModel"></param>
        /// <returns></returns>
        [Route("CheckResetPasswordLinkStatus")]
        [HttpPost]
        public async Task<IActionResult> CheckResetPasswordLinkStatus([FromBody] ResetPasswordRequestModel objModel)
        {
            JsonResponse<User> objResultList = new JsonResponse<User>();
            if (objModel != null)
            {
                try
                {
                    User response = await this.authService.CheckResetPasswordLinkStatus(objModel);
                    switch (response.Status)
                    {
                        case 0:
                            objResultList.Data = null;
                            objResultList.Status = StaticResource.NotFoundStatusCode;
                            objResultList.Message = StaticResource.LinkExpired;
                            break;
                        case 1:
                            objResultList.Data = null;
                            objResultList.Status = StaticResource.SuccessStatusCode;
                            objResultList.Message = StaticResource.SuccessMessage;
                            break;

                        default:
                            objResultList.Data = null;
                            objResultList.Status = StaticResource.NotFoundStatusCode;
                            objResultList.Message = StaticResource.LinkExpired;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    HttpContext.RiseError(ex);
                    objResultList.Data = null;
                    objResultList.Status = StaticResource.FailStatusCode;
                    objResultList.Message = StaticResource.FailMessage;
                    throw;
                }
            }
            else
            {
                objResultList.Data = null;
                objResultList.Status = StaticResource.FailStatusCode;
                objResultList.Message = StaticResource.InvalidMessage;
            }

            return new OkObjectResult(objResultList);
        }
        #endregion


        #region GetSpeciality 
        [Route("GetSpeciality")]
        [HttpGet]

        public async Task<IActionResult> GetSpeciality()
        {
            JsonResponse<List<ListVm>> objResult = new JsonResponse<List<ListVm>>();
            try
            {
                List<ListVm> listVms = await this.authService.GetSpeciality();
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


        #endregion



    }
}
