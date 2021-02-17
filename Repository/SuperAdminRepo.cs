
using Application.Abstractions.Repositories;
using Application.Dtos;
using Dapper;
using Domain.Entities;
using Infrastructure.Implemenatations.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using static Common.Common.CommonUtility;

namespace Repository
{
    public class SuperAdminRepo : ISuperAdminRepo
    {

        private readonly therapistContext therapistContext;
        private readonly IConfiguration config;
        private string _connectionString;
        public SuperAdminRepo(therapistContext therapistContext, IConfiguration configuration)
        {
            this.therapistContext = therapistContext;
            config = configuration;
            _connectionString = configuration.GetConnectionString("DBCNSTR");
        }
        public async Task<bool> SaveCompanyDetails(CompanyDetails companyDetails)
        {
            try
            {
                bool success = false;
                DynamicParameters ObjParm = new DynamicParameters();
                //company 
                ObjParm.Add("@CompanyAddress", companyDetails.CompanyAddress);
                ObjParm.Add("@CompanyCity", companyDetails.CompanyCity);
                ObjParm.Add("@CompanyCode", companyDetails.CompanyCode);
                ObjParm.Add("@CompanyCountry", companyDetails.CompanyCountry);
                ObjParm.Add("@CompanyLogo", companyDetails.CompanyLogo);
                ObjParm.Add("@CompanyName", companyDetails.CompanyName);
                ObjParm.Add("@CompanyPhone", companyDetails.CompanyPhone);
                ObjParm.Add("@CompanyState", companyDetails.CompanyState);
                ObjParm.Add("@CompanyUserCode", companyDetails.CompanyUserCode);
                ObjParm.Add("@CompanyWebsite", companyDetails.CompanyWebsite);
                ObjParm.Add("@CompanyZipCode", companyDetails.CompanyZipCode);
                ObjParm.Add("@RepresentativeId", companyDetails.RepresentativeId);

                //Billing Info
                ObjParm.Add("@CompanyName", companyDetails.CompanyName);
                ObjParm.Add("@CompanyBillingName", companyDetails.CompanyBillingName);
                ObjParm.Add("@FirstName", companyDetails.FirstName);
                ObjParm.Add("@LastName", companyDetails.LastName);
                ObjParm.Add("@City", companyDetails.City);
                ObjParm.Add("@State", companyDetails.State);
                ObjParm.Add("@Country", companyDetails.Country);
                ObjParm.Add("@ZipCode", companyDetails.ZipCode);
                ObjParm.Add("@Address", companyDetails.Address);
                //Company rate
                ObjParm.Add("@PlanID", companyDetails.PlanID);

                // company admin contact
                ObjParm.Add("@AdminFirstName", companyDetails.AdminFirstName);
                ObjParm.Add("@AdminLastName", companyDetails.AdminLastName);
                ObjParm.Add("@Email", companyDetails.Email);
                ObjParm.Add("@MobilePhoneNo", companyDetails.MobilePhoneNo);
                ObjParm.Add("@WorkPhoneNo", companyDetails.WorkPhoneNo);
                ObjParm.Add("@Position", companyDetails.Position);
                ObjParm.Add("@Password", companyDetails.Password);
                ObjParm.Add("@UserName", companyDetails.UserName);
                //common fields
                ObjParm.Add("@CreatedBy", companyDetails.CreatedBy);
                ObjParm.Add("@CreatedDate", companyDetails.CreatedDate);
                ObjParm.Add("@IsActive", companyDetails.IsActive);
                ObjParm.Add("@IsBlocked", companyDetails.IsBlocked);
                ObjParm.Add("@IsDeleted", companyDetails.IsDeleted);
                ObjParm.Add("@ModifiedBy", companyDetails.ModifiedBy);
                ObjParm.Add("@ModifiedDate", companyDetails.ModifiedDate);
                ObjParm.Add("@RoleId", Convert.ToInt32(RolesEnum.Admin));
                ObjParm.Add("@CompanyUserNameCode", "SDM");

                ObjParm.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    await con.QueryAsync("dbo.SSP_saveCompanyDetails", ObjParm, commandType: CommandType.StoredProcedure);
                    success = ObjParm.Get<bool>("@Success");
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
                bool success = false;
                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@CompanyID", companyDetails.CompanyID);
                ObjParm.Add("@UserId", companyDetails.UserId);
                //company 
                ObjParm.Add("@CompanyAddress", companyDetails.CompanyAddress);
                ObjParm.Add("@CompanyCity", companyDetails.CompanyCity);
                ObjParm.Add("@CompanyCode", companyDetails.CompanyCode);
                ObjParm.Add("@CompanyCountry", companyDetails.CompanyCountry);
                ObjParm.Add("@CompanyLogo", companyDetails.CompanyLogo);
                ObjParm.Add("@CompanyName", companyDetails.CompanyName);
                ObjParm.Add("@CompanyPhone", companyDetails.CompanyPhone);
                ObjParm.Add("@CompanyState", companyDetails.CompanyState);
                ObjParm.Add("@CompanyUserCode", companyDetails.CompanyUserCode);
                ObjParm.Add("@CompanyWebsite", companyDetails.CompanyWebsite);
                ObjParm.Add("@CompanyZipCode", companyDetails.CompanyZipCode);
                ObjParm.Add("@RepresentativeId", companyDetails.RepresentativeId);

                //Billing Info
                ObjParm.Add("@CompanyName", companyDetails.CompanyName);
                ObjParm.Add("@CompanyBillingName", companyDetails.CompanyBillingName);
                ObjParm.Add("@FirstName", companyDetails.FirstName);
                ObjParm.Add("@LastName", companyDetails.LastName);
                ObjParm.Add("@City", companyDetails.City);
                ObjParm.Add("@State", companyDetails.State);
                ObjParm.Add("@Country", companyDetails.Country);
                ObjParm.Add("@ZipCode", companyDetails.ZipCode);
                ObjParm.Add("@Address", companyDetails.Address);
                //Company rate
                ObjParm.Add("@PlanID", companyDetails.PlanID);

                // company admin contact
                ObjParm.Add("@AdminFirstName", companyDetails.AdminFirstName);
                ObjParm.Add("@AdminLastName", companyDetails.AdminLastName);
                ObjParm.Add("@Email", companyDetails.Email);
                ObjParm.Add("@MobilePhoneNo", companyDetails.MobilePhoneNo);
                ObjParm.Add("@WorkPhoneNo", companyDetails.WorkPhoneNo);
                ObjParm.Add("@Position", companyDetails.Position);

                //common fields
                ObjParm.Add("@IsActive", companyDetails.IsActive);
                ObjParm.Add("@IsBlocked", companyDetails.IsBlocked);
                ObjParm.Add("@IsDeleted", companyDetails.IsDeleted);
                ObjParm.Add("@ModifiedBy", companyDetails.ModifiedBy);
                ObjParm.Add("@ModifiedDate", companyDetails.ModifiedDate);


                ObjParm.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    await con.QueryAsync("dbo.SSP_updateCompanyDetails", ObjParm, commandType: CommandType.StoredProcedure);
                    success = ObjParm.Get<bool>("@Success");
                }
                return true;
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
                IEnumerable<CompanyList> companyDetails;
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    companyDetails = await con.QueryAsync<CompanyList>("dbo.SSP_getCompanyList", commandType: CommandType.StoredProcedure);
                }

