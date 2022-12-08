using ASL.Hrms.SharedKernel.Interfaces;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using EmployeeStatus = EmployeeEnrollment.Application.EmployeeStatus.Commands;
using Microsoft.Extensions.Configuration;
using EmployeeGrade = CompanySetup.Application.Grade.Commands;
using ConfirmationRule = CompanySetup.Application.ConfirmationRule.Commands;
using CompanyQuery = CompanySetup.Application.Company.Queries;
using Branch = CompanySetup.Application.Branch.Commands;
using Department = CompanySetup.Application.Department.Commands;
using Designation = CompanySetup.Application.Designation.Commands;
using FinancialYear = CompanySetup.Application.FinancialYear.Commands;
using MonthCycle = CompanySetup.Application.MonthCycle.Commands;
using EmployeeInfo = EmployeeEnrollment.Application.Employee.Commands;
using EmployeeEnrol = EmployeeEnrollment.Application.EmployeeEnrollment.Commands;
using ShiftGroup = Attendance.Application.ShiftGroup.Commands;
using Shift = Attendance.Application.Shift.Commands;
using ShiftAllocation = Attendance.Application.ShiftAllocation.Commands;
using Holiday = Attendance.Application.Holiday.Commands;
using LeaveTypeGroup = LeaveManagement.Application.LeaveTypeGroup.Commands;
using LeaveType = LeaveManagement.Application.LeaveTypes.Commands;
using LeaveBalanceProcess = LeaveManagement.Application.LeaveBalances;
using SalaryStructure = PayrollManagement.Application.SalaryStructure.Commands;
using SalaryStructureComponent = PayrollManagement.Application.SalaryStructureComponent.Commands;
using System.Linq;
using ASL.AccessControl.Identity;
using Microsoft.EntityFrameworkCore;
using ASL.AccessControl.Utility;
using ASL.AccessControl.Services.Security;
using ASL.Hrms.SharedKernel.Enums;
using ASL.Utility.EmailManager.Interfaces;
using Microsoft.Extensions.Options;
using ASL.Utility.EmailManager.Models;
using ASL.Hrms.SharedKernel.Models;

namespace Asl.Infrastructure
{
    public class CompanyApprovePostService : ICompanyApprovePostService
    {
        public class CustomDictionary
        {
            public Guid Id { get; set; }
            public string Name { get; set; }
            public string Type { get; set; }
            public int IntId { get; set; }
        }
        public CompanyApprovePostService(IConfiguration configuration,
            IMediator mediator,
            ILogger<CompanyApprovePostService> logger,
            AppIdentityDbContext accDBContext,
            IUserService userService,
            IEmailSender emailSender,
            IOptions<MailServerSettings> mailServerSettings,
            IOptions<PlanetHRSettings> settings
            )
        {
            _configuration = configuration;
            _mediator = mediator;
            _logger = logger;
            _customDictionary = new List<CustomDictionary>();
            _accDBContext = accDBContext;
            _userService = userService;
            _emailSender = emailSender;
            _mailServerSettings = mailServerSettings;
            _settings = settings;
        }
        private readonly IConfiguration _configuration;

        private readonly IMediator _mediator;
        private readonly ILogger<CompanyApprovePostService> _logger;
        private List<CustomDictionary> _customDictionary;
        private readonly AppIdentityDbContext _accDBContext;
        private readonly IUserService _userService;
        private readonly IEmailSender _emailSender;
        private readonly IOptions<MailServerSettings> _mailServerSettings;
        private readonly IOptions<PlanetHRSettings> _settings;
        public async Task AddDefaultValues(Guid? CompanyId)
        {
            await AddDefaultEmployeeStatuses(CompanyId);
            await AddDefaultGrades(CompanyId);
            await AddDefaultEmployeeConfirmationRules(CompanyId);
            await AddDefaultBranches(CompanyId);
            await AddDefaultDepartments(CompanyId);
            await AddDefaultDesignations(CompanyId);
            await AddDefaultFinancialYear(CompanyId);
            await AddDefaultMonthCycles(CompanyId);
            await AddDefaultShiftGroup(CompanyId);
            await AddDefaultShift(CompanyId);
            await AddDefaultHoliday(CompanyId);
            await AddDefaultLeaveTypeGroup(CompanyId);
            await AddDefaultLeaveType(CompanyId);
            await AddDefaultEmployees(CompanyId);
            await AddDefaultSalaryStructure(CompanyId);
            await AddDefaultSalaryStructureComponent(CompanyId);
            if (_mailServerSettings.Value.IsEnabled)
                await SendComanyApprovedEmail(CompanyId);
        }

        private async Task SendComanyApprovedEmail(Guid? CompanyId)
        {
            var user = await _accDBContext.Users.FirstOrDefaultAsync(x => x.CompanyId == CompanyId).ConfigureAwait(false);
            if (user != null && !string.IsNullOrEmpty(user.FullName))
            {
                var companyDetail = await _mediator.Send(new CompanyQuery.Queries.GetCompanyById
                {
                    CompanyId = CompanyId.Value
                }
            ).ConfigureAwait(false);

                StringBuilder sbBody = new StringBuilder();
                sbBody.AppendLine($"<html>");
                sbBody.AppendLine($"<body>");

                sbBody.AppendLine($"<html>");
                sbBody.AppendLine($"<body>");
                sbBody.AppendLine($"  <p>Dear {user.FullName},</p>");
                sbBody.AppendLine($"  <p>Your company has been approved with some default data in planetHR. You can start adding employees and do other HR works as per your need.</p>");

                sbBody.AppendLine($"<br />planetHR Admin Portal URL: <a href= \"{_settings.Value.ClientUrl}\">{_settings.Value.ClientUrl}</a>");
                sbBody.AppendLine($"<br />planetHR Mobile App download:");
                sbBody.AppendLine($"<br />Android: <a href= \"https://play.google.com/store/apps/details?id=com.aplectrum.planethr\">https://play.google.com/store/apps/details?id=com.aplectrum.planethr</a>");
                sbBody.AppendLine($"<br />iOS: <a href= \"https://apps.apple.com/us/app/planethr/id1521201088\">https://apps.apple.com/us/app/planethr/id1521201088</a>");

                sbBody.AppendLine($"<br />Please use <b>{_settings.Value.DomainName}</b> as the domain name when entering into the mobile apps.");
                //sbBody.AppendLine($"<br />For any query please visit: https://planethrms.com/ or mail us at: sales@aplectrum.com");
                sbBody.AppendLine($"<p>For any query please visit: <a href= \"https://planethrms.com/\"> https://planethrms.com/</a> or mail us at: <a href=\"mailto:sales@aplectrum.com\">sales@aplectrum.com</a></p>");


                sbBody.AppendLine($"  <p>Regards,</p>");
                sbBody.AppendLine("<p style=\"color:Green;\"><b>planetHR Team</b></p>");
                sbBody.AppendLine($"</body>");
                sbBody.AppendLine($"</html>");


                await _emailSender.SendMail(new EmailModel
                {
                    //Subject = $"planetHR > {companyDetail.CompanyName} has been approved",
                    Subject = $"Congratulation, your company \"{companyDetail.CompanyName}\" has been approved",
                    Body = sbBody.ToString(),
                    AllowHtml = true,
                    ReceiverMailIds = new List<string> { user.Email },
                    ReceiverBccs = new List<string> { "support@planethrms.com" }
                }, _mailServerSettings.Value);
            }
        }
        public async Task<bool> AddDefaultEmployeeStatuses(Guid? CompanyId)
        {
            _logger.LogInformation($"Employee statuses added for {CompanyId}");
            try
            {
                var processingCommand = new EmployeeStatus.Commands.V1.CreateEmployeeStatus
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    EmployeeStatusName = "Active",
                    Rank = 1,
                };
                await _mediator.Send(processingCommand).ConfigureAwait(false);

                processingCommand = new EmployeeStatus.Commands.V1.CreateEmployeeStatus
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    EmployeeStatusName = "Lefty",
                    Rank = 2,
                };
                await _mediator.Send(processingCommand).ConfigureAwait(false);

