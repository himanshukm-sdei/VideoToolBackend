using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace Common.Common
{
    public static class CommonUtility
    {
        public enum RolesEnum
        {
            Practitioner = 1,
            Member = 2,
            Admin = 3,
            CompanyAdmin = 4,
            SuperAdmin = 5,
        }
        public enum EmailTemplates
        {
            WelcomeAdmin = 1,
            WelcomeCompany = 2,
            WelcomePractitioner = 3,
        }
        public enum notificationEmailTypes
        {
            OnedayBefore = 1,
            OneHourBefore = 2,
        }
        public enum ErrorsEnum
        {
            NoError = 1,
            Error = 2,
            Duplicate = 3,
        }
        public class Enums
        {
            public enum ResetPasswordResponseType
            {
                [Description("Email successfully sent!")]
                Success = 1,
                [Description("Failed! Please try again later!")]
                Failed = 2,
                [Description("User not found!")]
                NoUserFound = 3,
                [Description("Wrong Keys! Please try again later.")]
                WrongKey = 4,
                [Description("No company found!")]
                NoCompanyFound = 5,
                [Description("Current Password not matched")]
                CurrentPasswordNotmatched = 6,
                [Description("Password Changed Successfully")]
                PasswordChangedSuccessfully = 7,
                [Description("Not able to update password")]
                UnsuccessfulAttempt = 8,
                [Description("Email Address not found")]
                EmailNotFound = 9,
            }
        }
    }
}