                return companyDetails;

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
                IEnumerable<CompanyMembersList> companyMembersDetails;
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    companyMembersDetails = await con.QueryAsync<CompanyMembersList>("dbo.SSP_getCompanyMembersList", new
                    {
                        CompanyId = companyId,
                        RoleId = Convert.ToInt32(RolesEnum.Member)
                    },
                        commandType: CommandType.StoredProcedure);
                }

                return companyMembersDetails;
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
                IEnumerable<PractitionersList> companyDetails;
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    companyDetails = await con.QueryAsync<PractitionersList>("dbo.SSP_getPractitionersList", new
                    {
                        FirstName = practitioners.FirstName,
                        LastName = practitioners.LastName,
                        Email = practitioners.Email,
                        UserName = practitioners.UserName,
                        Keywords = practitioners.Keywords,
                        OffSet = practitioners.OffSet,
                        Limit = practitioners.Limit,
                    }, commandType: CommandType.StoredProcedure);
                }

                return companyDetails;

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
                if (usersRequest.StartDate == null)
                {
                    usersRequest.StartDate = DateTime.Now.ToUniversalTime().ToString("yyyy-MM-dd");
                    usersRequest.EndDate = DateTime.Now.AddYears(1).ToUniversalTime().ToString("yyyy-MM-dd");
                }
                IEnumerable<CompanySessionsList> sessionLists = new List<CompanySessionsList>();
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    sessionLists = await con.QueryAsync<CompanySessionsList>("dbo.SSP_getAllSessionListForAdmin", new
                    {
                        SessionTitle = usersRequest.SessionTitle,
                        SessionType = usersRequest.SessionType,
                        PractitionerName = usersRequest.PractitionerName,
                        OffSet = usersRequest.OffSet,
                        Limit = usersRequest.Limit,
                        StartDate = usersRequest.StartDate,
                        EndDate = usersRequest.EndDate,
                        IsAccepted= usersRequest.IsAccepted
                    }, commandType: CommandType.StoredProcedure);
                }
                foreach (var item in sessionLists)
                {
                    if (item.ProfilePhoto != null)
                    {
                        item.ProfilePhoto = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "//images//" + item.ProfilePhoto;
                    }
                }
                return sessionLists;
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

                IEnumerable<SessionsTypes> sessionTypes = new List<SessionsTypes>();
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    sessionTypes = await con.QueryAsync<SessionsTypes>("dbo.SSP_getSessionTypesList", new
                    {
                        TypeName = usersRequest.TypeName,
                        OffSet = usersRequest.OffSet,
                        Limit = usersRequest.Limit
                    }, commandType: CommandType.StoredProcedure);
                }

                return sessionTypes;
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
                IEnumerable<DepartmentsList> departmentsList = new List<DepartmentsList>();
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    departmentsList = await con.QueryAsync<DepartmentsList>("dbo.SSP_getDepartmentsList", new
                    {
                        DepartmentName = usersRequest.DepartmentName,
                        DepartmentCode = usersRequest.DepartmentCode,
                        OffSet = usersRequest.OffSet,
                        Limit = usersRequest.Limit
                    }, commandType: CommandType.StoredProcedure);
                }

                return departmentsList;
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
                IEnumerable<SessionsTypes> sessionTypes = new List<SessionsTypes>();
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    sessionTypes = await con.QueryAsync<SessionsTypes>("dbo.SSP_getActiveSessionTypes", commandType: CommandType.StoredProcedure);
                }

                return sessionTypes;
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
                MasterSessionType types = await this.therapistContext.MasterSessionType.Where(x => x.TypeId == type.TypeId).FirstOrDefaultAsync();

                types.IsActive = type.IsActive;

                await this.therapistContext.SaveChangesAsync();
                return true;
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
                Session session = await this.therapistContext.Session.Where(x => x.SessionId == request.sessionId).FirstOrDefaultAsync();

                session.IsAccepted = request.IsAccepted;
                session.AcceptedBy = request.AcceptedBy;
                if(request.IsAccepted == 1)
                {
                    session.AcceptedDate = DateTime.Now;
                }
                
               

                await this.therapistContext.SaveChangesAsync();
                return true;
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
                MasterDepartment masterDepartment = await this.therapistContext.MasterDepartment.Where(x => x.DepartmentId == department.DepartmentId).FirstOrDefaultAsync();

                masterDepartment.IsActive = department.IsActive;

                await this.therapistContext.SaveChangesAsync();
                return true;
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
                if (type.TypeId == 0)
                {
                    MasterSessionType sessionType = new MasterSessionType
                    {
                        TypeId = 0,
                        TypeName = type.TypeName,
                        IsActive = true,
                        CreatedDate = DateTime.UtcNow,
                        ModifiedDate = null,
                        CreatedBy = type.CreatedBy,
                        ModifiedBy = null,
                        IsDeleted = false,
                    };
                    this.therapistContext.MasterSessionType.Add(sessionType);
                    await this.therapistContext.SaveChangesAsync();
                    //return sessionType.TypeId;
                }
                else
                {
                    MasterSessionType updateType = this.therapistContext.MasterSessionType.Where(x => x.TypeId == type.TypeId).FirstOrDefault();
                    updateType.TypeName = type.TypeName;
                    updateType.ModifiedBy = type.CreatedBy;
                    updateType.ModifiedDate = DateTime.UtcNow;

                    await this.therapistContext.SaveChangesAsync();
                }
                return true;
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
                var checkCode = this.therapistContext.MasterDepartment.Where(x => x.DepartmentCode == department.DepartmentCode).ToList();
                if (department.DepartmentId > 0)
                {
                    checkCode = this.therapistContext.MasterDepartment.Where(x => x.DepartmentCode == department.DepartmentCode && x.DepartmentId != department.DepartmentId).ToList();
                }
                if (checkCode.Count() == 0)
                {
                    IEnumerable<DepartmentsList> departmentsList = new List<DepartmentsList>();
                    using (IDbConnection con = new SqlConnection(_connectionString))
                    {
                        departmentsList = await con.QueryAsync<DepartmentsList>("dbo.SSP_addUpdateDepartment", new
                        {
                            DepartmentName = department.DepartmentName,
                            DepartmentCode = department.DepartmentCode,
                            DepartmentId = department.DepartmentId,
                            CreatedBy = department.CreatedBy,
                        }, commandType: CommandType.StoredProcedure);
                    }
                    return true;
                }
                else
                {
                    return false;
                }

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
                Session session = await this.therapistContext.Session.Where(x => x.SessionId == sessionId).FirstOrDefaultAsync();
                session.IsDeleted = true;
                await this.therapistContext.SaveChangesAsync();

                List<SessionTag> sessionTags = await this.therapistContext.SessionTag.Where(x => x.SessionId == sessionId).ToListAsync();

                foreach (var item in sessionTags)
                {
                    item.IsDeleted = true;
                }
                await this.therapistContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> DeleteDepartmentById(int typeId)
        {
            try
            {
                MasterDepartment department = await this.therapistContext.MasterDepartment.Where(x => x.DepartmentId == typeId).FirstOrDefaultAsync();
                department.IsDeleted = true;
                await this.therapistContext.SaveChangesAsync();

                return true;
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
                MasterTopic masterTopic = new MasterTopic();
                if (topic.TopicId == 0)
                {
                    masterTopic.TopicId = 0;
                    masterTopic.TopicName = topic.TopicName;
                    masterTopic.CreatedDate = DateTime.UtcNow;
                    masterTopic.CreatedBy = topic.CreatedBy;
                    masterTopic.IsActive = true;
                    masterTopic.IsDeleted = false;
                    this.therapistContext.MasterTopic.Add(masterTopic);
                }
                else
                {
                    MasterTopic master = await this.therapistContext.MasterTopic.Where(x => x.TopicId == topic.TopicId).FirstOrDefaultAsync();
                    master.TopicName = topic.TopicName;
                    master.ModifiedBy = topic.CreatedBy;
                    master.ModifiedDate = DateTime.UtcNow;
                }
                await this.therapistContext.SaveChangesAsync();
                return true;
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
                MasterSessionType sessionType = await this.therapistContext.MasterSessionType.Where(x => x.TypeId == typeId).FirstOrDefaultAsync();
                sessionType.IsDeleted = true;
                await this.therapistContext.SaveChangesAsync();

                return true;
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
                SessionsTypes sessionType = new SessionsTypes();
                MasterSessionType masterSessionType = await this.therapistContext.MasterSessionType.Where(x => x.TypeId == typeId).FirstOrDefaultAsync();
                sessionType.TypeId = masterSessionType.TypeId;
                sessionType.TypeName = masterSessionType.TypeName;
                sessionType.IsActive = masterSessionType.IsActive.Value;
                sessionType.IsDeleted = masterSessionType.IsDeleted.Value;
                return sessionType;
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
                DepartmentData department = new DepartmentData();
                MasterDepartment masterDepartment = await this.therapistContext.MasterDepartment.Where(x => x.DepartmentId == departmentId).FirstOrDefaultAsync();
                department.DepartmentId = Int32.Parse(masterDepartment.DepartmentId.ToString());
                department.DepartmentName = masterDepartment.DepartmentName;
                department.DepartmentCode = masterDepartment.DepartmentCode;

                return department;
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
                TopicList topic = new TopicList();
                MasterTopic master = await this.therapistContext.MasterTopic.Where(x => x.TopicId == TypeId).FirstOrDefaultAsync();
                topic.TopicId = master.TopicId;
                topic.TopicName = master.TopicName;
                topic.IsActive = master.IsActive;
                topic.IsDeleted = master.IsDeleted;
                topic.Total = 0;
                return topic;
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
                IEnumerable<TopicList> topicLists = new List<TopicList>();
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    topicLists = await con.QueryAsync<TopicList>("dbo.SSP_getTopicList", new
                    {
                        TopicName = request.TopicName,
                        OffSet = request.OffSet,
                        Limit = request.Limit
                    }, commandType: CommandType.StoredProcedure);
                }
                return topicLists;
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
                IEnumerable<Application.Dtos.MasterVideos> list = new List<Application.Dtos.MasterVideos>();
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    list = await con.QueryAsync<Application.Dtos.MasterVideos>("dbo.SSP_getVideoList", new
                    {
                        VideoName = request.VideosName,
                        OffSet = request.OffSet,
                        Limit = request.Limit,
                        sessionGuid = request.sessionGuid
                    }, commandType: CommandType.StoredProcedure);

                }
                return list;
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
                MasterTopic masterTopic = new MasterTopic();
                masterTopic = await this.therapistContext.MasterTopic.Where(x => x.TopicId == request.TopicId).FirstOrDefaultAsync();
                masterTopic.IsActive = request.IsActive;
                await this.therapistContext.SaveChangesAsync();
                return true;
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
                MasterTopic masterTopic = await this.therapistContext.MasterTopic.Where(x => x.TopicId == TypeId).FirstOrDefaultAsync();
                masterTopic.IsDeleted = true;
                await this.therapistContext.SaveChangesAsync();
                return true;
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
                
                IEnumerable<CompanySessionsList> sessionLists = new List<CompanySessionsList>();
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    sessionLists = await con.QueryAsync<CompanySessionsList>("dbo.SSP_getSessionAttendeeList", new
                    {
                        SessionId = usersRequest.SessionId
                    }, commandType: CommandType.StoredProcedure);
                }
                foreach (var item in sessionLists)
                {
                    if (item.ProfilePhoto != null)
                    {
                        item.ProfilePhoto = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "\\images\\" + item.ProfilePhoto;
                    }
                }
                return sessionLists;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
