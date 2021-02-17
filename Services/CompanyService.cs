using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Dtos;
using Domain.Entities;
using Infrastructure.Implemenatations.Context;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Services
{
    public class CompanyService : ICompany
    {
        private readonly ICompanyRepo _companyRepo;
        private readonly IAuthRepo authRepo;
        private readonly ICommonEmailsService commonEmailsService;
        public CompanyService(ICompanyRepo companyRepo, IAuthRepo _authRepo, ICommonEmailsService ICommonEmailsService)
        {
            this._companyRepo = companyRepo;
            authRepo = _authRepo;
            commonEmailsService = ICommonEmailsService;
        }
        public async Task<CompanyInfo> GetCompanyInfo(int userId)
        {
            try
            {
                return await this._companyRepo.GetCompanyInfo(userId);
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
                return await this._companyRepo.GetAccountInfo(userId);
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
                return await this._companyRepo.BulkUpload(uploads);
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
                return await this._companyRepo.UpdateCard(updateCardRequest);
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
                return await this._companyRepo.getCompanyRatePlan(userId);
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<UserCompanyInfo> getUserCompanyInfo(int comapnayId)
        {
            try
            {
                return await this._companyRepo.getUserCompanyInfo(comapnayId);
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
                return await this._companyRepo.GetCompanyUsers(usersRequest);
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
                return await this._companyRepo.GetCompanyInviteUsers(usersRequest);
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
                return await this._companyRepo.ChangeActiveStatus(members);
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
                return await this._companyRepo.DeleteUser(userId);

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
                return await this._companyRepo.DeleteInviteUser(user);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> InviteUsers(int companyId)
        {
            try
            {
                return await this._companyRepo.InviteUsers(companyId);
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
                return await this._companyRepo.GetCompanies();
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
                return await this._companyRepo.getEmailLogs(userId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<int> InviteIndividualUser(CompanyUsersRequest usersRequest, HttpContext context)
        {
            try
            {
                int returnResult = 0;
                bool emailExist = await this.authRepo.EmailExist(usersRequest.Email);
                if (emailExist)
                {
                    returnResult = 2;
                    return returnResult; // Email Exist
                }
                else
                {
                    Guid guid = Guid.NewGuid();
                    InviteUsersList inviteUser = new InviteUsersList();
                    inviteUser.EmailAddress = usersRequest.Email;
                    inviteUser.ActivationToken = guid;
                    InviteEmailData response = await this.commonEmailsService.SendUserInviteEmails(inviteUser, context);
                    if (response.InviteStatus == 1)
                        returnResult = await this._companyRepo.InviteIndividualUser(usersRequest, guid);
                }
                return returnResult;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
