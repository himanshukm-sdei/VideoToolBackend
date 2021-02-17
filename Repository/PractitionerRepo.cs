using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Dtos;
using AutoMapper;
using Common.Common;
using Dapper;
using Domain.Entities;
using Infrastructure.Implemenatations.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class PractitionerRepo : IPractitionerRepo
    {
        private readonly AppSettings appSettings;
        private readonly therapistContext therapistContext;
        private readonly IMapper mapper;
        private readonly IConfiguration config;
        private string _connectionString;
        private readonly ICommonEmailsService commonEmailsService;
        //public readonly HttpContext context;

        public PractitionerRepo(ICommonEmailsService ICommonEmailsService, IOptions<AppSettings> options, therapistContext therapistContext, IMapper mapper, IConfiguration configuration)
        {
            this.appSettings = options.Value;
            this.therapistContext = therapistContext;
            this.mapper = mapper;
            config = configuration;
            _connectionString = configuration.GetConnectionString("DBCNSTR");
            commonEmailsService = ICommonEmailsService;

        }

        public async Task<ProfileInfo> GetUserProfile(int? userId, Guid? guid)
        {
            try
            {
                ProfileInfo profile = new ProfileInfo();
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    var result = await con.QueryMultipleAsync("dbo.SSP_getProfileDetails", new
                    {
                        UserId = userId,
                        ProfileGuid = guid
                    }, commandType: CommandType.StoredProcedure);

                    profile = await result.ReadFirstOrDefaultAsync<ProfileInfo>();
                    if (profile != null)
                    {
                        var specialities = await result.ReadAsync<Specialities>();
                        if (specialities.Count() > 0)
                            profile.Specialities = specialities.Where(x => x.UserId == profile.UserId).ToList();
                    }
                }

                return profile;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<long> CreateSession(Session session)
        {
            try
            {
                if (session.TimezoneId == 0)
                {
                    session.TimezoneId = this.therapistContext.UserProfile.Where(x => x.UserId == session.UserId).Select(x => x.TimezoneId).FirstOrDefault().Value;
                }

                this.therapistContext.Session.Add(session);
                await this.therapistContext.SaveChangesAsync();
                return session.SessionId;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<long> UpdateSession(SessionRequest sessionRequest)
        {
            try
            {
                Session session = await this.therapistContext.Session.Where(x => x.SessionId == sessionRequest.sessionId).FirstOrDefaultAsync();
                session.SessionTitle = sessionRequest.SessionTitle;
                session.TopicId = sessionRequest.TopicId;
                session.SessionType = sessionRequest.SessionType;
                session.SessionShotDescription = sessionRequest.SessionShotDescription;
                session.SessionDescription = sessionRequest.SessionDescription;
                session.SessionDate = sessionRequest.SessionDate;
                session.SessionTime = sessionRequest.SessionTime;
                session.SessionLength = sessionRequest.SessionLength;
                session.NumberOfSeats = sessionRequest.NumberOfSeats;
                session.SeatPrice = sessionRequest.SeatPrice;
                session.IsWaitingList = sessionRequest.IsWaitingList;
                session.IsWaitingVideoRecording = sessionRequest.IsWaitingVideoRecording;
                session.PublishDate = sessionRequest.PublishDate;
                session.PublishTime = sessionRequest.PublishTime;
                session.ModifiedDate = DateTime.UtcNow;
                session.ModifiedBy = sessionRequest.CreatedBy;
                session.FirstResponder = sessionRequest.FirstResponder;
                session.UserId = sessionRequest.UserId;
                session.TimezoneId = sessionRequest.TimezoneId;
                //  session.IsAccepted = sessionRequest.IsAccepted;
                session.IsAccepted = sessionRequest.IsAccepted;
                session.AcceptedBy = sessionRequest.AcceptedBy;
                session.AcceptedDate = sessionRequest.AcceptedDate;

                await this.therapistContext.SaveChangesAsync();

                return session.SessionId;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<int> SaveTags(long sessionId, int userId, int[] tags)
        {
            try
            {

                List<SessionTag> sessionTags = new List<SessionTag>();
                foreach (int element in tags)
                {
                    SessionTag sessionTag = new SessionTag();
                    sessionTag.SessionTagId = 0;
                    sessionTag.SessionId = sessionId;
                    sessionTag.TagId = element;
                    sessionTag.CreatedDate = DateTime.UtcNow;
                    sessionTag.CreatedBy = userId;
                    sessionTag.ModifiedDate = null;
                    sessionTag.ModifiedBy = null;
                    sessionTag.IsActive = true;
                    sessionTag.IsDeleted = false;
                    sessionTags.Add(sessionTag);
                }
                this.therapistContext.SessionTag.AddRange(sessionTags);
                return await this.therapistContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<int> UpdateTags(long sessionId, int userId, int[] tags)
        {
            try
            {
                int[] sessionsTag = this.therapistContext.SessionTag.Where(x => x.SessionId == sessionId).Select(x => x.TagId).ToArray();

                var intersect = tags.Intersect(sessionsTag);

                var added = tags.Except(sessionsTag).ToArray();
                var removed = sessionsTag.Except(tags).ToArray();

                foreach (int element in removed)
                {
                    var data = this.therapistContext.SessionTag.Where(x => x.SessionId == sessionId && x.TagId == element).FirstOrDefault();
                    data.IsDeleted = true;
                    this.therapistContext.SaveChanges();
                }


                List<SessionTag> sessionTags = new List<SessionTag>();
                foreach (int element in added)
                {
                    SessionTag sessionTag = new SessionTag();
                    sessionTag.SessionTagId = 0;
                    sessionTag.SessionId = sessionId;
                    sessionTag.TagId = element;
                    sessionTag.CreatedDate = DateTime.UtcNow;
                    sessionTag.CreatedBy = userId;
                    sessionTag.ModifiedDate = null;
                    sessionTag.ModifiedBy = null;
                    sessionTag.IsActive = true;
                    sessionTag.IsDeleted = false;
                    sessionTags.Add(sessionTag);
                }
                if (sessionTags.Count != 0)
                {
                    this.therapistContext.SessionTag.AddRange(sessionTags);
                    return await this.therapistContext.SaveChangesAsync();
                }
                return userId;


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<ListVm>> GetTags()
        {
            try
            {
                return await this.therapistContext.MasterTag.Where(x => x.IsActive == true).Select(y => new ListVm { Value = y.TagId, ViewValue = y.TagName, }).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<ListVm>> GetTopics()
        {
            try
            {
                return await this.therapistContext.MasterTopic.Where(x => x.IsActive == true && x.IsDeleted == false).Select(y => new ListVm { Value = y.TopicId, ViewValue = y.TopicName }).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<ListVm>> GetTimezones()
        {
            try
            {
                return await this.therapistContext.MasterTimezone.Where(x => x.IsActive == true).Select(y => new ListVm { Value = (int)(y.TimezoneId), ViewValue = y.TimezoneText }).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<SessionList>> GetFilteredSessionList(FilteredSessionListRequest request, HttpContext context)
        {
            try
            {
                IEnumerable<SessionList> sessionLists = new List<SessionList>();
                IEnumerable<Timezone> userTimezone = new List<Timezone>();

                if (request.StartDate != null && request.EndDate != null)
                {
                    request.StartDate = request.StartDate.Value.AddDays(-1);
                    request.EndDate = request.EndDate.Value.AddDays(1);
                }

                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    var result = await con.QueryMultipleAsync("dbo.SSP_getFilteredSessionList", new
                    {

                        PractitionerId = request.PractitionerId,
                        MemberId = request.MemberId,
                        Category = request.Category,
                        SessionName = request.SessionName,
                        Type = request.Type,
                        StartDate = request.StartDate,
                        EndDate = request.EndDate,
                        OffSet = request.OffSet,
                        Limit = request.Limit,
                    }, commandType: CommandType.StoredProcedure);
                    userTimezone = await result.ReadAsync<Timezone>();
                    sessionLists = await result.ReadAsync<SessionList>();
                }

                if (sessionLists.Count() == 0)
                    return null;

                foreach (var item in sessionLists)
                {

                    DateTime dt = new DateTime(item.SessionDate.Value.Year, item.SessionDate.Value.Month, item.SessionDate.Value.Day,
                 item.SessionTime.Value.Hour, item.SessionTime.Value.Minute, item.SessionTime.Value.Second);
                    //TimeZoneInfo timezon = TimeZoneInfo.FindSystemTimeZoneById(userTimezone.FirstOrDefault().TimezoneName);
                    //DateTime convertedtime = TimeZoneInfo.ConvertTimeFromUtc(item.SessionTime.Value, timezon);
                    DateTime dataTimeByZoneId = CommonConversions.TimezoneConversion(dt, item.TimeZoneName, userTimezone.FirstOrDefault().TimezoneName);
                    item.SessionDate = new DateTime(dataTimeByZoneId.Ticks);
                    item.SessionTime = dataTimeByZoneId;

                }
                if (request.StartDate != null && request.EndDate != null)
                {
                    sessionLists = sessionLists.Where(x => x.SessionDate.Value.Date >= request.StartDate.Value.AddDays(1).Date && x.SessionDate.Value.Date <= request.EndDate.Value.AddDays(-1).Date).ToList();

                }
                return sessionLists;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<CategoryList>> GetSessionList(SessionListRequest request, HttpContext context)
        {
            try
            {
                string TypeName = null;
                bool? Full = null;
                bool? Available = null;
                IEnumerable<CategoryList> categories = new List<CategoryList>();
                IEnumerable<SessionList> sessionLists = new List<SessionList>();
                IEnumerable<TagsVm> vms = new List<TagsVm>();
                IEnumerable<Timezone> userTimezone = new List<Timezone>();
                Sessionrecords sessionrecords = new Sessionrecords();
                if (request.Open)
                    TypeName = "Open";
                if (request.Private)
                    TypeName = "Private";
                if (request.Open && request.Private)
                    TypeName = null;
                if (request.Type == "Full")
                    Full = true;
                if (request.Type == "Available")
                    Available = true;
                if (request.StartDate != null && request.EndDate != null)
                {
                    request.StartDate = request.StartDate.Value.AddDays(-1);
                    request.EndDate = request.EndDate.Value.AddDays(1);
                }

                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    var result = await con.QueryMultipleAsync("dbo.SSP_getSessionList", new
                    {
                        Keyword = request.Tag,
                        PractitionerId = request.PractitionerId,
                        Topic = request.Topic,
                        Title = request.Title,
                        PractitionerName = request.PractitionerName,
                        TypeName = TypeName,
                        StartDate = request.StartDate,
                        EndDate = request.EndDate,
                        Full = Full,
                        Available = Available,
                        MemberId = request.MemberId,
                    }, commandType: CommandType.StoredProcedure);

                    categories = await result.ReadAsync<CategoryList>();
                    userTimezone = await result.ReadAsync<Timezone>();
                    vms = await result.ReadAsync<TagsVm>();
                    sessionLists = await result.ReadAsync<SessionList>();
                }

                if (sessionLists.Count() == 0)
                    return null;
                sessionrecords.TotalRecords = sessionLists.Count();

                foreach (var item in sessionLists)
                {
                    if (item.ProfilePhoto != null)
                    {

                        item.ProfilePhoto = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "\\images\\" + item.ProfilePhoto;
                    }
                    if (item.SessionType == "Open")
                        sessionrecords.OpenRecords++;
                    else
                        sessionrecords.PrivateRecords++;
                    item.Tags = vms.Where(m => m.SessionId.Equals(item.SessionID)).ToList();

                    DateTime dt = new DateTime(item.SessionDate.Value.Year, item.SessionDate.Value.Month, item.SessionDate.Value.Day,
                 item.SessionTime.Value.Hour, item.SessionTime.Value.Minute, item.SessionTime.Value.Second);
                    //TimeZoneInfo timezon = TimeZoneInfo.FindSystemTimeZoneById(userTimezone.FirstOrDefault().TimezoneName);
                    //DateTime convertedtime = TimeZoneInfo.ConvertTimeFromUtc(item.SessionTime.Value, timezon);
                    DateTime dataTimeByZoneId = CommonConversions.TimezoneConversion(dt, item.TimeZoneName, userTimezone.FirstOrDefault().TimezoneName);
                    item.SessionDate = new DateTime(dataTimeByZoneId.Ticks);
                    item.SessionTime = dataTimeByZoneId;

                }
                if (request.StartDate != null && request.EndDate != null)
                {
                    sessionLists = sessionLists.Where(x => x.SessionDate.Value.Date >= request.StartDate.Value.AddDays(1).Date && x.SessionDate.Value.Date <= request.EndDate.Value.AddDays(-1).Date).ToList();

                }
                if (sessionLists.Count() == 0)
                    return null;
                foreach (var item in categories)
                {
                    item.Sessionrecords = sessionrecords;
                    item.sessionLists = sessionLists.Where(m => m.TopicId.Equals(item.CategoryId)).ToList();
                }
                return categories;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<CategoryList>> GetSessionListMember(SessionListRequest request, HttpContext context)
        {
            try
            {
                string TypeName = null;
                bool? Waiting = null;
                bool? Booked = null;
                bool? Available = null;
                IEnumerable<CategoryList> categories = new List<CategoryList>();
                IEnumerable<SessionList> sessionLists = new List<SessionList>();
                IEnumerable<TagsVm> vms = new List<TagsVm>();
                IEnumerable<Timezone> userTimezone = new List<Timezone>();
                Sessionrecords sessionrecords = new Sessionrecords();
                if (request.Open)
                    TypeName = "Open";
                if (request.Private)
                    TypeName = "Private";
                if (request.Open && request.Private)
                    TypeName = null;
                if (request.Type == "Booked" || request.onlyConfirmed)
                    Booked = true;
                if (request.Type == "Waiting")
                    Waiting = true;
                if (request.Type == "Available")
                    Available = true;
                if (request.StartDate != null && request.EndDate != null)
                {
                    request.StartDate = request.StartDate.Value.AddDays(-1);
                    request.EndDate = request.EndDate.Value.AddDays(1);
                }

                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    var result = await con.QueryMultipleAsync("dbo.SSP_getSessionListMember", new
                    {
                        Keyword = request.Tag,
                        PractitionerId = request.PractitionerId,
                        Topic = request.Topic,
                        Title = request.Title,
                        PractitionerName = request.PractitionerName,
                        TypeName = TypeName,
                        StartDate = request.StartDate,
                        EndDate = request.EndDate,
                        Booked = Booked,
                        Waiting = Waiting,
                        MemberId = request.MemberId,
                        Next = request.Next,
                        Previous = request.Previous,
                        Available = Available
                    }, commandType: CommandType.StoredProcedure);

                    categories = await result.ReadAsync<CategoryList>();
                    userTimezone = await result.ReadAsync<Timezone>();
                    vms = await result.ReadAsync<TagsVm>();
                    sessionLists = await result.ReadAsync<SessionList>();
                }

                if (sessionLists.Count() == 0)
                    return null;
                sessionrecords.TotalRecords = sessionLists.Count();

                foreach (var item in sessionLists)
                {
                    if (item.ProfilePhoto != null)
                    {

                        item.ProfilePhoto = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "\\images\\" + item.ProfilePhoto;
                    }
                    if (item.SessionType == "Open")
                        sessionrecords.OpenRecords++;
                    else
                        sessionrecords.PrivateRecords++;
                    item.Tags = vms.Where(m => m.SessionId.Equals(item.SessionID)).ToList();
                    ///for session date and time conversion///
                    DateTime dt = new DateTime(item.SessionDate.Value.Year, item.SessionDate.Value.Month, item.SessionDate.Value.Day,
                    item.SessionTime.Value.Hour, item.SessionTime.Value.Minute, item.SessionTime.Value.Second);
                    DateTime dataTimeByZoneId = CommonConversions.TimezoneConversion(dt, item.TimeZoneName, userTimezone.FirstOrDefault().TimezoneName);
                    item.SessionDate = new DateTime(dataTimeByZoneId.Ticks);
                    item.SessionTime = dataTimeByZoneId;
                    ////for publish date and time conversion/////
                    DateTime pt = new DateTime(item.PublishDate.Value.Year, item.PublishDate.Value.Month, item.PublishDate.Value.Day,
                    item.PublishTime.Value.Hour, item.PublishTime.Value.Minute, item.PublishTime.Value.Second);
                    DateTime publishdate = CommonConversions.TimezoneConversion(pt, item.TimeZoneName, userTimezone.FirstOrDefault().TimezoneName);
                    item.PublishDate = new DateTime(publishdate.Ticks);
                    item.PublishTime = publishdate;
                }
                var utc = DateTime.UtcNow;
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(userTimezone.FirstOrDefault().TimezoneName);
                DateTime converted = TimeZoneInfo.ConvertTimeFromUtc(utc, cstZone);
                sessionLists = sessionLists.Where(x => x.PublishDate.Value.Date <= converted.Date).ToList();
                if (sessionLists.Count() == 0)
                    return null;
                if (request.StartDate != null && request.EndDate != null)
                {
                    sessionLists = sessionLists.Where(x => x.SessionDate.Value.Date >= request.StartDate.Value.AddDays(1).Date && x.SessionDate.Value.Date <= request.EndDate.Value.AddDays(-1).Date).ToList();

                }
                if (request.Previous == true)
                {
                    sessionLists = sessionLists.Where(x => x.SessionDate.Value.Date < converted.Date).ToList();

                }
                if (request.Next == true)
                {
                    sessionLists = sessionLists.Where(x => x.SessionDate.Value.Date >= converted.Date).ToList();
                }
                if (sessionLists.Count() == 0)
                    return null;
                foreach (var item in categories)
                {
                    item.Sessionrecords = sessionrecords;
                    item.sessionLists = sessionLists.Where(m => m.TopicId.Equals(item.CategoryId)).ToList();
                }
                return categories;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<SessionList>> PractitionerRelated(int practitionerId, int userid, HttpContext context)
        {
            try
            {
                IEnumerable<SessionList> sessionLists = new List<SessionList>();
                IEnumerable<TagsVm> vms = new List<TagsVm>();
                IEnumerable<Timezone> userTimezone = new List<Timezone>();
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    var result = await con.QueryMultipleAsync("dbo.SSP_getPractitionerRealtedSessionList", new
                    {
                        PractitionerId = practitionerId,
                        MemberId = userid
                    }, commandType: CommandType.StoredProcedure);

                    userTimezone = await result.ReadAsync<Timezone>();
                    vms = await result.ReadAsync<TagsVm>();
                    sessionLists = await result.ReadAsync<SessionList>();
                }
                if (sessionLists.Count() == 0)
                    return null;
                foreach (var item in sessionLists)
                {
                    if (item.ProfilePhoto != null)
                    {

                        item.ProfilePhoto = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "\\images\\" + item.ProfilePhoto;
                    }
                    item.Tags = vms.Where(m => m.SessionId.Equals(item.SessionID)).ToList();
                    ///for session date and time conversion///
                    DateTime dt = new DateTime(item.SessionDate.Value.Year, item.SessionDate.Value.Month, item.SessionDate.Value.Day,
                    item.SessionTime.Value.Hour, item.SessionTime.Value.Minute, item.SessionTime.Value.Second);
                    DateTime dataTimeByZoneId = CommonConversions.TimezoneConversion(dt, item.TimeZoneName, userTimezone.FirstOrDefault().TimezoneName);
                    item.SessionDate = new DateTime(dataTimeByZoneId.Ticks);
                    item.SessionTime = dataTimeByZoneId;
                    ////for publish date and time conversion/////
                    DateTime pt = new DateTime(item.PublishDate.Value.Year, item.PublishDate.Value.Month, item.PublishDate.Value.Day,
                    item.PublishTime.Value.Hour, item.PublishTime.Value.Minute, item.PublishTime.Value.Second);
                    DateTime publishdate = CommonConversions.TimezoneConversion(pt, item.TimeZoneName, userTimezone.FirstOrDefault().TimezoneName);
                    item.PublishDate = new DateTime(publishdate.Ticks);
                    item.PublishTime = publishdate;
                }
                var utc = DateTime.UtcNow;
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(userTimezone.FirstOrDefault().TimezoneName);
                DateTime converted = TimeZoneInfo.ConvertTimeFromUtc(utc, cstZone);
                sessionLists = sessionLists.Where(x => x.PublishDate.Value.Date <= converted.Date).ToList();
                if (sessionLists.Count() == 0)
                    return null;
                sessionLists = sessionLists.Where(x => x.SessionDate.Value.Date >= converted.Date).ToList();
                if (sessionLists.Count() == 0)
                    return null;
                else
                    return sessionLists;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<List<SessionList>> TagsRelatedSession(int? userid, string tagids, HttpContext context)
        {
            try
            {
                IEnumerable<SessionList> sessionLists = new List<SessionList>();
                IEnumerable<TagsVm> vms = new List<TagsVm>();
                IEnumerable<Timezone> userTimezone = new List<Timezone>();
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    var result = await con.QueryMultipleAsync("dbo.SSP_getRelatedSession", new
                    {
                        MemberId = userid,
                        TagId = tagids
                    }, commandType: CommandType.StoredProcedure);

                    userTimezone = await result.ReadAsync<Timezone>();
                    vms = await result.ReadAsync<TagsVm>();
                    sessionLists = await result.ReadAsync<SessionList>();
                }
                if (sessionLists.Count() == 0)
                    return null;
                foreach (var item in sessionLists)
                {
                    if (item.ProfilePhoto != null)
                    {

                        item.ProfilePhoto = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "\\images\\" + item.ProfilePhoto;
                    }
                    item.Tags = vms.Where(m => m.SessionId.Equals(item.SessionID)).ToList();
                    ///for session date and time conversion///
                    DateTime dt = new DateTime(item.SessionDate.Value.Year, item.SessionDate.Value.Month, item.SessionDate.Value.Day,
                    item.SessionTime.Value.Hour, item.SessionTime.Value.Minute, item.SessionTime.Value.Second);
                    DateTime dataTimeByZoneId = CommonConversions.TimezoneConversion(dt, item.TimeZoneName, userTimezone.FirstOrDefault().TimezoneName);
                    item.SessionDate = new DateTime(dataTimeByZoneId.Ticks);
                    item.SessionTime = dataTimeByZoneId;
                    ////for publish date and time conversion/////
                    DateTime pt = new DateTime(item.PublishDate.Value.Year, item.PublishDate.Value.Month, item.PublishDate.Value.Day,
                    item.PublishTime.Value.Hour, item.PublishTime.Value.Minute, item.PublishTime.Value.Second);
                    DateTime publishdate = CommonConversions.TimezoneConversion(pt, item.TimeZoneName, userTimezone.FirstOrDefault().TimezoneName);
                    item.PublishDate = new DateTime(publishdate.Ticks);
                    item.PublishTime = publishdate;
                }
                var utc = DateTime.UtcNow;
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(userTimezone.FirstOrDefault().TimezoneName);
                DateTime converted = TimeZoneInfo.ConvertTimeFromUtc(utc, cstZone);
                sessionLists = sessionLists.Where(x => x.PublishDate.Value.Date <= converted.Date).ToList();
                if (sessionLists.Count() == 0)
                    return null;
                sessionLists = sessionLists.Where(x => x.SessionDate.Value.Date >= converted.Date).ToList();
                if (sessionLists.Count() == 0)
                    return null;
                else
                    return sessionLists.ToList();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<IEnumerable<CategoryList>> GetAllSession(SessionListRequest request, HttpContext context)
        {
            try
            {
                string TypeName = null;
                bool? Full = null;
                bool? Available = null;
                IEnumerable<CategoryList> categories = new List<CategoryList>();
                IEnumerable<SessionList> sessionLists = new List<SessionList>();
                IEnumerable<TagsVm> vms = new List<TagsVm>();
                IEnumerable<Timezone> userTimezone = new List<Timezone>();
                Sessionrecords sessionrecords = new Sessionrecords();
                if (request.Open)
                    TypeName = "Open";
                if (request.Private)
                    TypeName = "Private";
                if (request.Open && request.Private)
                    TypeName = null;
                if (request.Type == "Full")
                    Full = true;
                if (request.Type == "Available")
                    Available = true;

                if (request.StartDate != null && request.EndDate != null)
                {
                    request.StartDate = request.StartDate.Value.AddDays(-1);
                    request.EndDate = request.EndDate.Value.AddDays(1);
                }
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    var result = await con.QueryMultipleAsync("dbo.SSP_getAllSessionList", new
                    {
                        TopicId = request.TopicId,
                        Keyword = request.Tag,
                        PractitionerId = request.PractitionerId,
                        Topic = request.Topic,
                        Title = request.Title,
                        PractitionerName = request.PractitionerName,
                        TypeName = TypeName,
                        StartDate = request.StartDate,
                        EndDate = request.EndDate,
                        Full = Full,
                        Available = Available,
                        MemberId = request.MemberId,
                    }, commandType: CommandType.StoredProcedure);

                    categories = await result.ReadAsync<CategoryList>();
                    userTimezone = await result.ReadAsync<Timezone>();
                    vms = await result.ReadAsync<TagsVm>();
                    sessionLists = await result.ReadAsync<SessionList>();
                }

                if (sessionLists.Count() == 0)
                    return null;
                sessionrecords.TotalRecords = sessionLists.Count();


                foreach (var item in sessionLists)
                {
                    if (item.ProfilePhoto != null)
                    {

                        item.ProfilePhoto = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "\\images\\" + item.ProfilePhoto;
                    }
                    if (item.SessionType == "Open")
                        sessionrecords.OpenRecords++;
                    else
                        sessionrecords.PrivateRecords++;
                    item.Tags = vms.Where(m => m.SessionId.Equals(item.SessionID)).ToList();

                    DateTime dt = new DateTime(item.SessionDate.Value.Year, item.SessionDate.Value.Month, item.SessionDate.Value.Day,
                   item.SessionTime.Value.Hour, item.SessionTime.Value.Minute, item.SessionTime.Value.Second);
                    //TimeZoneInfo timezon = TimeZoneInfo.FindSystemTimeZoneById(userTimezone.FirstOrDefault().TimezoneName);
                    // DateTime convertedtime = TimeZoneInfo.ConvertTimeFromUtc(item.SessionTime.Value, timezon);
                    DateTime dataTimeByZoneId = CommonConversions.TimezoneConversion(dt, item.TimeZoneName, userTimezone.FirstOrDefault().TimezoneName);
                    item.SessionDate = new DateTime(dataTimeByZoneId.Ticks);
                    item.SessionTime = dataTimeByZoneId;
                }
                if (request.StartDate != null && request.EndDate != null)
                {
                    sessionLists = sessionLists.Where(x => x.SessionDate.Value.Date >= request.StartDate.Value.AddDays(1).Date && x.SessionDate.Value.Date <= request.EndDate.Value.AddDays(-1).Date).ToList();

                }
                if (sessionLists.Count() == 0)
                    return null;
                foreach (var item in categories)
                {
                    item.Sessionrecords = sessionrecords;
                    item.sessionLists = sessionLists.Where(m => m.TopicId.Equals(item.CategoryId)).ToList();
                }
                return categories;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<CategoryList>> GetAllSessionMember(SessionListRequest request, HttpContext context)
        {
            try
            {
                string TypeName = null;
                bool? Waiting = null;
                bool? Booked = null;
                bool? Available = null;
                IEnumerable<CategoryList> categories = new List<CategoryList>();
                IEnumerable<SessionList> sessionLists = new List<SessionList>();
                IEnumerable<TagsVm> vms = new List<TagsVm>();
                IEnumerable<Timezone> userTimezone = new List<Timezone>();
                Sessionrecords sessionrecords = new Sessionrecords();
                if (request.Open)
                    TypeName = "Open";
                if (request.Private)
                    TypeName = "Private";
                if (request.Open && request.Private)
                    TypeName = null;
                if (request.Type == "Booked" || request.onlyConfirmed)
                    Booked = true;
                if (request.Type == "Waiting")
                    Waiting = true;
                if (request.Type == "Available")
                    Available = true;
                if (request.StartDate != null && request.EndDate != null)
                {
                    request.StartDate = request.StartDate.Value.AddDays(-1);
                    request.EndDate = request.EndDate.Value.AddDays(1);
                }
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    var result = await con.QueryMultipleAsync("dbo.SSP_getAllSessionListMember", new
                    {
                        // TopicId = request.TopicId,
                        Keyword = request.Tag,
                        PractitionerId = request.PractitionerId,
                        Topic = request.Topic,
                        Title = request.Title,
                        PractitionerName = request.PractitionerName,
                        TypeName = TypeName,
                        StartDate = request.StartDate,
                        EndDate = request.EndDate,
                        Booked = Booked,
                        Waiting = Waiting,
                        MemberId = request.MemberId,
                        Next = request.Next,
                        Previous = request.Previous,
                        Available = Available
                    }, commandType: CommandType.StoredProcedure);

                    categories = await result.ReadAsync<CategoryList>();
                    userTimezone = await result.ReadAsync<Timezone>();
                    vms = await result.ReadAsync<TagsVm>();
                    sessionLists = await result.ReadAsync<SessionList>();
                }

                if (sessionLists.Count() == 0)
                    return null;
                sessionrecords.TotalRecords = sessionLists.Count();


                foreach (var item in sessionLists)
                {
                    if (item.ProfilePhoto != null)
                    {

                        item.ProfilePhoto = context.Request.Scheme + "://" + context.Request.Host + context.Request.PathBase + "\\images\\" + item.ProfilePhoto;
                    }
                    if (item.SessionType == "Open")
                        sessionrecords.OpenRecords++;
                    else
                        sessionrecords.PrivateRecords++;
                    item.Tags = vms.Where(m => m.SessionId.Equals(item.SessionID)).ToList();
                    ///for session date and time conversion///
                    DateTime dt = new DateTime(item.SessionDate.Value.Year, item.SessionDate.Value.Month, item.SessionDate.Value.Day,
                    item.SessionTime.Value.Hour, item.SessionTime.Value.Minute, item.SessionTime.Value.Second);
                    DateTime dataTimeByZoneId = CommonConversions.TimezoneConversion(dt, item.TimeZoneName, userTimezone.FirstOrDefault().TimezoneName);
                    item.SessionDate = new DateTime(dataTimeByZoneId.Ticks);
                    item.SessionTime = dataTimeByZoneId;
                    ////for publish date and time conversion/////
                    DateTime pt = new DateTime(item.PublishDate.Value.Year, item.PublishDate.Value.Month, item.PublishDate.Value.Day,
                    item.PublishTime.Value.Hour, item.PublishTime.Value.Minute, item.PublishTime.Value.Second);
                    DateTime publishdate = CommonConversions.TimezoneConversion(pt, item.TimeZoneName, userTimezone.FirstOrDefault().TimezoneName);
                    item.PublishDate = new DateTime(publishdate.Ticks);
                    item.PublishTime = publishdate;

                }
                var utc = DateTime.UtcNow;
                TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(userTimezone.FirstOrDefault().TimezoneName);
                DateTime converted = TimeZoneInfo.ConvertTimeFromUtc(utc, cstZone);
                sessionLists = sessionLists.Where(x => x.PublishDate.Value.Date <= converted.Date).ToList();
                if (sessionLists.Count() == 0)
                    return null;
                if (request.StartDate != null && request.EndDate != null)
                {
                    sessionLists = sessionLists.Where(x => x.SessionDate.Value.Date >= request.StartDate.Value.AddDays(1).Date && x.SessionDate.Value.Date <= request.EndDate.Value.AddDays(-1).Date).ToList();

                }
                if (request.Previous == true)
                {
                    sessionLists = sessionLists.Where(x => x.SessionDate.Value.Date < converted.Date).ToList();

                }
                if (request.Next == true)
                {
                    sessionLists = sessionLists.Where(x => x.SessionDate.Value.Date >= converted.Date).ToList();
                }
                if (sessionLists.Count() == 0)
                    return null;
                foreach (var item in categories)
                {
                    item.Sessionrecords = sessionrecords;
                    item.sessionLists = sessionLists.Where(m => m.TopicId.Equals(item.CategoryId)).ToList();
                }
                return categories;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<SesionCounts> GetAllSessionCountMember(int memberId)
        {
            try
            {
                SesionCounts counts = new SesionCounts();
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    var result = await con.QueryMultipleAsync("dbo.SSP_getAllSessionCountMember", new
                    {
                        MemberId = memberId
                    }, commandType: CommandType.StoredProcedure);

                    counts.PreviousCount = await result.ReadFirstOrDefaultAsync<int>();
                    counts.WaitingCount = await result.ReadFirstOrDefaultAsync<int>();
                    counts.UpcomingCount = await result.ReadFirstOrDefaultAsync<int>();
                    counts.AllCount = await result.ReadFirstOrDefaultAsync<int>();
                    counts.AvailableCount = await result.ReadFirstOrDefaultAsync<int>();
                }
                return counts;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<SessionDetails> SessionDetails(Guid guid, int? userId, HttpContext context)
        {
            try
            {
                SessionDetails sessionLists = new SessionDetails();
                string timezone = "";
                IEnumerable<TagsVm> vms = new List<TagsVm>();
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    var result = await con.QueryMultipleAsync("dbo.SSP_getSessionDetails", new
                    {
                        SessionGuId = guid
                    }, commandType: CommandType.StoredProcedure);
                    sessionLists = await result.ReadFirstOrDefaultAsync<SessionDetails>();
                    vms = await result.ReadAsync<TagsVm>();
                }
                if (sessionLists != null)
                {
                    if (vms.Count() > 0)
                    {
                        sessionLists.Tags = vms.Where(x => x.SessionId == sessionLists.SessionID).ToList();
                        string tagids = String.Join(",", sessionLists.Tags.Select(p => p.TagId));
                        if (userId != null)
                            sessionLists.RelatedSessions = await this.TagsRelatedSession(userId, tagids, context);
                    }
                    if (userId != null)
                    {
                        using (IDbConnection con = new SqlConnection(_connectionString))
                        {
                            var result = await con.QueryMultipleAsync("dbo.SSP_getUserTimezoneDetails", new
                            {
                                UserId = userId
                            }, commandType: CommandType.StoredProcedure);
                            timezone = await result.ReadFirstOrDefaultAsync<string>();

                        }

                    }
                    else
                    {
                        using (IDbConnection con = new SqlConnection(_connectionString))
                        {
                            var result = await con.QueryMultipleAsync("dbo.SSP_getUserTimezoneDetails", new
                            {
                                UserId = sessionLists.PractitionerId
                            }, commandType: CommandType.StoredProcedure);
                            timezone = await result.ReadFirstOrDefaultAsync<string>();

                        }
                    }
                    DateTime dt = new DateTime(sessionLists.SessionDate.Value.Year, sessionLists.SessionDate.Value.Month, sessionLists.SessionDate.Value.Day,
                    sessionLists.SessionTime.Value.Hour, sessionLists.SessionTime.Value.Minute, sessionLists.SessionTime.Value.Second);

                    //TimeZoneInfo cstZone = TimeZoneInfo.FindSystemTimeZoneById(timezone);
                    // DateTime cstTime = TimeZoneInfo.ConvertTimeFromUtc(dt, cstZone);

                    DateTime dataTimeByZoneId = CommonConversions.TimezoneConversion(dt, sessionLists.TimezoneName, timezone);
                    sessionLists.SessionDate = new DateTime(dataTimeByZoneId.Ticks);
                    sessionLists.SessionTime = dataTimeByZoneId;

                }

                return sessionLists;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<SessionBookingData> SessionBookingDetails(Guid guid, int userId)
        {
            try
            {
                // SessionDetails sessionLists = new SessionDetails();
                SessionBookingData data = new SessionBookingData();
                var session = await this.therapistContext.Session.Where(x => x.SessionGuid == guid).FirstOrDefaultAsync();
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    var result = await con.QueryMultipleAsync("dbo.SSP_getSessionBookingDetails", new
                    {
                        SessionGuid = guid,
                        UserId = userId
                    }, commandType: CommandType.StoredProcedure);
                    data = await result.ReadFirstOrDefaultAsync<SessionBookingData>();
                }
                if (data != null)
                {
                    if (data.SessionBookingStatus == 1)
                    {
                        data.IsBooked = true;
                    }
                    else
                    {
                        data.IsBooked = false;
                    }
                }
                else
                {
                    data = new SessionBookingData();
                    data.SessionId = session.SessionId;
                    data.IsBooked = false;
                    data.SessionBookingId = 0;
                    return data;
                }
                return data;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<SessionDetails> getSessionDetailsData(Guid guid)
        {
            SessionDetails sessionLists = new SessionDetails();
            var session = this.therapistContext.Session.Where(x => x.SessionGuid == guid).FirstOrDefault();
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                var result = await con.QueryMultipleAsync("dbo.SSP_getSessionDetails", new
                {
                    SessionGuId = guid
                }, commandType: CommandType.StoredProcedure);
                sessionLists = await result.ReadFirstOrDefaultAsync<SessionDetails>();
            }
            sessionLists.SessionGuid = guid;
            return sessionLists;
        }

        public async Task<bool> BookSession(Guid guid, int userId, HttpContext httpContext)
        {
            try
            {

                Guid BookingGuid = Guid.NewGuid();
                SessionDetails sessionList = await this.getSessionDetailsData(guid);
                // var session = this.therapistContext.Session.Where(x => x.SessionGuid == guid).FirstOrDefault();
                SessionBooking sessionBooking = await this.therapistContext.SessionBooking.Where(x => x.SessionId == sessionList.SessionID && x.UserId == userId).FirstOrDefaultAsync();
                if (sessionBooking != null)
                {
                    sessionBooking.ModifiedBy = userId;
                    sessionBooking.ModifiedDate = DateTime.UtcNow;
                    sessionBooking.SessionBookingStatus = 1;
                    await this.therapistContext.SaveChangesAsync();
                }
                else
                {
                    sessionBooking = new SessionBooking();

                    if (sessionList != null)
                    {
                        sessionBooking.SessionId = sessionList.SessionID;
                        sessionBooking.UserId = userId;
                        sessionBooking.BookingDate = DateTime.UtcNow;
                        sessionBooking.CreatedBy = userId;
                        sessionBooking.CreatedDate = DateTime.UtcNow;
                        sessionBooking.IsActive = true;
                        sessionBooking.IsDeleted = false;
                        sessionBooking.SessionBookingGuid = BookingGuid;
                        sessionBooking.SessionBookingStatus = 1;
                        sessionBooking.SessionType = (sessionList.SessionType == "Open") ? 1 : 2;


                        this.therapistContext.SessionBooking.Add(sessionBooking);
                        await this.therapistContext.SaveChangesAsync();
                    }
                    else
                    {
                        return false;
                    }
                }

                SessionBookingData data = await this.getBookingDataDetails(sessionList, sessionBooking, userId);
                if (data.Email != null || data.Email != "")
                {
                    await this.commonEmailsService.SessionBookedEmail(data.Email, data, httpContext);
                }

                return true;
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<SessionBookingData> getBookingDataDetails(SessionDetails sessionList, SessionBooking sessionBooking, int userId)
        {
            SessionBookingData data = new SessionBookingData();

            DateTime dt = new DateTime(sessionList.SessionDate.Value.Year, sessionList.SessionDate.Value.Month, sessionList.SessionDate.Value.Day,
                  sessionList.SessionTime.Value.Hour, sessionList.SessionTime.Value.Minute, sessionList.SessionTime.Value.Second);

            var timezone = "";
            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                var result = await con.QueryMultipleAsync("dbo.SSP_getUserTimezoneDetails", new
                {
                    UserId = userId
                }, commandType: CommandType.StoredProcedure);
                timezone = await result.ReadFirstOrDefaultAsync<string>();

            }

            DateTime dataTimeByZoneId = CommonConversions.TimezoneConversion(dt, sessionList.TimezoneName, timezone);
            sessionList.SessionDate = new DateTime(dataTimeByZoneId.Ticks);
            data.SessionTime = dataTimeByZoneId;
            data.BookingDate = sessionBooking.BookingDate;
            data.SessionBookingStatus = sessionBooking.SessionBookingStatus;
            data.SessionTitle = sessionList.SessionTitle;
            data.SessionDescription = sessionList.SessionDescription;
            data.SessionType = sessionList.SessionType;

            data.SessionTopic = sessionList.TopicName;
            data.SessionGuid = sessionList.SessionGuid;
            data.SessionDate = sessionList.SessionDate;
            data.SessionShotDescription = sessionList.SessionShotDescription;
            data.SessionGuid = sessionList.SessionGuid;

            using (IDbConnection con = new SqlConnection(_connectionString))
            {
                ProfileInfo profile = new ProfileInfo();
                var result = await con.QueryMultipleAsync("dbo.SSP_getProfileDetailsByUserid", new
                {
                    UserId = userId,
                }, commandType: CommandType.StoredProcedure);

                profile = await result.ReadFirstOrDefaultAsync<ProfileInfo>();

                if (profile != null)
                {
                    data.Email = profile.Email;
                }
            }


            return data;
        }
        public async Task<bool> EnableNotification(NotificationRequest notification, HttpContext httpContext)
        {
            try
            {
                var session = this.therapistContext.Session.Where(x => x.SessionId == notification.SessionId).FirstOrDefault();

                SessionBooking sessionBooking = await this.therapistContext.SessionBooking.Where(x => x.SessionId == notification.SessionId && x.UserId == notification.UserId).FirstOrDefaultAsync();
                if (sessionBooking != null)
                {
                    sessionBooking.ModifiedBy = notification.UserId;
                    sessionBooking.ModifiedDate = DateTime.UtcNow;
                    sessionBooking.AllowNotification = true;
                    sessionBooking.SessionBookingStatus = 2;
                    sessionBooking.IsWaitingList = notification.Enable;

                    await this.therapistContext.SaveChangesAsync();
                }
                else
                {
                    sessionBooking = new SessionBooking();


                    sessionBooking.SessionId = session.SessionId;
                    sessionBooking.UserId = notification.UserId;
                    sessionBooking.BookingDate = DateTime.UtcNow;
                    sessionBooking.CreatedBy = notification.UserId;
                    sessionBooking.CreatedDate = DateTime.UtcNow;
                    sessionBooking.IsActive = session.IsActive;
                    sessionBooking.IsDeleted = session.IsDeleted;
                    sessionBooking.SessionBookingGuid = Guid.NewGuid();
                    sessionBooking.SessionBookingStatus = 2;
                    sessionBooking.SessionType = session.SessionType;
                    sessionBooking.AllowNotification = true;
                    sessionBooking.IsWaitingList = notification.Enable;
                    //sessionBooking.SessionTypeNavigation = session.SessionTypeNavigation;
                    this.therapistContext.SessionBooking.Add(sessionBooking);
                    await this.therapistContext.SaveChangesAsync();


                }

                var sessionList = await this.getSessionDetailsData(session.SessionGuid);

                var data = await this.getBookingDataDetails(sessionList, sessionBooking, Int32.Parse(notification.UserId.ToString()));
                if (data.Email != null || data.Email != "")
                {
                    await this.commonEmailsService.SessionWaitingEmail(data.Email, data, httpContext);
                }
                return true;

            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<bool> CancelBookedSession(int bookedId, int sessionId, int userId, HttpContext httpContext)
        {
            try
            {

                SessionDetails sessionLists = new SessionDetails();
                SessionBooking sessionBooking = await this.therapistContext.SessionBooking.Where(x => x.SessionBookingId == bookedId && x.UserId == userId).FirstOrDefaultAsync();
                sessionBooking.CancelBy = userId;
                sessionBooking.CancelDate = DateTime.UtcNow;
                if (sessionBooking.SessionBookingStatus == 2)
                {
                    sessionBooking.SessionBookingStatus = 200;
                }
                else
                {
                    sessionBooking.SessionBookingStatus = 100;

                }

                await this.therapistContext.SaveChangesAsync();
                var session = this.therapistContext.Session.Where(x => x.SessionId == sessionBooking.SessionId).FirstOrDefault();
                var sessionList = await this.getSessionDetailsData(session.SessionGuid);
                var data = await this.getBookingDataDetails(sessionList, sessionBooking, userId);

                if (data.Email != null || data.Email != "")
                {
                    await this.commonEmailsService.SessionCanceledEmail(data.Email, data, httpContext);
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        //public async Task<bool> SendEmailToWaitingUsers(int sessionId, int userId, SessionBookingData data, HttpContext httpContext)
        //{
        //    try
        //    {
        //        IEnumerable<SessionBookedUsers> bookedUsers = null;

        //        if (bookedUsers.Count() > 0)
        //        {

        //            int departid = bookedUsers.Where(x => x.UserId == userId && x.AllowDepartment == true).Select(y => y.DepartmentId).FirstOrDefault();
        //            if (departid > 0)
        //            {
        //                var otherFromSameDepart = bookedUsers.Where(x => x.DepartmentId == departid && x.UserId != userId && x.SessionBookingStatus == 2).ToList();
        //                if (otherFromSameDepart.Count > 0)
        //                {

        //                    foreach (var item in otherFromSameDepart)
        //                    {
        //                        await this.commonEmailsService.SessionAvailableEmail(item.Email, data, httpContext);
        //                    }

        //                }
        //                else
        //                {
        //                    var otherDepart = bookedUsers.Where(x => x.UserId != userId && x.SessionBookingStatus == 2).ToList();
        //                    foreach (var item in otherDepart)
        //                    {
        //                        await this.commonEmailsService.SessionAvailableEmail(item.Email, data, httpContext);
        //                    }
        //                }
        //            }
        //            else
        //            {
        //                var otherDepart = bookedUsers.Where(x => x.UserId != userId && x.SessionBookingStatus == 2).ToList();
        //                foreach (var item in otherDepart)
        //                {
        //                    await this.commonEmailsService.SessionAvailableEmail(item.Email, data, httpContext);
        //                }
        //            }
        //        }


        //        return true;
        //    }
        //    catch (Exception ex)
        //    {
        //        throw ex;
        //    }

        //}
        public async Task<IEnumerable<SessionList>> GetPractitionerSessionList(FilteredSessionListRequest request, HttpContext context)
        {
            try
            {

                IEnumerable<SessionList> sessionLists = new List<SessionList>();
                IEnumerable<Timezone> userTimezone = new List<Timezone>();

                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    var result = await con.QueryMultipleAsync("dbo.SSP_getSessionListPractitioner", new
                    {
                        Keyword = request.Tags,
                        PractitionerId = request.PractitionerId,
                        MemberId = request.MemberId,
                        Category = request.Category,
                        SessionName = request.SessionName,
                        Type = request.Type,
                        StartDate = request.StartDate,
                        EndDate = request.EndDate,
                        FirstResponder = request.FirstResponder,
                        // IsApproved=  request.IsApproved,
                        SessionStatus = request.SessionApprovalStatus, // Admin part
                        SessionDeliveryStatus = request.SessionDeliveryStatus,  // delivery status
                                                                                //   IsApproved = request.IsApproved,
                                                                                //  SessionStatus = request.SessionStatus,
                        CreatedBy = request.CreatedBy,
                        Today = request.Today,
                        Previous = request.Previous,
                        Next = request.Next,
                        //OffSet = request.OffSet,
                        //Limit = request.Limit,
                    }, commandType: CommandType.StoredProcedure);
                    userTimezone = await result.ReadAsync<Timezone>();
                    sessionLists = await result.ReadAsync<SessionList>();
                }

                if (sessionLists.Count() == 0)
                    return null;

                return sessionLists;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> SessionvideoAssign(Guid guid, int videoId, int userId)
        {
            try
            {
                var session = this.therapistContext.Session.Where(x => x.SessionGuid == guid).FirstOrDefault();

                SessionVideos sessiondata = new SessionVideos();

                if (session != null)
                {
                    var videioData = this.therapistContext.SessionVideos.Where(x => x.SessionId == session.SessionId).FirstOrDefault();
                    if (videioData == null)
                    {
                        sessiondata.SessionId = session.SessionId;
                        sessiondata.SessionVideosGuid = Guid.NewGuid();
                        sessiondata.VideosId = videoId;
                        sessiondata.IsActive = true;
                        sessiondata.CreatedDate = DateTime.Now;
                        sessiondata.CreatedBy = userId;

                        this.therapistContext.SessionVideos.Add(sessiondata);
                        await this.therapistContext.SaveChangesAsync();
                    }
                    else
                    {
                        videioData.SessionId = session.SessionId;
                        videioData.VideosId = videoId;

                        await this.therapistContext.SaveChangesAsync();
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> UpdateSessionStatus(CompanySessionsRequest request)
        {
            try
            {
                Session session = await this.therapistContext.Session.Where(x => x.SessionId == request.SessionId && x.IsActive == true && x.SessionStatus == 0).FirstOrDefaultAsync();
                if (session != null)
                {
                    session.ModifiedBy = request.UserId;
                    session.ModifiedDate = DateTime.UtcNow;
                    session.SessionStatus = 100;
                    await this.therapistContext.SaveChangesAsync();
                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        //public async Task<SesionCounts> GetAllSessionCountPractitioner(int practitionerID)
        //{
        //    try
        //    {
        //        SesionCounts counts = new SesionCounts();
        //        using (IDbConnection con = new SqlConnection(_connectionString))
        //        {
        //            var result = await con.QueryMultipleAsync("dbo.SSP_getSessionCountPractitioner", new
        //            {
        //                practitionerID = practitionerID
        //            }, commandType: CommandType.StoredProcedure);

        //            counts.PreviousCount = await result.ReadFirstOrDefaultAsync<int>();
        //            counts.UpcomingCount = await result.ReadFirstOrDefaultAsync<int>();
        //            counts.Approved = await result.ReadFirstOrDefaultAsync<int>();
        //            counts.Pending = await result.ReadFirstOrDefaultAsync<int>();
        //            counts.Rejected = await result.ReadFirstOrDefaultAsync<int>();
        //            counts.Delivered = await result.ReadFirstOrDefaultAsync<int>();
        //            counts.Cancelled = await result.ReadFirstOrDefaultAsync<int>();
        //        }
        //        return counts;
        //    }
        //    catch (Exception ex)
        //    {

        //        throw ex;
        //    }
        //}

        public async Task<bool> SessionStartByPractitioner(CompanySessionsRequest request)
        {
            try
            {
                Session session = await this.therapistContext.Session.Where(x => x.SessionId == request.SessionId && x.IsActive == true && x.SessionStatus == 0).FirstOrDefaultAsync();
                if (session != null)
                {
                    session.ModifiedBy = request.UserId;
                    session.ModifiedDate = DateTime.UtcNow;
                    session.SessionStatus = 1;
                    session.IsSessionStarted = true;
                    session.SessionStart = DateTime.UtcNow;
                    session.SessionStatus = 100;
                    await this.therapistContext.SaveChangesAsync();
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
