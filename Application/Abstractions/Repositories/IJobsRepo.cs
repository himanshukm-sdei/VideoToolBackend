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
    public interface IJobsRepo
    {
        Task<IEnumerable<bool>> UpdateUserInvitationStatus(InviteEmailData data);
        Task<IEnumerable<InviteUsersList>> GetInviteUsers();
        Task<IEnumerable<SessionBookingData>> GetBookedSessions();
        Task<bool> SendBookedsessionEmailBeforeoneday(HttpContext data);
        Task<bool> SendBookedsessionEmailBeforeoneHour(HttpContext data);
        Task<bool> SendBookedAvailabilityEmailBeforeHalfHour(HttpContext data);
        Task<bool> SendBookedAvailabilityEmailBeforeFiveMinutes(HttpContext data);
    }
}
