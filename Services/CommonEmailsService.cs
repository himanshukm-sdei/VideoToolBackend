using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Domain.Entities;
using Application.Dtos;
using Infrastructure.Implemenatations.Context;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Linq;
using Microsoft.Extensions.Configuration;

namespace Services
{
    public class CommonEmailsService : ICommonEmailsService
    {
        private readonly IEmailSenderService _iEmailSenderService;
        private readonly ICommonEmailsRepo _ICommonEmailsRepo;
        private readonly IAuthRepo authRepo;
        private readonly IConfiguration _iconfiguration;


        public CommonEmailsService(IEmailSenderService emailSenderService, IAuthRepo _authRepo, ICommonEmailsRepo CommonEmailsRepo, IConfiguration configuration)
        {
            _iEmailSenderService = emailSenderService;
            _ICommonEmailsRepo = CommonEmailsRepo;
            authRepo = _authRepo;
            _iconfiguration = configuration;

        }

        public async Task<User> SendRestPasswordEmail(string emailid)
        {
            var inviteStatus = 0;
            string strEmailSubject = string.Empty;
            strEmailSubject = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:ForgotPassword");
            var GeneratePasswordUrl = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:GeneratePasswordUrl");
            try
            {
                Guid guid = Guid.NewGuid();
                string strEmailBody = string.Empty;

                string directory = $"{Directory.GetCurrentDirectory()}{@"\wwwroot"}";
                var emailHtml = System.IO.File.ReadAllText(directory + "/Templates/ResetPassword.html");
                emailHtml = emailHtml.Replace("{{LINK}}", GeneratePasswordUrl + guid);

                var update = await this.authRepo.UpdateResetPasswordLink(guid.ToString(), emailid);

                if (!string.IsNullOrEmpty(emailid) && update.Status == 1)
                {
                    string name = MaskDigits(update.Firstname);
                    emailHtml = emailHtml.Replace("{{NAME}}", name);

                    await _iEmailSenderService.SendEmailAsync(_iconfiguration.GetValue<string>("AuthMessageSenderOptions:SendGridEmail"), emailid, strEmailSubject, emailHtml);
                    update.Status = 1;
                    inviteStatus = 1;
                }

                return update;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> SendWelcomeEmailToPractitionerFromAdmin(string emailid, string userName, string password, HttpContext context)
        {
            bool status = false;
            string strEmailSubject = string.Empty;
            strEmailSubject = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:WelcomeEmail");
            var homeUrl = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:StaggingUrl");
            try
            {
                string directory = $"{Directory.GetCurrentDirectory()}{@"\wwwroot"}";
                var emailHtml = System.IO.File.ReadAllText(directory + "/Templates/WelcomeEmail_Practioner_FromAdmin.html");
                string logoPath = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "//images//sessionsLogoBlack.png";
                string newsletter = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "//images//newsletter.png";

                emailHtml = emailHtml.Replace("{{HOME}}", homeUrl);
                emailHtml = emailHtml.Replace("{{logo}}", logoPath);
                emailHtml = emailHtml.Replace("{{UserName}}", userName);
                emailHtml = emailHtml.Replace("{{Password}}", password);
                await _iEmailSenderService.SendEmailAsync(_iconfiguration.GetValue<string>("AuthMessageSenderOptions:SendGridEmail"), emailid, strEmailSubject, emailHtml);
                status = true;
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> SendWelcomeEmailToPractitioner(string emailid, HttpContext context)
        {
            bool status = false;
            string strEmailSubject = string.Empty;
            strEmailSubject = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:WelcomeEmail");
            var homeUrl = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:StaggingUrl");
            try
            {
                string directory = $"{Directory.GetCurrentDirectory()}{@"\wwwroot"}";
                var emailHtml = System.IO.File.ReadAllText(directory + "/Templates/WelcomeEmail_Practioner.html");
                string logoPath = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "//images//sessionsLogoBlack.png";
                string newsletter = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "//images//newsletter.png";

                emailHtml = emailHtml.Replace("{{HOME}}", homeUrl);
                emailHtml = emailHtml.Replace("{{logo}}", logoPath);
                emailHtml = emailHtml.Replace("{{newsletter}}", newsletter);

                await _iEmailSenderService.SendEmailAsync(_iconfiguration.GetValue<string>("AuthMessageSenderOptions:SendGridEmail"), emailid, strEmailSubject, emailHtml);
                status = true;
                return status;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> SendWelcomeEmailToSuperAdmin(string emailid, HttpContext context)
        {
            bool status = false;
            string strEmailSubject = string.Empty;
            strEmailSubject = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:WelcomeEmail");
            //string path = @"\wwwroot\images\sessionsLogoBlack.png";
            string path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\images\sessionsLogoBlack.png"}";
            try
            {
                string directory = $"{Directory.GetCurrentDirectory()}{@"\wwwroot"}";
                var emailHtml = System.IO.File.ReadAllText(directory + "/Templates/Welcome_Super_Admin.html");
                string logoPath = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "//images//sessionsLogoBlack.png";
                string newsletter = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "//images//newsletter.png";
                emailHtml = emailHtml.Replace("{{logo}}", logoPath);
                emailHtml = emailHtml.Replace("{{newsletter}}", newsletter);
                await _iEmailSenderService.SendEmailAsync(_iconfiguration.GetValue<string>("AuthMessageSenderOptions:SendGridEmail"), emailid, strEmailSubject, emailHtml.ToString());
                status = true;
                return status;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<bool> SendWelcomeEmailToMember(string emailid, HttpContext context)
        {
            bool status = false;
            string strEmailSubject = string.Empty;
            strEmailSubject = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:WelcomeEmail");
            var homeUrl = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:StaggingUrl");
            try
            {
                string directory = $"{Directory.GetCurrentDirectory()}{@"\wwwroot"}";
                var emailHtml = System.IO.File.ReadAllText(directory + "/Templates/WelcomeEmail_Member.html");
                string logoPath = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "//images//sessionsLogoBlack.png";
                string newsletter = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "//images//newsletter.png";

                emailHtml = emailHtml.Replace("{{HOME}}", homeUrl);
                emailHtml = emailHtml.Replace("{{logo}}", logoPath);
                emailHtml = emailHtml.Replace("{{newsletter}}", newsletter);
                await _iEmailSenderService.SendEmailAsync(_iconfiguration.GetValue<string>("AuthMessageSenderOptions:SendGridEmail"), emailid, strEmailSubject, emailHtml);
                status = true;
            }
            catch (Exception ex)
            {

                throw;
            }
            return status;
        }
        public async Task<bool> SendWelcomeEmailToMemberFromCompany(string emailid, string firstName, string lastName, string username, string password, string companyName, HttpContext context)
        {
            bool status = false;
            string strEmailSubject = string.Empty;
            strEmailSubject = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:WelcomeEmail");
            var homeUrl = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:StaggingUrl");
            try
            {

                string directory = $"{Directory.GetCurrentDirectory()}{@"\wwwroot"}";
                var emailHtml = System.IO.File.ReadAllText(directory + "/Templates/Welcome_Email_To_Member_FromCompany.html");
                emailHtml = emailHtml.Replace("{{FirstName}}", firstName);
                emailHtml = emailHtml.Replace("{{LastName}}", lastName);
                emailHtml = emailHtml.Replace("{{CompanyName}}", companyName);
                emailHtml = emailHtml.Replace("{{Username}}", username);
                emailHtml = emailHtml.Replace("{{Password}}", password);
                emailHtml = emailHtml.Replace("{{Login}}", homeUrl);
                string logoPath = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "//images//sessionsLogoBlack.png";
                string newsletter = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "//images//newsletter.png";
                emailHtml = emailHtml.Replace("{{logo}}", logoPath);
                emailHtml = emailHtml.Replace("{{newsletter}}", newsletter);
                await _iEmailSenderService.SendEmailAsync(_iconfiguration.GetValue<string>("AuthMessageSenderOptions:SendGridEmail"), emailid, strEmailSubject, emailHtml.ToString());
                status = true;
            }
            catch (Exception ex)
            {

                throw;
            }
            return status;
        }

        public async Task<InviteEmailData> SendUserInviteEmails(InviteUsersList emailData, HttpContext context)
        {
            bool status = false;
            InviteEmailData response = new InviteEmailData();
            string strEmailSubject = string.Empty;
            strEmailSubject = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:WelcomeEmail");

            string path = $"{Directory.GetCurrentDirectory()}{@"\wwwroot\images\sessionsLogoBlack.png"}";
            try
            {
                string directory = $"{Directory.GetCurrentDirectory()}{@"\wwwroot"}";
                var emailHtml = System.IO.File.ReadAllText(directory + "/Templates/Invite_Company_Members.html");
                emailHtml = emailHtml.Replace("{{ActivationToken}}", emailData.ActivationToken.ToString());
                var membersignupURL= _iconfiguration.GetValue<string>("AuthMessageSenderOptions:MemberSignupUrl");
                string logoPath = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "//images//sessionsLogoBlack.png";
                string newsletter = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "//images//newsletter.png";
                response.EmailSubject = strEmailSubject;
                response.EmailSender = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:SendGridEmail");
                response.EmailBody = emailHtml;
                response.InviteStatus = 1;
                emailHtml = emailHtml.Replace("{{logo}}", logoPath);
                emailHtml = emailHtml.Replace("{{newsletter}}", newsletter);
                emailHtml = emailHtml.Replace("{{membersignupURL}}", membersignupURL);

                await _iEmailSenderService.SendEmailAsync(_iconfiguration.GetValue<string>("AuthMessageSenderOptions:SendGridEmail"), emailData.EmailAddress, strEmailSubject, emailHtml);
                status = true;
                if (status == true)
                {
                    response.InviteStatus = 1;
                }
                else
                {
                    response.InviteStatus = 100;
                }
                return response;
            }
            catch (Exception ex)
            {
                response.InviteStatus = 100;
                return response;
            }
        }

        public async Task<bool> UpdateEmailLog(string emailid, string emailSender, string strEmailSubject, string emailBody, int inviteStatus, int userId, int userEmailTemplate)
        {
            try
            {
                await _ICommonEmailsRepo.UpdateUserEmailLogging(emailid, emailSender, strEmailSubject, emailBody, inviteStatus, userId, userEmailTemplate);
                return true;
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public string MaskDigits(string input)
        {
            //take first 6 characters
            string firstPart = input.Substring(0, 1);

            //take last 4 characters
            int len = input.Length;
            string lastPart = input.Substring(len - 1, 1);

            //take the middle part (XXXXXXXXX)
            int middlePartLenght = input.Substring(1, len - 1).Count();
            string middlePart = new String('*', 5);

            return firstPart + middlePart + lastPart;
        }

        public async Task<User> SendRestPasswordEmailByAdmin(string emailid, long UserId , HttpContext context)
        {
            bool status = false;
            var inviteStatus = 0;
            string strEmailSubject = string.Empty;
            string emailBody = string.Empty;
            strEmailSubject = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:ForgotPassword");
            var GeneratePasswordUrl = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:GeneratePasswordUrl");
            try
            {
                Guid guid = Guid.NewGuid();
                string strEmailBody = string.Empty;

                string directory = $"{Directory.GetCurrentDirectory()}{@"\wwwroot"}";
                var emailHtml = System.IO.File.ReadAllText(directory + "/Templates/ResetPassword.html");

                emailHtml = emailHtml.Replace("{{LINK}}", GeneratePasswordUrl + guid);

                var update = await this.authRepo.SendRestPasswordEmailByAdmin(guid.ToString(), emailid, UserId);

                if (!string.IsNullOrEmpty(emailid) && update.Status == 1)
                {
                    string name = MaskDigits(update.Firstname);
                    emailHtml = emailHtml.Replace("{{NAME}}", name);
                    string logoPath = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "//images//sessionsLogoBlack.png";
                    string newsletter = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "//images//newsletter.png";

                    //emailHtml = emailHtml.Replace("{{HOME}}", homeUrl);
                    emailHtml = emailHtml.Replace("{{logo}}", logoPath);
                    emailHtml = emailHtml.Replace("{{newsletter}}", newsletter);
                    await _iEmailSenderService.SendEmailAsync(_iconfiguration.GetValue<string>("AuthMessageSenderOptions:SendGridEmail"), emailid, strEmailSubject, emailHtml);
                    update.Status = 1;
                    inviteStatus = 1;
                }

                return update;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<User> ResendEmailToUser(ForgetPasswordRequestModel forgetPasswordModel, HttpContext context)
        {

            string strEmailSubject = string.Empty;
            User obj = new User();
            strEmailSubject = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:WelcomeEmail");
            var homeUrl = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:StaggingUrl");
            try
            {
                var emailHtml = "";
                string directory = $"{Directory.GetCurrentDirectory()}{@"\wwwroot"}";
                if (forgetPasswordModel.TemplateID == 1)
                {
                    emailHtml = System.IO.File.ReadAllText(directory + "/Templates/Welcome_Super_Admin.html"); //

                }
                else if (forgetPasswordModel.TemplateID == 2)
                {
                    emailHtml = System.IO.File.ReadAllText(directory + "/Templates/WelcomeEmail_Member.html");
                }
                else
                {
                    emailHtml = System.IO.File.ReadAllText(directory + "/Templates/WelcomeEmail_Practioner.html");
                }
                string logoPath = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "//images//sessionsLogoBlack.png";
                string newsletter = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "//images//newsletter.png";

                emailHtml = emailHtml.Replace("{{HOME}}", homeUrl);
                emailHtml = emailHtml.Replace("{{logo}}", logoPath);
                emailHtml = emailHtml.Replace("{{newsletter}}", newsletter);
                await _iEmailSenderService.SendEmailAsync(_iconfiguration.GetValue<string>("AuthMessageSenderOptions:SendGridEmail"), forgetPasswordModel.EmailId, strEmailSubject, emailHtml);
                obj.Status = 1;
                return obj;

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        public async Task<bool> SessionBookedEmail(string emailid, SessionBookingData data, HttpContext context)
        {
            bool status = false;
            string strEmailSubject = "Session Booked Successfully.";
            strEmailSubject = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:SessionBooked");
            var homeUrl = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:StaggingUrl");
            var MemberSessionsUrl = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:MemberSessionsUrl");
            try
            {
                string directory = $"{Directory.GetCurrentDirectory()}{@"\wwwroot"}";
                var emailHtml = System.IO.File.ReadAllText(directory + "/Templates/SessionBookedEmail.html");
                string logoPath = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "//images//sessionsLogoBlack.png";
                string newsletter = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "//images//newsletter.png";

                emailHtml = emailHtml.Replace("{{HOME}}", homeUrl);
                emailHtml = emailHtml.Replace("{{Cancelbooking}}", homeUrl + "?redirectUrl=" + MemberSessionsUrl + data.SessionGuid);
                emailHtml = emailHtml.Replace("{{logo}}", logoPath);
                emailHtml = emailHtml.Replace("{{BookingDate}}", data.BookingDate.ToString("MM-dd-yyyy"));
                emailHtml = emailHtml.Replace("{{SessionTitle}}", data.SessionTitle);
                emailHtml = emailHtml.Replace("{{SessionTime}}", data.SessionTime.Value.ToShortTimeString());
                emailHtml = emailHtml.Replace("{{SessionType}}", data.SessionType);
                emailHtml = emailHtml.Replace("{{SessionTopic}}", data.SessionTopic);
                await _iEmailSenderService.SendEmailAsync(_iconfiguration.GetValue<string>("AuthMessageSenderOptions:SendGridEmail"), emailid, "Session Booked Successfully.", emailHtml, true, data);
                status = true;
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> SessionWaitingEmail(string emailid, SessionBookingData data, HttpContext context)
        {
            bool status = false;
            string strEmailSubject = "Session Booked Successfully.";
            strEmailSubject = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:SessionBooked");
            var homeUrl = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:StaggingUrl");
            var MemberSessionsUrl = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:MemberSessionsUrl");
            try
            {
                string directory = $"{Directory.GetCurrentDirectory()}{@"\wwwroot"}";
                var emailHtml = System.IO.File.ReadAllText(directory + "/Templates/SessionAddToWaitingEmail.html");
                string logoPath = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "//images//sessionsLogoBlack.png";
                string newsletter = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "//images//newsletter.png";

                emailHtml = emailHtml.Replace("{{HOME}}", homeUrl);
                emailHtml = emailHtml.Replace("{{Cancelbooking}}", homeUrl + "?redirectUrl=" + MemberSessionsUrl + data.SessionGuid);
                emailHtml = emailHtml.Replace("{{logo}}", logoPath);
                emailHtml = emailHtml.Replace("{{BookingDate}}", data.BookingDate.ToString("MM-dd-yyyy"));
                emailHtml = emailHtml.Replace("{{SessionTitle}}", data.SessionTitle);
                emailHtml = emailHtml.Replace("{{SessionTime}}", data.SessionTime.Value.ToShortTimeString());
                emailHtml = emailHtml.Replace("{{SessionType}}", data.SessionType);
                emailHtml = emailHtml.Replace("{{SessionTopic}}", data.SessionTopic);
                await _iEmailSenderService.SendEmailAsync(_iconfiguration.GetValue<string>("AuthMessageSenderOptions:SendGridEmail"), emailid, "Session added in waiting list Successfully.", emailHtml);
                status = true;
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<bool> SessionCanceledEmail(string emailid, SessionBookingData data, HttpContext context)
        {
            bool status = false;
            string strEmailSubject = "Session Cancelled Successfully.";
            strEmailSubject = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:SessionBooked");
            var homeUrl = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:StaggingUrl");
            var MemberSessionsUrl = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:MemberSessionsUrl");
            try
            {
                string directory = $"{Directory.GetCurrentDirectory()}{@"\wwwroot"}";
                var emailHtml = "";
                if (data.SessionBookingStatus == 100)
                {
                    emailHtml = System.IO.File.ReadAllText(directory + "/Templates/SessionCancelledEmail.html");
                }
                else
                {
                    emailHtml = System.IO.File.ReadAllText(directory + "/Templates/SessionWaitinglistCancelledEmail.html");
                }

                string logoPath = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "//images//sessionsLogoBlack.png";
                string newsletter = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "//images//newsletter.png";

                emailHtml = emailHtml.Replace("{{HOME}}", homeUrl);
                emailHtml = emailHtml.Replace("{{Cancelbooking}}", homeUrl + "?redirectUrl=" + MemberSessionsUrl + data.SessionGuid);
                emailHtml = emailHtml.Replace("{{logo}}", logoPath);
                emailHtml = emailHtml.Replace("{{BookingDate}}", data.BookingDate.ToString("MM-dd-yyyy"));
                emailHtml = emailHtml.Replace("{{SessionTitle}}", data.SessionTitle);
                emailHtml = emailHtml.Replace("{{SessionTime}}", data.SessionTime.Value.ToShortTimeString());
                emailHtml = emailHtml.Replace("{{SessionType}}", data.SessionType);
                emailHtml = emailHtml.Replace("{{SessionTopic}}", data.SessionTopic);
                emailHtml = emailHtml.Replace("{{status}}", data.SessionBookingStatus.Value.ToString());
                await _iEmailSenderService.SendEmailAsync(_iconfiguration.GetValue<string>("AuthMessageSenderOptions:SendGridEmail"), emailid, "Session Cancelled Successfully.", emailHtml);
                status = true;
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<bool> SessionAvailableEmail(string emailid, SessionWaitingList data, HttpContext context)
        {
            bool status = false;
            string strEmailSubject = "Session Availability.";
            var homeUrl = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:StaggingUrl");
            var MemberSessionsUrl = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:MemberSessionsUrl");
            try
            {
                string directory = $"{Directory.GetCurrentDirectory()}{@"\wwwroot"}";
                var emailHtml = "";
                emailHtml = System.IO.File.ReadAllText(directory + "/Templates/SessionAvailabilityEmail.html");
                string logoPath = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "//images//sessionsLogoBlack.png";
                string newsletter = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "//images//newsletter.png";
                emailHtml = emailHtml.Replace("{{HOME}}", homeUrl + "?redirectUrl=" + MemberSessionsUrl + data.SessionGuid);
                emailHtml = emailHtml.Replace("{{bookingSession}}", homeUrl + "?redirectUrl=" + MemberSessionsUrl + data.SessionGuid);
                emailHtml = emailHtml.Replace("{{logo}}", logoPath);
                emailHtml = emailHtml.Replace("{{SessionTitle}}", data.SessionTitle);
                emailHtml = emailHtml.Replace("{{description}}", data.SessionDescription);
                emailHtml = emailHtml.Replace("{{SessionTopic}}", data.TopicName);
                await _iEmailSenderService.SendEmailAsync(_iconfiguration.GetValue<string>("AuthMessageSenderOptions:SendGridEmail"), emailid, strEmailSubject, emailHtml);
                status = true;
                return status;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        public async Task<bool> CancelSessionByPractitioner(CompanySessionsList data, HttpContext context)
        {
            bool status = false;
            string strEmailSubject = "Session Cancelled.";
            strEmailSubject = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:SessionBooked");
            var homeUrl = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:StaggingUrl");
            try
            {
                string directory = $"{Directory.GetCurrentDirectory()}{@"\wwwroot"}";
                var emailHtml = "";
                if (data.SessionBookingStatus == 1)
                {
                    emailHtml = System.IO.File.ReadAllText(directory + "/Templates/SessionCancelledEmailByPractitioner.html");
                }
                else
                {
                    emailHtml = System.IO.File.ReadAllText(directory + "/Templates/SessionWaitinglistCancelledEmailByPractitioner.html");
                }


                string logoPath = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "\\images\\sessionsLogoBlack.png";
                string newsletter = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "\\images\\newsletter.png";

                emailHtml = emailHtml.Replace("{{HOME}}", homeUrl);
                emailHtml = emailHtml.Replace("{{logo}}", logoPath);
                emailHtml = emailHtml.Replace("{{BookingDate}}", data.BookingDate.ToString("MM-dd-yyyy"));
                emailHtml = emailHtml.Replace("{{SessionTitle}}", data.SessionTitle);
                emailHtml = emailHtml.Replace("{{SessionTime}}", data.SessionTime.Value.ToShortTimeString());
                emailHtml = emailHtml.Replace("{{SessionType}}", data.SessionType);
              //  emailHtml = emailHtml.Replace("{{SessionTopic}}", "");
                await _iEmailSenderService.SendEmailAsync(_iconfiguration.GetValue<string>("AuthMessageSenderOptions:SendGridEmail"), data.Email, "Session cancelled.", emailHtml);
                status = true;
                return status;


            }

            catch (Exception ex)
            {

                throw;
            }
           // return true;
        }

        public async Task<string> SessionBookedReminderEmail(string emailid, SessionBookingData data, HttpContext context, string subject, string template)
        {
            string status = "";
            string strEmailSubject = "Session Reminder.";
            strEmailSubject = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:SessionBooked");
            var homeUrl = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:StaggingUrl");
            var MemberSessionsUrl = _iconfiguration.GetValue<string>("AuthMessageSenderOptions:MemberSessionsUrl");

            try
            {
                string directory = $"{Directory.GetCurrentDirectory()}{@"\wwwroot"}";
                var emailHtml = template;
                string logoPath = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "//images//sessionsLogoBlack.png";
                string newsletter = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "//images//newsletter.png";

                emailHtml = emailHtml.Replace("{{HOME}}", homeUrl);
                emailHtml = emailHtml.Replace("{{Cancelbooking}}", homeUrl + "?redirectUrl=" + MemberSessionsUrl + data.SessionGuid);
                emailHtml = emailHtml.Replace("{{logo}}", logoPath);
                emailHtml = emailHtml.Replace("{{BookingDate}}", data.BookingDate.ToString("MM-dd-yyyy"));
                emailHtml = emailHtml.Replace("{{SessionTitle}}", data.SessionTitle);
                emailHtml = emailHtml.Replace("{{SessionDate}}", data.SessionDate.Value.ToString("MM-dd-yyyy"));
                emailHtml = emailHtml.Replace("{{SessionTime}}", data.SessionTime.Value.ToShortTimeString());
                emailHtml = emailHtml.Replace("{{SessionType}}", data.SessionType);
                emailHtml = emailHtml.Replace("{{SessionTopic}}", data.SessionTopic);
                emailHtml = emailHtml.Replace("{{Description}}", data.SessionShotDescription);
                emailHtml = emailHtml.Replace("{{PractitionerName}}", data.PractitionerName);

                await _iEmailSenderService.SendEmailAsync(_iconfiguration.GetValue<string>("AuthMessageSenderOptions:SendGridEmail"), emailid, subject, emailHtml);
                status = emailHtml;

                return status;
            }
            catch (Exception ex)
            {

                throw;
            }
        }

    }
}
