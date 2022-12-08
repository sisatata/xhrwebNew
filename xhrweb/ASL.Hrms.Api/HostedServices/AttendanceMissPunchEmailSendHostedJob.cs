using ASL.Utility.EmailManager.Interfaces;
using ASL.Utility.EmailManager.Models;
using Attendance.Application.AttendanceProcessedData.Queries.Models;
using AutoMapper.Configuration;
using LeaveManagement.Application.LeaveApplications.Queries.Models;
using MediatR;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using static Attendance.Application.AttendanceProcessedData.Queries.Queries;
using static LeaveManagement.Application.LeaveApplications.Queries.Queries;

namespace ASL.Hrms.Api.HostedServices
{
    public class AttendanceMissPunchEmailSendHostedJob : CronJobService
    {
        private readonly ILogger<AttendanceMissPunchEmailSendHostedJob> _logger;
        private readonly IConfiguration _configuration;
        private readonly IMediator _mediator;
        private readonly IEmailSender _emailSender;
        private readonly IOptions<MailServerSettings> _mailServerSettings;
        public AttendanceMissPunchEmailSendHostedJob(IScheduleConfig<AttendanceMissPunchEmailSendHostedJob> config,
            ILogger<AttendanceMissPunchEmailSendHostedJob> logger,
            //IConfiguration configuration,
            IMediator mediator,
            IEmailSender emailSender,
            IOptions<MailServerSettings> mailServerSettings
            )
            : base(config.CronExpression, TimeZoneInfo.Local)
        {
            //_configuration = configuration;
            _mediator = mediator;
            _logger = logger;
            _emailSender = emailSender;
            _mailServerSettings = mailServerSettings;
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("AttendanceMissPunchEmailSendHostedJob starts.");
            return base.StartAsync(cancellationToken);
        }

        public override async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation($"{DateTime.Now:hh:mm:ss} AttendanceMissPunchEmailSendHostedJob is working.");

            //_logger.LogInformation($"AttendanceMissPunchEmailSendJob called {scheduledTime}");
            var attendanceMissPunchEmailSendToAdmin = new AttendanceMissPunchEmailSendToAdmin();
            //{
            //    CompanyId = new Guid("ab5aeca2-7a4a-4a20-bb96-383e72e839dc"),
            //    EmailSendingDate = DateTime.Now.Date
            //};
            var data = await _mediator.Send(attendanceMissPunchEmailSendToAdmin);
            _logger.LogInformation($"attendanceMissPunchEmailSendToAdmin called {data.Count}");
            var leaveQuery = new GetLeaveDataToSendEmail();

            var leaveData = await _mediator.Send(leaveQuery);
            _logger.LogInformation($"GetLeaveDataToSendEmail called {leaveData.Count}");

            if (data != null && data.Count > 0)
            {
                foreach (var item in data)
                {

                    if (item.InTime.ToLower() == "missed")
                    {
                        Thread.Sleep(900);
                        SendClockInMissedEmailToEmployee(item);
                    }
                    if (item.OutTime.ToLower() == "missed")
                    {
                        Thread.Sleep(900);
                        SendClockOutMissedEmailToEmployee(item);
                    }
                }

                // send to manager
                var groups = data.GroupBy(x => x.ManagerEmail)
                    .Select(grop => new { GroupId = grop.Key, Datas = grop.ToList() })
                    .ToList();
                _logger.LogInformation($"grouping done");


                foreach (var group in groups)
                {
                    Thread.Sleep(900);
                    var leaveDataByManager = leaveData.FindAll(x => x.ManagerEmail == group.Datas[0].ManagerEmail);
                    SendEmailToAdmin(group.Datas, leaveDataByManager);
                }

            }

            await Task.CompletedTask;
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("AttendanceMissPunchEmailSendHostedJob is stopping.");
            return base.StopAsync(cancellationToken);
        }

        private async void SendClockInMissedEmailToEmployee(MissPunchAttendanceDataVM command)
        {
            try
            {
                StringBuilder sbMailBody = new StringBuilder();

                sbMailBody.AppendLine("<html>");
                sbMailBody.AppendLine("<body>");
                sbMailBody.AppendFormat("<p>Dear {0},</p>", command.FullName);
                sbMailBody.AppendFormat("<p>You have missed <b>Clock In</b> on dated <b>{0}</b>.</p>", command.Date);
                sbMailBody.AppendLine("Please submit your <b>Clock In</b> request for admin approval.");
                sbMailBody.AppendLine("<br /><br />");
                sbMailBody.AppendLine("<p>Thank you!</p>");
                sbMailBody.AppendLine("<p style=\"color:Green;\"><b>planetHR</b></p>");
                sbMailBody.AppendLine("</body>");
                sbMailBody.AppendLine("</html>");

                await _emailSender.SendMail(new EmailModel
                {
                    Subject = $"planetHR > You have missed Clock In on dated {command.Date}",
                    Body = sbMailBody.ToString(),
                    AllowHtml = true,
                    ReceiverMailIds = new List<string> { command.EmployeeEmail } //{ "azhar@aplectrum.com" }//
                }, _mailServerSettings.Value);
            }
            catch (Exception ex)
            {
                _logger.LogError($"SendClockInMissedEmailToEmployee, user retrieval error:  {ex.Message}");
                throw;
            }
        }

