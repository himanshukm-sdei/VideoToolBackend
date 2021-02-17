
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
using Twilio.Jwt.AccessToken;
using static Common.Common.CommonUtility;

namespace Repository
{
    public class VideoRepo : IVideoRepo
    {

        private readonly therapistContext therapistContext;
        private readonly IConfiguration config;
        private string _connectionString;
        private readonly ICommonEmailsService commonEmailsService;

        public VideoRepo(ICommonEmailsService ICommonEmailsService, therapistContext therapistContext, IConfiguration configuration)
        {
            this.therapistContext = therapistContext;
            config = configuration;
            commonEmailsService = ICommonEmailsService;
            _connectionString = configuration.GetConnectionString("DBCNSTR");
        }

        public async Task<string> GetTwilioToken()
        {
            try
            {
                 string twilioAccountSid  = config.GetValue<string>("TwilioSettings:AccountSid");
                 string twilioApiKey = config.GetValue<string>("TwilioSettings:ApiKey");
                string twilioApiSecret = config.GetValue<string>("TwilioSettings:ApiSecret");

                // These are specific to Video
                const string identity = "user";

                // Create a Video grant for this token
                var grant = new VideoGrant();
                grant.Room = "cool room";

                var grants = new HashSet<IGrant> { grant };

                // Create an Access Token generator
                var token = new Token(
                    twilioAccountSid,
                    twilioApiKey,
                    twilioApiSecret,
                    identity: identity,
                    grants: grants);

                //Console.WriteLine(token.ToJwt());
                return token.ToJwt();

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

      
    }
}
