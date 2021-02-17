using Application.Dtos;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Services
{
    public interface IEmailSenderService
    {
        Task SendEmailAsync(string EmailFrom, string emailto, string subject, string EmailBody , bool? isBooking = false, SessionBookingData? data = null);
        Task<bool> SendEmails(string EmailFrom, string emailto, string subject, string EmailBody);

    }
}
