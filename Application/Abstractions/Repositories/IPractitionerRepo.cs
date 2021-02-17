using Application.Dtos;
using Domain.Entities;
using Infrastructure.Implemenatations.Context;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Repositories
{
    public interface IPractitionerRepo
    {
        Task<ProfileInfo> GetUserProfile(int? userId, Guid? guid);
        Task<long> CreateSession(Session session);
        Task<long> UpdateSession(SessionRequest session);
        Task<int> SaveTags(long sessionId, int userId, int[] tags);
        Task<int> UpdateTags(long sessionId, int userId, int[] tags);
        Task<List<ListVm>> GetTags();
        Task<List<ListVm>> GetTopics();
        Task<List<ListVm>> GetTimezones();
        Task<IEnumerable<CategoryList>> GetSessionList(SessionListRequest request, HttpContext context);
        Task<IEnumerable<SessionList>> PractitionerRelated(int practitionerId, int userid, HttpContext context);
        Task<IEnumerable<CategoryList>> GetSessionListMember(SessionListRequest request, HttpContext context);
        Task<IEnumerable<SessionList>> GetFilteredSessionList(FilteredSessionListRequest request, HttpContext context);
        Task<IEnumerable<CategoryList>> GetAllSession(SessionListRequest request, HttpContext context);
        Task<IEnumerable<CategoryList>> GetAllSessionMember(SessionListRequest request, HttpContext context);
        Task<SessionDetails> SessionDetails(Guid guid, int? userId, HttpContext context);
        Task<SessionBookingData> SessionBookingDetails(Guid guid, int userId);
        Task<bool> SessionvideoAssign(Guid guid, int videoId, int userId);
        Task<bool> BookSession(Guid guid, int userId, HttpContext httpContext);

        Task<bool> EnableNotification(NotificationRequest notification, HttpContext httpContext);
        Task<bool> CancelBookedSession(int bookedId, int sessionId, int userId, HttpContext httpContext);
        Task<IEnumerable<SessionList>> GetPractitionerSessionList(FilteredSessionListRequest request, HttpContext context);
        //Task<bool> CancelSessionByPractitioner(long SessionId, HttpContext httpContext);

        Task<bool> UpdateSessionStatus(CompanySessionsRequest request);
        Task<SesionCounts> GetAllSessionCountMember(int memberId);

        //  Task<SesionCounts> GetAllSessionCountPractitioner(int practitionerID);
        Task<bool> SessionStartByPractitioner(CompanySessionsRequest request);
    }
}
