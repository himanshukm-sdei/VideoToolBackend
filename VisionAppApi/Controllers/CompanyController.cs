using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Claims;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Application.Abstractions.Services;
using Application.Dtos;
using Domain.Entities;
using ElmahCore;
using Infrastructure.Implemenatations.Context;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using Services.JsonResponse;
using Services.StaticResources;
using static Common.Common.CommonUtility;

namespace VisionAppApi.Controllers
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/[controller]")]
    [ApiController]
    public class CompanyController : ControllerBase
    {
        private readonly ICompany _companyService;
        private readonly IAuthService authService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CompanyController(ICompany companyService, IAuthService authService, IWebHostEnvironment webHostEnvironment)
        {
            this._companyService = companyService;
            this.authService = authService;
            _webHostEnvironment = webHostEnvironment;
        }
        [Route("GetCompanyInfo")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetCompanyInfo(int UserId)
        {
            JsonResponse<CompanyInfo> objResult = new JsonResponse<CompanyInfo>();
            try
            {
                CompanyInfo info = new CompanyInfo();
                info = await this._companyService.GetCompanyInfo(UserId);
                if (info != null)
                {
                    objResult.Data = info;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
                else
                {
                    objResult.Data = null;
                    objResult.Status = StaticResource.NotFoundStatusCode;
                    objResult.Message = StaticResource.NotFoundMessage;
                    return new OkObjectResult(objResult);
                }
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                objResult.Data = null;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = StaticResource.FailMessage;
            }
            return new OkObjectResult(objResult);
        }
        [Route("GetAccountInfo")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> GetAccountInfo(int UserId)
        {
            JsonResponse<AccountInfo> objResult = new JsonResponse<AccountInfo>();

            try
            {
                AccountInfo account = new AccountInfo();
                account = await this._companyService.GetAccountInfo(UserId);
                if (account != null)
                {
                    objResult.Data = account;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
                else
                {
                    objResult.Data = null;
                    objResult.Status = StaticResource.NotFoundStatusCode;
                    objResult.Message = StaticResource.NotFoundMessage;
                    return new OkObjectResult(objResult);
                }

            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                objResult.Data = null;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = StaticResource.FailMessage;
            }
            return new OkObjectResult(objResult);

        }
        [Route("BulkUpload")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> BulkUpload([FromForm] BulkRequest request)
        {
            JsonResponse<List<CompanyUpload>> objResult = new JsonResponse<List<CompanyUpload>>();
            try
            {
                if (ModelState.IsValid)
                {
                    //string FilePath = string.Empty;
                    string folderName = "Upload";
                    string webRootPath = $"{Directory.GetCurrentDirectory()}{@"\wwwroot"}";
                    string newPath = Path.Combine(webRootPath, folderName);
                    string fullPath = Path.Combine(newPath, request.file.FileName);
                    //FilePath = Path.GetTempFileName();
                    //var fileLocation = new FileInfo(FilePath);
                    ISheet sheet;
                    string sFileExtension = Path.GetExtension(request.file.FileName).ToLower();
                    List<CompanyUpload> listUpload = new List<CompanyUpload>();
                    using (var stream = new FileStream(fullPath, FileMode.Create))
                    {
                        request.file.CopyTo(stream);
                        stream.Position = 0;
                        if (sFileExtension == ".xls")
                        {
                            HSSFWorkbook hssfwb = new HSSFWorkbook(stream); //This will read the Excel 97-2000 formats  
                            sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook  
                        }
                        else
                        {
                            XSSFWorkbook hssfwb = new XSSFWorkbook(stream); //This will read 2007 Excel format  
                            sheet = hssfwb.GetSheetAt(0); //get first sheet from workbook   
                        }
                    }

                    IRow headerRow = sheet.GetRow(0); //Get Header Row
                    int cellCount = headerRow.LastCellNum;
                    UserCompanyInfo companyInfo = new UserCompanyInfo();
                    bool getCompanyDetails = false;
                    for (int i = (sheet.FirstRowNum + 1); i <= sheet.LastRowNum; i++) //Read Excel File
                    {
                        bool isError = false;
                        bool isMaximumLen = false;
                        bool isDupliacate = false;
                        CompanyUpload upload = new CompanyUpload();
                        upload.UploadId = 0;
                        upload.UploadGuid = System.Guid.NewGuid();
                        //upload.CompanyId = request.CompanyId;
                        //upload.CreatedBy = request.CompanyUserId;   
                        upload.CreatedDate = DateTime.UtcNow;
                        upload.ModifiedBy = null;
                        upload.ModifiedDate = null;
                        upload.IsActive = true;
                        upload.IsDeleted = false;
                        IRow row = sheet.GetRow(i);
                        if (row != null)
                        {
                            if (row.GetCell(0) == null || row.GetCell(0).ToString() == "")
                            {
                                upload.FirstName = null;
                                isError = true;
                            }
                            else
                            {
                                upload.FirstName = row.GetCell(0).ToString();
                                int leng = upload.FirstName.Length;
                                if (leng > 100)
                                    isMaximumLen = true;
                                else
                                    upload.StatusCode = (int)ErrorsEnum.NoError;
                            }
                            if (row.GetCell(1) == null || row.GetCell(1).ToString() == "")
                            {
                                upload.LastName = null;
                                isError = true;
                            }
                            else
                            {
                                upload.LastName = row.GetCell(1).ToString();
                                int leng = upload.LastName.Length;
                                if (leng > 100)
                                    isMaximumLen = true;
                                else
                                    upload.StatusCode = (int)ErrorsEnum.NoError;
                            }
                            if (row.GetCell(2) == null || row.GetCell(2).ToString() == "")
                            {
                                upload.Email = null;
                                isError = true;
                            }
                            else
                            {
                                bool validEmail = Regex.IsMatch(row.GetCell(2).ToString(), @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z", RegexOptions.IgnoreCase);
                                if (!validEmail)
                                {
                                    upload.Email = row.GetCell(2).ToString();
                                    isError = true; ;
                                }
                                var existinfile = listUpload.Find(x => x.Email == row.GetCell(2).ToString());
                                if (existinfile != null)
                                {
                                    upload.Email = row.GetCell(2).ToString();
                                    isDupliacate = true; ;
                                }
                                bool exist = await this.authService.CheckEmailExist(row.GetCell(2).ToString());
                                if (exist)
                                {
                                    upload.Email = row.GetCell(2).ToString();
                                    isDupliacate = true; ;
                                }
                                else
                                {
                                    upload.Email = row.GetCell(2).ToString();
                                    int leng = upload.Email.Length;
                                    if (leng > 100)
                                        isMaximumLen = true;
                                    upload.StatusCode = (int)ErrorsEnum.NoError;
                                }
                            }
                            if (row.GetCell(3) == null || row.GetCell(3).ToString() == "")
                            {
                                isError = true;
                            }
                            else
                            {

                                var companyId = Convert.ToInt32(row.GetCell(3).ToString());
                                if (!getCompanyDetails)
                                {
                                    companyInfo = await this._companyService.getUserCompanyInfo(companyId);
                                }
                                if (companyInfo != null)
                                {
                                    upload.CompanyId = companyInfo.CompanyId;
                                    upload.CreatedBy = companyInfo.UserId;
                                    getCompanyDetails = true;
                                }
                                else
                                {
                                    isError = true;
                                }

                            }
                        }
                        if (isMaximumLen)
                            upload.StatusCode = (int)ErrorsEnum.Error;
                        if (isError)
                            upload.StatusCode = (int)ErrorsEnum.Error;
                        if (isDupliacate)
                            upload.StatusCode = (int)ErrorsEnum.Duplicate;
                        if (upload.StatusCode == 1)
                        {
                            var uniquename = await this.authService.GenerateUserName(upload.UploadGuid, upload.FirstName, upload.LastName);
                            //upload.UserName = request.CompanyUserNameCode + "-" + uniquename;       
                            upload.UserName = companyInfo.CompanyUserNameCode + "-" + uniquename;
                            upload.UserPassword = this.authService.CreateRandomAlphabet();
                        }

                        if (upload.FirstName == null && upload.LastName == null && upload.Email == null)
                        { }
                        else
                            listUpload.Add(upload);
                    }
                    listUpload = await this._companyService.BulkUpload(listUpload);
                    objResult.Data = listUpload;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                }
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                objResult.Data = null;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = StaticResource.FailMessage;
            }
            return new OkObjectResult(objResult);
        }

        [Route("UpdateCard")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UpdateCard([FromBody] UpdateCardRequest updateCard)
        {
            JsonResponse<bool> objResult = new JsonResponse<bool>();
            try
            {
                bool success = await this._companyService.UpdateCard(updateCard);
                if (success)
                {
                    objResult.Data = success;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                }
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                objResult.Data = false;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = StaticResource.FailMessage;
            }
            return new OkObjectResult(objResult);
        }
        [Route("InviteUsers")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> InviteUsers(int CompanyId)
        {
            JsonResponse<string> objResult = new JsonResponse<string>();
            try
            {
                bool success = await this._companyService.InviteUsers(CompanyId);
                if (success)
                {
                    objResult.Data = StaticResource.ExportedSuccessfully;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
                else
                {
                    objResult.Data = StaticResource.SomethingWentWrong;
                    objResult.Status = StaticResource.FailStatusCode;
                    objResult.Message = StaticResource.FailMessage;
                    return new OkObjectResult(objResult);
                }

            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                objResult.Data = ex.Message;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = StaticResource.FailMessage;
            }
            return new OkObjectResult(objResult);
        }

        [Route("UserCompanyInfo")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> UserCompanyInfo(int UserId)
        {
            JsonResponse<UserCompanyInfo> objResult = new JsonResponse<UserCompanyInfo>();
            try
            {
                UserCompanyInfo companyInfo = await this._companyService.getUserCompanyInfo(UserId);
                if (companyInfo != null)
                {
                    objResult.Data = companyInfo;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                objResult.Data = null;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = "Exception occured"; ;
            }
            return new OkObjectResult(objResult);

        }
        [Route("GetCompanyUsers")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCompanyUsers([FromBody] CompanyUsersRequest request)
        {
            JsonResponse<IEnumerable<CompanyMembers>> objResult = new JsonResponse<IEnumerable<CompanyMembers>>();
            try
            {
                IEnumerable<CompanyMembers> companyInfo = await this._companyService.GetCompanyUsers(request);
                if (companyInfo != null && companyInfo.Count() > 0)
                {
                    objResult.Data = companyInfo;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
                else
                {
                    objResult.Data = null;
                    objResult.Status = StaticResource.NotFoundStatusCode;
                    objResult.Message = StaticResource.NotFoundMessage;
                    return new OkObjectResult(objResult);
                }
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                objResult.Data = null;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = StaticResource.FailMessage; ;
            }
            return new OkObjectResult(objResult);

        }
        [Route("GetCompanyRate")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCompanyRate(int userId)
        {
            JsonResponse<RatePlanInfo> objResult = new JsonResponse<RatePlanInfo>();
            try
            {
                RatePlanInfo planInfo = new RatePlanInfo();
                planInfo = await this._companyService.getCompanyRatePlan(userId);
                if (planInfo != null)
                {
                    objResult.Data = planInfo;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                objResult.Data = null;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = StaticResource.FailMessage;
            }
            return new OkObjectResult(objResult);
        }
        [Route("ChangeActiveStatus")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ChangeActiveStatus([FromBody] CompanyMembers member)
        {
            JsonResponse<bool> objResult = new JsonResponse<bool>();
            try
            {
                bool success = await this._companyService.ChangeActiveStatus(member);
                if (success)
                {
                    objResult.Data = success;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                }
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                objResult.Data = false;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = StaticResource.FailMessage;
            }
            return new OkObjectResult(objResult);
        }

        [Route("DeleteUser")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteUser(int UserId)
        {
            JsonResponse<bool> objResult = new JsonResponse<bool>();
            try
            {

                bool successs = await this._companyService.DeleteUser(UserId);
                if (successs)
                {
                    objResult.Data = successs;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                objResult.Data = false;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = StaticResource.FailMessage;
            }
            return new OkObjectResult(objResult);
        }
        [Route("DeleteInviteUser")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> DeleteInviteUser(CompanyInviteUsersList User)
        {
            JsonResponse<bool> objResult = new JsonResponse<bool>();
            try
            {

                bool successs = await this._companyService.DeleteInviteUser(User);
                if (successs)
                {
                    objResult.Data = successs;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                objResult.Data = false;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = StaticResource.FailMessage;
            }
            return new OkObjectResult(objResult);
        }

        [Route("GetCompanyInviteUsers")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCompanyInviteUsers([FromBody] CompanyUsersRequest request)
        {
            JsonResponse<IEnumerable<CompanyInviteUsersList>> objResult = new JsonResponse<IEnumerable<CompanyInviteUsersList>>();
            try
            {
                IEnumerable<CompanyInviteUsersList> companyInfo = await this._companyService.GetCompanyInviteUsers(request);
                if (companyInfo != null && companyInfo.Count() > 0)
                {
                    objResult.Data = companyInfo;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
                else
                {
                    objResult.Data = null;
                    objResult.Status = StaticResource.NotFoundStatusCode;
                    objResult.Message = StaticResource.NotFoundMessage;
                    return new OkObjectResult(objResult);
                }
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                objResult.Data = null;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = StaticResource.FailMessage;
            }
            return new OkObjectResult(objResult);

        }

        #region Email Log
        [Route("GetEmailLogs")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetEmailLogs(int UserId)
        {
            JsonResponse<IEnumerable<Application.Dtos.UserEmailLogging>> objResult = new JsonResponse<IEnumerable<Application.Dtos.UserEmailLogging>>();

            try
            {
                IEnumerable<Application.Dtos.UserEmailLogging> emailLogs = await this._companyService.getEmailLogs(UserId);
                if (emailLogs != null)
                {
                    objResult.Data = emailLogs;
                    objResult.Status = StaticResource.SuccessStatusCode;
                    objResult.Message = StaticResource.SuccessMessage;
                    return new OkObjectResult(objResult);
                }
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                objResult.Data = null;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = StaticResource.FailMessage; ;
            }
            return new OkObjectResult(objResult);

        }
        [Route("SampleExcel")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> SampleExcel()
        {
            JsonResponse<FileStreamResult> objResult = new JsonResponse<FileStreamResult>();
            try
            {
                string fileName = @"Sample.xlsx";
                string URL = string.Format("{0}://{1}/{2}", Request.Scheme, Request.Host, fileName);
                FileInfo file = new FileInfo(Path.Combine("", fileName));
                var memoryStream = new MemoryStream();
                // --- Below code would create excel file with dummy data----  
                using (var fs = new FileStream(Path.Combine("", fileName), FileMode.Create, FileAccess.Write))
                {
                    IWorkbook workbook = new XSSFWorkbook();
                    ISheet excelSheet = workbook.CreateSheet("Testingdummy");




                    IRow row = excelSheet.CreateRow(0);
                    row.CreateCell(0).SetCellValue("FirstName");
                    row.CreateCell(1).SetCellValue("LastName");
                    row.CreateCell(2).SetCellValue("Email");
                    row.CreateCell(3).SetCellValue("CompanyId");
                    //row.CreateCell(1).SetCellValue("Name");

                    row = excelSheet.CreateRow(1);
                    row.CreateCell(0).SetCellValue("");
                    row.CreateCell(1).SetCellValue("");
                    row.CreateCell(2).SetCellValue("");
                    row.CreateCell(3).SetCellValue(3);

                    //row = excelSheet.CreateRow(2);
                    //row.CreateCell(0).SetCellValue(2);
                    //row.CreateCell(1).SetCellValue("James");

                    workbook.Write(fs);
                }
                using (var fileStream = new FileStream(Path.Combine("", fileName), FileMode.Open))
                {
                    await fileStream.CopyToAsync(memoryStream);
                }
                memoryStream.Position = 0;
                return File(memoryStream, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", fileName);
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                objResult.Data = null;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = "Exception occured";
            }
            return new OkObjectResult(objResult);
        }
        [Route("GetCompanies")]
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> GetCompanies()
        {
            JsonResponse<IEnumerable<ListVm>> objResult = new JsonResponse<IEnumerable<ListVm>>();
            try
            {
                IEnumerable<ListVm> lists = new List<ListVm>();
                lists = await this._companyService.GetCompanies();
                if (lists.Count() > 0 && lists != null)
                {
                    objResult.Data = lists;
                    objResult.Message = StaticResource.SuccessMessage;
                    objResult.Status = StaticResource.SuccessStatusCode;
                }
                else
                {
                    objResult.Data = lists;
                    objResult.Message = StaticResource.NotFoundMessage;
                    objResult.Status = StaticResource.NotFoundStatusCode;
                }
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                objResult.Data = null;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = "Exception occured";
            }
            return new OkObjectResult(objResult);

        }




        [Route("ResendEmailToCompanyUsers")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ResendEmailToCompanyUsers([FromBody] ForgetPasswordRequestModel objModel)
        {
            JsonResponse<User> objResultList = new JsonResponse<User>();
            HttpContext context = HttpContext;
            try
            {
                User response = await this.authService.SendRestPasswordEmailByAdmin(objModel, context);
                switch (response.Status)
                {
                    case 1:
                        objResultList.Data = null;
                        objResultList.Status = StaticResource.SuccessStatusCode;
                        objResultList.Message = StaticResource.EmailSent;
                        break;
                    case 0:
                        objResultList.Data = null;
                        objResultList.Status = StaticResource.NotFoundStatusCode;
                        objResultList.Message = StaticResource.EmailNotExist;
                        break;
                    default:
                        objResultList.Data = null;
                        objResultList.Status = StaticResource.NotFoundStatusCode;
                        objResultList.Message = StaticResource.NotFoundMessage;
                        break;
                }
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                objResultList.Data = null;
                objResultList.Status = StaticResource.FailStatusCode;
                objResultList.Message = StaticResource.FailMessage; ;
            }
            return new OkObjectResult(objResultList);

        }


        [Route("ResendEmailToUsers")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> ResendEmailToUsers([FromBody] ForgetPasswordRequestModel objModel)
        {
            JsonResponse<User> objResultList = new JsonResponse<User>();
            HttpContext context = HttpContext;

            try
            {
                User response = await this.authService.ResendEmailToUser(objModel, context);
                switch (response.Status)
                {
                    case 1:
                        objResultList.Data = null;
                        objResultList.Status = StaticResource.SuccessStatusCode;
                        objResultList.Message = StaticResource.EmailSent;
                        break;
                    case 0:
                        objResultList.Data = null;
                        objResultList.Status = StaticResource.NotFoundStatusCode;
                        objResultList.Message = StaticResource.EmailNotExist;
                        break;
                    default:
                        objResultList.Data = null;
                        objResultList.Status = StaticResource.NotFoundStatusCode;
                        objResultList.Message = StaticResource.NotFoundMessage;
                        break;
                }
            }
            catch (Exception ex)
            {
                HttpContext.RiseError(ex);
                objResultList.Data = null;
                objResultList.Status = StaticResource.FailStatusCode;
                objResultList.Message = StaticResource.FailMessage; ;
            }
            return new OkObjectResult(objResultList);

        }
        #endregion

        #region Invite Users

        [Route("InviteIndividualUser")]
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> InviteIndividualUser([FromBody] CompanyUsersRequest request)
        {
            JsonResponse<User> objResult = new JsonResponse<User>();
            if (request != null)
            {
                try
                {
                    HttpContext context = HttpContext;
                    int info = await this._companyService.InviteIndividualUser(request, context);

                    switch (info)
                    {
                        case 0:
                            objResult.Data = null;
                            objResult.Status = StaticResource.NotFoundStatusCode;
                            objResult.Message = StaticResource.SomethingWentWrong;
                            break;
                        case 1:
                            objResult.Data = null;
                            objResult.Status = StaticResource.SuccessStatusCode;
                            objResult.Message = StaticResource.ActivationLinkSent;
                            break;
                        case 2:
                            objResult.Data = null;
                            objResult.Status = StaticResource.DuplicateStatusCode;
                            objResult.Message = StaticResource.EmailExist;
                            break;
                        default:
                            objResult.Data = null;
                            objResult.Status = StaticResource.NotFoundStatusCode;
                            objResult.Message = StaticResource.SomethingWentWrong;
                            break;
                    }
                }
                catch (Exception ex)
                {
                    HttpContext.RiseError(ex);
                    objResult.Data = null;
                    objResult.Status = StaticResource.FailStatusCode;
                    objResult.Message = StaticResource.FailMessage;
                }
            }
            else
            {
                objResult.Data = null;
                objResult.Status = StaticResource.FailStatusCode;
                objResult.Message = StaticResource.InvalidMessage;
            }
            return new OkObjectResult(objResult);

        }
        #endregion
    }
}
