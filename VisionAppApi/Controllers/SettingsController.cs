using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Application.Abstractions.Services;
using Application.Dtos;
using Common.Common;
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
using Stripe;
using static Common.Common.CommonUtility.Enums;

namespace VisionAppApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class SettingsController : ControllerBase
    {
        private readonly IWebHostEnvironment _env;
        private readonly ISettingsService _settingService;
        private readonly IAuthService _authService;
        public SettingsController(ISettingsService settingService, IWebHostEnvironment env, IAuthService authService)
        {
            this._settingService = settingService;
            this._env = env;
            this._authService = authService;
        }

        [Route("BlockOrFollowUser")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> BlockOrFollowUser(int userId, int blockedOrFollowerId, bool isBlock)
        {

            JsonResponse<string> objResult = new JsonResponse<string>();
            try
            {
                int Id = 0;
                if (isBlock)
                    Id = await this._settingService.BlockUser(userId, blockedOrFollowerId);
                else
                    Id = await this._settingService.FollowPractitioner(userId, blockedOrFollowerId);

                if (Id > 0)
                {
                    string message = isBlock == true ? "Blocked" : "Followed";
                    objResult.Data = message;
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
        [Route("GetBlockedUsers")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBlockedUsers(int userId)
        {
            JsonResponse<IEnumerable<BlockedUserList>> objResult = new JsonResponse<IEnumerable<BlockedUserList>>();
            try
            {
                IEnumerable<BlockedUserList> lists;
                lists = await this._settingService.GetBlockedUsers(userId);
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
        [Route("UnblockUser")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UnblockUser([FromBody] UnblockUserRequest unblockUser)
        {
            JsonResponse<string> objResult = new JsonResponse<string>();
            try
            {
                bool unblocked = await this._settingService.UnblockUser(unblockUser);
                if (unblocked)
                {
                    objResult.Data = StaticResource.Unblocked;
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
        [Route("UnFollowUser")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UnFollowUser([FromBody]UnfollowUserRequest unfollowUser)
        {
            JsonResponse<string> objResult = new JsonResponse<string>();
            try
            {
                bool unfollowed = await this._settingService.UnFollowUser(unfollowUser);
                if (unfollowed)
                {
                    objResult.Data = StaticResource.Unfollowed;
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

        [Route("GetFollowedUsers")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetFollowedUsers(int userId)
        {
            JsonResponse<IEnumerable<FollowedUserList>> objResult = new JsonResponse<IEnumerable<FollowedUserList>>();
            try
            {
                IEnumerable<FollowedUserList> lists;
                lists = await this._settingService.GetFollowedPractitioner(userId);
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
        [Route("DeleteOrDeactivate")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteOrDeactivate(int userId, bool isDeactivate)
        {
            JsonResponse<string> objResult = new JsonResponse<string>();
            try
            {
                bool success = await this._settingService.DeleteOrDeactivate(userId, isDeactivate);
                if (success)
                {
                    string message = isDeactivate == true ? "Deactivated" : "Deleted";
                    objResult.Data = message;
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

        [Route("SaveProfileInfo")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SaveProfileInfo([FromBody] ProfileInfo profile)
        {
            JsonResponse<string> objResult = new JsonResponse<string>();
            try
            {
                bool success = await this._settingService.SaveProfileInfo(profile);
                if (success)
                {
                    objResult.Data = "Success";
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
                else
                {
                    objResult.Data = StaticResource.EmailExist;
                    objResult.Status = StaticResource.DuplicateStatusCode;
                    objResult.Message = StaticResource.DuplicateMessage;
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
        [Route("ProfilePicture")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ProfilePicture([FromForm]ProfileRequest request)
        {
            JsonResponse<string> objResult = new JsonResponse<string>();
            try
            {
                string uploadsFolder = Path.Combine(this._env.WebRootPath, "images");
                string imageName = await this._settingService.UploadedFile(request.file, uploadsFolder);
                bool success = await this._settingService.SaveProfilePicture(request.userId, imageName);
                if (success)
                {
                    objResult.Data = "Success";
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

        [Route("GetCountries")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCountries()
        {
            JsonResponse<List<ListVm>> objResult = new JsonResponse<List<ListVm>>();
            try
            {
                List<ListVm> listVms = await this._settingService.GetCountries();
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

        //Get Departments
        [Route("GetDepartments")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetDepartments()
        {
            JsonResponse<List<DepartmentListVm>> objResult = new JsonResponse<List<DepartmentListVm>>();
            try
            {
                List<DepartmentListVm> listVms = await this._settingService.GetDepartments();
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
        [Route("GetMemberDetails")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetMemberDetails(int userId)
        {
            JsonResponse<CompanyMemberDetails> objResult = new JsonResponse<CompanyMemberDetails>();
            try
            {
                CompanyMemberDetails data = await this._settingService.GetMemberDetails(userId);
                if (data != null)
                {
                    objResult.Data = data;
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

        [Route("GetStates")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetStates()
        {
            JsonResponse<List<ListVm>> objResult = new JsonResponse<List<ListVm>>();
            try
            {
                List<ListVm> listVms = await this._settingService.GetStates();
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
        [Route("SaveCardDetails")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SaveCardDetails([FromBody]BillingInfo billingInfo)
        {
            JsonResponse<string> objResult = new JsonResponse<string>();
            try
            {

                bool success = await this._settingService.SaveCardDetails(billingInfo);
                if (success)
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
        [Route("GetCardList")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCardList(int UserId)
        {
            JsonResponse<List<CardList>> objResult = new JsonResponse<List<CardList>>();
            try
            {

                IEnumerable<BillingInfo> cards = await this._settingService.GetCardList(UserId);
                if (cards != null && cards.Count() > 0)
                {
                    List<CardList> lists = new List<CardList>();
                    foreach (var item in cards)
                    {
                        CardList card = new CardList();
                        var service = new TokenService();
                        var details = service.Get(item.TokenId);
                        card.UserCreditCardId = item.UserCreditCardId;
                        card.Year = details.Card.ExpYear;
                        card.Month = details.Card.ExpMonth;
                        card.Name = item.NameOnCard;
                        card.CardType = details.Card.Brand;
                        card.Last4 = details.Card.Last4;
                        card.IsPrimary = item.IsPrimary;
                        lists.Add(card);
                    }
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
        [Route("SessionBooking")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SessionBooking([FromBody] SessionBookingRequest request)
        {
            JsonResponse<string> objResult = new JsonResponse<string>();
            try
            {
                bool success = await this._settingService.SessionBooking(request);
                if (success)
                {
                    objResult.Data = StaticResource.Booked;
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
        [Route("DeleteCard")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteCard(int Id)
        {
            JsonResponse<string> objResult = new JsonResponse<string>();
            try
            {
                bool success = await this._settingService.DeleteCard(Id);
                if (success)
                {
                    objResult.Data = StaticResource.CardDeleted;
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

        [Route("SubscriptionInfo")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SubscriptionInfo(int UserId)
        {
            JsonResponse<SubscriptionInfo> objResult = new JsonResponse<SubscriptionInfo>();
            try
            {
                SubscriptionInfo info = new SubscriptionInfo();
                info = await this._settingService.SubscriptionInfo(UserId);
                if (info != null)
                {
                    objResult.Data = info;
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
        [Route("GetSession")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSession(int sessionId)
        {
            JsonResponse<PractitionerSession> objResult = new JsonResponse<PractitionerSession>();
            try
            {
                PractitionerSession session = new PractitionerSession();
                session = await this._settingService.GetSession(sessionId);
                if (session != null)
                {
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

        [Route("GetSessionActivationDetails")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetSessionActivationDetails(int sessionId)
        {
            JsonResponse<SessionActivation> objResult = new JsonResponse<SessionActivation>();
            try
            {
                HttpContext context = HttpContext;
                SessionActivation activation = await this._settingService.GetSessionActivationDetails(sessionId);
                if (activation != null)
                {
                    activation.VideosSrc = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "\\images\\" + activation.VideosSrc;
                    objResult.Data = activation;
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


        [Route("GetBillingInfo")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetBillingInfo(int userId)
        {
            JsonResponse<Billing> objResult = new JsonResponse<Billing>();
            try
            {
                Billing billing = await this._settingService.GetBillingInfo(userId);
                if (billing != null)
                {
                    objResult.Data = billing;
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
        [Route("SaveBillingInfo")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SaveBillingInfo([FromBody]Billing billing)
        {
            JsonResponse<string> objResult = new JsonResponse<string>();
            try
            {
                bool succes = await this._settingService.SaveBillingInfo(billing);
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

        #region Change Password
        /// <summary>
        /// API to change the password
        /// </summary>
        /// <param name="objModel"></param>
        /// <returns></returns>
        [Route("ChangePassword")]
        [HttpPost]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePasswordRequestModel objModel)
        {
            JsonResponse<string> objResultList = new JsonResponse<string>();
            if (objModel != null)
            {
                try
                {
                    bool matched = await this._authService.MatchSecretKey(objModel.SecretKey);
                    if (!matched)
                    {
                        objResultList.Data = StaticResource.UnauthorizedMessage;
                        objResultList.Status = StaticResource.UnauthorizedStatusCode;
                        objResultList.Message = StaticResource.UnauthorizedMessage;
                        return new OkObjectResult(objResultList);
                    }
                    else
                    {
                        ResetPasswordResponseType objCommonStatusResponse = await this._settingService.ChangePassword(objModel);

                        if (objCommonStatusResponse == ResetPasswordResponseType.Success)
                        {
                            objResultList.Data = StaticResource.PasswordUpdated;
                            //Read dynamic msg deom enum
                            ResetPasswordResponseType obj_enum = (ResetPasswordResponseType)objCommonStatusResponse;
                            objResultList.Message = CommonConversions.ToEnumDescription(obj_enum);
                            objResultList.Status = StaticResource.SuccessStatusCode;
                        }
                        else
                        {
                            objResultList.Data = StaticResource.PassNotMatch;
                            ResetPasswordResponseType obj_enum = (ResetPasswordResponseType)objCommonStatusResponse;
                            objResultList.Message = CommonConversions.ToEnumDescription(obj_enum);
                            objResultList.Status = StaticResource.FailStatusCode;
                        }
                    }

                }
                catch (Exception ex)
                {
                    HttpContext.RiseError(ex);
                    objResultList.Data = "";
                    objResultList.Status = StaticResource.FailStatusCode;
                    objResultList.Message = StaticResource.FailMessage;
                    throw;
                }
            }
            else
            {
                objResultList.Data = "";
                objResultList.Status = StaticResource.FailStatusCode;
                objResultList.Message = StaticResource.InvalidMessage;
            }

            return new OkObjectResult(objResultList);
        }

        #endregion

        #region Logout
        [Route("Logout")]
        [HttpGet]
        public async Task<IActionResult> Logout(int userId)
        {
            JsonResponse<string> objResult = new JsonResponse<string>();
            try
            {
                bool success = await this._settingService.Logout(userId);
                if (success)
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
        #endregion

        #region SessionListUsers 

        [Route("GetSessionListForUsers")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetSessionListForUsers([FromBody] FilteredSessionListRequest sessionListRequest)
        {
            JsonResponse<IEnumerable<UsersSessionList>> objResult = new JsonResponse<IEnumerable<UsersSessionList>>();

            try
            {
                HttpContext context = HttpContext;
                IEnumerable<UsersSessionList> sessionLists = await this._settingService.GetUsersSessionList(sessionListRequest);
                if (sessionLists != null && sessionLists.Count() > 0)
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
        #endregion

    }
}