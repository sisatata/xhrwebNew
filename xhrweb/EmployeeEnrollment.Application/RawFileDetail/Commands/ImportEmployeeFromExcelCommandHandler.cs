using ASL.Hrms.SharedKernel.Interfaces;
using ASL.Hrms.SharedKernel.Models;
using EmployeeEnrollment.Core.Entities.EmployeeRawDataProcessAggregate;
using EmployeeEnrollment.Core.Interfaces;
using EmployeeEnrollment.Core.Specifications;
using MediatR;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using EmployeeEntities = EmployeeEnrollment.Core.Entities;
using EmployeeCommand = EmployeeEnrollment.Application.Employee.Commands;
using EnrollmentCommand = EmployeeEnrollment.Application.EmployeeEnrollment.Commands;
using EmployeeDetailCommand = EmployeeEnrollment.Application.EmployeeDetail.Commands;
using EmployeeEmailCommand = EmployeeEnrollment.Application.EmployeeEmail.Commands;
using EmployeePhoneCommand = EmployeeEnrollment.Application.EmployeePhone.Commands;
using EmployeeAddressCommand = EmployeeEnrollment.Application.EmployeeAddress.Commands;

using ASL.AccessControl.Services.Security;
using ASL.Utility.EmailManager.Interfaces;
using Microsoft.Extensions.Options;
using ASL.Utility.EmailManager.Models;
using ASL.AccessControl.Models;
using ASL.AccessControl.Utility;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Logging;
using System.Globalization;
using ASL.Hrms.SharedKernel.Enums;
using Microsoft.Extensions.Configuration;

namespace EmployeeEnrollment.Application.RawFileDetail.Commands
{
    public class ImportEmployeeFromExcelCommandHandler : IRequestHandler<Commands.V1.ImportEmployeeFromExcel, ImportEmployeeFromExcelVM>
    {
        public ImportEmployeeFromExcelCommandHandler(IAsyncRepository<EmployeeEntities.RawFileDetail, Guid> repository, IFileManager fileManager, IUriComposer uriComposer,
            IEmployeeRawDataPrepRepository employeeRawDataPrepRepository,
            IMediator mediator,
            IReferenceRepository<EmployeeEntities.CompanyOrganogram, Guid> repositoryCompanyOrganogram,
            IReferenceRepository<EmployeeEntities.CompanyWiseConfiguration, Guid> repositoryCompanyWiseConfiguration,
            IReferenceRepository<EmployeeEntities.CommonLookUpType, Guid> repositoryCommonLookUpType,
            IReferenceRepository<EmployeeEntities.Grade, Guid> repositoryGrade,
            IReferenceRepository<EmployeeEntities.LeaveTypeGroup, int> repositoryLeaveTypeGroup,
            IReferenceRepository<EmployeeEntities.ConfirmationRule, Guid> repositoryConfirmationRule,
            IReferenceRepository<EmployeeEntities.SalaryStructure, Guid> repositorySalaryStructure,
            IReferenceRepository<EmployeeEntities.PaymentOption, Guid> repositoryPaymentOption,
            IReferenceRepository<EmployeeEntities.District, Guid> repositoryDistrict,
            IReferenceRepository<EmployeeEntities.Upazila, Guid> repositoryUpazila,

            IUserService userService,
             IEmailSender emailSender,
             IOptions<MailServerSettings> mailServerSettings,
            IOptions<PlanetHRSettings> settings,
            ILogger<ImportEmployeeFromExcelCommandHandler> logger,
            IServiceBus serviceBus,
            IConfiguration configuration,
            IActivityLogService activityLogService,
            IEmployeeNoteService employeeNoteService)
        {
            _repository = repository;
            _fileManager = fileManager;
            _uriComposer = uriComposer;
            _employeeRawDataPrepRepository = employeeRawDataPrepRepository;
            _mediator = mediator;
            _repositoryCompanyOrganogram = repositoryCompanyOrganogram;
            _repositoryCompanyWiseConfiguration = repositoryCompanyWiseConfiguration;
            _repositoryCommonLookUpType = repositoryCommonLookUpType;
            _repositoryGrade = repositoryGrade;
            _repositoryLeaveTypeGroup = repositoryLeaveTypeGroup;
            _repositoryConfirmationRule = repositoryConfirmationRule;
            _repositorySalaryStructure = repositorySalaryStructure;
            _repositoryPaymentOption = repositoryPaymentOption;
            _repositoryDistrict = repositoryDistrict;
            _repositoryUpazila = repositoryUpazila;

            _userService = userService;
            _emailSender = emailSender;
            _mailServerSettings = mailServerSettings;
            _settings = settings;
            _logger = logger;
            _serviceBus = serviceBus;
            _configuration = configuration;
            _activityLogService = activityLogService;
            _employeeNoteService = employeeNoteService;
        }

