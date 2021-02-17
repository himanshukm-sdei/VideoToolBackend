
using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Dtos;
using Dapper;
using Domain.Entities;
using Infrastructure.Implemenatations.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace Repository
{
    public class CompanyRepo : ICompanyRepo
    {

        private readonly therapistContext therapistContext;
        private readonly IConfiguration config;
        private string _connectionString;
        private ICommonEmailsService commonEmailsService;

        public CompanyRepo(therapistContext therapistContext, IConfiguration configuration, ICommonEmailsService ICommonEmailsService)
        {
            this.therapistContext = therapistContext;
            config = configuration;
            _connectionString = configuration.GetConnectionString("DBCNSTR");
            commonEmailsService = ICommonEmailsService;
        }

        public async Task<CompanyInfo> GetCompanyInfo(int userId)
        {
            try
            {
                CompanyInfo companyInfo = new CompanyInfo();
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    companyInfo = await con.QueryFirstOrDefaultAsync<CompanyInfo>("dbo.SSP_getCompanyDetails", new
                    {
                        UserId = userId
                    }, commandType: CommandType.StoredProcedure);
                }

                return companyInfo;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<AccountInfo> GetAccountInfo(int userId)
        {
            try
            {
                AccountInfo accountInfo = new AccountInfo();
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    accountInfo = await con.QueryFirstOrDefaultAsync<AccountInfo>("dbo.SSP_getAccountDetails", new
                    {
                        UserId = userId
                    }, commandType: CommandType.StoredProcedure);
                }
                return accountInfo;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<CompanyUpload>> BulkUpload(List<CompanyUpload> uploads)
        {
            try
            {
                await this.therapistContext.CompanyUpload.AddRangeAsync(uploads);
                await this.therapistContext.SaveChangesAsync();
                return uploads;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> UpdateCard(UpdateCardRequest updateCardRequest)
        {
            try
            {
                if (updateCardRequest.value)
                {
                    List<UserCreditCard> creditCards = await this.therapistContext.UserCreditCard.Where(x => x.UserId == updateCardRequest.userId).ToListAsync();
                    foreach (var item in creditCards)
                    {
                        if (item.UserCreditCardId == updateCardRequest.userCreditCardId)
                            item.IsPrimary = true;
                        else
                            item.IsPrimary = false;
                    }
                }
                else
                {
                    UserCreditCard creditCard = await this.therapistContext.UserCreditCard.Where(x => x.UserCreditCardId == updateCardRequest.userCreditCardId).FirstOrDefaultAsync();
                    creditCard.IsPrimary = false;
                }
                await this.therapistContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<RatePlanInfo> getCompanyRatePlan(int userId)
        {
            try
            {
                RatePlanInfo info = new RatePlanInfo();
                info = await (from rate in this.therapistContext.CompanyRatePlan
                              join plan in this.therapistContext.MasterPlan
                              on rate.PlanId equals plan.PlanId
                              join usercompany in this.therapistContext.UserCompany
                              on rate.CompanyId equals usercompany.CompanyId
                              where usercompany.UserId == userId && usercompany.IsActive == true && usercompany.IsDeleted == false && rate.IsActive == true && rate.IsDeleted == false && rate.ActivePlan == 1
                              select new RatePlanInfo
                              {
                                  PlanName = plan.PlanName,
                                  PlanAmount = plan.PlanAmount,
                                  PlanStartDate = rate.PlanStartDate,
                                  PlanEndDate = rate.PlanEndDate,
                                  IsRecurring = rate.IsRecurring,
                                  Active = rate.ActivePlan == 1 ? true : false,
                                  PlanId = plan.PlanId,
                                  CompanyRateTableId = rate.CompanyRateTableId
                              }).FirstOrDefaultAsync();
                return info;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<bool> InviteUsers(int companyId)
        {
            try
            {
                bool success = false;
                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@CompanyId ", companyId);
                ObjParm.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    await con.QueryAsync("dbo.SSP_insertInviteUserDetails", ObjParm, commandType: CommandType.StoredProcedure);
                    success = ObjParm.Get<bool>("@Success");
                }
                return success;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<UserCompanyInfo> getUserCompanyInfo(int comapnayId)
        {
            try
            {
                UserCompanyInfo companyInfo = new UserCompanyInfo();
                companyInfo = await (from usercompany in this.therapistContext.UserCompany
                                     join company in this.therapistContext.Company
                                     on usercompany.CompanyId equals company.CompanyId
                                     where usercompany.CompanyId == comapnayId && usercompany.IsActive == true && usercompany.IsDeleted == false
                                     select new UserCompanyInfo
                                     {
                                         UserCompanyGuid = usercompany.UserCompanyGuid,
                                         UserId = usercompany.UserId,
                                         CompanyId = company.CompanyId,
                                         CompanyUserNameCode = company.CompanyUserNameCode
                                     }).FirstOrDefaultAsync();


                return companyInfo;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<CompanyMembers>> GetCompanyUsers(CompanyUsersRequest usersRequest)
        {
            try
            {
                IEnumerable<CompanyMembers> userCompany = null;
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    userCompany = await con.QueryAsync<CompanyMembers>("dbo.SSP_getCompanyUsersList", new
                    {
                        CompanyId = usersRequest.CompanyId,
                        FirstName = usersRequest.FirstName,
                        LastName = usersRequest.LastName,
                        Email = usersRequest.Email,
                        OffSet = usersRequest.OffSet,
                        Limit = usersRequest.Limit,
                    }, commandType: CommandType.StoredProcedure);
                }
                return userCompany;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<CompanyInviteUsersList>> GetCompanyInviteUsers(CompanyUsersRequest usersRequest)
        {
            try
            {
                IEnumerable<CompanyInviteUsersList> userCompany = null;
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    userCompany = await con.QueryAsync<CompanyInviteUsersList>("dbo.SSP_getCompanyInviteUsersList",
                        new
                        {
                            FirstName = usersRequest.FirstName,
                            LastName = usersRequest.LastName,
                            Email = usersRequest.Email,
                            OffSet = usersRequest.OffSet,
                            Limit = usersRequest.Limit,
                        }, commandType: CommandType.StoredProcedure);
                }
                return userCompany;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> ChangeActiveStatus(CompanyMembers members)
        {
            try
            {
                Users users = await this.therapistContext.Users.Where(x => x.UserId == members.UserId).FirstOrDefaultAsync();
                if (members.Block ?? true)
                {
                    users.IsBlocked = members.IsBlocked;
                }
                else
                {
                    users.IsActive = members.IsActive;
                }
                await this.therapistContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> DeleteUser(int userId)
        {
            try
            {
                Users users = await this.therapistContext.Users.Where(x => x.UserId == userId).FirstOrDefaultAsync();
                users.IsDeleted = true;
                await this.therapistContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> DeleteInviteUser(CompanyInviteUsersList user)
        {
            try
            {
                UserInvites userInvites = await this.therapistContext.UserInvites.Where(x => x.UserInviteGuid == user.UserInviteGuid).FirstOrDefaultAsync();
                userInvites.IsDeleted = true;
                await this.therapistContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<Application.Dtos.UserEmailLogging>> getEmailLogs(int userId)
        {
            try
            {
                IEnumerable<Application.Dtos.UserEmailLogging> emailDetails = null;
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    emailDetails = await con.QueryAsync<Application.Dtos.UserEmailLogging>("dbo.SSP_getEmailHistory", new
                    {
                        UserId = userId
                    }, commandType: CommandType.StoredProcedure);
                }

                return emailDetails;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<ListVm>> GetCompanies()
        {
            try
            {
                IEnumerable<ListVm> list = new List<ListVm>();
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    list = await con.QueryAsync<ListVm>("dbo.SSP_getCompanies", new
                    {
                    }, commandType: CommandType.StoredProcedure);
                }
                return list;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<int> InviteIndividualUser(CompanyUsersRequest usersRequest, Guid guid)
        {
            try
            {
                int status = 0;
                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@FirstName", usersRequest.FirstName);
                ObjParm.Add("@LastName", usersRequest.LastName);
                ObjParm.Add("@CompanyId", usersRequest.CompanyId);
                ObjParm.Add("@EmailAddress", usersRequest.Email);
                ObjParm.Add("@ActivationToken", guid);
                ObjParm.Add("@EmailSender", config.GetValue<string>("AuthMessageSenderOptions:SendGridEmail"));
                ObjParm.Add("@EmailSubject", config.GetValue<string>("AuthMessageSenderOptions:WelcomeEmail"));
                ObjParm.Add("@ReturnStatus", dbType: DbType.Int32, direction: ParameterDirection.Output);
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    await con.QueryAsync("dbo.SSP_addIndividualInviteUser", ObjParm, commandType: CommandType.StoredProcedure);
                    status = ObjParm.Get<int>("@ReturnStatus");
                }
                return status;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
