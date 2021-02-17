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


namespace Services
{
    public class SuperAdminnService : ISuperAdminService
    {
        private readonly ISuperAdminRepo _superAdminRepo;
        private readonly IAuthRepo authRepo;
        private readonly IAuthService authService;
        private readonly ICommonEmailsService commonEmailsService;

        public SuperAdminnService(IAuthService _authService, ISuperAdminRepo superAdminRepo, IAuthRepo _authRepo, ICommonEmailsService ICommonEmailsService)
        {
            _superAdminRepo = superAdminRepo;
            authRepo = _authRepo;
            authService = _authService;
            commonEmailsService = ICommonEmailsService;
        }
        public async Task<bool> SaveCompanyDetails(CompanyDetails companyDetails, HttpContext context)
        {
            try
            {
                var guid = System.Guid.NewGuid();
                companyDetails.UserName = await this.authRepo.GenerateUserName(guid, companyDetails.AdminFirstName, companyDetails.AdminLastName);
                companyDetails.Password = authService.CreateRandomAlphabet();

                bool success = await _superAdminRepo.SaveCompanyDetails(companyDetails);
                if (success == true)
                {
                    await this.commonEmailsService.SendWelcomeEmailToSuperAdmin(companyDetails.Email, context);
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> UpdateCompanyDetails(CompanyDetails companyDetails)
        {
            try
            {
                bool success = await _superAdminRepo.UpdateCompanyDetails(companyDetails);
                return success;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<CompanyList>> GetCompanyList()
        {
            try
            {
                return await this._superAdminRepo.GetCompanyList();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<CompanyMembersList>> GetCompanyMembersList(int companyId)
        {
            try
            {
                return await this._superAdminRepo.GetCompanyMembersList(companyId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<PractitionersList>> GetPractitionersList(PractitionersRequest practitioners)
        {
            try
            {
                return await this._superAdminRepo.GetPractitionersList(practitioners);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<CompanySessionsList>> GetAllSessionsList(CompanySessionsRequest usersRequest, HttpContext context)
        {
            try
            {
                return await this._superAdminRepo.GetAllSessionsList(usersRequest, context);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<SessionsTypes>> GetSessionsTypes(SessionsTypes usersRequest)
        {
            try
            {
                return await this._superAdminRepo.GetSessionsTypes(usersRequest);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<DepartmentsList>> GetDepartmentsList(DepartmentsList usersRequest)
        {
            try
            {
                return await this._superAdminRepo.GetDepartmentsList(usersRequest);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<SessionsTypes>> GetActiveSessionTypes()
        {
            try
            {
                return await this._superAdminRepo.GetActiveSessionTypes();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> ChangeSessionsTypesStatus(SessionsTypes type)
        {
            try
            {
                return await this._superAdminRepo.ChangeSessionsTypesStatus(type);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        } 
        public async Task<bool> ChangeSessionsIsAcceptedStatus(sessionstatusData request)
        {
            try
            {
                return await this._superAdminRepo.ChangeSessionsIsAcceptedStatus(request);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> ChangeDepartmentStatus(DepartmentsList department)
        {
            try
            {
                return await this._superAdminRepo.ChangeDepartmentStatus(department);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> AddUpdateSessionType(SessionTypeData type)
        {
            try
            {
                return await this._superAdminRepo.AddUpdateSessionType(type);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> AddUpdateDepartment(DepartmentData department)
        {
            try
            {
                return await this._superAdminRepo.AddUpdateDepartment(department);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<DepartmentData> GetDepartmentById(int departmentId)
        {
            try
            {
                return await this._superAdminRepo.GetDepartmentById(departmentId);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> DeleteSessionBySessionId(int sessionId)
        {
            try
            {
                return await this._superAdminRepo.DeleteSessionBySessionId(sessionId);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> DeleteDepartmentById(int departmentId)
        {
            try
            {
                return await this._superAdminRepo.DeleteDepartmentById(departmentId);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> DeleteSessionType(int typeId)
        {
            try
            {
                return await this._superAdminRepo.DeleteSessionType(typeId);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<SessionsTypes> GetSessionTypeById(int typeId)
        {
            try
            {
                return await this._superAdminRepo.GetSessionTypeById(typeId);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> AddorUpdateTopics(TopicRequestdata topic)
        {
            try
            {
                return await this._superAdminRepo.AddorUpdateTopics(topic);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<TopicList>> GetTopicList(TopicRequest request)
        {
            try
            {
                return await this._superAdminRepo.GetTopicList(request);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        } 
        public async Task<IEnumerable<Application.Dtos.MasterVideos>> GetvideosList(MasterVideosRequest request)
        {
            try
            {
                return await this._superAdminRepo.GetvideosList(request);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> ChangeTopicStatus(TopicList request)
        {
            try
            {
                return await this._superAdminRepo.ChangeTopicStatus(request);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<TopicList> GetTopicById(int TypeId)
        {
            try
            {
                return await this._superAdminRepo.GetTopicById(TypeId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> DeleteTopicById(int TypeId)
        {
            try
            {
                return await this._superAdminRepo.DeleteTopicById(TypeId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<IEnumerable<CompanySessionsList>> GetAttendeeList(CompanySessionsRequest usersRequest, HttpContext context)
        {
            try
            {
                return await this._superAdminRepo.GetAttendeeList(usersRequest, context);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
