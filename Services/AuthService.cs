using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Dtos;
using Domain.Entities;
using Infrastructure.Implemenatations.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using static Common.Common.CommonUtility;
using static Common.Common.CommonUtility.Enums;

namespace Services
{
    public class AuthService : IAuthService
    {
        private readonly IAuthRepo authRepo;
        private readonly ICommonEmailsService commonEmailsService;


        public AuthService(IAuthRepo _authRepo, ICommonEmailsService ICommonEmailsService)
        {
            authRepo = _authRepo;
            commonEmailsService = ICommonEmailsService;
        }
        public async Task<User> Authenticate(string UserName, string Password)
        {
            User user = new User();
            try
            {
                user = await this.authRepo.Authenticate(UserName, Password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return user;
        }
        public async Task<User> CreateUser(SignupRequest model, string fileName, int RoleId, HttpContext context)
        {
            int userId = 0;
            var now = DateTime.UtcNow;
            var guid = System.Guid.NewGuid();
            var userName = await this.authRepo.GenerateUserName(guid, model.FirstName, model.LastName);
            User createPractioner = null;

            var user = new Users
            {
                UserId = 0,
                UserGuid = guid,
                //UserName = userName,
                //UserPassword = CreateRandomAlphabet(),
                IsActive = (model.FromAdmin != null) ? true : false,
                IsBlocked = false,
                LastLogin = null,
                CreatedDate = now,
                CreatedBy = model.CreatedBy,
                ModifiedDate = null,
                ModifiedBy = null,
                IsDeleted = false,
            };

            UserProfile userProfile = new UserProfile
            {
                ProfileId = 0,
                ProfileGuid = System.Guid.NewGuid(),
                UserId = userId,
                //FirstName = model.FirstName,
                //LastName = model.LastName,
                //Email = model.Email,
                ProfilePhoto = fileName,
                Gender = null,
                City = null,
                State = null,
                Country = null,
                ZipCode = null,
                //Specialities = String.Join(",", model.Specialities),
                Qualification = null,
                Education = null,
                IsFeature = null,
                Ranked = null,
                BriefBio = model.BriefBio,
                InstagramProfile = model.InstagramProfile,
                LinkedInProfile = model.LinkedInProfile,
                FaceBookProfile = model.FaceBookProfile,
                ContactNo1 = null,
                ContactNo2 = null,
                TimezoneId = model.Timezone,
                //AcceptedDate = null,
                //AcceptedBy = null,
                CreatedDate = now,
                ModifiedDate = null,
                CreatedBy = userId,
                ModifiedBy = null,
                PractitionerSince = DateTime.Now,
                Description = null,
                IsActive = (model.FromAdmin != null) ? true : false,
                IsDeleted = false,
                Language = null,
                IsDeactivated = false,
                PublicProfile = true,
                UserTypeText = (RoleId == (int)RolesEnum.Practitioner) ? "Practitioner" : null,
            };

            UserInfo UserInfo = new UserInfo()
            {
                UserName = userName,
                Password = CreateRandomAlphabet(),
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
            };
            var speciality = model.Specialities;

            try
            {
                createPractioner = await this.authRepo.CreateUserAsync(user, userProfile, fileName, UserInfo, RoleId, model.Specialities, null, null, 0, 0);
                if (createPractioner != null && createPractioner.UserId > 0)
                {
                    if (model.FromAdmin != null)
                        await this.commonEmailsService.SendWelcomeEmailToPractitionerFromAdmin(model.Email, UserInfo.UserName, UserInfo.Password, context);
                    else
                        await this.commonEmailsService.SendWelcomeEmailToPractitioner(model.Email, context);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return createPractioner;
        }
        public async Task<IEnumerable<DepartmentDetails>> GetDepartmentDetails(int userId, int sessionId)
        {
            try
            {
                return await this.authRepo.GetDepartmentDetails(userId, sessionId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> UserCompanyLog(long CompanyUserId, long userId, long CompanyId)
        {
            try
            {
                var now = DateTime.UtcNow;
                var guid = System.Guid.NewGuid();
                UserCompany userCompany = new UserCompany
                {
                    UserCompanyId = 0,
                    UserCompanyGuid = guid,
                    UserId = userId,
                    CompanyId = CompanyId,
                    CreatedBy = CompanyUserId,
                    CreatedDate = now,
                    ModifiedBy = null,
                    ModifiedDate = null,
                    IsActive = true,
                    IsDeleted = false
                };
                return await this.authRepo.SaveUserCompanyLog(userCompany);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<int> CreateCompanyUser(CompanyUser signupRequest)
        {
            int userId = 0;
            var now = DateTime.UtcNow;
            var guid = System.Guid.NewGuid();
            try
            {
                bool userNameExist = await this.authRepo.CheckUserNameExist(signupRequest.UserName);
                if (userNameExist)
                    return -1;
                var user = new Users
                {
                    UserId = 0,
                    UserGuid = guid,
                    //UserName = signupRequest.UserName,
                    //UserPassword = signupRequest.UserPassword,
                    IsActive = true,
                    IsBlocked = false,
                    LastLogin = null,
                    CreatedDate = now,
                    CreatedBy = null,
                    ModifiedDate = null,
                    ModifiedBy = null,
                    IsDeleted = false,
                };
                //  userId = await this.authRepo.CreateUserAsync(user);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return userId;
        }
        public async Task<User> CompanyMemberSignup(CompanyMemberSignup request, int RoleId, HttpContext context)
        {
            int userId = 0;
            User createMember = null;
            var now = DateTime.UtcNow;
            var guid = System.Guid.NewGuid();
            try
            {
                var user = new Users
                {
                    UserId = 0,
                    UserGuid = guid,
                    IsActive = true,
                    IsBlocked = false,
                    LastLogin = null,
                    CreatedDate = now,
                    CreatedBy = null,
                    ModifiedDate = null,
                    ModifiedBy = null,
                    IsDeleted = false,
                };

                UserProfile userProfile = new UserProfile
                {
                    ProfileId = 0,
                    ProfileGuid = System.Guid.NewGuid(),
                    UserId = userId,
                    ProfilePhoto = null,
                    Gender = null,
                    City = null,
                    State = null,
                    Country = null,
                    ZipCode = null,
                    Qualification = null,
                    Education = null,
                    IsFeature = null,
                    Ranked = null,
                    BriefBio = null,
                    InstagramProfile = null,
                    LinkedInProfile = null,
                    FaceBookProfile = null,
                    ContactNo1 = null,
                    ContactNo2 = null,
                    TimezoneId = request.TimezoneId,
                    IsApproved = null,
                    ApprovedDate = null,
                    //
                    CreatedDate = now,
                    ModifiedDate = null,
                    CreatedBy = userId,
                    ModifiedBy = null,
                    IsActive = true,
                    PractitionerSince = null,
                    Description = request.Description,
                    Language = null,
                    IsDeleted = false,
                    PublicProfile = true,
                    IsDeactivated = false
                };
                UserInfo userInfo = new UserInfo()
                {
                    UserName = request.CompanyUserNameCode + "_" + request.UserName,
                    Password = request.UserPassword,
                    FirstName = request.FirstName,
                    LastName = request.LastName,
                    Email = request.Email,
                };
                bool IsCompany = true;
                createMember = await this.authRepo.CreateUserAsync(user, userProfile, null, userInfo, RoleId, "", IsCompany, request.InvitedGuid, request.CompanyId, request.CompanyAdminId);
                if (createMember.UserId > 0)
                {
                    await this.commonEmailsService.SendWelcomeEmailToMemberFromCompany(request.Email, request.FirstName, request.LastName, userInfo.UserName, request.UserPassword, request.CompanyName, context);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return createMember;
        }


        public async Task<User> CreateMemberUser(MemberSignupRequest model, string filename, int RoleId, HttpContext context)
        {
            int userId = 0;
            User createMember = null;
            var now = DateTime.UtcNow;
            var guid = System.Guid.NewGuid();
            //bool userNameExist = await this.authRepo.CheckUserNameExist(model.UserName);
            //if (userNameExist)
            //    return -1;


            var user = new Users
            {
                UserId = 0,
                UserGuid = guid,
                //UserName = model.UserName,
                //UserPassword = model.UserPassword,
                IsActive = true,
                IsBlocked = false,
                LastLogin = null,
                CreatedDate = now,
                CreatedBy = null,
                ModifiedDate = null,
                ModifiedBy = null,
                IsDeleted = false,
            };

            UserProfile userProfile = new UserProfile
            {
                ProfileId = 0,
                ProfileGuid = System.Guid.NewGuid(),
                UserId = userId,
                //FirstName = model.FirstName,
                //LastName = model.LastName,
                //Email = model.Email,
                ProfilePhoto = filename,
                Gender = null,
                City = null,
                State = null,
                Country = null,
                ZipCode = null,
                //Specialities = null,
                Qualification = null,
                Education = null,
                IsFeature = null,
                Ranked = null,
                BriefBio = null,
                InstagramProfile = null,
                LinkedInProfile = null,
                FaceBookProfile = null,
                ContactNo1 = null,
                ContactNo2 = null,
                //AcceptedDate = null,
                //AcceptedBy = null,
                CreatedDate = now,
                ModifiedDate = null,
                CreatedBy = userId,
                ModifiedBy = null,
                IsActive = true,
                PractitionerSince = null,
                Description = null,
                Language = null,
                IsDeleted = false,
                PublicProfile = true,
                IsDeactivated = false
            };

            UserInfo userInfo = new UserInfo()
            {
                UserName = model.UserName,
                Password = model.UserPassword,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Email = model.Email,
            };

            try
            {
                createMember = await this.authRepo.CreateUserAsync(user, userProfile, filename, userInfo, RoleId, "", null, null, 0, 0);
                if (createMember != null && createMember.UserId > 0)
                {
                    await this.commonEmailsService.SendWelcomeEmailToMember(model.Email, context);
                }
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return createMember;
        }
        public async Task<bool> CheckEmailExist(string email)
        {
            try
            {
                return await this.authRepo.EmailExist(email);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<bool> CheckUserNameExist(string userName)
        {
            try
            {
                return await this.authRepo.CheckUserNameExist(userName);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> MatchSecretKey(string key)
        {
            try
            {
                return await this.authRepo.MatchSecretKey(key);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<string> UploadedFile(IFormFile file, string folderPath)
        {
            try
            {
                return await this.authRepo.UploadedFile(file, folderPath);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public string CreateRandomAlphabet()
        {
            var chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            var stringChars = new char[8];
            var random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
            {
                stringChars[i] = chars[random.Next(chars.Length)];
            }

            var finalString = new String(stringChars);
            return finalString;
        }
        public async Task<UserProfile> CreateUserProfile(SignupRequest model, int userId, string fileName)
        {
            var now = DateTime.UtcNow;
            UserProfile userProfile = new UserProfile
            {
                ProfileId = 0,
                ProfileGuid = System.Guid.NewGuid(),
                UserId = userId,
                //FirstName = model.FirstName,
                //LastName = model.LastName,
                //Email = model.Email,
                ProfilePhoto = fileName,
                Gender = null,
                City = null,
                State = null,
                Country = null,
                ZipCode = null,
                //Specialities = String.Join(",", model.Specialities),
                Qualification = null,
                Education = null,
                IsFeature = null,
                Ranked = null,
                BriefBio = model.BriefBio,
                InstagramProfile = model.InstagramProfile,
                LinkedInProfile = model.LinkedInProfile,
                FaceBookProfile = model.FaceBookProfile,
                ContactNo1 = null,
                ContactNo2 = null,
                //AcceptedDate = null,
                //AcceptedBy = null,
                CreatedDate = now,
                ModifiedDate = null,
                CreatedBy = userId,
                ModifiedBy = null,
                PractitionerSince = null,
                Description = null,
                IsActive = false,
                IsDeleted = false,
                Language = null,
                IsDeactivated = false,
                PublicProfile = true
            };
            try
            {
                await this.authRepo.CreateUserProfileAsync(userProfile);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return userProfile;
        }
        public async Task<int> SaveUserRole(int userId, int roleId)
        {
            try
            {
                var now = DateTime.UtcNow;
                UserRole userRole = new UserRole
                {
                    UserRoleId = 0,
                    UserRoleGuid = System.Guid.NewGuid(),
                    UserId = userId,
                    RoleId = roleId,
                    CreatedDate = now,
                    CreatedBy = userId,
                    ModifiedDate = null,
                    ModifiedBy = null,
                    IsActive = true,
                    IsDeleted = false
                };
                return await this.authRepo.SaveUserRole(userRole);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<UserProfile> CreateMemberUserProfile(MemberSignupRequest model, int userId, string fileName, int RoleId)
        {
            var now = DateTime.UtcNow;
            UserProfile userProfile = new UserProfile
            {
                ProfileId = 0,
                ProfileGuid = System.Guid.NewGuid(),
                UserId = userId,
                //FirstName = model.FirstName,
                //LastName = model.LastName,
                //Email = model.Email,
                ProfilePhoto = fileName,
                Gender = null,
                City = null,
                State = null,
                Country = null,
                ZipCode = null,
                //Specialities = null,
                Qualification = null,
                Education = null,
                IsFeature = null,
                Ranked = null,
                BriefBio = null,
                InstagramProfile = null,
                LinkedInProfile = null,
                FaceBookProfile = null,
                ContactNo1 = null,
                ContactNo2 = null,
                //AcceptedDate = null,
                //AcceptedBy = null,
                CreatedDate = now,
                ModifiedDate = null,
                CreatedBy = userId,
                ModifiedBy = null,
                IsActive = false,
                PractitionerSince = null,
                Description = null,
                Language = null,
                IsDeleted = false,
                PublicProfile = true
            };
            try
            {
                await this.authRepo.CreateUserProfileAsync(userProfile);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return userProfile;
        }
        public async Task<UserProfile> CreateCompanyUserProfile(CompanyUser model, int userId, string fileName)
        {
            var now = DateTime.UtcNow;
            UserProfile userProfile = new UserProfile
            {
                ProfileId = 0,
                ProfileGuid = System.Guid.NewGuid(),
                UserId = userId,
                //FirstName = model.FirstName,
                //LastName = model.LastName,
                //Email = model.Email,
                ProfilePhoto = null,
                Gender = null,
                City = null,
                State = null,
                Country = null,
                ZipCode = null,
                //Specialities = null,
                Qualification = null,
                Education = null,
                IsFeature = null,
                Ranked = null,
                BriefBio = null,
                InstagramProfile = null,
                LinkedInProfile = null,
                FaceBookProfile = null,
                ContactNo1 = null,
                ContactNo2 = null,
                //AcceptedDate = null,
                //AcceptedBy = null,
                CreatedDate = now,
                ModifiedDate = null,
                CreatedBy = userId,
                ModifiedBy = null,
                IsActive = false,
                PractitionerSince = null,
                Description = null,
                Language = null,
                IsDeleted = false,
                PublicProfile = true
            };
            try
            {
                await this.authRepo.CreateUserProfileAsync(userProfile);
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return userProfile;
        }

        /// <summary>
        /// Send Reset password Email to user
        /// </summary>
        /// <param name="obj_ForgetPasswordRequestModel"></param>
        /// <returns></returns>
        public async Task<User> ResetPassword(ForgetPasswordRequestModel obj_ForgetPasswordRequestModel)
        {
            try
            {
                var result = await commonEmailsService.SendRestPasswordEmail(obj_ForgetPasswordRequestModel.EmailId);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<MasterPlan>> GetPlans(int? role)
        {
            try
            {
                return await this.authRepo.GetPlans(role);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> SaveMemberShipPlan(int? Id, long UserId)
        {
            try
            {

                var lastdte = DateTime.UtcNow;
                //statically adding months or years corresponding to master plan table
                if (Id == 1)
                {
                    lastdte = lastdte.AddYears(1);
                }
                else
                {
                    lastdte = lastdte.AddMonths(1);
                }
                UserMembership membership = new UserMembership
                {
                    UserMembershipId = 0,
                    UserMembershipGuid = System.Guid.NewGuid(),
                    MembershipStartDate = DateTime.UtcNow,
                    MembershipEndDate = lastdte,
                    ActiveMembershipPlan = 1,
                    IsRecurring = true,
                    PlanId = Id,
                    InvoiceId = null,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = UserId,
                    IsActive = true,
                    IsDeleted = false,
                    UserId = UserId
                };
                return await this.authRepo.SaveMemberShipPlan(membership);
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<BulkCreatedUsers> GetMemberDetails(Guid guid)
        {
            try
            {
                return await this.authRepo.GetMemberDetails(guid);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<string> GenerateUserName(Guid guid, string firstName, string lastName)
        {
            try
            {
                return await this.authRepo.GenerateUserName(guid, firstName, lastName);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        /// <summary>
        /// Send Reset password Email to user
        /// </summary>
        /// <param name="obj_ForgetPasswordRequestModel"></param>
        /// <returns></returns>
        public async Task<User> ResetPasswordFromLink(ResetPasswordRequestModel obj_ForgetPasswordRequestModel)
        {
            try
            {
                User obj_Users = await this.authRepo.UpdateNewPassword(obj_ForgetPasswordRequestModel);
                return obj_Users;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Send Reset password Email to user
        /// </summary>
        /// <param name="obj_ForgetPasswordRequestModel"></param>
        /// <returns></returns>
        public async Task<User> CheckResetPasswordLinkStatus(ResetPasswordRequestModel obj_ForgetPasswordRequestModel)
        {
            try
            {
                User obj_Users = await this.authRepo.CheckResetPasswordLinkStatus(obj_ForgetPasswordRequestModel);
                return obj_Users;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<List<ListVm>> GetSpeciality()
        {
            try
            {
                return await this.authRepo.GetSpeciality();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<User> SendRestPasswordEmailByAdmin(ForgetPasswordRequestModel obj_ForgetPasswordRequestModel, HttpContext context)
        {
            try
            {
                var result = await commonEmailsService.SendRestPasswordEmailByAdmin(obj_ForgetPasswordRequestModel.EmailId, obj_ForgetPasswordRequestModel.UserId, context);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> ResendEmailToUser(ForgetPasswordRequestModel obj_ForgetPasswordRequestModel, HttpContext context)
        {
            try
            {
                var result = await commonEmailsService.ResendEmailToUser(obj_ForgetPasswordRequestModel, context);
                return result;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
