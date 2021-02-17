using Application.Abstractions.Repositories;
using Domain.Entities;
using Infrastructure.Implemenatations.Context;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Dapper;
using Application.Dtos;
using Microsoft.AspNetCore.Http;
using System.IO;
using AutoMapper;
using ProfileInfo = Application.Dtos.ProfileInfo;

namespace Repository
{
    public class SettingsRepo : ISettingsRepo
    {
        private readonly therapistContext therapistContext;
        private readonly IConfiguration config;
        private string _connectionString;
        public SettingsRepo(therapistContext therapistContext, IConfiguration configuration)
        {
            this.therapistContext = therapistContext;
            config = configuration;
            _connectionString = configuration.GetConnectionString("DBCNSTR");
        }
        public async Task<int> BlockUser(UserBlocked blocked)
        {
            try
            {
                int resultedId = 0;
                var reslt = await this.therapistContext.UserBlocked.FirstOrDefaultAsync(x => x.UserId == blocked.UserId && x.BlockedUserId == blocked.BlockedUserId);
                if (reslt != null)
                {
                    reslt.IsActive = true;
                    reslt.IsDeleted = false;
                    await this.therapistContext.SaveChangesAsync();
                    resultedId = (int)reslt.UserBlockedId;
                }
                else
                {
                    this.therapistContext.UserBlocked.Add(blocked);
                    await this.therapistContext.SaveChangesAsync();
                    resultedId = Convert.ToInt32(blocked.UserBlockedId);
                }
                await this.SaveBlockHistory(blocked.UserId, blocked.BlockedUserId, true);
                return resultedId;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> SaveBlockHistory(long userId, long blockedOrUnblockedId, bool isBlocked)
        {
            try
            {
                BlockUserHistory userHistory = new BlockUserHistory();
                userHistory.HistoryId = 0;
                userHistory.UserId = userId;
                if (isBlocked)
                {
                    userHistory.BlockedUserId = blockedOrUnblockedId;
                    userHistory.UnblockedUserId = null;

                }
                else
                {
                    userHistory.UnblockedUserId = blockedOrUnblockedId;
                    userHistory.BlockedUserId = null;

                }
                userHistory.CreatedBy = userId;
                userHistory.CreatedDate = DateTime.UtcNow;
                userHistory.ModifiedBy = null;
                userHistory.ModifiedDate = null;
                userHistory.IsActive = true;
                userHistory.IsDeleted = false;
                this.therapistContext.BlockUserHistory.Add(userHistory);
                await this.therapistContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> FollowPractitioner(UserFollower follower)
        {
            try
            {
                int resultedId = 0;
                var reslt = await this.therapistContext.UserFollower.FirstOrDefaultAsync(x => x.UserId == follower.UserId && x.FollowerUserId == follower.FollowerUserId);
                if (reslt != null)
                {
                    reslt.IsDeleted = false;
                    reslt.IsActive = true;
                    resultedId = (int)reslt.UserFollowerId;
                }
                else
                {
                    this.therapistContext.UserFollower.Add(follower);
                    resultedId = Convert.ToInt32(follower.UserFollowerId);
                }
                await this.therapistContext.SaveChangesAsync();
                return resultedId;
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
                IEnumerable<BlockedUserList> userLists;
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    userLists = await con.QueryAsync<BlockedUserList>("dbo.SSP_getBlockedUsersList", new
                    {
                        UserId = userId
                    }, commandType: CommandType.StoredProcedure);
                }

                return userLists;
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

                IEnumerable<FollowedUserList> userLists;
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    userLists = await con.QueryAsync<FollowedUserList>("dbo.SSP_getFollowedUsersList", new
                    {
                        UserId = userId
                    }, commandType: CommandType.StoredProcedure);
                }
                return userLists;
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
                UserBlocked userBlocked = await this.therapistContext.UserBlocked.FirstOrDefaultAsync(x => x.UserId.Equals(unblockUser.userId) && x.UserBlockedId.Equals(unblockUser.userBlockedId));
                userBlocked.IsActive = false;
                userBlocked.IsDeleted = true;
                userBlocked.ModifiedDate = DateTime.UtcNow;
                userBlocked.ModifiedBy = unblockUser.userId;
                await this.therapistContext.SaveChangesAsync();
                await this.SaveBlockHistory(unblockUser.userId, unblockUser.blockedUserId, false);
                return true;
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
                UserFollower userFollower = await this.therapistContext.UserFollower.FirstOrDefaultAsync(x => x.UserId.Equals(unfollowUser.userId) && x.UserFollowerId.Equals(unfollowUser.userFollowerId));
                userFollower.IsActive = false;
                userFollower.IsDeleted = true;
                userFollower.ModifiedDate = DateTime.UtcNow;
                userFollower.ModifiedBy = unfollowUser.userId;
                await this.therapistContext.SaveChangesAsync();
                return true;
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
                UserProfile profile = await this.therapistContext.UserProfile.FirstOrDefaultAsync(x => x.UserId.Equals(userId));
                profile.ModifiedDate = DateTime.UtcNow;
                profile.ModifiedBy = userId;
                if (isDeactivate)
                {
                    profile.IsDeactivated = true;
                    profile.IsActive = false;
                }
                else
                {
                    profile.IsDeleted = true;
                }
                await this.therapistContext.SaveChangesAsync();
                return true;
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
                bool success = false;
                string Specialities = null;
                if (profile.Specialities.Count > 0)
                {
                    Specialities = "";
                    foreach (var item in profile.Specialities)
                    {
                        Specialities = Specialities + item.SpecialityId.ToString();
                        Specialities += ",";
                    }
                    Specialities = Specialities.Remove(Specialities.Length - 1);
                }
                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@ProfileId", profile.ProfileId);
                ObjParm.Add("@FirstName", profile.FirstName);
                ObjParm.Add("@LastName", profile.LastName);
                ObjParm.Add("@Email", profile.Email);
                ObjParm.Add("@Gender", profile.Gender);
                ObjParm.Add("@City", profile.City);
                ObjParm.Add("@State", profile.State);
                ObjParm.Add("@Country", profile.Country);
                ObjParm.Add("@UserId", profile.UserId);
                ObjParm.Add("@PractitionerSince", profile.PractitionerSince);
                ObjParm.Add("@ZipCode", profile.ZipCode);
                ObjParm.Add("@Qualification", profile.Qualification);
                ObjParm.Add("@Education", profile.Education);
                ObjParm.Add("@IsFeature", profile.IsFeature);
                ObjParm.Add("@Ranked", profile.Ranked);
                ObjParm.Add("@BriefBio", profile.BriefBio);
                ObjParm.Add("@InstagramProfile", profile.InstagramProfile);
                ObjParm.Add("@LinkedInProfile", profile.LinkedInProfile);
                ObjParm.Add("@FaceBookProfile", profile.FaceBookProfile);
                ObjParm.Add("@ContactNo1", profile.ContactNo1);
                ObjParm.Add("@ContactNo2", profile.ContactNo2);
                ObjParm.Add("@Description", profile.Description);
                ObjParm.Add("@Language", profile.Language);
                ObjParm.Add("@PublicProfile", profile.PublicProfile);
                ObjParm.Add("@FirstResponder", profile.FirstResponder);
                ObjParm.Add("@DepartmentId", profile.DepartmentId);
                ObjParm.Add("@Specialities", Specialities);
                ObjParm.Add("@Position", profile.Position);
                ObjParm.Add("@EmployeeNumber", profile.EmployeeNumber);
                ObjParm.Add("@AllowDepartment", profile.AllowDepartment);
                ObjParm.Add("@TimezoneId", profile.TimezoneId);
                ObjParm.Add("@UserTypeText", profile.UserTypeText);
                ObjParm.Add("@IsUpdated", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    await con.QueryAsync("dbo.SSP_updateProfileDetails", ObjParm, commandType: CommandType.StoredProcedure);
                    success = ObjParm.Get<bool>("@IsUpdated");
                }

                return success;

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
                CompanyMemberDetails memberDetails = new CompanyMemberDetails();
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    memberDetails = await con.QueryFirstOrDefaultAsync<CompanyMemberDetails>("dbo.SSP_getCompanyMemberDetails", new
                    {
                        UserId = userId
                    }, commandType: CommandType.StoredProcedure);
                }
                return memberDetails;
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
                string fileName = file.FileName;
                string uniqueFileName = null;
                if (file.FileName.Length > 20)
                {
                    string extension = Path.GetExtension(file.FileName);
                    fileName = file.FileName.ToString().Substring(0, 20) + extension;
                }
                uniqueFileName = Guid.NewGuid().ToString().Substring(0, 25) + "_" + fileName;
                string filePath = Path.Combine(folderPath, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return uniqueFileName;
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
                UserProfile profile = await this.therapistContext.UserProfile.FirstOrDefaultAsync(x => x.UserId.Equals(userId));
                profile.ProfilePhoto = name;
                await this.therapistContext.SaveChangesAsync();
                return true;
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
                List<ListVm> lists = await this.therapistContext.MasterCountry.Where(x => x.IsActive ?? true).Select(y => new ListVm { Value = (int)y.CountryId, ViewValue = y.CountrName }).ToListAsync();
                return lists;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<DepartmentListVm>> GetDepartments()
        {
            try
            {
                List<DepartmentListVm> lists = await this.therapistContext.MasterDepartment.Where(x => x.IsActive ?? true).Select(y => new DepartmentListVm { Value = (int)y.DepartmentId, ViewValue = y.DepartmentName, Code = y.DepartmentCode }).ToListAsync();
                return lists;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<ListVm>> GetStates()
        {
            try
            {
                List<ListVm> lists = await this.therapistContext.MasterState.Where(x => x.IsActive ?? true).Select(y => new ListVm { Value = (int)y.StateId, ViewValue = y.StateName, ForeignValue = (int)y.CountryId }).ToListAsync();
                return lists;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> SaveCardDetails(BillingInfo billingInfo)
        {
            try
            {
                bool success = false;
                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@UserId", billingInfo.UserId);
                ObjParm.Add("@IsPrimary", billingInfo.IsPrimary);
                ObjParm.Add("@CardId", billingInfo.CardId);
                ObjParm.Add("@TokenId", billingInfo.TokenId);
                ObjParm.Add("@NameOnCard", billingInfo.NameOnCard);
                ObjParm.Add("@IsAdded", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    await con.QueryAsync("dbo.SSP_saveCardDetails", ObjParm, commandType: CommandType.StoredProcedure);
                    success = ObjParm.Get<bool>("@IsAdded");
                }
                return success;
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
                IEnumerable<BillingInfo> creditCards;
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    creditCards = await con.QueryAsync<BillingInfo>("dbo.SSP_getCardDetails", new
                    {
                        UserId = userId
                    }, commandType: CommandType.StoredProcedure);
                }
                return creditCards;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> SessionBooking(SessionBooking booking)
        {
            try
            {
                this.therapistContext.SessionBooking.Add(booking);
                await this.therapistContext.SaveChangesAsync();
                return true;
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
                UserCreditCard userCredit = await this.therapistContext.UserCreditCard.FirstOrDefaultAsync(x => x.UserCreditCardId.Equals(Id));
                userCredit.IsActive = false;
                userCredit.IsDeleted = true;
                userCredit.ModifiedDate = DateTime.UtcNow;
                await this.therapistContext.SaveChangesAsync();
                return true;
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<SubscriptionInfo> SubscriptionInfo(int userId)
        {
            try
            {
                SubscriptionInfo info = new SubscriptionInfo(); ;
                info = await (from membership in this.therapistContext.UserMembership
                              join plan in this.therapistContext.MasterPlan
                              on membership.PlanId equals plan.PlanId
                              where membership.UserId == userId && membership.IsActive == true && membership.IsDeleted == false
                              select new SubscriptionInfo
                              {
                                  UserMembershipId = membership.UserMembershipId,
                                  MembershipStartDate = membership.MembershipStartDate,
                                  MembershipEndDate = membership.MembershipEndDate,
                                  IsRecurring = membership.IsRecurring,
                                  PlanId = membership.PlanId,
                                  Active = membership.ActiveMembershipPlan == 1 ? true : false,
                                  PlanName = plan.PlanName,
                                  PlanAmount = plan.PlanAmount,
                                  PlanDuration = plan.PlanDuration,
                              }).FirstOrDefaultAsync();
                return info;
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
                PractitionerSession session = new PractitionerSession();
                session = await (from sesion in this.therapistContext.Session
                                 join topic in this.therapistContext.MasterTopic
                                 on sesion.TopicId equals topic.TopicId
                                 join sessiontype in this.therapistContext.MasterSessionType
                                 on sesion.SessionType equals sessiontype.TypeId
                                 where sesion.SessionId.Equals(sessionId) && sesion.IsActive.Equals(true) && sesion.IsDeleted.Equals(false)
                                 select new PractitionerSession
                                 {
                                     SessionId = sesion.SessionId,
                                     UserId = sesion.UserId,
                                     SessionTitle = sesion.SessionTitle,
                                     TopicName = topic.TopicName,
                                     SessionType = sessiontype.TypeName,
                                     SessionShotDescription = sesion.SessionShotDescription,
                                     SessionDescription = sesion.SessionDescription,
                                     SessionDate = sesion.SessionDate,
                                     SessionTime = sesion.SessionTime,
                                     SessionLength = sesion.SessionLength,
                                     NumberOfSeats = sesion.NumberOfSeats,
                                     SeatPrice = sesion.SeatPrice,
                                     PublishDate = sesion.PublishDate,
                                     PublishTime = sesion.PublishTime
                                 }).FirstOrDefaultAsync();
                return session;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<Billing> GetBillingInfo(int Id)
        {
            try
            {

                Billing billing = new Billing();
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    billing = await con.QueryFirstOrDefaultAsync<Billing>("dbo.SSP_getBillingDetails", new
                    {
                        UserId = Id,

                    }, commandType: CommandType.StoredProcedure);
                }
                return billing;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> SaveBillingInfo(Billing billing)
        {
            try
            {
                bool sucsess = false;
                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@UserBillingInformationId", billing.UserBillingInformationId);
                ObjParm.Add("@UserId", billing.UserId);
                ObjParm.Add("@FirstName", billing.FirstName);
                ObjParm.Add("@LastName", billing.LastName);
                ObjParm.Add("@City", billing.City);
                ObjParm.Add("@State", billing.State);
                ObjParm.Add("@Country", billing.Country);
                ObjParm.Add("@ZipCode", billing.ZipCode);
                ObjParm.Add("@Address", billing.Address);
                ObjParm.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    await con.QueryAsync("dbo.SSP_saveBillingDetails", ObjParm
                   , commandType: CommandType.StoredProcedure);
                    sucsess = ObjParm.Get<bool>("@Success");
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> UpdatePassword(ChangePasswordRequestModel model)
        {
            try
            {

                bool success = false;
                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@Password", model.NewPassword);
                ObjParm.Add("@UserId", model.UserId);
                ObjParm.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    await con.QueryAsync("dbo.SSP_ChangePassword", ObjParm, commandType: CommandType.StoredProcedure);
                    success = ObjParm.Get<bool>("@Success");
                }

                if (success)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        /// <summary>
        /// Get user Detail By UserId
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="companyId"></param>
        /// <param name="userId"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async Task<User> GetUserByUserId(long UserId)
        {
            try
            {
                var userInfo = new User();

                using (var dbcontext = this.therapistContext)
                {
                    using (IDbConnection con = new SqlConnection(_connectionString))
                    {
                        userInfo = await con.QueryFirstOrDefaultAsync<User>("dbo.SSP_getUserDetails", new
                        {
                            UserId = UserId,
                        }, commandType: CommandType.StoredProcedure);
                    }
                    return userInfo;
                }

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> Logout(long UserId)
        {
            try
            {
                bool result = false;
                UserLogActivity logActivity = new UserLogActivity();
                logActivity.UserLogActivityGuid = System.Guid.NewGuid();
                logActivity.UserId = UserId;
                logActivity.LogActivityId = 2;
                logActivity.CreatedDate = DateTime.UtcNow;
                this.therapistContext.UserLogActivity.Add(logActivity);
                await this.therapistContext.SaveChangesAsync();
                result = true;
                if (result)
                {
                    //  Users updateStatus = new Users();
                    var updateStatus = await this.therapistContext.Users.FirstOrDefaultAsync(x => x.UserId == UserId && x.IsActive == true && x.IsDeleted == false && x.IsBlocked == false);
                    if (updateStatus != null)
                    {
                        updateStatus.UnsuccessfulAttempt = 0;
                        updateStatus.ModifiedDate = DateTime.UtcNow;
                        this.therapistContext.Users.Update(updateStatus);
                        await this.therapistContext.SaveChangesAsync();
                    }
                    result = true;
                }
                return result;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<IEnumerable<UsersSessionList>> GetUsersSessionList(FilteredSessionListRequest sessionListRequest)
        {
            try
            {
                IEnumerable<UsersSessionList> UsersSessionLists = new List<UsersSessionList>();

                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    UsersSessionLists = await con.QueryAsync<UsersSessionList>("SSP_getSessionListByUserId", new
                    {
                        UserId = sessionListRequest.MemberId,
                        SessionValue = sessionListRequest.SessionValue,
                        Category = sessionListRequest.Category,
                        SessionName = sessionListRequest.SessionName,
                        Type = sessionListRequest.Type,
                        StartDate = sessionListRequest.StartDate,
                        EndDate = sessionListRequest.EndDate,
                        OffSet = sessionListRequest.OffSet,
                        Limit = sessionListRequest.Limit,
                    }, commandType: CommandType.StoredProcedure);
                }

                if (UsersSessionLists.Count() == 0)
                    return null;

                return UsersSessionLists;
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
                SessionActivation activation = new SessionActivation();
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    activation = await con.QueryFirstOrDefaultAsync<SessionActivation>("dbo.SSP_getSessionActivationDetails", new
                    {
                        SessionID = sessionId
                    }, commandType: CommandType.StoredProcedure);
                }
               
                return activation;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
