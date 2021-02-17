using Application.Dtos;
using Domain.Entities;
using Infrastructure.Implemenatations.Context;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Services
{
    public interface ICompany
    {
        Task<CompanyInfo> GetCompanyInfo(int userId);
        Task<AccountInfo> GetAccountInfo(int userId);
        Task<List<CompanyUpload>> BulkUpload(List<CompanyUpload> uploads);
        Task<bool> UpdateCard(UpdateCardRequest updateCardRequest);
        Task<RatePlanInfo> getCompanyRatePlan(int userId);
        Task<UserCompanyInfo> getUserCompanyInfo(int comapnayId);
        //Task<IEnumerable<CompanyMembers>> GetCompanyUsers(int userId);
        Task<IEnumerable<CompanyInviteUsersList>> GetCompanyInviteUsers(CompanyUsersRequest usersRequest);
        Task<IEnumerable<CompanyMembers>> GetCompanyUsers(CompanyUsersRequest usersRequest);
        Task<bool> ChangeActiveStatus(CompanyMembers members);
        Task<bool> DeleteUser(int userId);
        Task<bool> DeleteInviteUser(CompanyInviteUsersList user);
        Task<bool> InviteUsers(int companyId);
        Task<IEnumerable<Dtos.UserEmailLogging>> getEmailLogs(int userId);
        Task<IEnumerable<ListVm>> GetCompanies();
        Task<int> InviteIndividualUser(CompanyUsersRequest User, HttpContext context);

    }
}
