using Application.Dtos;
using Domain.Entities;
using Infrastructure.Implemenatations.Context;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Repositories
{
    public interface ISettingsRepo
    {
        Task<int> BlockUser(UserBlocked userBlocked);
        Task<int> FollowPractitioner(UserFollower userFollower);
        Task<IEnumerable<BlockedUserList>> GetBlockedUsers(int userId);
        Task<IEnumerable<FollowedUserList>> GetFollowedPractitioner(int userId);
        Task<CompanyMemberDetails> GetMemberDetails(int userId);
        Task<bool> UnblockUser(UnblockUserRequest unblockUser);
        Task<bool> UnFollowUser(UnfollowUserRequest unfollowUse);
        Task<bool> DeleteOrDeactivate(int userId, bool isDeactivate);
        Task<bool> SaveProfileInfo(ProfileInfo profile);
        Task<string> UploadedFile(IFormFile formFile, string folderPath);
        Task<bool> SaveProfilePicture(int userId, string name);
        Task<List<ListVm>> GetCountries();
        Task<List<DepartmentListVm>> GetDepartments();
        Task<List<ListVm>> GetStates();
        Task<bool> SaveCardDetails(BillingInfo card);
        Task<IEnumerable<BillingInfo>> GetCardList(int userId);
        Task<bool> SessionBooking(SessionBooking request);
        Task<bool> DeleteCard(int Id);
        Task<SubscriptionInfo> SubscriptionInfo(int userId);
        Task<PractitionerSession> GetSession(int sessionId);
        Task<Billing> GetBillingInfo(int Id);
        Task<bool> SaveBillingInfo(Billing billing);
        Task<bool> UpdatePassword(ChangePasswordRequestModel Password);
        Task<User> GetUserByUserId(long UserId);
        Task<bool> Logout(long UserId);
        Task<SessionActivation> GetSessionActivationDetails(int sessionId);

        Task<IEnumerable<UsersSessionList>> GetUsersSessionList(FilteredSessionListRequest sessionListRequest);
    }
}
