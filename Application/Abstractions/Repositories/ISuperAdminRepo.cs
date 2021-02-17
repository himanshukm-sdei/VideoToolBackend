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
    public interface ISuperAdminRepo
    {
        Task<bool> SaveCompanyDetails(CompanyDetails companyDetails);
        Task<bool> UpdateCompanyDetails(CompanyDetails companyDetails);
        Task<IEnumerable<CompanyList>> GetCompanyList();
        Task<IEnumerable<PractitionersList>> GetPractitionersList(PractitionersRequest practitioners);
        Task<IEnumerable<CompanyMembersList>> GetCompanyMembersList(int companyId);
        Task<IEnumerable<CompanySessionsList>> GetAllSessionsList(CompanySessionsRequest usersRequest, HttpContext context); 
        Task<IEnumerable<SessionsTypes>> GetSessionsTypes(SessionsTypes usersRequest);
        Task<IEnumerable<DepartmentsList>> GetDepartmentsList(DepartmentsList usersRequest);
        Task<IEnumerable<SessionsTypes>> GetActiveSessionTypes();
        Task<bool> ChangeSessionsTypesStatus(SessionsTypes type);
        Task<bool> ChangeSessionsIsAcceptedStatus(sessionstatusData request);
        Task<bool> ChangeDepartmentStatus(DepartmentsList department);
        Task<bool> AddUpdateSessionType(SessionTypeData type);
        Task<bool> AddUpdateDepartment(DepartmentData department);
        Task<bool> DeleteSessionBySessionId(int SessionId);  
        Task<bool> DeleteDepartmentById(int TypeId); 
        Task<bool> DeleteSessionType(int TypeId);
        Task<bool> DeleteTopicById(int TypeId);
        Task<bool> AddorUpdateTopics(TopicRequestdata topic);
        Task<SessionsTypes> GetSessionTypeById(int TypeId);
        Task<TopicList> GetTopicById(int TypeId);
        Task<DepartmentData> GetDepartmentById(int DepartmentData);
        Task<IEnumerable<TopicList>> GetTopicList(TopicRequest request); 
        Task<IEnumerable<Dtos.MasterVideos>> GetvideosList(MasterVideosRequest request);
        Task<bool> ChangeTopicStatus(TopicList request);
        Task<IEnumerable<CompanySessionsList>> GetAttendeeList(CompanySessionsRequest usersRequest, HttpContext context);
    }
}