        private readonly IAsyncRepository<EmployeeEntities.RawFileDetail, Guid> _repository;
        private readonly IFileManager _fileManager;
        private readonly IUriComposer _uriComposer;
        private readonly IEmployeeRawDataPrepRepository _employeeRawDataPrepRepository;
        private readonly IMediator _mediator;
        private readonly IReferenceRepository<EmployeeEntities.CompanyOrganogram, Guid> _repositoryCompanyOrganogram;
        private readonly IReferenceRepository<EmployeeEntities.CompanyWiseConfiguration, Guid> _repositoryCompanyWiseConfiguration;
        private readonly IReferenceRepository<EmployeeEntities.CommonLookUpType, Guid> _repositoryCommonLookUpType;
        private readonly IReferenceRepository<EmployeeEntities.Grade, Guid> _repositoryGrade;
        private readonly IReferenceRepository<EmployeeEntities.LeaveTypeGroup, int> _repositoryLeaveTypeGroup;
        private readonly IReferenceRepository<EmployeeEntities.ConfirmationRule, Guid> _repositoryConfirmationRule;
        private readonly IReferenceRepository<EmployeeEntities.SalaryStructure, Guid> _repositorySalaryStructure;
        private readonly IReferenceRepository<EmployeeEntities.PaymentOption, Guid> _repositoryPaymentOption;
        private readonly IReferenceRepository<EmployeeEntities.District, Guid> _repositoryDistrict;
        private readonly IReferenceRepository<EmployeeEntities.Upazila, Guid> _repositoryUpazila;

        private readonly IUserService _userService;
        private readonly IEmailSender _emailSender;
        private readonly IOptions<MailServerSettings> _mailServerSettings;
        private readonly IOptions<PlanetHRSettings> _settings;
        private readonly ILogger<ImportEmployeeFromExcelCommandHandler> _logger;

        private readonly IServiceBus _serviceBus;
        private readonly IConfiguration _configuration;
        private readonly IActivityLogService _activityLogService;
        private readonly IEmployeeNoteService _employeeNoteService;

