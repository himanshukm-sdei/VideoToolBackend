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
    public interface ISettingsService
    {
        Task<int> BlockUser(int userId, int blockedOrFollowerId);
        Task<int> FollowPractitioner(int userId, int blockedOrFollowerId);
        Task<IEnumerable<BlockedUserList>> GetBlockedUsers(int userId);
        Task<IEnumerable<FollowedUserList>> GetFollowedPractitioner(int userId);
        Task<bool> UnblockUser(UnblockUserRequest unblockUser);
        Task<bool> UnFollowUser(UnfollowUserRequest unfollowUser);
        Task<bool> DeleteOrDeactivate(int userId, bool isDeactivate);
        Task<bool> SaveProfileInfo(ProfileInfo profile);
        Task<bool> SaveProfilePicture(int userId, string name);
        Task<string> UploadedFile(IFormFile formFile, string folderPath);
        Task<List<ListVm>> GetCountries();
        Task<List<DepartmentListVm>> GetDepartments();
        Task<CompanyMemberDetails> GetMemberDetails(int userId);
        Task<List<ListVm>> GetStates();
        Task<bool> SaveCardDetails(BillingInfo billingInfo);
        Task<IEnumerable<BillingInfo>> GetCardList(int userId);
        Task<bool> SessionBooking(SessionBookingRequest request);
        Task<bool> DeleteCard(int Id);
        Task<Billing> GetBillingInfo(int Id);
        Task<SubscriptionInfo> SubscriptionInfo(int userId);
        Task<PractitionerSession> GetSession(int sessionId);
        Task<bool> SaveBillingInfo(Billing billing);
        Task<ResetPasswordResponseType> ChangePassword(ChangePasswordRequestModel obj_ForgetPasswordRequestModel);
        Task<bool> Logout(long UserId);
        Task<SessionActivation> GetSessionActivationDetails(int sessionId);
        Task<IEnumerable<UsersSessionList>> GetUsersSessionList(FilteredSessionListRequest sessionListRequest);

    }
}
