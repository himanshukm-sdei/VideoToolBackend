using Application.Dtos;
using Domain.Entities;
using Infrastructure.Implemenatations.Context;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Services
{
    public interface IVideoService
    {
        Task<string> GetTwilioToken(HttpContext context);
       
    }
}
