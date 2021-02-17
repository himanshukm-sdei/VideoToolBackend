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
    public class VideoService : IVideoService
    {
        private readonly IVideoRepo videoRepo;
        private readonly IAuthRepo authRepo;
        private readonly IAuthService authService;
        private readonly ICommonEmailsService commonEmailsService;
        private readonly IEmailSenderService _iEmailSenderService;

        public VideoService(IEmailSenderService emailSenderService, IAuthService _authService, IVideoRepo videoRepository, IAuthRepo _authRepo, ICommonEmailsService ICommonEmailsService)
        {
            videoRepo = videoRepository;
            authRepo = _authRepo;
            authService = _authService;
            commonEmailsService = ICommonEmailsService;
            _iEmailSenderService = emailSenderService;
        }
        public async Task<string> GetTwilioToken(HttpContext context)
        {
            try
            {

                var list = await videoRepo.GetTwilioToken();
                return list;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }


    }
}
