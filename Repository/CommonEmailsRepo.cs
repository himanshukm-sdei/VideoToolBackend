
using Application.Abstractions.Repositories;
using Application.Dtos;
using Dapper;
using Domain.Entities;
using Infrastructure.Implemenatations.Context;
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
    public class CommonEmailsRepo : ICommonEmailsRepo
    {

        private readonly therapistContext therapistContext;
        private readonly IConfiguration config;
        private string _connectionString;
        public CommonEmailsRepo(therapistContext therapistContext, IConfiguration configuration)
        {
            this.therapistContext = therapistContext;
            config = configuration;
            _connectionString = configuration.GetConnectionString("DBCNSTR");
        }

        public async Task<IEnumerable<bool>> UpdateUserEmailLogging(string emailid, string emailSender, string strEmailSubject, string emailBody, int inviteStatus, int userId, int userEmailTemplate)
        {
            try
            {
                IEnumerable<bool> status;
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    status = await con.QueryAsync<bool>("dbo.SSP_UpdateUserEmailLogging", new
                    {
                        EmailReceiver  = emailid,
                        EmailSender = emailSender,
                        EmailBody = emailBody,
                        InviteStatus = inviteStatus,
                        EmailSubject = strEmailSubject,
                        UserId = userId,
                        UserEmailTemplate = userEmailTemplate


                    }, commandType: CommandType.StoredProcedure);
                }

                return status;

            }
            catch (Exception ex)
            {

                throw ex;
            }
            
        }

    }
}
