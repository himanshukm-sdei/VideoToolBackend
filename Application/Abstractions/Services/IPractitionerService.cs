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
    public interface IPractitionerService
    {
        Task<ProfileInfo> GetUserProfile(int? userId, Guid? guid);
        Task<long> CreateSession(SessionRequest request);
        Task<long> UpdateSession(SessionRequest request);
        Task<int> SaveTags(long sessionId, int userId, int[] tags);
        Task<int> UpdateTags(long sessionId, int userId, int[] tags);
        Task<List<ListVm>> GetTags();
        Task<List<ListVm>> GetTopics();
        Task<List<ListVm>> GetTimezones();
        Task<IEnumerable<CategoryList>> GetSessionList(SessionListRequest request, HttpContext context);
        Task<IEnumerable<CategoryList>> GetSessionListMember(SessionListRequest request, HttpContext context);
        Task<IEnumerable<SessionList>> PractitionerRelated(int practitionerId, int userid, HttpContext context);
        Task<IEnumerable<SessionList>> GetFilteredSessionList(FilteredSessionListRequest request, HttpContext context);
        Task<IEnumerable<CategoryList>> GetAllSession(SessionListRequest request, HttpContext context);
        Task<IEnumerable<CategoryList>> GetAllSessionMember(SessionListRequest request, HttpContext context);
        Task<SesionCounts> GetAllSessionCountMember(int memberId);
        Task<SessionDetails> SessionDetails(Guid guid, int? userId, HttpContext context);
        Task<SessionBookingData> SessionBookingDetails(Guid guid, int userId);
        Task<bool> SessionvideoAssign(Guid guid, int videoId, int userId);

        Task<bool> EnableNotification(NotificationRequest notification, HttpContext httpContext);
        Task<bool> BookSession(Guid guid, int userId, HttpContext httpContext);
        Task<bool> CancelBookedSession(int bookedId, int sessionId, int userId, HttpContext httpContext);
        Task<IEnumerable<SessionList>> GetPractitionerSessionList(FilteredSessionListRequest request, HttpContext context);
        Task<bool> CancelSessionByPractitioner(SessionBookingRequest model, HttpContext httpContext);
        //    Task<SesionCounts> GetAllSessionCountPractitioner(int practitionerID);
        Task<bool> SessionStartByPractitioner(SessionBookingRequest model, HttpContext httpContext);
    }
}
