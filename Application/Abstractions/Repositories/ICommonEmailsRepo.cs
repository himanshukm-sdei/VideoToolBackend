using Application.Dtos;
using Domain.Entities;
using Infrastructure.Implemenatations.Context;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Application.Abstractions.Repositories
{
    public interface ICommonEmailsRepo
    {
        Task<IEnumerable<bool>> UpdateUserEmailLogging(string emailid, string emailSender, string strEmailSubject, string emailBody, int inviteStatus, int userId, int userEmailTemplate);
    }
}
