using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Dtos;
using Domain.Entities;
using Infrastructure.Implemenatations.Context;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using static Common.Common.CommonUtility.Enums;

namespace Services
{
    public class SettingsService : ISettingsService
    {
        private readonly ISettingsRepo _settingsRepo;


        public SettingsService(ISettingsRepo settingsRepo)
        {
            _settingsRepo = settingsRepo;


        }

        public async Task<int> BlockUser(int userId, int blockedOrFollowerId)
        {
            try
            {
                var now = DateTime.UtcNow;
                UserBlocked blocked = new UserBlocked
                {
                    UserBlockedId = 0,
                    UserBlockedGuid = System.Guid.NewGuid(),
                    UserId = userId,
                    BlockedUserId = blockedOrFollowerId,
                    BlockedDate = now,
                    CreatedDate = now,
                    CreatedBy = userId,
                    ModifiedDate = null,
                    ModifiedBy = null,
                    IsActive = true,
                    IsDeleted = false
                };
                return await this._settingsRepo.BlockUser(blocked);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<int> FollowPractitioner(int userId, int blockedOrFollowerId)
        {
            try
            {
                var now = DateTime.UtcNow;
                UserFollower follower = new UserFollower
                {
                    UserFollowerId = 0,
                    UserFollowerGuid = System.Guid.NewGuid(),
                    UserId = userId,
                    FollowerUserId = blockedOrFollowerId,
                    FollowerDate = now,
                    CreatedDate = now,
                    CreatedBy = userId,
                    ModifiedDate = null,
                    ModifiedBy = null,
                    IsActive = true,
                    IsDeleted = false
                };
                return await this._settingsRepo.FollowPractitioner(follower);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> UnblockUser(UnblockUserRequest unblockUser)
        {
            try
            {
                return await this._settingsRepo.UnblockUser(unblockUser);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> UnFollowUser(UnfollowUserRequest unfollowUser)
        {
            try
            {
                return await this._settingsRepo.UnFollowUser(unfollowUser);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<BlockedUserList>> GetBlockedUsers(int userId)
        {
            try
            {
                return await this._settingsRepo.GetBlockedUsers(userId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<FollowedUserList>> GetFollowedPractitioner(int userId)
        {
            try
            {
                return await this._settingsRepo.GetFollowedPractitioner(userId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> DeleteOrDeactivate(int userId, bool isDeactivate)
        {
            try
            {
                return await this._settingsRepo.DeleteOrDeactivate(userId, isDeactivate);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> SaveProfileInfo(ProfileInfo profile)
        {
            try
            {
                return await this._settingsRepo.SaveProfileInfo(profile);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<CompanyMemberDetails> GetMemberDetails(int userId)
        {
            try
            {
                return await this._settingsRepo.GetMemberDetails(userId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
       public async Task<SessionActivation> GetSessionActivationDetails(int sessionId)
        {
            try
            {
                return await this._settingsRepo.GetSessionActivationDetails(sessionId);
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
                return await this._settingsRepo.UploadedFile(file, folderPath);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> SaveProfilePicture(int userId, string name)
        {
            try
            {
                return await this._settingsRepo.SaveProfilePicture(userId, name);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<ListVm>> GetCountries()
        {
            try
            {
                return await this._settingsRepo.GetCountries();
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<List<DepartmentListVm>> GetDepartments()
        {
            try
            {
                return await this._settingsRepo.GetDepartments();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<List<ListVm>> GetStates()
        {
            try
            {
                return await this._settingsRepo.GetStates();
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> SaveCardDetails(BillingInfo billingInfo)
        {
            try
            {

                return await this._settingsRepo.SaveCardDetails(billingInfo);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<BillingInfo>> GetCardList(int userId)
        {
            try
            {
                return await this._settingsRepo.GetCardList(userId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> SessionBooking(SessionBookingRequest request)
        {
            try
            {
                SessionBooking booking = new SessionBooking
                {
                    SessionBookingId = 0,
                    SessionBookingGuid = System.Guid.NewGuid(),
                    SessionId = request.SessionId,
                    BookingDate = request.BookingDate,
                    //MemberId = request.MemberId,
                    SessionType = request.SessionType,
                    CreatedDate = DateTime.UtcNow,
                    CreatedBy = request.UserId,
                    IsActive = true,
                    IsDeleted = false,
                    ModifiedBy = null,
                    ModifiedDate = null
                };
                return await this._settingsRepo.SessionBooking(booking);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> DeleteCard(int Id)
        {
            try
            {
                return await this._settingsRepo.DeleteCard(Id);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<SubscriptionInfo> SubscriptionInfo(int userId)
        {
            try
            {
                return await this._settingsRepo.SubscriptionInfo(userId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<PractitionerSession> GetSession(int sessionId)
        {
            try
            {

                return await this._settingsRepo.GetSession(sessionId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Billing> GetBillingInfo(int Id)
        {
            {
                try
                {
                    return await this._settingsRepo.GetBillingInfo(Id);
                }
                catch (Exception ex)
                {

                    throw ex;
                }
            }
        }
        public async Task<bool> SaveBillingInfo(Billing billing)
        {
            try
            {
                return await this._settingsRepo.SaveBillingInfo(billing);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        /// <summary>
        /// Change password
        /// </summary>
        /// <param name="obj_ForgetPasswordRequestModel"></param>
        /// <returns></returns>
        public async Task<ResetPasswordResponseType> ChangePassword(ChangePasswordRequestModel obj_ForgetPasswordRequestModel)
        {
            try
            {
                User obj_Users = await this._settingsRepo.GetUserByUserId(obj_ForgetPasswordRequestModel.UserId);
                if (obj_Users != null)
                {
                    //check for User Current Password
                    if (obj_Users.UserPassword == obj_ForgetPasswordRequestModel.OldPassword)
                    {
                        bool result = await this._settingsRepo.UpdatePassword(obj_ForgetPasswordRequestModel);
                        if (result)
                        {
                            return ResetPasswordResponseType.Success;
                        }
                        else
                        {
                            return ResetPasswordResponseType.Failed;
                        }
                    }
                    else
                    {
                        return ResetPasswordResponseType.CurrentPasswordNotmatched;
                    }
                }
                else
                {
                    return ResetPasswordResponseType.NoUserFound;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> Logout(long UserId)
        {
            try
            {
                return await this._settingsRepo.Logout(UserId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<UsersSessionList>> GetUsersSessionList(FilteredSessionListRequest sessionListRequest)
        {
            try
            {
                return await this._settingsRepo.GetUsersSessionList(sessionListRequest);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