        private async void SendClockOutMissedEmailToEmployee(MissPunchAttendanceDataVM command)
        {
            try
            {


                StringBuilder sbMailBody = new StringBuilder();

                sbMailBody.AppendLine("<html>");
                sbMailBody.AppendLine("<body>");
                sbMailBody.AppendFormat("<p>Dear {0},</p>", command.FullName);
                sbMailBody.AppendFormat("<p>You have missed <b>Clock Out</b> on dated <b>{0}</b>.</p>", command.Date);
                sbMailBody.AppendLine("Please submit your <b>Clock Out</b> request for admin approval.");
                sbMailBody.AppendLine("<br /><br />");
                sbMailBody.AppendLine("<p>Thank you!</p>");
                sbMailBody.AppendLine("<p style=\"color:Green;\"><b>planetHR</b></p>");
                sbMailBody.AppendLine("</body>");
                sbMailBody.AppendLine("</html>");

                await _emailSender.SendMail(new EmailModel
                {
                    Subject = $"planetHR > You have missed Clock Out on dated {command.Date}",
                    Body = sbMailBody.ToString(),
                    AllowHtml = true,
                    ReceiverMailIds = new List<string> { command.EmployeeEmail } //{ "azhar@aplectrum.com" }//
                }, _mailServerSettings.Value);
            }
            catch (Exception ex)
            {
                _logger.LogError($"SendClockOutMissedEmailToEmployee, user retrieval error:  {ex.Message}");
                throw;
            }
        }