        public async Task<ImportEmployeeFromExcelVM> Handle(Commands.V1.ImportEmployeeFromExcel request, CancellationToken cancellationToken)
        {
            var response = new ImportEmployeeFromExcelVM
            {
                Status = false,
                Message = "error"
            };


            try
            {
                var employeeImportFileUri = string.Empty;
                employeeImportFileUri = _fileManager.UploadFile(request.EmployeeExcelFile,
                        $"{request.Settings.ContentRoot.RootPath}{request.Settings.ContentRoot.EmployeeImagePath}",
                        new FileSettings
                        {
                            MaxFileSize = request.Settings.PlanetHRUploadFileSettings.MaxFileSize,
                            ValidExtensions = request.Settings.PlanetHRUploadFileSettings.ValidExtensions.Split(',').ToList()
                        });
                

                var entity = EmployeeEntities.RawFileDetail.Create(
                         employeeImportFileUri,
                         "EMPLOYEEIMPORT",
                         request.Comments,
                         request.CompanyId
                );

                var data = await _repository.AddAsync(entity);

                var employeeRawDataProcessAggregate = new EmployeeRawDataProcessAggregate(employeeImportFileUri, request.CompanyId.Value, data.Id, request.Settings);
                employeeRawDataProcessAggregate.StartEmployeeImportProcess();

                await _employeeRawDataPrepRepository.Save(employeeRawDataProcessAggregate.EmployeeRawDataPrepList).ConfigureAwait(false);
                data.UpdateTotalRecords(employeeRawDataProcessAggregate.EmployeeRawDataPrepList.Count);
                await _repository.UpdateAsync(data).ConfigureAwait(false);

                var companyOrg = await _repositoryCompanyOrganogram.GetAll().ConfigureAwait(false);

                var companyWiseConfigurations = await _repositoryCompanyWiseConfiguration.GetAll().ConfigureAwait(false);
                var commonLookUpTypes = await _repositoryCommonLookUpType.GetAll().ConfigureAwait(false);
                var grades = (await _repositoryGrade.GetAll().ConfigureAwait(false)).ToList().FindAll(x => x.IsDeleted == false && x.CompanyId == request.CompanyId);
                var leaveTypeGroups = (await _repositoryLeaveTypeGroup.GetAll().ConfigureAwait(false)).ToList().FindAll(x => x.IsDeleted == false && x.CompanyId == request.CompanyId);

                var confirmationRules = (await _repositoryConfirmationRule.GetAll().ConfigureAwait(false)).ToList().FindAll(x => x.IsDeleted == false && x.CompanyId == request.CompanyId);
                var salaryStructures = (await _repositorySalaryStructure.GetAll().ConfigureAwait(false)).ToList().FindAll(x => x.IsDeleted == false && x.CompanyId == request.CompanyId);
                var paymentOptions = (await _repositoryPaymentOption.GetAll().ConfigureAwait(false)).ToList().FindAll(x => x.IsDeleted == false);

                var districts = (await _repositoryDistrict.GetAll().ConfigureAwait(false)).ToList().FindAll(x => x.IsDeleted == false);
                var upazillas = (await _repositoryUpazila.GetAll().ConfigureAwait(false)).ToList().FindAll(x => x.IsDeleted == false);

                foreach (var employee in employeeRawDataProcessAggregate.EmployeeRawDataPrepList)
                {
                    try
                    {
                        //employee.RawFileDetailId = data.Id;
                        var oValidCompany = companyOrg.FirstOrDefault(x => x.CompanyName.Trim().ToLower() == employee.Company.Trim().ToLower());
                        if (oValidCompany == null)
                        {
                            employee.Status = "E";
                            employee.ErrorDescription = "Invalid Company Name";
                            employee.State = TrackingState.Modified;
                            continue;
                        }

                        var oValidBranch = companyOrg.FirstOrDefault(x => x.CompanyName.Trim().ToLower() == employee.Company.Trim().ToLower()
                        && x.BranchName.Trim().ToLower() == employee.Branch.Trim().ToLower());
                        if (oValidBranch == null)
                        {
                            employee.Status = "E";
                            employee.ErrorDescription = "Invalid Branch Name";
                            employee.State = TrackingState.Modified;
                            continue;
                        }

                        var oValidDepartment = companyOrg.FirstOrDefault(x => x.CompanyName.Trim().ToLower() == employee.Company.Trim().ToLower()
                        && x.BranchName.Trim().ToLower() == employee.Branch.Trim().ToLower()
                        && x.DepartmentName.Trim().ToLower() == employee.Department.Trim().ToLower());
                        if (oValidDepartment == null)
                        {
                            employee.Status = "E";
                            employee.ErrorDescription = "Invalid Department Name";
                            employee.State = TrackingState.Modified;
                            continue;
                        }


                        var oCompany = companyOrg.FirstOrDefault(x => x.DesignationName.Trim().ToLower() == employee.Position.Trim().ToLower()
                             && x.DepartmentName.Trim().ToLower() == employee.Department.ToLower().Trim()
                             && x.BranchName.Trim().ToLower() == employee.Branch.Trim().ToLower()
                              && x.CompanyName.Trim().ToLower() == employee.Company.Trim().ToLower()
                             );
                        if (oCompany == null)
                        {
                            employee.Status = "E";
                            employee.ErrorDescription = "Invalid Designation Name";
                            employee.State = TrackingState.Modified;
                            continue;
                        }

                        Guid? nationalityId = commonLookUpTypes.FirstOrDefault(x => (x.CompanyId == null || x.CompanyId == request.CompanyId.Value)
                            && x.LookUpTypeName.Trim().ToLower() == employee.Nationality.Trim().ToLower() && x.IsDeleted == false)?.Id;

                        if (nationalityId == null || nationalityId == Guid.Empty)
                        {
                            employee.Status = "E";
                            employee.ErrorDescription = "Invalid Nationality";
                            employee.State = TrackingState.Modified;
                            continue;
                        }

                        Guid? genderId = commonLookUpTypes.FirstOrDefault(x => (x.CompanyId == null || x.CompanyId == request.CompanyId.Value)
                             && x.LookUpTypeName.Trim().ToLower() == employee.Gender.Trim().ToLower())?.Id;

                        if (genderId == null || genderId == Guid.Empty)
                        {
                            employee.Status = "E";
                            employee.ErrorDescription = "Invalid Nationality";
                            employee.State = TrackingState.Modified;
                            continue;
                        }

                        Guid? maritalStatusId = commonLookUpTypes.FirstOrDefault(x => (x.CompanyId == null || x.CompanyId == request.CompanyId.Value)
                             && x.LookUpTypeName.Trim().ToLower() == employee.MaritalStatus.Trim().ToLower())?.Id;

                       /* if (maritalStatusId == null || maritalStatusId == Guid.Empty)
                        {
                            employee.Status = "E";
                            employee.ErrorDescription = "Invalid Marital Status";
                            employee.State = TrackingState.Modified;
                            continue;
                        }*/

                        Guid? religionId = commonLookUpTypes.FirstOrDefault(x => (x.CompanyId == null || x.CompanyId == request.CompanyId.Value)
                             && x.LookUpTypeName.Trim().ToLower() == employee.Religion.Trim().ToLower())?.Id;

                        /*if (religionId == null || religionId == Guid.Empty)
                        {
                            employee.Status = "E";
                            employee.ErrorDescription = "Invalid Religion";
                            employee.State = TrackingState.Modified;
                            continue;
                        }
*/
                        Guid? bloodGroupId = commonLookUpTypes.FirstOrDefault(x => (x.CompanyId == null || x.CompanyId == request.CompanyId.Value)
                             && x.LookUpTypeName.Trim().ToLower() == employee.BloodGroup.Trim().ToLower())?.Id;
/*
                        if (bloodGroupId == null || bloodGroupId == Guid.Empty)
                        {
                            employee.Status = "E";
                            employee.ErrorDescription = "Invalid Blood Group";
                            employee.State = TrackingState.Modified;
                            continue;
                        }*/

                        Guid? gradeId = grades.FirstOrDefault(x => x.GradeName.Trim().ToLower() == employee.Grade.Trim().ToLower())?.Id;

                        if (gradeId == null || gradeId == Guid.Empty)
                        {
                            employee.Status = "E";
                            employee.ErrorDescription = "Invalid Grade";
                            employee.State = TrackingState.Modified;
                            continue;
                        }

                        Guid? confirmationRuleId = confirmationRules.FirstOrDefault(x => x.RuleName.Trim().ToLower() == employee.ConfirmationRuleName.Trim().ToLower()
                        && x.StartDate.Date <= DateTime.Now.Date && x.EndDate.Date >= DateTime.Now.Date)?.Id;

                        if (confirmationRuleId == null || confirmationRuleId == Guid.Empty)
                        {
                            employee.Status = "E";
                            employee.ErrorDescription = "Invalid Confirmation Rule Name";
                            employee.State = TrackingState.Modified;
                            continue;
                        }


                        Guid? salaryStructureId = salaryStructures.FirstOrDefault(x => x.StructureName.Trim().ToLower() == employee.SalaryStructureName.Trim().ToLower()
                        && x.StartDate.Value.Date <= DateTime.Now.Date && x.EndDate.Value.Date >= DateTime.Now.Date)?.Id;

                        if (salaryStructureId == null || salaryStructureId == Guid.Empty)
                        {
                            employee.Status = "E";
                            employee.ErrorDescription = "Invalid Salary Structure Name";
                            employee.State = TrackingState.Modified;
                            continue;
                        }

                        Int16? paymentOptionId = paymentOptions.FirstOrDefault(x => x.OptionName.Trim().ToLower() == employee.PaymentOptionName.Trim().ToLower()
                        )?.PaymentOptionId;

                        if (paymentOptionId == null || paymentOptionId < 1)
                        {
                            employee.Status = "E";
                            employee.ErrorDescription = "Invalid Salary Payment Option Name";
                            employee.State = TrackingState.Modified;
                            continue;
                        }

                        int leaveTypeGroupId = leaveTypeGroups.FirstOrDefault(x => x.LeaveTypeGroupName.Trim().ToLower() == employee.LeaveTypeGroupName.Trim().ToLower()).Id;

                        //if (gradeId == null || gradeId == Guid.Empty)
                        //{
                        //    employee.Status = "E";
                        //    employee.ErrorDescription = "Invalid Grade";
                        //    employee.State = TrackingState.Modified;
                        //    continue;
                        //}

                        var processingCommand = new EmployeeCommand.Commands.V1.CreateEmployee
                        {
                            CompanyId = (request.CompanyId != null && request.CompanyId != Guid.Empty) ? request.CompanyId.Value : Guid.Empty,
                            FullName = employee.FullName,
                            EmployeeId = employee.EmployeeCode,
                            DateOfBirth = DateTime.ParseExact(employee.DOB, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                            PositionId = oCompany.DesignationId,
                            DepartmentId = oCompany.DepartmentId,
                            BranchId = oCompany.BranchId,
                            NationalityId = nationalityId.Value,
                            GenderId = genderId.Value,
                            UserId = employee.Emailaddress,
                            Password = "Us3r124$",

                        };
                        var employeeResponse = await _mediator.Send(processingCommand).ConfigureAwait(false);

                        // login and role create begin
                        if (employeeResponse.Status)
                        {
                            var user = await _userService.CreateUser(new UserModel { Email = employee.Emailaddress, Password = "Us3r124$", CompanyId = request.CompanyId, PhoneNumber = employee.MobileNo });
                            if (!user.ToLower().Contains("error"))
                            {
                                //var companyDetail = await Mediator.Send(new CompanyQuery.Queries.GetCompanyById { CompanyId = command.CompanyId }).ConfigureAwait(false);

                                var role = await _userService.AssignRole(user, new string[] { AuthorizationConstants.Roles.EMPLOYEEROLE });
                                var dataLogin = await _mediator.Send(new EmployeeCommand.Commands.V1.UpdateLoginIdEmployee { Id = employeeResponse.Id, LoginId = new Guid(user) });
                                SendNewEmployeeWelcomeEmail(employee.FullName, "Us3r124$", employee.Emailaddress, employee.Company);
                                _logger.LogError(user);
                            }
                            else
                            {
                                var deleteEmployee = await _mediator.Send(new EmployeeCommand.Commands.V1.MarkAsDeleteEmployee { Id = data.Id });
                                employeeResponse.Status = false;
                                employeeResponse.Message = user;
                                employeeResponse.Id = Guid.Empty;
                            }

                            // employee enrollment
                            var enrollmentCommand = new EnrollmentCommand.Commands.V1.CreateEmployeeEnrollment
                            {
                                EmployeeId = employeeResponse.Id,
                                JoiningDate = DateTime.ParseExact(employee.JoiningDate, "dd/MM/yyyy", CultureInfo.InvariantCulture),
                                PermanentDate = !string.IsNullOrEmpty(employee.PermanentDate) ? DateTime.ParseExact(employee.PermanentDate, "dd/MM/yyyy", CultureInfo.InvariantCulture) : (DateTime?)null,
                                StatusId = 1,
                                GradeId = gradeId,
                                LeaveTypeGroup = string.IsNullOrEmpty(employee.LeaveTypeGroupName) ? "" : employee.LeaveTypeGroupName.Trim(),
                                ConfirmationId = confirmationRuleId
                            };

                            var enrollmentResponse = await _mediator.Send(enrollmentCommand).ConfigureAwait(false);

                            //--- employee detail
                            var employeeDetailCommand = new EmployeeDetailCommand.Commands.V1.CreateEmployeeDetail
                            {
                                EmployeeId = employeeResponse.Id,
                                FathersName = employee.NameofFather,
                                MothersName = employee.NameofMother,
                                SpouseName = employee.NameofSpouce,
                                MaritalStatusId = maritalStatusId,
                                ReligionId = religionId,
                                NID = employee.NID,
                                BID = employee.BID,
                                BloodGroupId = bloodGroupId
                            };

                            var employeeDetailResponse = await _mediator.Send(employeeDetailCommand).ConfigureAwait(false);
                            //-----------

                            //--- employee email
                            if (!string.IsNullOrEmpty(employee.Emailaddress))
                            {
                                var employeeEmailCommand = new EmployeeEmailCommand.Commands.V1.CreateEmployeeEmail
                                {
                                    EmployeeId = employeeResponse.Id,
                                    EmailAddress = employee.Emailaddress,
                                    IsPrimary = true

                                };

                                var employeeEmailResponse = await _mediator.Send(employeeEmailCommand).ConfigureAwait(false);
                            }
                            //-----------

                            //--- employee phone
                            if (!string.IsNullOrEmpty(employee.MobileNo))
                            {
                                var employeePhoneCommand = new EmployeePhoneCommand.Commands.V1.CreateEmployeePhone
                                {
                                    EmployeeId = employeeResponse.Id,
                                    PhoneNumber = employee.MobileNo


                                };
                                var employeePhoneResponse = await _mediator.Send(employeePhoneCommand).ConfigureAwait(false);
                            }
                            //-----------

                            //--- employee phone
                            if (!string.IsNullOrEmpty(employee.EmergencyContact))
                            {
                                var employeeEmPhoneCommand = new EmployeePhoneCommand.Commands.V1.CreateEmployeePhone
                                {
                                    EmployeeId = employeeResponse.Id,
                                    PhoneNumber = employee.EmergencyContact

                                };
                                var employeeEmPhoneResponse = await _mediator.Send(employeeEmPhoneCommand).ConfigureAwait(false);
                            }

                            //--- employee present address
                            if (!string.IsNullOrEmpty(employee.PresentAddressLine1))
                            {
                                var districtId = districts.FirstOrDefault(x => x.Name.Trim().ToLower() == employee.PresentDistrict.Trim().ToLower())?.Id;

                                var employeePresentAddressCommand = new EmployeeAddressCommand.Commands.V1.CreateEmployeeAddress
                                {
                                    EmployeeId = employeeResponse.Id,
                                    AddressLine1 = employee.PresentAddressLine1,
                                    AddressLine2 = employee.PresentAddressLine2,
                                    Village = employee.PresentVillage,
                                    PostOffice = employee.PresentPostOffice,
                                    DistrictId = districtId,
                                    ThanaId = upazillas.FirstOrDefault(x => x.DistrictId == districtId && x.UpazilaName.Trim().ToLower() == employee.PresentThana.Trim().ToLower())?.Id,
                                    IsDeleted = false

                                };
                                var employeePresentAddrResponse = await _mediator.Send(employeePresentAddressCommand).ConfigureAwait(false);
                            }

                            //--- employee permanent address
                            if (!string.IsNullOrEmpty(employee.PermanentAddressLine1))
                            {
                                var districtId = districts.FirstOrDefault(x => x.Name.Trim().ToLower() == employee.PermanentDistrict.Trim().ToLower())?.Id;
                                var employeePermanentAddressCommand = new EmployeeAddressCommand.Commands.V1.CreateEmployeeAddress
                                {
                                    EmployeeId = employeeResponse.Id,
                                    AddressLine1 = employee.PermanentAddressLine1,
                                    AddressLine2 = employee.PermanentAddressLine2,
                                    Village = employee.PermanentVillage,
                                    PostOffice = employee.PermanentPostOffice,
                                    DistrictId = districtId,
                                    ThanaId = upazillas.FirstOrDefault(x => x.DistrictId == districtId && x.UpazilaName.Trim().ToLower() == employee.PermanentThana.Trim().ToLower())?.Id,
                                    IsDeleted = false

                                };
                                var employeePermanentAddrResponse = await _mediator.Send(employeePermanentAddressCommand).ConfigureAwait(false);
                            }

                            // publish the event in message queue.

                            var @event = new Core.Events.PromotionTransferApproveEvent
                            {
                                EmployeeId = employeeResponse.Id,
                                BranchId = oCompany.BranchId,
                                DepartmentId = oCompany.DepartmentId,
                                PositionId = oCompany.DesignationId,

                                GradeId = gradeId,
                                SalaryStructureId = salaryStructureId,
                                PaymentOptionId = paymentOptionId,
                                GrossSalary = string.IsNullOrEmpty(employee.Gross) ? (decimal?)0 : Convert.ToDecimal(employee.Gross.Trim()),
                                CompanyId = oCompany.CompanyId,
                                StartDate = string.IsNullOrEmpty(employee.JoiningDate) ? (DateTime?)null : Convert.ToDateTime(employee.JoiningDate.Trim()),
                                EndDate = DateTime.Now.AddYears(5),
                                IsDeleted = false,
                                CommandPublished = DateTime.Now
                            };

                            await _serviceBus.PublishWithRetry(_configuration.GetValue<string>("TransactionQueueName"), 1, TimeSpan.Zero, @event, false).ConfigureAwait(false);

                            //-----------
                            employee.Status = "S";
                            employee.State = TrackingState.Modified;
                        }
                        else
                        {

                            employee.Status = "E";
                            employee.ErrorDescription = employeeResponse.Message;
                            employee.State = TrackingState.Modified;
                        }
                        //----------------------

                    }
                    catch (Exception ex)
                    {
                        employee.Status = "E";
                        employee.ErrorDescription = ex.Message;
                        employee.State = TrackingState.Modified;
                        continue;
                    }

                } // end for loop

                await _employeeRawDataPrepRepository.Save(employeeRawDataProcessAggregate.EmployeeRawDataPrepList).ConfigureAwait(false);

                data.CompleteProcessing(employeeRawDataProcessAggregate.EmployeeRawDataPrepList.FindAll(x => x.Status == "S").Count,
                    employeeRawDataProcessAggregate.EmployeeRawDataPrepList.FindAll(x => x.Status == "E" || string.IsNullOrEmpty(x.Status)).Count);
                await _repository.UpdateAsync(data).ConfigureAwait(false);

                response.Status = true;
                if (employeeImportFileUri.Contains("This file already uploaded"))
                {

                    response.Status = false;
                  
                }
                var status =
                response.Message = data.Status == "S" ? $"{data.FileName} has been proccessed successfully. Total records in file : {data.TotalRecord}. Total success records : {data.TotalSuccess} and failed records {data.TotalFail}" : $"{data.FileName} has been failed to proccess. Please see history for detail";
            }
            catch (Exception ex)
            {
                response.Message = $"Exception happened during {request.EmployeeExcelFile} file processing. Errored : {ex.Message}";
            }

            return response;
        }

        private async void SendNewEmployeeWelcomeEmail(string fullName, string password, string emailId, string _companyName)
        {
            //var companyDetail = await Mediator.Send(new CompanyQuery.Queries.GetCompanyById { CompanyId = command.CompanyId }).ConfigureAwait(false);
            StringBuilder sbBody = new StringBuilder();
            sbBody.AppendLine($"<html> <body>");
            sbBody.AppendLine($"<p>Dear {fullName},</p>");

            sbBody.AppendLine($"<p>");

            sbBody.AppendLine($"Welcome to <b>{_companyName}</b>. We are excited to have you and look forward to see you into our organization.");
            sbBody.AppendLine($"</p>");
            sbBody.AppendLine($"<p>");
            sbBody.AppendLine($"From now on, please record your every HR activity into the planetHR app. Here are your login credentials to access the application");
            sbBody.AppendLine($"</p>");
            sbBody.AppendLine($"<p>");
            sbBody.AppendLine($"Password: {password}");
            sbBody.AppendLine($"</p>");
            sbBody.AppendLine($"<p>For App download:");
            sbBody.AppendLine($"<br />Android: <a href= \"https://play.google.com/store/apps/details?id=com.aplectrum.planethr\">https://play.google.com/store/apps/details?id=com.aplectrum.planethr</a>");
            sbBody.AppendLine($"<br />iOS: <a href= \"https://apps.apple.com/us/app/planethr/id1521201088\">https://apps.apple.com/us/app/planethr/id1521201088</a>");
            sbBody.AppendLine($"</p>");
            sbBody.AppendLine($"<br />Please use <b>{_settings.Value.DomainName}</b> as the domain name when entering into the mobile apps.");

            sbBody.AppendLine($"<br />planetHR Web Portal URL: <a href= \"{_settings.Value.ClientUrl}\">{_settings.Value.ClientUrl}</a>");
            sbBody.AppendLine($"<br />");

            sbBody.AppendLine($"Regards,");
            sbBody.AppendLine($"<br />");
            sbBody.AppendLine($"<br />");
            sbBody.AppendLine("<p style=\"color:Green;\"><b>planetHR Team</b></p>");
            sbBody.AppendLine($"</body>");
            sbBody.AppendLine($"</html>");

            await _emailSender.SendMail(new EmailModel
            {
                Subject = $"Congratulations, your account is created successfully in planetHR by your company.",
                Body = sbBody.ToString(),
                AllowHtml = true,
                ReceiverMailIds = new List<string> { emailId },
                ReceiverBccs = new List<string> { "azhar@aplectrum.com" }
            }, _mailServerSettings.Value);
        }
    }
}

