using Application.Dtos;
using Domain.Entities;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Services
{
    public interface ICommonEmailsService
    {
        Task<User> SendRestPasswordEmail(string emailid);
        Task<bool> SendWelcomeEmailToMember(string emailid, HttpContext context);
        Task<bool> SendWelcomeEmailToPractitioner(string emailid, HttpContext context);
        Task<bool> SendWelcomeEmailToPractitionerFromAdmin(string emailid, string UserName, string Password, HttpContext context);
        Task<bool> SendWelcomeEmailToMemberFromCompany(string emailid, string firstName, string lastName, string username, string password, string companyName, HttpContext context);
        Task<bool> SendWelcomeEmailToSuperAdmin(string emailid, HttpContext context);
        //       Task<bool> SendWelcomeEmailToMemberFromCompany(string emailid, string username,string password);
        Task<InviteEmailData> SendUserInviteEmails(InviteUsersList emailData, HttpContext context);
        Task<bool> UpdateEmailLog(string emailid, string emailSender, string strEmailSubject, string emailBody, int inviteStatus, int userId, int userEmailTemplate);

        Task<User> SendRestPasswordEmailByAdmin(string emailid, long UserId , HttpContext context);
        Task<User> ResendEmailToUser(ForgetPasswordRequestModel obj_ForgetPasswordRequestModel, HttpContext context);
        Task<bool> SessionBookedEmail(string emailid, SessionBookingData data, HttpContext context);
        Task<bool> SessionCanceledEmail(string emailid, SessionBookingData data, HttpContext context);
        Task<bool> SessionAvailableEmail(string emailid, SessionWaitingList data, HttpContext context);
        Task<bool> SessionWaitingEmail(string emailid, SessionBookingData data, HttpContext context);
        Task<bool> CancelSessionByPractitioner(CompanySessionsList data, HttpContext context);

        Task<string> SessionBookedReminderEmail(string emailid, SessionBookingData data, HttpContext context, string strEmailSubject, string emailBody);

     //   Task<SesionCounts> GetAllSessionCountPractitioner(int practitionerID);

    }
}
