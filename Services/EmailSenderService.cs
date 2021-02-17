using Application.Abstractions.Services;
using Application.Dtos;
using Microsoft.Extensions.Options;
using Services.CommonClasses;
using Services.Options;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Services
{
    public class EmailSenderService : IEmailSenderService 
    {
        public AuthMessageSenderOptions Options { get; }
        public EmailSenderService(IOptions<AuthMessageSenderOptions> optionsAccessor)
        {
            Options = optionsAccessor.Value;

        }
        #region Send Emails
        public Task SendEmailAsync(string EmailFrom, string emailto, string subject, string EmailBody , bool? isBooked = false, SessionBookingData? data = null)
        {
            try
            {
                return EmailHelper.Execute(Options.SendGridKey, subject, EmailBody.ToString(), EmailFrom, emailto, isBooked, data);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
        #region Send Emails AWS SES
        public Task<bool> SendEmails(string EmailFrom, string emailto, string subject, string EmailBody)
        {
            try
            {
                return EmailHelper.SendEmails(subject, EmailBody, EmailFrom, emailto /*, "ashish.verma@smartdatainc,net"*/);
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        #endregion
    }
}
