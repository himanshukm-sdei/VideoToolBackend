using Application.Dtos;
using Domain.Entities;
using Infrastructure.Implemenatations.Context;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using static Common.Common.CommonUtility.Enums;

namespace Application.Abstractions.Repositories
{
    public interface IAuthRepo
    {
        Task<User> Authenticate(string UserName, string Password);
        Task<User> CreateUserAsync( Users users, UserProfile userProfile, string filename , UserInfo model, int RoleId, string speciality, bool? IsCompany, Guid? InvitedGuid, int CompanyId, int CompanyAdminId);
        Task<int> SaveUserRole(UserRole userRole);
        Task<UserProfile> CreateUserProfileAsync(UserProfile user);
        Task<bool> EmailExist(string email);
        Task<string> GenerateUserName(Guid Guid, string firstName, string lastName);
        Task<bool> CheckUserNameExist(string userName);
        Task<bool> MatchSecretKey(string key);
        Task<string> UploadedFile(IFormFile formFile, string folderPath);
        Task<bool> SaveMemberShipPlan(UserMembership membership);
        Task<bool> SaveUserCompanyLog(UserCompany userCompany);
        Task<Users> GetUserByEmailId(string EmailId);
        Task<List<MasterPlan>> GetPlans(int? role);
        Task<User> UpdateResetPasswordLink(string guid, string Email);
        Task<User> UpdateNewPassword(ResetPasswordRequestModel objModel);
        Task<User> CheckResetPasswordLinkStatus(ResetPasswordRequestModel objModel);
        Task<bool> CheckForRequest(long UserId);
        Task<bool> checkEmailForResetPassword(ResetPasswordRequestModel objModel);
        Task<List<ListVm>> GetSpeciality();
        Task<BulkCreatedUsers> GetMemberDetails(Guid guid);
        Task<User> SendRestPasswordEmailByAdmin(string guid, string Email, long UserId);
        Task<IEnumerable<DepartmentDetails>> GetDepartmentDetails(int userId, int sessionId);

    }
}
