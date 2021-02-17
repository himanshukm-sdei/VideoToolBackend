
using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Dtos;
using Common.Common;
using Dapper;
using Domain.Entities;
using Infrastructure.Implemenatations.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using static Common.Common.CommonUtility;

namespace Repository
{
    public class JobsRepo : IJobsRepo
    {

        private readonly therapistContext therapistContext;
        private readonly IConfiguration config;
        private string _connectionString;
        private readonly ICommonEmailsService commonEmailsService;

        public JobsRepo(ICommonEmailsService ICommonEmailsService, therapistContext therapistContext, IConfiguration configuration)
        {
            this.therapistContext = therapistContext;
            config = configuration;
            commonEmailsService = ICommonEmailsService;
            _connectionString = configuration.GetConnectionString("DBCNSTR");
        }

        public async Task<IEnumerable<InviteUsersList>> GetInviteUsers()
        {
            try
            {
                IEnumerable<InviteUsersList> inviteUsersList;
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    inviteUsersList = await con.QueryAsync<InviteUsersList>("dbo.SSP_getInviteUsersList", commandType: CommandType.StoredProcedure);
                }

                return inviteUsersList;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<bool>> UpdateUserInvitationStatus(InviteEmailData data)
        {
            try
            {
                IEnumerable<bool> status;
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    status = await con.QueryAsync<bool>("dbo.SSP_UpdateUserInvitationStatus", new
                    {
                        UserInviteGuId = data.UserInviteGuId,
                        EmailSender = data.EmailSender,
                        EmailBody = data.EmailBody,
                        InviteStatus = data.InviteStatus,
                        EmailSubject = data.EmailSubject

                    }, commandType: CommandType.StoredProcedure);
                }

                return status;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<SessionBookingData>> GetBookedSessions()
        {
            IEnumerable<SessionBookingData> sessionData;
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                sessionData = await con.QueryAsync<SessionBookingData>("dbo.SSP_getAllBookedSessionList", commandType: CommandType.StoredProcedure);
            }
            var data = sessionData;
            foreach (var item in sessionData)
            {
                DateTime dt = new DateTime(item.SessionDate.Value.Year, item.SessionDate.Value.Month, item.SessionDate.Value.Day,
             item.SessionTime.Value.Hour, item.SessionTime.Value.Minute, item.SessionTime.Value.Second);
                DateTime dataTimeByZoneId = CommonConversions.TimezoneConversion(dt, item.SessionTimezone, item.UserTimezone);
                item.SessionDate = new DateTime(dataTimeByZoneId.Ticks);
                item.SessionTime = dataTimeByZoneId;

            }
            return sessionData;
        }

        public async Task<bool> SendBookedsessionEmailBeforeoneday(HttpContext context)
        {
            try
            {
                var sessionData = await GetBookedSessions();
                var today = DateTime.Today;
                var tomorrow = today.AddDays(1);
                sessionData = sessionData.Where(x => x.SessionDate.Value.Date == tomorrow.Date).ToList();

                foreach (var item in sessionData)
                {
                    var template = this.therapistContext.MasterNotification.Where(x => x.NotificationId == (int)notificationEmailTypes.OnedayBefore).FirstOrDefault();
                    if (item.Email != null || item.Email != "")
                    {
                        var response = await this.commonEmailsService.SessionBookedReminderEmail(item.Email, item, context, template.NotificationSubject, template.NotificationTemplate);
                        UserNotification noti = new UserNotification();
                        noti.UserNotificationGuid = System.Guid.NewGuid();
                        noti.CreatedDate = DateTime.Now;
                        noti.NotificationId = template.NotificationId;
                        noti.UserNotificationSentDate = DateTime.Now;
                        noti.UserId = item.UserId;
                        noti.UserNotificationText = response;
                        if (response != "")
                        {
                            noti.UserNotificationStatus = 1;
                        }
                        else
                        {
                            noti.UserNotificationStatus = 0;
                        }

                        this.therapistContext.UserNotification.Add(noti);
                        await this.therapistContext.SaveChangesAsync();
                    }
                }


                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<bool> SendBookedsessionEmailBeforeoneHour(HttpContext context)
        {
            try
            {
                var today = DateTime.Now;
                var timeInterval = today.AddMinutes(90);
                var halfHrFromInterval = timeInterval.AddMinutes(30);
                TimeSpan starttTime = new TimeSpan(timeInterval.Hour, timeInterval.Minute, 00);
                TimeSpan endTime = new TimeSpan(halfHrFromInterval.Hour, halfHrFromInterval.Minute, 00);
                TimeSpan now = DateTime.Now.TimeOfDay;

                var sessionData = await GetBookedSessions();

                sessionData = sessionData.Where(x => x.SessionDate.Value.Date == today.Date && ((x.SessionTime.Value.TimeOfDay > starttTime) && (x.SessionTime.Value.TimeOfDay < endTime))).ToList();

                foreach (var item in sessionData)
                {
                    var template = this.therapistContext.MasterNotification.Where(x => x.NotificationId == (int)notificationEmailTypes.OneHourBefore).FirstOrDefault();
                    if (item.Email != null || item.Email != "")
                    {
                        var response = await this.commonEmailsService.SessionBookedReminderEmail(item.Email, item, context, template.NotificationSubject, template.NotificationTemplate);
                        UserNotification noti = new UserNotification();
                        noti.UserNotificationGuid = System.Guid.NewGuid();
                        noti.CreatedDate = DateTime.Now;
                        noti.NotificationId = template.NotificationId;
                        noti.UserNotificationSentDate = DateTime.Now;
                        noti.UserId = item.UserId;
                        noti.UserNotificationText = response;
                        if (response != "")
                        {
                            noti.UserNotificationStatus = 1;
                        }
                        else
                        {
                            noti.UserNotificationStatus = 0;
                        }

                        this.therapistContext.UserNotification.Add(noti);
                        await this.therapistContext.SaveChangesAsync();
                    }
                }


                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> SendBookedAvailabilityEmailBeforeHalfHour(HttpContext context)
        {
            try
            {
                var today = DateTime.Now;
                var timeInterval = today.AddMinutes(40);
                var halfHrFromInterval = timeInterval.AddMinutes(30);
                TimeSpan starttTime = new TimeSpan(timeInterval.Hour, timeInterval.Minute, 00);
                TimeSpan endTime = new TimeSpan(halfHrFromInterval.Hour, halfHrFromInterval.Minute, 00);
                TimeSpan now = DateTime.Now.TimeOfDay;
                IEnumerable<SessionWaitingList> waiting = null;
                IEnumerable<AvailableSessions> availables = null;
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    var reader = await con.QueryMultipleAsync("dbo.SSP_getSessionWaitingUserList", new
                    {
                    }, commandType: CommandType.StoredProcedure);
                    availables = await reader.ReadAsync<AvailableSessions>();
                    waiting = await reader.ReadAsync<SessionWaitingList>();
                }
                foreach (var item in availables)
                {
                    waiting = waiting.Where(x => x.SessionId == item.SessionId).ToList();
                    foreach (var wait in waiting)
                    {
                        DateTime dt = new DateTime(wait.SessionDate.Value.Year, wait.SessionDate.Value.Month, wait.SessionDate.Value.Day,
                       wait.SessionTime.Value.Hour, wait.SessionTime.Value.Minute, wait.SessionTime.Value.Second);
                        DateTime dataTimeByZoneId = CommonConversions.TimezoneConversion(dt, wait.SessionTimezone, wait.UserTimezone);
                        wait.SessionDate = new DateTime(dataTimeByZoneId.Ticks);
                        wait.SessionTime = dataTimeByZoneId;

                    }
                    waiting = waiting.Where(x => x.SessionDate.Value.Date == today.Date && ((x.SessionTime.Value.TimeOfDay > starttTime) && (x.SessionTime.Value.TimeOfDay < endTime))).ToList();
                    foreach (var finalwait in waiting)
                    {
                        if (finalwait.Email != null || finalwait.Email != "")
                        {
                            await this.commonEmailsService.SessionAvailableEmail(finalwait.Email, finalwait, context);
                        }
                    }
                }
                return true;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> SendBookedAvailabilityEmailBeforeFiveMinutes(HttpContext context)
        {
            try
            {
                var today = DateTime.Now;
                var timeInterval = today.AddMinutes(10);
                var fiveMinFromInterval = timeInterval.AddMinutes(5);
                TimeSpan starttTime = new TimeSpan(timeInterval.Hour, timeInterval.Minute, 00);
                TimeSpan endTime = new TimeSpan(fiveMinFromInterval.Hour, fiveMinFromInterval.Minute, 00);
                TimeSpan now = DateTime.Now.TimeOfDay;
                IEnumerable<SessionWaitingList> waiting = null;
                IEnumerable<AvailableSessions> availables = null;
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    var reader = await con.QueryMultipleAsync("dbo.SSP_getSessionWaitingUserList", new
                    {
                    }, commandType: CommandType.StoredProcedure);
                    availables = await reader.ReadAsync<AvailableSessions>();
                    waiting = await reader.ReadAsync<SessionWaitingList>();
                }
                foreach (var item in availables)
                {
                    waiting = waiting.Where(x => x.SessionId == item.SessionId).ToList();
                    foreach (var wait in waiting)
                    {
                        DateTime dt = new DateTime(wait.SessionDate.Value.Year, wait.SessionDate.Value.Month, wait.SessionDate.Value.Day,
                       wait.SessionTime.Value.Hour, wait.SessionTime.Value.Minute, wait.SessionTime.Value.Second);
                        DateTime dataTimeByZoneId = CommonConversions.TimezoneConversion(dt, wait.SessionTimezone, wait.UserTimezone);
                        wait.SessionDate = new DateTime(dataTimeByZoneId.Ticks);
                        wait.SessionTime = dataTimeByZoneId;

                    }
                    waiting = waiting.Where(x => x.SessionDate.Value.Date == today.Date && ((x.SessionTime.Value.TimeOfDay > starttTime) && (x.SessionTime.Value.TimeOfDay < endTime))).ToList();
                    foreach (var finalwait in waiting)
                    {
                        if (finalwait.Email != null || finalwait.Email != "")
                        {
                            await this.commonEmailsService.SessionAvailableEmail(finalwait.Email, finalwait, context);
                        }
                    }
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
