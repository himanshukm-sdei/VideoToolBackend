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
    public interface IJobsService
    {
        Task<bool> SendInvitationEmails(HttpContext context);
        Task<bool> SendBookedsessionEmailBeforeoneday(HttpContext data);
        Task<bool> SendBookedsessionEmailBeforeoneHour(HttpContext data);
        Task<bool> SendBookedAvailabilityEmailBeforeHalfHour(HttpContext data);
        Task<bool> SendBookedAvailabilityEmailBeforeFiveMinutes(HttpContext data);
    }
}
