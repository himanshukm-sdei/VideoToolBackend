using Application.Dtos;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using Ical.Net;
using Ical.Net.DataTypes;
using Ical.Net.Serialization;
using Ical.Net.CalendarComponents;
using System.IO;

namespace Services.CommonClasses
{
    public class EmailHelper
    {
        public async static Task<string> Execute(string apiKey, string subject, string EmailBody, string EmailFrom, string emailto ,bool? isBooked = false, SessionBookingData? data = null)
        {
            try
            {
                apiKey = "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";
                var client = new SendGridClient(apiKey);
                //var from = new EmailAddress("visionlabsession@gmail.com", "Example User");
                //var Subject = "Sending with SendGrid is Fun";
                var to = new System.Net.Mail.MailAddress(emailto);
                //var plainTextContent = "and easy to do anywhere, even with C#";
                //var htmlContent = "<strong>and easy to do anywhere, even with C#</strong>";
                //var msg = MailHelper.CreateSingleEmail(from, to, Subject, plainTextContent, htmlContent);
                //var response = client.SendEmailAsync(msg);
                SendGrid.Helpers.Mail.Attachment attachment = new SendGrid.Helpers.Mail.Attachment();
                var file = "";
                if (isBooked == true && data != null)
                {
                    var calendar = new Ical.Net.Calendar();
                    var vEvent = new CalendarEvent
                    {
                        Class = "PUBLIC",
                        Summary = data.SessionTitle,
                        Created = new CalDateTime(DateTime.Now),
                        Description = data.SessionShotDescription,
                        Start = new CalDateTime(data.SessionDate.Value),
                        //End = new CalDateTime(Convert.ToDateTime(res.EndDate)),
                        Sequence = 0,
                        Uid = data.SessionGuid.ToString(),
                        // Location = res.Location,
                    };

                    calendar.Events.Add(vEvent);
                    var serializer = new CalendarSerializer(new SerializationContext());
                    var serializedCalendar = serializer.SerializeToString(calendar);
                    var bytesCalendar = Encoding.UTF8.GetBytes(serializedCalendar);
                    MemoryStream ms = new MemoryStream(bytesCalendar);
                    //attachment = new System.Net.Mail.Attachment(ms, "event.ics", "text/calendar");
                    file = Convert.ToBase64String(bytesCalendar);

                }

                //var apiKey = _configuration.GetSection("SENDGRID_API_KEY").Value;
                //var client = new SendGridClient(apiKey);
                var from = new System.Net.Mail.MailAddress("testingdata7120@gmail.com");

                var sendEmail = MailHelper.CreateSingleEmail(new EmailAddress(from.Address), new EmailAddress(to.Address), subject, "", EmailBody);
                sendEmail.SetSpamCheck(false);

                if (isBooked == true && data != null)
                {
                    sendEmail.AddAttachment("session.ics", file, "text/calendar", "attachment");
                }

                var response = await client.SendEmailAsync(sendEmail);

                return "";
                //return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async static Task<bool> SendEmails(string subject, string EmailBody, string EmailFrom, string emailto)
        {

            // String FROM = "testingdata7120@gmail.com";
            // String FROMNAME = "testingdata7120@gmail.com";
            // String TO = "shital@yopmail.com";
            var response = new InviteEmailData();
            String SMTP_USERNAME = "AKIA4IN42QIMB3XNYBWN"; //from 
            String SMTP_PASSWORD = "BJ659C/S1ZOdSiw23rv8pqUseZ7xbA8wDnEvOGeUqNUP";
            String CONFIGSET = "ConfigSet";
            String HOST = "email-smtp.us-west-2.amazonaws.com";
         
            int PORT = 587;

            String BODY = EmailBody;
            // Create and build a new MailMessage object
            MailMessage message = new MailMessage();
            message.IsBodyHtml = true;
            message.From = new MailAddress(EmailFrom);
            message.To.Add(new MailAddress(emailto));
            message.Subject = subject;
            message.Body = BODY;
            // Comment or delete the next line if you are not using a configuration set
            message.Headers.Add("X-SES-CONFIGURATION-SET", CONFIGSET);
            using (var client = new System.Net.Mail.SmtpClient(HOST, PORT))
            {
                // Pass SMTP credentials
                client.Credentials =
                new NetworkCredential(SMTP_USERNAME, SMTP_PASSWORD);
                // Enable SSL encryption
                client.EnableSsl = true;
                client.UseDefaultCredentials = false;
                // Try to send the message. Show status in console.
                try
                {
                    
                    client.Send(message);
                    return true;
                }
                catch (Exception ex)
                {
                    return false;
                }
            }
        }
    }
}
