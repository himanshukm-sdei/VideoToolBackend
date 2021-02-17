using Application.Abstractions.Repositories;
using Application.Dtos;
using AutoMapper;
using Dapper;
using Domain.Entities;
using Infrastructure.Implemenatations.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IdentityModel.Tokens.Jwt;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.ComTypes;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using static Common.Common.CommonUtility;

namespace Repository
{
    public class AuthRepo : IAuthRepo
    {
        private readonly AppSettings appSettings;
        private readonly therapistContext therapistContext;
        private readonly IMapper mapper;
        private string _connectionString;
        private readonly IConfiguration config;
        public AuthRepo(IOptions<AppSettings> options, therapistContext therapistContext, IMapper mapper, IConfiguration configuration)
        {
            this.appSettings = options.Value;
            this.therapistContext = therapistContext;
            this.mapper = mapper;
            config = configuration;
            _connectionString = configuration.GetConnectionString("DBCNSTR");
        }
        public async Task<User> Authenticate(string UserName, string Password)
        {
            var userInfo = new User();

            try
            {
                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@UserName", UserName);
                ObjParm.Add("@UserPassword", Password);
                ObjParm.Add("@IPInformation", "");
                ObjParm.Add("@LoginStatus", dbType: DbType.Int32, direction: ParameterDirection.Output);
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    var user = await con.QueryAsync<User>("dbo.SSP_getLoginDetail", ObjParm, commandType: CommandType.StoredProcedure);
                    var userSorted = user.FirstOrDefault();
                    int status = ObjParm.Get<int>("@LoginStatus");

                    if (user.Count() > 0)
                    {
                        string Role = string.Empty;
                        if (userSorted.RoleId > 0)
                        {
                            Role = Enum.GetName(typeof(RolesEnum), userSorted.RoleId);
                        }

                        else //if User Role value is null ,by default the user is a member
                        {
                            Role = Enum.GetName(typeof(RolesEnum), RolesEnum.Member);
                        }
                        var tokenHandler = new JwtSecurityTokenHandler();
                        var key = Encoding.ASCII.GetBytes(this.appSettings.Key);
                        var tokenDescribtor = new SecurityTokenDescriptor
                        {
                            Subject = new ClaimsIdentity(new Claim[] {
                             new Claim(ClaimTypes.Name,userSorted.UserName),
                             new Claim(ClaimTypes.Role,Role),
                             new Claim(ClaimTypes.Version,"v3.1"),
                             new Claim("UserId",userSorted.UserId.ToString()),
               }),

                            Expires = DateTime.UtcNow.AddDays(2),
                            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                        };
                        var token = tokenHandler.CreateToken(tokenDescribtor);
                        userInfo = this.mapper.Map<User>(userSorted);
                        userInfo.Token = tokenHandler.WriteToken(token);

                        userInfo.UserPassword = string.Empty;
                        userInfo.RoleId = userSorted.RoleId;
                        userInfo.Status = status;

                        return userInfo;
                    }
                    else
                    {
                        userInfo.Status = status;
                    }
                }
                return userInfo;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public async Task<User> CreateUserAsync(Users users, UserProfile userProfile, string fileName, UserInfo model, int RoleId, string speciality, bool? IsCompany, Guid? InvitedGuid, int companyId, int companyAdminId)
        {
            try
            {
                User createUser = new User();
                int UserId;
                int Success;
                DynamicParameters ObjParm = new DynamicParameters();
                //Users
                ObjParm.Add("@UserGuid", users.UserGuid);
                ObjParm.Add("@UserName", model.UserName);
                ObjParm.Add("@UserPassword", model.Password);
                ObjParm.Add("@IsActiveUser", users.IsActive);
                ObjParm.Add("@IsBlocked", users.IsBlocked);
                ObjParm.Add("@LastLogin", users.LastLogin);
                ObjParm.Add("@CreatedDateUser", users.CreatedDate);
                ObjParm.Add("@CreatedByUser", users.CreatedBy);
                ObjParm.Add("@ModifiedDateUser", users.ModifiedDate);
                ObjParm.Add("@ModifiedByUser", users.ModifiedBy);
                ObjParm.Add("@IsDeletedUser", users.IsDeleted);
                ObjParm.Add("@IsCompany", IsCompany);
                ObjParm.Add("@InvitedGuid", InvitedGuid);
                ObjParm.Add("@CompanyId", companyId);
                ObjParm.Add("@CompanyAdminId", companyAdminId);

                // Profile
                ObjParm.Add("@ProfileGuid", userProfile.ProfileGuid);
                ObjParm.Add("@FirstName", model.FirstName);
                ObjParm.Add("@LastName", model.LastName);
                ObjParm.Add("@Email", model.Email);
                ObjParm.Add("@ProfilePhoto", fileName);
                ObjParm.Add("@Gender", userProfile.Gender);
                ObjParm.Add("@City", userProfile.City);
                ObjParm.Add("@State", userProfile.State);
                ObjParm.Add("@Country", userProfile.Country);
                ObjParm.Add("@ZipCode", userProfile.ZipCode);
                ObjParm.Add("@Specialities", speciality);
                ObjParm.Add("@Qualification", userProfile.Qualification);
                ObjParm.Add("@Education", userProfile.Education);
                ObjParm.Add("@IsFeature", userProfile.IsFeature);
                ObjParm.Add("@Ranked", userProfile.Ranked);
                ObjParm.Add("@BriefBio", userProfile.BriefBio);
                ObjParm.Add("@InstagramProfile", userProfile.InstagramProfile);
                ObjParm.Add("@LinkedInProfile", userProfile.LinkedInProfile);
                ObjParm.Add("@FaceBookProfile", userProfile.FaceBookProfile);
                ObjParm.Add("@ContactNo1", userProfile.ContactNo1);
                ObjParm.Add("@ContactNo2", userProfile.ContactNo2);
                ObjParm.Add("@ApprovedDate", null);
                ObjParm.Add("@IsApproved", null);
                ObjParm.Add("@CreatedDateProfile", userProfile.CreatedDate);
                ObjParm.Add("@ModifiedDateProfile", userProfile.ModifiedDate);
                ObjParm.Add("@CreatedByProfile", userProfile.CreatedBy);
                ObjParm.Add("@ModifiedByProfile", userProfile.ModifiedBy);
                ObjParm.Add("@IsActiveProfile", userProfile.IsActive);
                ObjParm.Add("@PractitionerSince", userProfile.PractitionerSince);
                ObjParm.Add("@Description", userProfile.Description);
                ObjParm.Add("@Language", userProfile.Language);
                ObjParm.Add("@IsDeletedProfile", userProfile.IsDeleted);
                ObjParm.Add("@IsDeactivated", userProfile.IsDeactivated);
                ObjParm.Add("@PublicProfile", userProfile.PublicProfile);
                ObjParm.Add("@UserTypeText",userProfile.UserTypeText);
                ObjParm.Add("@Timezone", userProfile.TimezoneId);

                // SaveRole

                ObjParm.Add("@RoleId ", RoleId);
                ObjParm.Add("@UserId", dbType: DbType.Int32, direction: ParameterDirection.Output);
                ObjParm.Add("@Success", dbType: DbType.Int32, direction: ParameterDirection.Output);

                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    await con.QueryAsync("dbo.SSP_memberSignup", ObjParm, commandType: CommandType.StoredProcedure);
                    UserId = ObjParm.Get<int>("@UserId");
                    Success = ObjParm.Get<int>("@Success");
                    createUser.UserId = UserId;
                    createUser.Status = Success;

                }
                return createUser;
            }

            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<string> GenerateUserName(Guid guid, string firstName, string lastName)
        {
            try
            {
                string userName = firstName.Substring(0, 1) + guid.ToString().Substring(0, 6) + lastName.Substring(0, 1);
                bool exist = await CheckUserNameExist(userName);
                if (exist)
                {
                    guid = System.Guid.NewGuid();
                    await GenerateUserName(guid, firstName, lastName);
                }
                return userName;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public async Task<bool> CheckUserNameExist(string userName)
        {
            try
            {
                bool success = false;
                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@UserName", userName);
                ObjParm.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    await con.QueryAsync("dbo.SSP_CheckUsernameExist", ObjParm, commandType: CommandType.StoredProcedure);
                    success = ObjParm.Get<bool>("@Success");
                }
                // return success;

                if (success)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }

        }
        public async Task<bool> MatchSecretKey(string key)
        {
            try
            {
                return await Task.Run(() =>
                {
                    return this.appSettings.SecretKey.Equals(key);
                });

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<string> UploadedFile(IFormFile file, string folderPath)
        {
            try
            {
                string fileName = file.FileName;
                string uniqueFileName = null;
                if (file.FileName.Length > 20)
                {
                    string extension = Path.GetExtension(file.FileName);
                    fileName = file.FileName.ToString().Substring(0, 20) + extension;
                }
                uniqueFileName = Guid.NewGuid().ToString().Substring(0, 25) + "_" + fileName;
                string filePath = Path.Combine(folderPath, uniqueFileName);
                using (var fileStream = new FileStream(filePath, FileMode.Create))
                {
                    await file.CopyToAsync(fileStream);
                }
                return uniqueFileName;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> SaveUserCompanyLog(UserCompany userCompany)
        {
            try
            {
                this.therapistContext.UserCompany.Add(userCompany);
                await this.therapistContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<bool> EmailExist(String email)
        {
            try
            {
                // var existingEmail = await this.therapistContext.UserProfile.FirstOrDefaultAsync(x => x.Email.Equals(email));

                bool success = false;
                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@Email ", email);
                ObjParm.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    await con.QueryAsync("dbo.SSP_CheckEmailExist", ObjParm, commandType: CommandType.StoredProcedure);
                    success = ObjParm.Get<bool>("@Success");
                }
                if (success)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<IEnumerable<DepartmentDetails>> GetDepartmentDetails(int userId, int sessionId)
        {
            try
            {
                IEnumerable<DepartmentDetails> details = new List<DepartmentDetails>();
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    details = await con.QueryAsync<DepartmentDetails>("dbo.SSP_getDepartmentDetails", new
                    {
                        UserId = userId,
                        sessionId = sessionId
                    }, commandType: CommandType.StoredProcedure);
                }
                return details;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<int> SaveUserRole(UserRole userRole)
        {
            try
            {
                this.therapistContext.UserRole.Add(userRole);
                await this.therapistContext.SaveChangesAsync();
                return Convert.ToInt32(userRole.UserRoleId);
            }
            catch (Exception)
            {

                throw;
            }
        }
        public async Task<UserProfile> CreateUserProfileAsync(UserProfile user)
        {
            try
            {
                this.therapistContext.UserProfile.Add(user);
                await this.therapistContext.SaveChangesAsync();
                return user;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        /// <summary>
        /// Get user Detail By UserId
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="companyId"></param>
        /// <param name="userId"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async Task<Users> GetUserByEmailId(string EmailId)
        {
            try
            {
                var userInfo = new Users();

                //using (var dbcontext = this.therapistContext)
                //{
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    userInfo = await con.QueryFirstOrDefaultAsync<Users>("dbo.SSP_getUserByEmailId", new
                    {
                        Email = EmailId,
                    }, commandType: CommandType.StoredProcedure);
                    //}
                    return userInfo;
                }
            }
            catch (Exception)
            {
                throw;
            }
        }


        public async Task<List<MasterPlan>> GetPlans(int? role)
        {
            try
            {
                List<MasterPlan> masterPlans = new List<MasterPlan>();
                if (role > 0)
                {
                    //for role specific pans
                    masterPlans = await this.therapistContext.MasterPlan.Where(x => x.IsActive == true && x.IsDeleted == false && x.PlanFor == role.ToString()).ToListAsync();
                }
                else
                {
                    //for all plans
                    masterPlans = await this.therapistContext.MasterPlan.Where(x => x.IsActive == true && x.IsDeleted == false).ToListAsync();
                }
                return masterPlans;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public async Task<bool> SaveMemberShipPlan(UserMembership membership)
        {
            try
            {
                this.therapistContext.UserMembership.Add(membership);
                await this.therapistContext.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {

                throw;
            }
        }
        public async Task<BulkCreatedUsers> GetMemberDetails(Guid guid)
        {
            try
            {
                BulkCreatedUsers createdUsers = new BulkCreatedUsers();
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    createdUsers = await con.QueryFirstOrDefaultAsync<BulkCreatedUsers>("dbo.SSP_getInvitedUserDetails", new
                    {
                        TokenGuid = guid
                    }, commandType: CommandType.StoredProcedure);
                }
                return createdUsers;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<User> UpdateResetPasswordLink(string guid, string Email)
        {
            try
            {
                //this.therapistContext.UserPasswordRequest.Add(request);
                //await this.therapistContext.SaveChangesAsync();
                //return true;


                User response = new User();
                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@PasswordRequestLink", guid);
                ObjParm.Add("@Email", Email);
                ObjParm.Add("@Success", dbType: DbType.Int32, direction: ParameterDirection.Output);
                ObjParm.Add("@FirstName", dbType: DbType.String, direction: ParameterDirection.Output, size: 20);

                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    var user = await con.QueryAsync("dbo.UpdateResetPasswordLink", ObjParm, commandType: CommandType.StoredProcedure);
                    int success = ObjParm.Get<int>("@Success");
                    string name = ObjParm.Get<string>("@FirstName");

                    response.Status = success;
                    response.Firstname = name;
                }
                return response;

                //if (success)
                //    return true;
                //else
                //    return false;


            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<User> UpdateNewPassword(ResetPasswordRequestModel request)
        {
            try
            {
                User response = new User();
                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@Password", request.Password);
                ObjParm.Add("@Email", request.Email);
                ObjParm.Add("@Success", dbType: DbType.Int32, direction: ParameterDirection.Output);
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    await con.QueryAsync("dbo.SSP_updateNewForgotPassword", ObjParm, commandType: CommandType.StoredProcedure);
                    int success = ObjParm.Get<int>("@Success");
                    response.Status = success;
                }
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<User> CheckResetPasswordLinkStatus(ResetPasswordRequestModel request)
        {
            try
            {
                User response = new User();
                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@Guid", request.Guid.ToString());
                ObjParm.Add("@Success", dbType: DbType.Int32, direction: ParameterDirection.Output);
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    await con.QueryAsync("dbo.SSP_getResetPassLinkStatus", ObjParm, commandType: CommandType.StoredProcedure);
                    int success = ObjParm.Get<int>("@Success");
                    response.Status = success;
                }
                return response;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        /// <summary>
        /// Check that request already made for reset password or not 
        /// </summary>
        /// <param name="serverId"></param>
        /// <param name="companyId"></param>
        /// <param name="userId"></param>
        /// <param name="connection"></param>
        /// <returns></returns>
        public async Task<bool> CheckForRequest(long UserId)
        {
            try

            //SSP_updatePasswordRequest
            {
                bool result = false;
                var updateStatus = new UserPasswordRequest();
                updateStatus = await this.therapistContext.UserPasswordRequest.FirstOrDefaultAsync(x => x.UserId == UserId && x.PasswordRequestStatus == 1);
                if (updateStatus == null)
                {
                    return result = true;

                }
                else
                {
                    updateStatus.PasswordRequestStatus = 2;
                    updateStatus.ModifiedDate = DateTime.UtcNow;
                    this.therapistContext.UserPasswordRequest.Update(updateStatus);
                    await this.therapistContext.SaveChangesAsync();
                    result = true;
                    return result;
                }


                //bool success = false;
                //DynamicParameters ObjParm = new DynamicParameters();
                //ObjParm.Add("@UserId", UserId);
                //ObjParm.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                //using (IDbConnection con = new SqlConnection(_connectionString))
                //{
                //    await con.QueryAsync("dbo.SSP_updatePasswordRequest", ObjParm, commandType: CommandType.StoredProcedure);
                //    success = ObjParm.Get<bool>("@Success");
                //}

                //if (success)
                //    return true;
                //else
                //    return false;


            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public async Task<bool> checkEmailForResetPassword(ResetPasswordRequestModel objModel)
        {
            try
            {
                bool success = false;
                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@UserGuid", objModel.Guid);
                ObjParm.Add("@Email", objModel.Email);
                ObjParm.Add("@Success", dbType: DbType.Boolean, direction: ParameterDirection.Output);
                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    await con.QueryAsync("dbo.SSP_CheckUserByEmailAndGUID", ObjParm, commandType: CommandType.StoredProcedure);
                    success = ObjParm.Get<bool>("@Success");
                }

                if (success)
                    return true;
                else
                    return false;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public async Task<List<ListVm>> GetSpeciality()
        {
            try
            {
                return await this.therapistContext.MasterSpeciality.Where(x => x.IsActive == true).Select(y => new ListVm { Value = y.SpecialityId, ViewValue = y.SpecialityName }).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


        public async Task<User> SendRestPasswordEmailByAdmin(string guid, string Email, long UserId)
        {
            try
            {
                //this.therapistContext.UserPasswordRequest.Add(request);
                //await this.therapistContext.SaveChangesAsync();
                //return true;


                User response = new User();
                DynamicParameters ObjParm = new DynamicParameters();
                ObjParm.Add("@PasswordRequestLink", guid);
                ObjParm.Add("@Email", Email);
                ObjParm.Add("@UserId", UserId);
                ObjParm.Add("@Success", dbType: DbType.Int32, direction: ParameterDirection.Output);
                ObjParm.Add("@FirstName", dbType: DbType.String, direction: ParameterDirection.Output, size: 20);

                using (IDbConnection con = new SqlConnection(_connectionString))
                {
                    var user = await con.QueryAsync("dbo.SSP_ForgetPasswordEmailFromAdmin", ObjParm, commandType: CommandType.StoredProcedure);
                    int success = ObjParm.Get<int>("@Success");
                    string name = ObjParm.Get<string>("@FirstName");

                    response.Status = success;
                    response.Firstname = name;
                }
                return response;

                //if (success)
                //    return true;
                //else
                //    return false;


            }
            catch (Exception ex)
            {
                throw;
            }

        }

    }
}