        private async void SendEmailToAdmin(List<MissPunchAttendanceDataVM> command, List<LeaveDataForEmailVM> leaveData)
        {
            try
            {
                StringBuilder sbMailBody = new StringBuilder();

                sbMailBody.AppendLine("<html>");

                sbMailBody.AppendLine("<head>");
                sbMailBody.AppendLine("<style>");
                sbMailBody.AppendLine("#customers {");
                //sbMailBody.AppendLine(" font-family:\"Trebuchet MS\", Arial, Helvetica, sans-serif;");
                sbMailBody.AppendLine(" border-collapse: collapse;");
                sbMailBody.AppendLine(" width: 100%;");
                sbMailBody.AppendLine("    }");
                sbMailBody.AppendLine("");
                sbMailBody.AppendLine("#customers td, #customers th {");
                sbMailBody.AppendLine(" border: 1px solid #ddd;");
                sbMailBody.AppendLine(" padding: 8px;");
                sbMailBody.AppendLine("}");
                sbMailBody.AppendLine("#bb {");
                sbMailBody.AppendLine("margin: 30px;");
                sbMailBody.AppendLine("}");

                sbMailBody.AppendLine("");
                sbMailBody.AppendLine("#customers tr:nth-child(even){background-color: #f2f2f2;}");
                sbMailBody.AppendLine("");
                sbMailBody.AppendLine("#customers tr:hover {background-color: #ddd;}");
                sbMailBody.AppendLine("");
                sbMailBody.AppendLine("#customers th {");
                sbMailBody.AppendLine("  padding-top: 12px;");
                sbMailBody.AppendLine("  padding-bottom: 12px;");
                sbMailBody.AppendLine("  text-align: left;");
                sbMailBody.AppendLine("  background-color: #4CAF50;");
                sbMailBody.AppendLine("  color: white;");
                sbMailBody.AppendLine("}");
                sbMailBody.AppendLine("</style>");
                sbMailBody.AppendLine("</head>");
                sbMailBody.AppendLine("<body id =\"bb\">");
                sbMailBody.AppendFormat("<p>Dear Concern,</p>");
                sbMailBody.AppendFormat("<p>Missed punch list on dated {0} are given below:</p>", command[0].Date);
                sbMailBody.AppendLine("<table id=\"customers\">");
                sbMailBody.AppendLine("<tr><th>Employee Id</th> <th>Full Name</th> <th>Designation</th> <th>Department</th> <th>In Time</th> <th>Out Time</th> <th>Shift Code</th> <th>Status</th> </tr>");
                // sbMailBody.AppendLine("<tbody>");
                foreach (var item in command)
                {
                    sbMailBody.AppendLine($"<tr><td>{item.EmployeeId}</td> <td>{item.FullName} </td><td>{item.Designation} </td><td>{item.Department} </td>");
                    if (item.InTime.ToLower() == "missed")
                    {
                        sbMailBody.AppendLine($"<td style=\"color:Tomato;\">{item.InTime }</td>");
                    }
                    else
                    {
                        sbMailBody.AppendLine($"<td>{item.InTime }</td>");
                    }

                    if (item.OutTime.ToLower() == "missed")
                    {
                        sbMailBody.AppendLine($"<td style=\"color:Tomato;\">{item.OutTime }</td>");
                    }
                    else
                    {
                        sbMailBody.AppendLine($"<td>{item.OutTime }</td>");
                    }


                    sbMailBody.AppendLine($"<td>{item.ShiftCode }</td> <td>{item.Status} </td> </tr>");
                }
                //sbMailBody.AppendLine("</tbody>");
                sbMailBody.AppendFormat("</table>");
                sbMailBody.AppendLine("");

                //======================= absent list
                var absentList = command.FindAll(r => r.Status == "A");
                if (absentList.Count() > 0)
                {
                    sbMailBody.AppendFormat("<p><b>Absent List: </b></p>", command[0].Date);
                    sbMailBody.AppendLine("<table id=\"customers\">");
                    sbMailBody.AppendLine("<tr ><th>Employee Id</th> <th>Full Name</th> <th>Designation</th> <th>Department</th> <th>Shift Code</th> <th>Status</th> </tr>");
                    foreach (var item in absentList)
                    {
                        sbMailBody.AppendLine($"<tr><td>{item.EmployeeId}</td> <td>{item.FullName} </td><td>{item.Designation} </td><td>{item.Department} </td>");
                        sbMailBody.AppendLine($"<td>{item.ShiftCode }</td> <td style=\"color:Tomato;\">{item.Status} </td> </tr>");
                    }
                    //sbMailBody.AppendLine("</tbody>");
                    sbMailBody.AppendFormat("</table>");
                    sbMailBody.AppendLine("");
                }
                //====================== End Absent List

                //======================= absent list
                //var absentList = command.FindAll(r => r.Status == "A");
                if (leaveData.Count() > 0)
                {
                    sbMailBody.AppendFormat("<p><b>Leave List:</b></p>", leaveData[0].TodayDate);
                    sbMailBody.AppendLine("<table id=\"customers\">");
                    sbMailBody.AppendLine("<tr style=\"color:MediumSeaGreen;\"><th>Employee Id</th> <th>Full Name</th> <th>Designation</th> <th>Department</th> <th>Leave Type</th> <th>Approval Status</th> <th>Reason</th>  </tr>");
                    foreach (var item in leaveData)
                    {
                        sbMailBody.AppendLine($"<tr><td>{item.EmployeeId}</td> <td>{item.FullName} </td><td>{item.Designation} </td><td>{item.Department} </td>");
                        sbMailBody.AppendLine($"<td>{item.LeaveTypeShortName }</td> <td style=\"color:Tomato;\">{item.ApprovalStatusText} </td><td>{item.Reason }</td> </tr>");
                    }
                    //sbMailBody.AppendLine("</tbody>");
                    sbMailBody.AppendFormat("</table>");
                    sbMailBody.AppendLine("");
                }

                _logger.LogInformation($"mail body done : {sbMailBody.ToString()}");
                _logger.LogInformation($"Manager email : {command[0].ManagerEmail}");
                //====================== End Absent List
                sbMailBody.AppendLine("<p>Thank you</p>");
                sbMailBody.AppendLine("<p style=\"color:Green;\"><b>planetHR</b></p>");
                sbMailBody.AppendLine("</body>");
                sbMailBody.AppendLine("</html>");
                await _emailSender.SendMail(new EmailModel
                {
                    Subject = $"planetHR > Missed punch on dated {command[0].Date}",
                    Body = sbMailBody.ToString(),
                    AllowHtml = true,
                    ReceiverMailIds = new List<string> { command[0].ManagerEmail },// 
                    ReceiverBccs= new List<string>{ "azhar@aplectrum.com" } //
                }, _mailServerSettings.Value);
                _logger.LogInformation($"email sent to : {command[0].ManagerEmail}");
            }
            catch (Exception ex)
            {
                _logger.LogError($"SendEmailToAdmin, user retrieval error:  {ex.Message}");
                throw;
            }
        }
    }
}
