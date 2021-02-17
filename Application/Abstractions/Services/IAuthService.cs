using Application.Dtos;
using Domain.Entities;
using Infrastructure.Implemenatations.Context;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Common.Common.CommonUtility.Enums;

namespace Application.Abstractions.Services
{
    public interface IAuthService
    {
        Task<User> Authenticate(string UserName, string Password);
        Task<User> CreateUser(SignupRequest signupRequest, string filename, int RoleId, HttpContext context);
        Task<int> CreateCompanyUser(CompanyUser signupRequest);
        Task<int> SaveUserRole(int userId, int roleId);
        Task<User> CreateMemberUser(MemberSignupRequest request, string filename, int RoleId, HttpContext context);
        Task<User> CompanyMemberSignup(CompanyMemberSignup request, int RoleId,HttpContext httpContext);
        Task<UserProfile> CreateUserProfile(SignupRequest Request, int userId, string fileName);
        Task<UserProfile> CreateCompanyUserProfile(CompanyUser request, int userId, string fileName);
        Task<bool> CheckEmailExist(string email);
        Task<bool> CheckUserNameExist(string userName);
        Task<bool> MatchSecretKey(string Key);
        Task<string> UploadedFile(IFormFile formFile, string folderPath);
        Task<List<MasterPlan>> GetPlans(int? role);
        Task<User> ResetPassword(ForgetPasswordRequestModel obj_ForgetPasswordRequestModel);

        Task<User> ResetPasswordFromLink(ResetPasswordRequestModel obj_ResetPasswordRequestModel);
        Task<User> CheckResetPasswordLinkStatus(ResetPasswordRequestModel obj_ResetPasswordRequestModel);

        Task<bool> SaveMemberShipPlan(int? Id, long UserId);
        Task<bool> UserCompanyLog(long CompanyUserId, long userId, long CompanyId);
        public string CreateRandomAlphabet();

        Task<List<ListVm>> GetSpeciality();
        Task<string> GenerateUserName(Guid guid, string firstName, string lastName);
        Task<BulkCreatedUsers> GetMemberDetails(Guid guid);
        Task<IEnumerable<DepartmentDetails>> GetDepartmentDetails(int userId,int sessionId);
        Task<User> SendRestPasswordEmailByAdmin(ForgetPasswordRequestModel obj_ForgetPasswordRequestModel , HttpContext context);
        Task<User> ResendEmailToUser(ForgetPasswordRequestModel obj_ForgetPasswordRequestModel, HttpContext context);


    }
}
