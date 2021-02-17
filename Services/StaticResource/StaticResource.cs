using System;
using System.Collections.Generic;
using System.Text;

namespace Services.StaticResources
{
    public static class StaticResource
    {
        public const int SuccessStatusCode = 200;
        public const string SuccessMessage = "Success";
        public const string SavedSuccessfulMessage = "Saved successfully";
        public const string UpdatedSuccessfulMessage = "Updated successfully";
        public const string PractionerSuccessMessage = "Request created successfully. Your will get the mail once your account is approved on your registered email address. You will be informed through email.";

        public const int NotFoundStatusCode = 404;
        public const string NotFoundMessage = "No record found";

        public const int UnauthorizedStatusCode = 401;
        public const string UnauthorizedMessage = "You are not authorised to use the app.";
        public const string UnAuthorizedCompanyMessage = "This company is not authorized to log in";

        public const int DuplicateStatusCode = 409;
        public const string DuplicateMessage = "Duplicate record";
        public const int FailStatusCode = 500;
        public const string FailMessage = "Error occurred. Please contact Support team";
        public const int InvalidStatusCode = 501;
        public const string InvalidMessage = "Invalid input";
        public const int FileBlankCode = 405;
        public const string FileBlankMessage = "File is blank";
        public const int InvalidCardCode = 402;
        public const string InvalidCardMessage = "Invalid card detail";
        public const string InvalidAchDetail = "Invalid ach detail";
        public const int duplicateNameCode = 410;
        public const string duplicateNameMessage = "Already exist";
        public const string UnauthorisedMessage = "User Not Authorised";
        public const string DeletedMessage = "Deleted Successfully";

        public const string InvalidUserNamePass = "Invalid Username/Password ";
        public const string UserInactive = "User is Inactive.Please contact Admin";
        public const string DeactivateMessage = "Your account is Deactivated/Blocked.Please Contact Customer Support";
        public const string PasswordExpired = "Your Password is Expired.Please click on forget password to reset your password";
        public const string UserNotExist = "User not exist";

        public const string EmailExist = "Email Already Exist";
        public const string UsernameExist = "Username Already Exist";
        public const string EmailNotExist = "Email Address not Exist. Please contact Customer Support";
        public const string EmailSent = "Email Successfully Sent";
        public const string PasswordUpdated = "Password Updated Successfully";
        public const string LinkExpired = "Reset password Link Expired";  //7
        public const string ExportedSuccessfully = "Exported Successfully";  //7
        public const string SomethingWentWrong = "Something went wrong";
        public const string Unblocked = "Unblocked Successfully";
        public const string Unfollowed = "Unfollowed Successfully";
        public const string Booked = "Booked";
        public const string CardDeleted = "Card Deleted";
        public const string PassNotMatch = "Current Password does not match";
        public const string SessionCreated = "Session created successfully";
        public const string DepartmentCodeExist = "Duplicate Depertment Code";
        public const string ActivationLinkSent = "Activation Link Sent successfully";
        public const string SessionCancel = "Session Cancelled successfully";
        public const string SessionStarted = "Session Started";




        //SET @LoginStatus = 0--InValid'
        //SET @LoginStatus = 1--Success
        //SET @LoginStatus = 2 --Inactive'
        //SET @LoginStatus = 3-- - IsBlocked'
        //SET @LoginStatus = 4-- - Expired'
        //SET @LoginStatus = 5-- - Email Exist'
        //SET @LoginStatus = 6-- - Username Exist'
        //SET @ResetLinkStatus = 7-- - Link expired Exist'

        //SET @LoginStatus = 10--User not exist'

    }
}
