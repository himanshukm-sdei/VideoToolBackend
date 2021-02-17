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
    public class PractitionerService : IPractitionerService
    {
        private readonly IPractitionerRepo practitionerRepo;
        private readonly ISuperAdminRepo _superAdminRepo;
        private readonly ICommonEmailsService commonEmailsService;
        public PractitionerService(IPractitionerRepo practitionerRepo, ISuperAdminRepo superAdminRepo, ICommonEmailsService ICommonEmailsService)
        {
            this.practitionerRepo = practitionerRepo;
            _superAdminRepo = superAdminRepo;
            commonEmailsService = ICommonEmailsService;
        }
        public async Task<ProfileInfo> GetUserProfile(int? userId, Guid? guid)
        {
            try
            {
                var profile = await this.practitionerRepo.GetUserProfile(userId, guid);
                return profile;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<long> CreateSession(SessionRequest sessionRequest)
        {
            try
            {
                if (sessionRequest.PublishDate == null)
                {
                    sessionRequest.PublishDate = DateTime.UtcNow;
                    sessionRequest.PublishTime = DateTime.UtcNow;
                }


                Session session = new Session
                {
                    SessionId = 0,
                    SessionGuid = System.Guid.NewGuid(),
                    UserId = sessionRequest.UserId,
                    SessionTitle = sessionRequest.SessionTitle,
                    TopicId = sessionRequest.TopicId,
                    SessionType = sessionRequest.SessionType,
                    SessionShotDescription = sessionRequest.SessionShotDescription,
                    SessionDescription = sessionRequest.SessionDescription,
                    SessionDate = sessionRequest.SessionDate,
                    SessionTime = sessionRequest.SessionTime,
                    SessionLength = sessionRequest.SessionLength,
                    NumberOfSeats = sessionRequest.NumberOfSeats,
                    SeatPrice = sessionRequest.SeatPrice,
                    IsWaitingList = sessionRequest.IsWaitingList,
                    IsWaitingVideoRecording = sessionRequest.IsWaitingVideoRecording,
                    PublishDate = sessionRequest.PublishDate,
                    PublishTime = sessionRequest.PublishTime,
                    IsActive = true,
                    SessionStatus = 0,
                    AcceptedDate = null,
                    AcceptedBy = null,
                    CreatedDate = DateTime.UtcNow,
                    ModifiedDate = null,
                    CreatedBy = sessionRequest.CreatedBy,
                    ModifiedBy = null,
                    IsDeleted = false,
                    FirstResponder = sessionRequest.FirstResponder,
                    TimezoneId = sessionRequest.TimezoneId,
                    IsAccepted = sessionRequest.IsAccepted
                };
                return await this.practitionerRepo.CreateSession(session);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<long> UpdateSession(SessionRequest sessionRequest)
        {
            try
            {
                if (sessionRequest.PublishDate == null)
                {
                    sessionRequest.PublishDate = DateTime.UtcNow;
                    sessionRequest.PublishTime = DateTime.UtcNow;
                }
                return await this.practitionerRepo.UpdateSession(sessionRequest);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> SaveTags(long sessionId, int userId, int[] tags)
        {
            try
            {

                return await this.practitionerRepo.SaveTags(sessionId, userId, tags);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<int> UpdateTags(long sessionId, int userId, int[] tags)
        {
            try
            {

                return await this.practitionerRepo.UpdateTags(sessionId, userId, tags);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<SesionCounts> GetAllSessionCountMember(int memberId)
        {
            try
            {
                return await this.practitionerRepo.GetAllSessionCountMember(memberId);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<List<ListVm>> GetTags()
        {
            try
            {
                return await this.practitionerRepo.GetTags();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<ListVm>> GetTopics()
        {
            try
            {
                return await this.practitionerRepo.GetTopics();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<ListVm>> GetTimezones()
        {
            try
            {
                return await this.practitionerRepo.GetTimezones();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<CategoryList>> GetSessionList(SessionListRequest request, HttpContext context)
        {
            try
            {
                return await this.practitionerRepo.GetSessionList(request, context);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<SessionList>> PractitionerRelated(int practitionerId, int userid, HttpContext context)
        {
            try
            {
                return await this.practitionerRepo.PractitionerRelated(practitionerId, userid, context);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<CategoryList>> GetSessionListMember(SessionListRequest request, HttpContext context)
        {
            try
            {
                return await this.practitionerRepo.GetSessionListMember(request, context);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<IEnumerable<SessionList>> GetFilteredSessionList(FilteredSessionListRequest request, HttpContext context)
        {
            try
            {
                return await this.practitionerRepo.GetFilteredSessionList(request, context);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<CategoryList>> GetAllSession(SessionListRequest request, HttpContext context)
        {
            try
            {
                return await this.practitionerRepo.GetAllSession(request, context);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<CategoryList>> GetAllSessionMember(SessionListRequest request, HttpContext context)
        {
            try
            {
                return await this.practitionerRepo.GetAllSessionMember(request, context);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<SessionDetails> SessionDetails(Guid guid, int? userId, HttpContext context)
        {
            try
            {
                return await this.practitionerRepo.SessionDetails(guid, userId, context);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<SessionBookingData> SessionBookingDetails(Guid guid, int userId)
        {
            try
            {
                return await this.practitionerRepo.SessionBookingDetails(guid, userId);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> BookSession(Guid guid, int userId, HttpContext httpContext)
        {
            try
            {
                return await this.practitionerRepo.BookSession(guid, userId, httpContext);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> EnableNotification(NotificationRequest notification, HttpContext httpContext)
        {
            try
            {
                return await this.practitionerRepo.EnableNotification(notification, httpContext);
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> CancelBookedSession(int bookedId, int sessionId, int userId, HttpContext httpContext)
        {
            try
            {
                return await this.practitionerRepo.CancelBookedSession(bookedId, sessionId, userId, httpContext);

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<SessionList>> GetPractitionerSessionList(FilteredSessionListRequest request, HttpContext context)
        {
            try
            {
                return await this.practitionerRepo.GetPractitionerSessionList(request, context);
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> SessionvideoAssign(Guid guid, int videoId, int userId)
        {
            try
            {
                return await this.practitionerRepo.SessionvideoAssign(guid, videoId, userId);

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> CancelSessionByPractitioner(SessionBookingRequest model, HttpContext httpContext)
        {
            try
            {
                CompanySessionsRequest usersRequest = new CompanySessionsRequest();
                usersRequest.SessionId = model.SessionId;
                usersRequest.UserId = model.UserId;

                IEnumerable<CompanySessionsList> result = await this._superAdminRepo.GetAttendeeList(usersRequest, httpContext);
                if (result != null)
                {
                    foreach (var item in result)
                    {
                        var sendEmail = await commonEmailsService.CancelSessionByPractitioner(item, httpContext);

                    }
                    //     return await this.practitionerRepo.CancelSessionByPractitioner(SessionId, httpContext);

                }
                await this.practitionerRepo.UpdateSessionStatus(usersRequest);

                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //public async Task<SesionCounts> GetAllSessionCountPractitioner(int practitionerID)
        //{
        //    try
        //    {
        //        return await this.practitionerRepo.GetAllSessionCountPractitioner(practitionerID);
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        public async Task<bool> SessionStartByPractitioner(SessionBookingRequest model, HttpContext httpContext)
        {
            try
            {
                CompanySessionsRequest usersRequest = new CompanySessionsRequest();
                usersRequest.SessionId = model.SessionId;
                usersRequest.UserId = model.UserId;
                await this.practitionerRepo.SessionStartByPractitioner(usersRequest);
                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