                processingCommand = new EmployeeStatus.Commands.V1.CreateEmployeeStatus
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    EmployeeStatusName = "Resignation (Notice)",
                    Rank = 3,
                };
                await _mediator.Send(processingCommand).ConfigureAwait(false);

                processingCommand = new EmployeeStatus.Commands.V1.CreateEmployeeStatus
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    EmployeeStatusName = "Resignation (Without Notice)",
                    Rank = 4,
                };
                await _mediator.Send(processingCommand).ConfigureAwait(false);

                processingCommand = new EmployeeStatus.Commands.V1.CreateEmployeeStatus
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    EmployeeStatusName = "Termination",
                    Rank = 5,
                };
                await _mediator.Send(processingCommand).ConfigureAwait(false);

                processingCommand = new EmployeeStatus.Commands.V1.CreateEmployeeStatus
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    EmployeeStatusName = "Dismiss",
                    Rank = 6,
                };
                await _mediator.Send(processingCommand).ConfigureAwait(false);

                processingCommand = new EmployeeStatus.Commands.V1.CreateEmployeeStatus
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    EmployeeStatusName = "Inactive",
                    Rank = 7,
                };
                await _mediator.Send(processingCommand).ConfigureAwait(false);

                _logger.LogInformation($"Employee statuses added for {CompanyId}");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while statuses adding for new company {CompanyId}");
            }
            return true;
        }

        public async Task<bool> AddDefaultGrades(Guid? CompanyId)
        {
            _logger.LogInformation($"Employee grades called for {CompanyId}");
            try
            {
                var processingCommand = new EmployeeGrade.Commands.V1.CreateGrade
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    GradeName = "Grade-1",
                    Rank = 1,
                    IsDeleted = false
                };
                await _mediator.Send(processingCommand).ConfigureAwait(false);

                processingCommand = new EmployeeGrade.Commands.V1.CreateGrade
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    GradeName = "Grade-2",
                    Rank = 2,
                };
                await _mediator.Send(processingCommand).ConfigureAwait(false);

                processingCommand = new EmployeeGrade.Commands.V1.CreateGrade
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    GradeName = "Grade-3",
                    Rank = 3,
                };
                await _mediator.Send(processingCommand).ConfigureAwait(false);

                processingCommand = new EmployeeGrade.Commands.V1.CreateGrade
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    GradeName = "Grade-4",
                    Rank = 4,
                };
                await _mediator.Send(processingCommand).ConfigureAwait(false);

                processingCommand = new EmployeeGrade.Commands.V1.CreateGrade
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    GradeName = "Grade-4",
                    Rank = 5,
                };
                await _mediator.Send(processingCommand).ConfigureAwait(false);

                processingCommand = new EmployeeGrade.Commands.V1.CreateGrade
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    GradeName = "Grade-6",
                    Rank = 6,
                };
                await _mediator.Send(processingCommand).ConfigureAwait(false);
                _logger.LogInformation($"Employee grades added for {CompanyId}");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while grades adding for new company {CompanyId}");
            }
            return true;
        }

        public async Task<bool> AddDefaultEmployeeConfirmationRules(Guid? CompanyId)
        {
            _logger.LogInformation($"Employee confirmation rule method called for {CompanyId}");
            try
            {
                var processingCommand = new ConfirmationRule.Commands.V1.CreateConfirmationRule
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    RuleName = "Confirmation after 6 months",
                    Description = "Employee will be confirmed after six months",
                    StartDate = DateTime.Now.AddYears(-20),
                    EndDate = DateTime.Now.AddYears(10),
                    ConfirmationType = 1,
                    ConfirmationMonths = 6,
                    SortOrder = 1,
                    IsActive = true,
                    IsDeleted = false
                };
                await _mediator.Send(processingCommand).ConfigureAwait(false);

                processingCommand = new ConfirmationRule.Commands.V1.CreateConfirmationRule
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    RuleName = "Confirmation after 3 months",
                    Description = "Employee will be confirmed after tree months",
                    StartDate = DateTime.Now.AddYears(-20),
                    EndDate = DateTime.Now.AddYears(10),
                    ConfirmationType = 2,
                    ConfirmationMonths = 3,
                    SortOrder = 2,
                    IsActive = true,
                    IsDeleted = false
                };
                await _mediator.Send(processingCommand).ConfigureAwait(false);

                processingCommand = new ConfirmationRule.Commands.V1.CreateConfirmationRule
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    RuleName = "Confirmation based on performance",
                    Description = "Employee will be confirmed based on performance",
                    StartDate = DateTime.Now.AddYears(-20),
                    EndDate = DateTime.Now.AddYears(10),
                    ConfirmationType = -1,
                    ConfirmationMonths = 0,
                    SortOrder = 3,
                    IsActive = true,
                    IsDeleted = false
                };
                await _mediator.Send(processingCommand).ConfigureAwait(false);


                _logger.LogInformation($"Employee confirmation rule added for {CompanyId}");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while confirmation rule adding for new company {CompanyId}");
            }
            return true;
        }

        // Branch
        public async Task<bool> AddDefaultBranches(Guid? CompanyId)
        {
            _logger.LogInformation($"AddDefaultBranches method called for {CompanyId}");
            try
            {
                var processingCommand = new Branch.BranchCommands.V1.CreateBranch
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    BranchName = "Head Office",
                    ShortName = "HO",
                    BranchLocalizedName = "",
                    SortOrder = 1,
                };
                var response = await _mediator.Send(processingCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.BranchName,
                    Id = response.Id,
                    Type = "Branch"
                });

                processingCommand = new Branch.BranchCommands.V1.CreateBranch
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    BranchName = "Warehouse",
                    ShortName = "WH",
                    BranchLocalizedName = "",
                    SortOrder = 2,
                };

                response = await _mediator.Send(processingCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.BranchName,
                    Id = response.Id,
                    Type = "Branch"
                });

                _logger.LogInformation($"AddDefaultBranches rule added for {CompanyId}");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while AddDefaultBranches adding for new company {CompanyId}");
            }
            return true;
        }

        // Department
        public async Task<bool> AddDefaultDepartments(Guid? CompanyId)
        {
            _logger.LogInformation($"AddDefaultDepartments method called for {CompanyId}");
            try
            {
                var processingCommand = new Department.DepartmentCommands.V1.CreateDepartment
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    DepartmentName = "Management",
                    ShortName = "Mng",
                    DepartmentLocalizedName = "",
                    BranchId = _customDictionary.FirstOrDefault(x => x.Type == "Branch" && x.Name == "Head Office").Id,
                    SortOrder = 1,
                };
                var response = await _mediator.Send(processingCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.DepartmentName,
                    Id = response.Id,
                    Type = "Department"
                });

                processingCommand = new Department.DepartmentCommands.V1.CreateDepartment
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    DepartmentName = "Accounts",
                    ShortName = "AC",
                    DepartmentLocalizedName = "",
                    BranchId = _customDictionary.FirstOrDefault(x => x.Type == "Branch" && x.Name == "Head Office").Id,
                    SortOrder = 1,
                };
                response = await _mediator.Send(processingCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.DepartmentName,
                    Id = response.Id,
                    Type = "Department"
                });

                processingCommand = new Department.DepartmentCommands.V1.CreateDepartment
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    DepartmentName = "Human Resource",
                    ShortName = "HR",
                    DepartmentLocalizedName = "",
                    BranchId = _customDictionary.FirstOrDefault(x => x.Type == "Branch" && x.Name == "Head Office").Id,
                    SortOrder = 1,
                };
                response = await _mediator.Send(processingCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.DepartmentName,
                    Id = response.Id,
                    Type = "Department"
                });


                _logger.LogInformation($"AddDefaultBranches rule added for {CompanyId}");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while AddDefaultBranches adding for new company {CompanyId}");
            }
            return true;
        }

        // Department
        public async Task<bool> AddDefaultDesignations(Guid? CompanyId)
        {
            _logger.LogInformation($"AddDefaultDesignations method called for {CompanyId}");
            try
            {
                var processingCommand = new Designation.DesignationCommands.V1.CreateDesignation
                {
                    //CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    DesignationName = "Chairman",
                    ShortName = "Chairman",
                    DesignationLocalizedName = "",
                    LinkedEntityType = "Department",
                    LinkedEntityId = _customDictionary.FirstOrDefault(x => x.Type == "Department" && x.Name == "Management").Id,
                    SortOrder = 1,
                };
                var response = await _mediator.Send(processingCommand).ConfigureAwait(false);

                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.DesignationName,
                    Id = response.Id,
                    Type = "Designation"
                });

                processingCommand = new Designation.DesignationCommands.V1.CreateDesignation
                {
                    //CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    DesignationName = "Managing Director",
                    ShortName = "MD",
                    DesignationLocalizedName = "",
                    LinkedEntityType = "Department",
                    LinkedEntityId = _customDictionary.FirstOrDefault(x => x.Type == "Department" && x.Name == "Management").Id,
                    SortOrder = 1,
                };
                response = await _mediator.Send(processingCommand).ConfigureAwait(false);

                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.DesignationName,
                    Id = response.Id,
                    Type = "Designation"
                });

                processingCommand = new Designation.DesignationCommands.V1.CreateDesignation
                {
                    DesignationName = "Director",
                    ShortName = "Director",
                    DesignationLocalizedName = "",
                    LinkedEntityType = "Department",
                    LinkedEntityId = _customDictionary.FirstOrDefault(x => x.Type == "Department" && x.Name == "Management").Id,
                    SortOrder = 1,
                };
                response = await _mediator.Send(processingCommand).ConfigureAwait(false);

                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.DesignationName,
                    Id = response.Id,
                    Type = "Designation"
                });

                //GM
                processingCommand = new Designation.DesignationCommands.V1.CreateDesignation
                {
                    DesignationName = "General Manager",
                    ShortName = "GM",
                    DesignationLocalizedName = "",
                    LinkedEntityType = "Department",
                    LinkedEntityId = _customDictionary.FirstOrDefault(x => x.Type == "Department" && x.Name == "Management").Id,
                    SortOrder = 1,
                };
                response = await _mediator.Send(processingCommand).ConfigureAwait(false);

                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.DesignationName,
                    Id = response.Id,
                    Type = "Designation"
                });

                //AGM
                processingCommand = new Designation.DesignationCommands.V1.CreateDesignation
                {
                    DesignationName = "Assistant General Manager",
                    ShortName = "AGM",
                    DesignationLocalizedName = "",
                    LinkedEntityType = "Department",
                    LinkedEntityId = _customDictionary.FirstOrDefault(x => x.Type == "Department" && x.Name == "Management").Id,
                    SortOrder = 1,
                };
                response = await _mediator.Send(processingCommand).ConfigureAwait(false);

                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.DesignationName,
                    Id = response.Id,
                    Type = "Designation"
                });

                //Manager
                processingCommand = new Designation.DesignationCommands.V1.CreateDesignation
                {
                    DesignationName = "Manager",
                    ShortName = "Manager",
                    DesignationLocalizedName = "",
                    LinkedEntityType = "Department",
                    LinkedEntityId = _customDictionary.FirstOrDefault(x => x.Type == "Department" && x.Name == "Management").Id,
                    SortOrder = 1,
                };
                response = await _mediator.Send(processingCommand).ConfigureAwait(false);

                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.DesignationName,
                    Id = response.Id,
                    Type = "Designation"
                });

                //Asst Manager
                processingCommand = new Designation.DesignationCommands.V1.CreateDesignation
                {
                    DesignationName = "Assistant Manager",
                    ShortName = "Asst. Manager",
                    DesignationLocalizedName = "",
                    LinkedEntityType = "Department",
                    LinkedEntityId = _customDictionary.FirstOrDefault(x => x.Type == "Department" && x.Name == "Management").Id,
                    SortOrder = 1,
                };
                response = await _mediator.Send(processingCommand).ConfigureAwait(false);

                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.DesignationName,
                    Id = response.Id,
                    Type = "Designation"
                });

                // Sr. Executive

                processingCommand = new Designation.DesignationCommands.V1.CreateDesignation
                {
                    DesignationName = "Senior Executive",
                    ShortName = "Sr. Executive",
                    DesignationLocalizedName = "",
                    LinkedEntityType = "Department",
                    LinkedEntityId = _customDictionary.FirstOrDefault(x => x.Type == "Department" && x.Name == "Management").Id,
                    SortOrder = 1,
                };
                response = await _mediator.Send(processingCommand).ConfigureAwait(false);

                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.DesignationName,
                    Id = response.Id,
                    Type = "Designation"
                });

                //Executive
                processingCommand = new Designation.DesignationCommands.V1.CreateDesignation
                {
                    DesignationName = "Executive",
                    ShortName = "Executive",
                    DesignationLocalizedName = "",
                    LinkedEntityType = "Department",
                    LinkedEntityId = _customDictionary.FirstOrDefault(x => x.Type == "Department" && x.Name == "Management").Id,
                    SortOrder = 1,
                };
                response = await _mediator.Send(processingCommand).ConfigureAwait(false);

                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.DesignationName,
                    Id = response.Id,
                    Type = "Designation"
                });

                //Jr. Executive
                processingCommand = new Designation.DesignationCommands.V1.CreateDesignation
                {
                    DesignationName = "Junior Executive",
                    ShortName = "Jr. Executive",
                    DesignationLocalizedName = "",
                    LinkedEntityType = "Department",
                    LinkedEntityId = _customDictionary.FirstOrDefault(x => x.Type == "Department" && x.Name == "Management").Id,
                    SortOrder = 1,
                };
                response = await _mediator.Send(processingCommand).ConfigureAwait(false);

                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.DesignationName,
                    Id = response.Id,
                    Type = "Designation"
                });

                _logger.LogInformation($"AddDefaultBranches rule added for {CompanyId}");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while AddDefaultBranches adding for new company {CompanyId}");
            }
            return true;
        }

        public async Task<bool> AddDefaultFinancialYear(Guid? CompanyId)
        {
            _logger.LogInformation($"AddDefaultFinancialYear method called for {CompanyId}");
            try
            {
                var processingCommand = new FinancialYear.FinancialYearCommands.V1.CreateFinancialYear
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    FinancialYearName = DateTime.Now.Year.ToString(),
                    FinancialYearLocalizedName = DateTime.Now.Year.ToString(),
                    FinancialYearStartDate = new DateTime(DateTime.Now.Year, 1, 1),
                    FinancialYearEndDate = new DateTime(DateTime.Now.Year, 12, 31),
                    SortOrder = 1,
                };
                var response = await _mediator.Send(processingCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.FinancialYearName,
                    Id = response.Id,
                    Type = "FinancialYear"
                });
                _logger.LogInformation($"AddDefaultFinancialYear rule added for {CompanyId}");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while AddDefaultFinancialYear adding for new company {CompanyId}");
            }
            return true;
        }

        public async Task<bool> AddDefaultMonthCycles(Guid? CompanyId)
        {
            _logger.LogInformation($"AddDefaultMonthCycles method called for {CompanyId}");
            try
            {
                //January
                await AddMonth((CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                      "January", new DateTime(DateTime.Now.Year, 1, 1), new DateTime(DateTime.Now.Year, 1, 31),
                      22, 1, _customDictionary.FirstOrDefault(x => x.Type == "FinancialYear").Id);

                //February
                await AddMonth((CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                      "February", new DateTime(DateTime.Now.Year, 2, 1), new DateTime(DateTime.Now.Year, 2, 28),
                      22, 2, _customDictionary.FirstOrDefault(x => x.Type == "FinancialYear").Id);

                //March 3
                await AddMonth((CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                      "March", new DateTime(DateTime.Now.Year, 3, 1), new DateTime(DateTime.Now.Year, 3, 31),
                      22, 3, _customDictionary.FirstOrDefault(x => x.Type == "FinancialYear").Id);

                //April 4
                await AddMonth((CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                      "April", new DateTime(DateTime.Now.Year, 4, 1), new DateTime(DateTime.Now.Year, 4, 30),
                      22, 4, _customDictionary.FirstOrDefault(x => x.Type == "FinancialYear").Id);

                //May 5
                await AddMonth((CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                      "May", new DateTime(DateTime.Now.Year, 5, 1), new DateTime(DateTime.Now.Year, 5, 31),
                      22, 5, _customDictionary.FirstOrDefault(x => x.Type == "FinancialYear").Id);

                //June 6
                await AddMonth((CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                      "June", new DateTime(DateTime.Now.Year, 6, 1), new DateTime(DateTime.Now.Year, 6, 30),
                      22, 6, _customDictionary.FirstOrDefault(x => x.Type == "FinancialYear").Id);

                //July 7
                await AddMonth((CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                      "July", new DateTime(DateTime.Now.Year, 7, 1), new DateTime(DateTime.Now.Year, 7, 31),
                      22, 7, _customDictionary.FirstOrDefault(x => x.Type == "FinancialYear").Id);

                //August 8
                await AddMonth((CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                      "August", new DateTime(DateTime.Now.Year, 8, 1), new DateTime(DateTime.Now.Year, 8, 31),
                      22, 8, _customDictionary.FirstOrDefault(x => x.Type == "FinancialYear").Id);

                //September 9
                await AddMonth((CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                      "September", new DateTime(DateTime.Now.Year, 9, 1), new DateTime(DateTime.Now.Year, 9, 30),
                      22, 9, _customDictionary.FirstOrDefault(x => x.Type == "FinancialYear").Id);

                //October 10
                await AddMonth((CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                      "October", new DateTime(DateTime.Now.Year, 10, 1), new DateTime(DateTime.Now.Year, 10, 31),
                      22, 10, _customDictionary.FirstOrDefault(x => x.Type == "FinancialYear").Id);

                //November 11
                await AddMonth((CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                      "November", new DateTime(DateTime.Now.Year, 11, 1), new DateTime(DateTime.Now.Year, 11, 30),
                      22, 11, _customDictionary.FirstOrDefault(x => x.Type == "FinancialYear").Id);

                //December 12
                await AddMonth((CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                      "December", new DateTime(DateTime.Now.Year, 12, 1), new DateTime(DateTime.Now.Year, 12, 31),
                      22, 12, _customDictionary.FirstOrDefault(x => x.Type == "FinancialYear").Id);

                _logger.LogInformation($"AddDefaultMonthCycles rule added for {CompanyId}");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while AddDefaultMonthCycles adding for new company {CompanyId}");
            }
            return true;
        }

        public async Task<bool> AddDefaultShiftGroup(Guid? CompanyId)
        {
            _logger.LogInformation($"AddDefaultShiftGroup method called for {CompanyId}");
            try
            {
                var processingCommand = new ShiftGroup.ShiftGroupCommands.V1.CreateShiftGroup
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    ShiftGroupName = "Group-1",
                    ShiftGroupNameLC = "",
                };
                var response = await _mediator.Send(processingCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.ShiftGroupName,
                    Id = response.Id,
                    Type = "ShiftGroup"
                });
                processingCommand = new ShiftGroup.ShiftGroupCommands.V1.CreateShiftGroup
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    ShiftGroupName = "Group-2",
                    ShiftGroupNameLC = "",
                };
                response = await _mediator.Send(processingCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.ShiftGroupName,
                    Id = response.Id,
                    Type = "ShiftGroup"
                });
                processingCommand = new ShiftGroup.ShiftGroupCommands.V1.CreateShiftGroup
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    ShiftGroupName = "Group-3",
                    ShiftGroupNameLC = "",
                };
                response = await _mediator.Send(processingCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.ShiftGroupName,
                    Id = response.Id,
                    Type = "ShiftGroup"
                });
                _logger.LogInformation($"AddDefaultShiftGroup rule added for {CompanyId}");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while AddDefaultFinancialYear adding for new company {CompanyId}");
            }
            return true;
        }

        public async Task<bool> AddDefaultShift(Guid? CompanyId)
        {
            _logger.LogInformation($"AddDefaultShift method called for {CompanyId}");
            try
            {
                var processingCommand = new Shift.ShiftCommands.V1.ShiftConfiguration
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    ShiftName = "General Shift",
                    ShiftCode = "GS",
                    ShiftGroupID = _customDictionary.FirstOrDefault(x => x.Type == "ShiftGroup" && x.Name == "Group-1").Id,
                    ShiftIn = DateTime.ParseExact("9:00", "H:mm", null, System.Globalization.DateTimeStyles.None),
                    ShiftLate = DateTime.ParseExact("9:15", "H:mm", null, System.Globalization.DateTimeStyles.None),
                    ShiftOut = DateTime.ParseExact("17:00", "H:mm", null, System.Globalization.DateTimeStyles.None),
                    EarlyOut = DateTime.ParseExact("16:00", "H:mm", null, System.Globalization.DateTimeStyles.None),
                    LunchBreakOut = DateTime.ParseExact("13:00", "H:mm", null, System.Globalization.DateTimeStyles.None),
                    LunchBreakIn = DateTime.ParseExact("14:00", "H:mm", null, System.Globalization.DateTimeStyles.None),
                    RegHour = DateTime.ParseExact("8:00", "H:mm", null, System.Globalization.DateTimeStyles.None),
                    RamadanIn = DateTime.ParseExact("9:00", "H:mm", null, System.Globalization.DateTimeStyles.None),

                    RamadanOut = DateTime.ParseExact("17:00", "H:mm", null, System.Globalization.DateTimeStyles.None),

                    RamadanLate = DateTime.ParseExact("9:30", "H:mm", null, System.Globalization.DateTimeStyles.None),
                    RamadanEarlyOut = DateTime.ParseExact("15:00", "H:mm", null, System.Globalization.DateTimeStyles.None),
                    RamadanLunchBreakOut = DateTime.ParseExact("13:00", "H:mm", null, System.Globalization.DateTimeStyles.None),
                    RamadanLunchBreakIn = DateTime.ParseExact("13:30", "H:mm", null, System.Globalization.DateTimeStyles.None),

                    StartTime = "7:01",
                    EndTime = "6:59",
                    IsActive = true,
                };
                var response = await _mediator.Send(processingCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.ShiftName,
                    Id = response.Id,
                    Type = "Shift"
                });
                _logger.LogInformation($"AddDefaultShift rule added for {CompanyId}");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while AddDefaultShift adding for new company {CompanyId}");
            }
            return true;
        }

        public async Task<bool> AddDefaultHoliday(Guid? CompanyId)
        {
            _logger.LogInformation($"AddDefaultHoliday method called for {CompanyId}");
            try
            {
                var processingCommand = new Holiday.Commands.V1.CreateHoliday
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    Reason = "Shaheed Day",
                    HolidayDate = new DateTime(DateTime.Now.Year, 2, 21),
                    ReasonLocalized = "",
                };
                var response = await _mediator.Send(processingCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.Reason,
                    Id = response.Id,
                    Type = "Holiday"
                });

                processingCommand = new Holiday.Commands.V1.CreateHoliday
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    Reason = "Independence Day Bangladesh",
                    HolidayDate = new DateTime(DateTime.Now.Year, 3, 26),
                    ReasonLocalized = "",
                };
                response = await _mediator.Send(processingCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.Reason,
                    Id = response.Id,
                    Type = "Holiday"
                });


                processingCommand = new Holiday.Commands.V1.CreateHoliday
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    Reason = "Bengali New Year",
                    HolidayDate = new DateTime(DateTime.Now.Year, 4, 14),
                    ReasonLocalized = "",
                };
                response = await _mediator.Send(processingCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.Reason,
                    Id = response.Id,
                    Type = "Holiday"
                });

                processingCommand = new Holiday.Commands.V1.CreateHoliday
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    Reason = "Labour Day",
                    HolidayDate = new DateTime(DateTime.Now.Year, 5, 1),
                    ReasonLocalized = "",
                };
                response = await _mediator.Send(processingCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.Reason,
                    Id = response.Id,
                    Type = "Holiday"
                });

                processingCommand = new Holiday.Commands.V1.CreateHoliday
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    Reason = "Victory Day Bangladesh",
                    HolidayDate = new DateTime(DateTime.Now.Year, 12, 16),
                    ReasonLocalized = "",
                };
                response = await _mediator.Send(processingCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.Reason,
                    Id = response.Id,
                    Type = "Holiday"
                });

                processingCommand = new Holiday.Commands.V1.CreateHoliday
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    Reason = "Christmas Day",
                    HolidayDate = new DateTime(DateTime.Now.Year, 12, 25),
                    ReasonLocalized = "",
                };
                response = await _mediator.Send(processingCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.Reason,
                    Id = response.Id,
                    Type = "Holiday"
                });

                _logger.LogInformation($"AddDefaultHoliday rule added for {CompanyId}");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while AddDefaultHoliday adding for new company {CompanyId}");
            }
            return true;
        }

        // Leave Type
        public async Task<bool> AddDefaultLeaveTypeGroup(Guid? CompanyId)
        {
            _logger.LogInformation($"AddDefaultLeaveTypeGroup method called for {CompanyId}");
            try
            {
                var processingCommand = new LeaveTypeGroup.Commands.V1.CreateLeaveTypeGroup
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    LeaveTypeGroupName = "Leave Group All",
                    LeaveTypeGroupNameLC = "",
                    IsDeleted = false
                };
                var response = await _mediator.Send(processingCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.LeaveTypeGroupName,
                    IntId = response.Id,
                    Type = "LeaveTypeGroup"
                });
            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while AddDefaultLeaveTypeGroup adding for new company {CompanyId}");
            }
            return true;
        }
        // Leave Type
        public async Task<bool> AddDefaultLeaveType(Guid? CompanyId)
        {
            _logger.LogInformation($"AddDefaultLeaveType method called for {CompanyId}");
            try
            {
                var processingCommand = new LeaveType.LeaveTypeCommands.V1.CreateLeaveType
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    LeaveTypeName = "Casual Leave",
                    LeaveTypeShortName = "CL",
                    LeaveTypeLocalizedName = "",
                    Balance = 10,
                    LeaveTypeGroupId = _customDictionary.FirstOrDefault(x => x.Type == "LeaveTypeGroup" && x.Name == "Leave Group All").IntId
                };
                var response = await _mediator.Send(processingCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.LeaveTypeShortName,
                    Id = response.Id,
                    Type = "LeaveType"
                });

                processingCommand = new LeaveType.LeaveTypeCommands.V1.CreateLeaveType
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    LeaveTypeName = "Sick Leave",
                    LeaveTypeShortName = "SL",
                    LeaveTypeLocalizedName = "",
                    Balance = 14,
                    LeaveTypeGroupId = _customDictionary.FirstOrDefault(x => x.Type == "LeaveTypeGroup" && x.Name == "Leave Group All").IntId
                };
                response = await _mediator.Send(processingCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.LeaveTypeShortName,
                    Id = response.Id,
                    Type = "LeaveType"
                });

                processingCommand = new LeaveType.LeaveTypeCommands.V1.CreateLeaveType
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    LeaveTypeName = "Maternity Leave",
                    LeaveTypeShortName = "ML",
                    LeaveTypeLocalizedName = "",
                    Balance = 90,
                    LeaveTypeGroupId = _customDictionary.FirstOrDefault(x => x.Type == "LeaveTypeGroup" && x.Name == "Leave Group All").IntId
                };
                response = await _mediator.Send(processingCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.LeaveTypeShortName,
                    Id = response.Id,
                    Type = "LeaveType"
                });

                processingCommand = new LeaveType.LeaveTypeCommands.V1.CreateLeaveType
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    LeaveTypeName = "Earn Leave",
                    LeaveTypeShortName = "EL",
                    LeaveTypeLocalizedName = "",
                    Balance = 18,
                    LeaveTypeGroupId = _customDictionary.FirstOrDefault(x => x.Type == "LeaveTypeGroup" && x.Name == "Leave Group All").IntId
                };
                response = await _mediator.Send(processingCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.LeaveTypeShortName,
                    Id = response.Id,
                    Type = "LeaveType"
                });

                _logger.LogInformation($"AddDefaultLeaveType rule added for {CompanyId}");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while AddDefaultLeaveType adding for new company {CompanyId}");
            }
            return true;
        }

        //employee

        // Leave Type

        public async Task<bool> AddDefaultEmployees(Guid? CompanyId)
        {
            _logger.LogInformation($"AddDefaultEmployees method called for {CompanyId}");
            try
            {
                var processingCommand = new EmployeeInfo.Commands.V1.CreateEmployee
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    FullName = "Mike Harison",
                    EmployeeId = "10001",
                    DateOfBirth = new DateTime(1980, 12, 26),
                    PositionId = _customDictionary.FirstOrDefault(x => x.Type == "Designation" && x.Name == "Assistant Manager").Id,
                    DepartmentId = _customDictionary.FirstOrDefault(x => x.Type == "Department" && x.Name == "Human Resource").Id,
                    BranchId = _customDictionary.FirstOrDefault(x => x.Type == "Branch" && x.Name == "Head Office").Id,
                    NationalityId = new Guid("caa67d50-e1e7-4d9b-b772-522f0683a90d"),
                    GenderId = new Guid("f914e6ae-59a5-439e-bd4f-351a025e439d"),
                    UserId = "mikeharison@gma.com",
                    Password = "Mike1234$",

                };
                var response = await _mediator.Send(processingCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.FullName,
                    Id = response.Id,
                    Type = "Employee"
                });
                // enrollment
                var enrollmentCommand = new EmployeeEnrol.Commands.V1.CreateEmployeeEnrollment
                {
                    EmployeeId = response.Id,
                    JoiningDate = DateTime.Now.AddDays(-27),
                    StatusId = 1
                };
                var responseEnroll = await _mediator.Send(enrollmentCommand).ConfigureAwait(false);


                processingCommand = new EmployeeInfo.Commands.V1.CreateEmployee
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    FullName = "Raina Morshed",
                    EmployeeId = "10002",
                    DateOfBirth = new DateTime(1989, 3, 17),
                    PositionId = _customDictionary.FirstOrDefault(x => x.Type == "Designation" && x.Name == "Manager").Id,
                    DepartmentId = _customDictionary.FirstOrDefault(x => x.Type == "Department" && x.Name == "Accounts").Id,
                    BranchId = _customDictionary.FirstOrDefault(x => x.Type == "Branch" && x.Name == "Head Office").Id,
                    NationalityId = new Guid("fdbab0c2-12ed-6261-4de6-a4fd354ab45c"),
                    GenderId = new Guid("eb1a01e3-7030-4904-97f0-3ec1da826c10"),
                    UserId = "rainamorshed@gma.com",
                    Password = "Raina1234$",

                };
                response = await _mediator.Send(processingCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.FullName,
                    Id = response.Id,
                    Type = "Employee"
                });

                // enrollment
                enrollmentCommand = new EmployeeEnrol.Commands.V1.CreateEmployeeEnrollment
                {
                    EmployeeId = response.Id,
                    JoiningDate = DateTime.Now.AddDays(-1),
                    StatusId = 1
                };
                responseEnroll = await _mediator.Send(enrollmentCommand).ConfigureAwait(false);

                processingCommand = new EmployeeInfo.Commands.V1.CreateEmployee
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    FullName = "Rifath Al Sadman",
                    EmployeeId = "10003",
                    DateOfBirth = new DateTime(1983, 11, 23),
                    PositionId = _customDictionary.FirstOrDefault(x => x.Type == "Designation" && x.Name == "Director").Id,
                    DepartmentId = _customDictionary.FirstOrDefault(x => x.Type == "Department" && x.Name == "Management").Id,
                    BranchId = _customDictionary.FirstOrDefault(x => x.Type == "Branch" && x.Name == "Head Office").Id,
                    NationalityId = new Guid("f1bdb38b-8af2-df0d-32d5-048303af5584"),
                    GenderId = new Guid("f914e6ae-59a5-439e-bd4f-351a025e439d"),
                    UserId = "rifathalsadman@gma.com",
                    Password = "Rifat1234$",

                };
                response = await _mediator.Send(processingCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.FullName,
                    Id = response.Id,
                    Type = "Employee"
                });

                // enrollment
                enrollmentCommand = new EmployeeEnrol.Commands.V1.CreateEmployeeEnrollment
                {
                    EmployeeId = response.Id,
                    JoiningDate = DateTime.Now.AddDays(-16),
                    StatusId = 1
                };
                responseEnroll = await _mediator.Send(enrollmentCommand).ConfigureAwait(false);

                //Managing Director


                var user = await _accDBContext.Users.FirstOrDefaultAsync(x => x.CompanyId == CompanyId).ConfigureAwait(false);
                if (user != null && !string.IsNullOrEmpty(user.FullName))
                {
                    processingCommand = new EmployeeInfo.Commands.V1.CreateEmployee
                    {
                        CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                        FullName = user?.FullName,
                        EmployeeId = "10004",
                        DateOfBirth = new DateTime(1983, 11, 23),
                        PositionId = _customDictionary.FirstOrDefault(x => x.Type == "Designation" && x.Name == "Managing Director").Id,
                        DepartmentId = _customDictionary.FirstOrDefault(x => x.Type == "Department" && x.Name == "Management").Id,
                        BranchId = _customDictionary.FirstOrDefault(x => x.Type == "Branch" && x.Name == "Head Office").Id,
                        NationalityId = new Guid("f1bdb38b-8af2-df0d-32d5-048303af5584"),
                        GenderId = new Guid("f914e6ae-59a5-439e-bd4f-351a025e439d"),
                    };
                    response = await _mediator.Send(processingCommand).ConfigureAwait(false);

                    var role = await _userService.AssignRole(user.Id, new string[] { AuthorizationConstants.Roles.ADMINISTRATORS,
                                                                                        AuthorizationConstants.Roles.ADMINISTRATOR,
                                                                                        AuthorizationConstants.Roles.EMPLOYEE,
                                                                                        AuthorizationConstants.Roles.EMPLOYEEROLE,
                                                                                        AuthorizationConstants.Roles.HRMANAGER,
                                                                                        AuthorizationConstants.Roles.PAYROLLMANAGER,
                                                                                        AuthorizationConstants.Roles.SUPERADMIN,
                                                                                        AuthorizationConstants.Roles.REPORTINGMANAGER});

                    var re = await _mediator.Send(new EmployeeInfo.Commands.V1.UpdateLoginIdEmployee
                    {
                        Id = response.Id,
                        LoginId = new Guid(user.Id)
                    }).ConfigureAwait(false);

                    _customDictionary.Add(new CustomDictionary()
                    {
                        Name = processingCommand.FullName,
                        Id = response.Id,
                        Type = "Employee"
                    });

                    // enrollment
                    enrollmentCommand = new EmployeeEnrol.Commands.V1.CreateEmployeeEnrollment
                    {
                        EmployeeId = response.Id,
                        JoiningDate = DateTime.Now.AddDays(-16),
                        StatusId = 1
                    };
                    responseEnroll = await _mediator.Send(enrollmentCommand).ConfigureAwait(false);
                }
                _logger.LogInformation($"AddDefaultEmployees rule added for {CompanyId}");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while AddDefaultEmployees adding for new company {CompanyId}");
            }
            return true;
        }

        public async Task<bool> AddDefaultLeaveProcess(Guid? CompanyId)
        {
            _logger.LogInformation($"AddDefaultLeaveProcess method called for {CompanyId}");
            try
            {
                var processingCommand = new LeaveBalanceProcess.LeaveBalanceCommands.V1.ProcessLeaveBalanceCommand
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    LeaveCalendarId = _customDictionary.FirstOrDefault(x => x.Type == "FinancialYear" && x.Name == DateTime.Now.Year.ToString()).Id,
                };
                var response = await _mediator.Send(processingCommand).ConfigureAwait(false);
                //_customDictionary.Add(new CustomDictionary()
                //{
                //    Name = processingCommand.FinancialYearName,
                //    Id = response.Id,
                //    Type = "FinancialYear"
                //});
                _logger.LogInformation($"AddDefaultLeaveProcess rule added for {CompanyId}");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while AddDefaultLeaveProcess adding for new company {CompanyId}");
            }
            return true;
        }
        private async Task AddMonth(Guid companyId, string monthName, DateTime startDate, DateTime endDate, int totaWorkingDay, int sortOrder,
            Guid financialId)
        {
            var processingCommand = new MonthCycle.MonthCycleCommands.CreateMonthCycle
            {
                CompanyId = companyId,
                MonthCycleName = monthName,
                MonthCycleLocalizedName = "",
                MonthStartDate = startDate,
                MonthEndDate = endDate,
                TotalWorkingDays = totaWorkingDay,
                FinancialYearId = financialId,// _customDictionary.FirstOrDefault(x => x.Type == "FinancialYear").Id,
                IsSelected = monthName == DateTime.Now.ToString("MMMM"),
                RunningFlag = monthName == DateTime.Now.ToString("MMMM"),
                SortOrder = sortOrder,
            };
            var response = await _mediator.Send(processingCommand).ConfigureAwait(false);
            _customDictionary.Add(new CustomDictionary()
            {
                Name = processingCommand.MonthCycleName,
                Id = response.Id,
                Type = "MonthCycle"
            });
        }

        // salary 

        public async Task<bool> AddDefaultSalaryStructure(Guid? CompanyId)
        {
            _logger.LogInformation($"AddDefaultSalaryStructure method called for {CompanyId}");
            try
            {
                var processingCommand = new SalaryStructure.Commands.V1.CreateSalaryStructure
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    StructureName = $"SalaryStructure-{DateTime.Now.Year.ToString()}",
                    StartDate = new DateTime(DateTime.Now.Year, 1, 1),
                    EndDate = new DateTime(DateTime.Now.Year, 12, 31),
                    IsDeleted = false,
                };
                var response = await _mediator.Send(processingCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.StructureName,
                    Id = response.Id,
                    Type = "SalaryStructure"
                });
                _logger.LogInformation($"AddDefaultSalaryStructure rule added for {CompanyId}");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while AddDefaultSalaryStructure adding for new company {CompanyId}");
            }
            return true;
        }

        public async Task<bool> AddDefaultSalaryStructureComponent(Guid? CompanyId)
        {
            _logger.LogInformation($"AddDefaultSalaryStructureComponent method called for {CompanyId}");
            try
            {
                var processingCommand = new SalaryStructureComponent.Commands.V1.CreateSalaryStructureComponent
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    SalaryStrutureId = _customDictionary.FirstOrDefault(x => x.Type == "SalaryStructure" && x.Name == $"SalaryStructure-{DateTime.Now.Year.ToString()}").Id,
                    PercentOn = "1", //                   PercentOnTypes.Gross
                    SalaryComponentName = "Basic",
                    ValueType = "2",//                    ValueTypes.Percent
                    Value = 60,
                    IsDeleted = false,
                    SortOrder = 1
                };
                var response = await _mediator.Send(processingCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = processingCommand.SalaryComponentName,
                    Id = response.Id,
                    Type = "SalaryStructureComponent"
                });

                var houseCommand = new SalaryStructureComponent.Commands.V1.CreateSalaryStructureComponent
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    SalaryStrutureId = _customDictionary.FirstOrDefault(x => x.Type == "SalaryStructure" && x.Name == $"SalaryStructure-{DateTime.Now.Year.ToString()}").Id,
                    PercentOn = "1", //                   PercentOnTypes.Gross
                    SalaryComponentName = "House Rent",
                    ValueType = "2",//                    ValueTypes.Percent
                    Value = 25,
                    IsDeleted = false,
                    SortOrder = 2
                };
                var houseRent = await _mediator.Send(houseCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = houseCommand.SalaryComponentName,
                    Id = houseRent.Id,
                    Type = "SalaryStructureComponent"
                });


                //Medical

                var medicalCommand = new SalaryStructureComponent.Commands.V1.CreateSalaryStructureComponent
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    SalaryStrutureId = _customDictionary.FirstOrDefault(x => x.Type == "SalaryStructure" && x.Name == $"SalaryStructure-{DateTime.Now.Year.ToString()}").Id,
                    PercentOn = "1", //                   PercentOnTypes.Gross
                    SalaryComponentName = "Medical",
                    ValueType = "2",//                    ValueTypes.Percent
                    Value = 10,
                    IsDeleted = false,
                    SortOrder = 3
                };
                var medical = await _mediator.Send(medicalCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = medicalCommand.SalaryComponentName,
                    Id = medical.Id,
                    Type = "SalaryStructureComponent"
                });

                //Conveyance

                var conveyanceCommand = new SalaryStructureComponent.Commands.V1.CreateSalaryStructureComponent
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    SalaryStrutureId = _customDictionary.FirstOrDefault(x => x.Type == "SalaryStructure" && x.Name == $"SalaryStructure-{DateTime.Now.Year.ToString()}").Id,
                    PercentOn = "1", //                   PercentOnTypes.Gross
                    SalaryComponentName = "Conveyance",
                    ValueType = "2",//                    ValueTypes.Percent
                    Value = 5,
                    IsDeleted = false,
                    SortOrder = 4
                };
                var conveyance = await _mediator.Send(conveyanceCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = conveyanceCommand.SalaryComponentName,
                    Id = conveyance.Id,
                    Type = "SalaryStructureComponent"
                });

                //Mobile

                var mobCommand = new SalaryStructureComponent.Commands.V1.CreateSalaryStructureComponent
                {
                    CompanyId = (CompanyId != null && CompanyId != Guid.Empty) ? CompanyId.Value : Guid.Empty,
                    SalaryStrutureId = _customDictionary.FirstOrDefault(x => x.Type == "SalaryStructure" && x.Name == $"SalaryStructure-{DateTime.Now.Year.ToString()}").Id,
                    //PercentOn = "1", //                   PercentOnTypes.Gross
                    SalaryComponentName = "Mobile Bill",
                    ValueType = "1",//                    ValueTypes.Percent
                    Value = 500,
                    IsDeleted = false,
                    SortOrder = 5
                };
                var mob = await _mediator.Send(mobCommand).ConfigureAwait(false);
                _customDictionary.Add(new CustomDictionary()
                {
                    Name = mobCommand.SalaryComponentName,
                    Id = mob.Id,
                    Type = "SalaryStructureComponent"
                });
                _logger.LogInformation($"AddDefaultSalaryStructureComponent rule added for {CompanyId}");

            }
            catch (Exception ex)
            {
                _logger.LogError($"Exception while AddDefaultSalaryStructureComponent adding for new company {CompanyId}");
            }
            return true;
        }
    }
}

