using Application.Abstractions.Repositories;
using Application.Abstractions.Services;
using Application.Dtos;
using Domain.Entities;
using Infrastructure.Implemenatations.Context;
using Microsoft.AspNetCore.Http;
using Services.CommonClasses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Threading.Tasks;


namespace Services
{
    public class JobsService : IJobsService
    {
        private readonly IJobsRepo _jobsRepo;
        private readonly IAuthRepo authRepo;
        private readonly IAuthService authService;
        private readonly ICommonEmailsService commonEmailsService;
        private readonly IEmailSenderService _iEmailSenderService;

        public JobsService(IEmailSenderService emailSenderService, IAuthService _authService, IJobsRepo jobsRepo, IAuthRepo _authRepo, ICommonEmailsService ICommonEmailsService)
        {
            _jobsRepo = jobsRepo;
            authRepo = _authRepo;
            authService = _authService;
            commonEmailsService = ICommonEmailsService;
            _iEmailSenderService = emailSenderService;
        }
        public async Task<bool> SendInvitationEmails(HttpContext context)
        {
            try
            {

                IEnumerable<InviteUsersList> list = await _jobsRepo.GetInviteUsers();
                if (list != null && list.Count() > 0)
                {
                    foreach (var email in list)
                    {
                        //  string url = "http://52.25.96.244:7051/Company/MemberSignup/" + email.ActivationToken;
                        InviteEmailData response = await this.commonEmailsService.SendUserInviteEmails(email, context);
                        response.UserInviteGuId = email.UserInviteGuId;
                        await _jobsRepo.UpdateUserInvitationStatus(response);

                    }

                }
                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public async Task<bool> SendBookedsessionEmailBeforeoneday(HttpContext context)
        {
            try
            {
                var data = await _jobsRepo.SendBookedsessionEmailBeforeoneday(context);

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
                var data = await _jobsRepo.SendBookedsessionEmailBeforeoneHour(context);

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
                var data = await _jobsRepo.SendBookedAvailabilityEmailBeforeHalfHour(context);

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
                var data = await _jobsRepo.SendBookedAvailabilityEmailBeforeFiveMinutes(context);

                return true;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

    }
}
