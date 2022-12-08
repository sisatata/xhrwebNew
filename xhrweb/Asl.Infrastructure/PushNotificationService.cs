using Asl.Infrastructure.Interfaces;
using ASL.Hrms.SharedKernel.Interfaces;
using EmployeeEnrollment.Persistence;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
//using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using System;
using System.Linq;
using System.Net.Http;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using CompanySetup.Application.Notification.Commands;
using CompanySetup.Application.Notification.Queries;
using ASL.Hrms.SharedKernel.Enums;
using CompanySetup.Application.Notification.Queries.Models;
using System.Collections.Generic;

namespace Asl.Infrastructure
{
    public class PushNotificationService : IPushNotificationService
    {
        public PushNotificationService(IConfiguration configuration, EmployeeContext dataContext, IMediator mediator
            //,ILogger<PushNotificationService> logger
            )
        {
            _configuration = configuration;
            _dataContext = dataContext;
            _mediator = mediator;
            //_logger = logger;
        }
        private readonly IConfiguration _configuration;
        private readonly EmployeeContext _dataContext;
        private readonly IMediator _mediator;
        //private readonly ILogger<PushNotificationService> _logger;

        public async Task<bool> SendPushNotification(string[] accessTokens, string subject, string details, object data)
        {
            var sendStatus = true;
            var firebaseUrl = _configuration.GetValue<string>("PushNotification:FireBasePushNotificationsURL");
            var serverKey = _configuration.GetValue<string>("PushNotification:ServerKey");
            var senderId = _configuration.GetValue<string>("PushNotification:SenderId");

            if (accessTokens.Any())
            {
                var mobileMessage = new
                {
                    registration_ids = accessTokens,
                    data = new { model = data },
                    //sound = "default",
                    content_available = true,
                    notification = new
                    {
                        title = subject,
                        body = details,
                        sound = "default",
                        //badge = 1,
                        payload = new
                        {
                            CreatedAt = "27-dec-2018",
                            //CreatedAt = "30-jul-2020",
                            body = details,
                            title = subject
                        }
                    }
                    //,
                    //android = new
                    //{
                    //    notification = new { 
                    //    sound = "default"
                    //    }
                    //}
                };

                var jsonMessage = JsonConvert.SerializeObject(mobileMessage, new JsonSerializerSettings { ContractResolver = new CamelCasePropertyNamesContractResolver() });

                var request = new HttpRequestMessage(HttpMethod.Post, firebaseUrl);
                request.Headers.TryAddWithoutValidation("Authorization", $"key={ serverKey }");
                request.Headers.TryAddWithoutValidation("Sender", $"id={senderId}");
                request.Content = new StringContent(jsonMessage, Encoding.UTF8, "application/json");

                HttpResponseMessage result;
                using (var client = new HttpClient())
                {
                    result = await client.SendAsync(request);
                    sendStatus = sendStatus && result.IsSuccessStatusCode;
                }
            }
            return sendStatus;
        }

        public async Task<bool> SendLeaveApplyNotification(Guid ManagerId, Guid EmployeeId, double NoOfDays, Guid ApplicationId,
            DateTime StartDate, DateTime EndDate, string Reason, Boolean IsHalfDayLeave)
        {
            var result = false;
            var EmployeeName = "";
            //if (!string.IsNullOrEmpty(EmployeeId))
            //{
            try
            {
                var employee = await (from e in _dataContext.Employees.AsNoTracking()
                                      where e.Id == EmployeeId
                                      select e).FirstOrDefaultAsync();
                if (employee != null)
                {
                    EmployeeName = employee.FullName;
                }

                var tokens = await (from ed in _dataContext.EmployeeDevices.AsNoTracking()
                                        //join em in _dataContext.EmployeeManagers.AsNoTracking()
                                        //    on ed.EmployeeId equals em.ManagerId
                                    where ed.EmployeeId == ManagerId
                                    select new { ed.AccessToken }).ToListAsync();

                //var consumerIds = tokens.Select(c => c.ConsumerId).ToArray();
                var deviceTokens = tokens.Select(c => c.AccessToken).Distinct().ToArray();
                string message = $"{EmployeeName} has applied for {NoOfDays} days leave";
                string messageBody = $"{EmployeeName} has applied for {NoOfDays} days leave from {StartDate.ToString("dd/MM/yyyy")} to " +
                    $"{EndDate.ToString("dd/MM/yyyy")}. Reason is {Reason}";
                var dataObject = new { message = $"{EmployeeName} has applied for {NoOfDays} days leave", ManagerId, EmployeeName, NoOfDays, ApplicationId };

                await SaveNotification(ApplicationId, EmployeeId, ManagerId, message, messageBody, (short)NotificationTypes.Leave, ManagerId);

                var sendStatus = await SendPushNotification(deviceTokens, message, messageBody, dataObject);

                result = true;
            }
            catch (Exception ex)
            {
                //_logger.LogInformation($"Logging from service {nameof(PushNotificationService)}");
                //_logger.LogError($"Notification send fail: {ex.Message}");
            }
            return result;
        }

        public async Task<bool> SendLeaveApproveNotification(Guid ManagerId, Guid EmployeeId, double NoOfDays, Guid ApplicationId,
            DateTime StartDate, DateTime EndDate, string Note, Boolean IsHalfDayLeave)
        {
            var result = false;
            var EmployeeName = "";
            //if (!string.IsNullOrEmpty(EmployeeId))
            //{
            try
            {
                var employee = await (from e in _dataContext.Employees.AsNoTracking()
                                      where e.Id == EmployeeId
                                      select e).FirstOrDefaultAsync();
                if (employee != null)
                {
                    EmployeeName = employee.FullName;
                }
                var tokens = await (from ed in _dataContext.EmployeeDevices.AsNoTracking()
                                    where ed.EmployeeId == EmployeeId
                                    select new { ed.AccessToken }).ToListAsync();

                //var consumerIds = tokens.Select(c => c.ConsumerId).ToArray();
                var deviceTokens = tokens.Select(c => c.AccessToken).Distinct().ToArray();
                string message = $"Your applied leave for {NoOfDays} days has been approved";
                string messageBody = $"Your applied leave for {NoOfDays} days  from {StartDate.ToString("dd/MM/yyyy")} to {EndDate.ToString("dd/MM/yyyy")}" +
                    $" has been approved";
                messageBody += string.IsNullOrEmpty(Note) ? "" : $" and note from manager is {Note}";

                var dataObject = new { message = $"Your applied leave for {NoOfDays} days has been approved", EmployeeId, EmployeeName, NoOfDays, ApplicationId };

                await SaveNotification(ApplicationId, EmployeeId, ManagerId, message, messageBody, (short)NotificationTypes.Leave, EmployeeId);

                var sendStatus = await SendPushNotification(deviceTokens, message, messageBody, dataObject);

                result = true;
            }
            catch (Exception ex)
            {
                //_logger.LogInformation($"Logging from service {nameof(PushNotificationService)}");
                //_logger.LogError($"Notification send fail: {ex.Message}");
            }
            //}
            return result;
        }

        public async Task<bool> SendLeaveDeclineNotification(Guid ManagerId, Guid EmployeeId, double NoOfDays, Guid ApplicationId,
            DateTime StartDate, DateTime EndDate, string Note, Boolean IsHalfDayLeave)
        {
            var result = false;
            var EmployeeName = "";
            //if (!string.IsNullOrEmpty(EmployeeId))
            //{
            try
            {
                var employee = await (from e in _dataContext.Employees.AsNoTracking()
                                      where e.Id == EmployeeId
                                      select e).FirstOrDefaultAsync();
                if (employee != null)
                {
                    EmployeeName = employee.FullName;
                }
                var tokens = await (from ed in _dataContext.EmployeeDevices.AsNoTracking()
                                    where ed.EmployeeId == EmployeeId
                                    select new { ed.AccessToken }).ToListAsync();

                //var consumerIds = tokens.Select(c => c.ConsumerId).ToArray();
                var deviceTokens = tokens.Select(c => c.AccessToken).Distinct().ToArray();
                string message = $"Your applied leave for {NoOfDays} days has been declined";
                string messageBody = $"Your applied leave for {NoOfDays} days from {StartDate.ToString("dd/MM/yyyy")} to {EndDate.ToString("dd/MM/yyyy")}" +
                    $" has been declined";
                messageBody += string.IsNullOrEmpty(Note) ? "" : $" and note from manager is {Note}";
                var dataObject = new { message = $"Your applied leave for {NoOfDays} days has been declined", EmployeeId, EmployeeName, NoOfDays, ApplicationId };

                await SaveNotification(ApplicationId, EmployeeId, ManagerId, message, messageBody, (short)NotificationTypes.Leave, EmployeeId);

                var sendStatus = await SendPushNotification(deviceTokens, message, messageBody, dataObject);

                result = true;
            }
            catch (Exception ex)
            {
                //_logger.LogInformation($"Logging from service {nameof(PushNotificationService)}");
                //_logger.LogError($"Notification send fail: {ex.Message}");
            }
            //}
            return result;
        }

        // Attendance
        public async Task<bool> SendAttendanceApplyNotification(Guid ManagerId, Guid EmployeeId, string RequestType, DateTime StartDate, DateTime EndDate, Guid ApplicationId)
        {
            var result = false;
            var EmployeeName = "";
            //if (!string.IsNullOrEmpty(EmployeeId))
            //{
            try
            {
                var employee = await (from e in _dataContext.Employees.AsNoTracking()
                                      where e.Id == EmployeeId
                                      select e).FirstOrDefaultAsync();
                if (employee != null)
                {
                    EmployeeName = employee.FullName;
                }
                //var tokens = await (from ed in _dataContext.EmployeeDevices.AsNoTracking()
                //                    join em in _dataContext.EmployeeManagers.AsNoTracking()
                //                        on ed.EmployeeId equals em.ManagerId
                //                    where em.EmployeeId == EmployeeId
                //                    select new { ed.AccessToken }).ToListAsync();

                var tokens = await (from ed in _dataContext.EmployeeDevices.AsNoTracking()
                                        //join em in _dataContext.EmployeeManagers.AsNoTracking()
                                        //    on ed.EmployeeId equals em.ManagerId
                                    where ed.EmployeeId == ManagerId
                                    select new { ed.AccessToken }).ToListAsync();

                //var consumerIds = tokens.Select(c => c.ConsumerId).ToArray();
                var deviceTokens = tokens.Select(c => c.AccessToken).Distinct().ToArray();
                string message = $"{EmployeeName} has requested for {RequestType} on {StartDate.ToString("dd/MM/yyyy")}";
                string messageBody = $"{EmployeeName} has requested for {RequestType} on {StartDate.ToString("dd/MM/yyyy")}";
                var dataObject = new { message = $"{EmployeeName} has requested for {RequestType} on {StartDate.ToString("dd/MM/yyyy")}", ManagerId, EmployeeName, RequestType, StartDate, EndDate, ApplicationId };

                await SaveNotification(ApplicationId, EmployeeId, ManagerId, message, messageBody, (short)NotificationTypes.Attendance, ManagerId);

                var sendStatus = await SendPushNotification(deviceTokens, message, messageBody, dataObject);

                result = true;
            }
            catch (Exception ex)
            {
                //_logger.LogInformation($"Logging from service {nameof(PushNotificationService)}");
                //_logger.LogError($"Notification send fail: {ex.Message}");
            }
            return result;
        }

        public async Task<bool> SendAttendanceApproveNotification(Guid ManagerId, Guid EmployeeId, string RequestType, DateTime StartDate, DateTime EndDate, Guid ApplicationId)
        {
            var result = false;
            var EmployeeName = "";
            //if (!string.IsNullOrEmpty(EmployeeId))
            //{
            try
            {
                var employee = await (from e in _dataContext.Employees.AsNoTracking()
                                      where e.Id == EmployeeId
                                      select e).FirstOrDefaultAsync();
                if (employee != null)
                {
                    EmployeeName = employee.FullName;
                }
                var tokens = await (from ed in _dataContext.EmployeeDevices.AsNoTracking()
                                    where ed.EmployeeId == EmployeeId
                                    select new { ed.AccessToken }).ToListAsync();

                //var consumerIds = tokens.Select(c => c.ConsumerId).ToArray();
                var deviceTokens = tokens.Select(c => c.AccessToken).Distinct().ToArray();
                string message = $"Your attendance request on {StartDate.ToString("dd/MM/yyyy")} for {RequestType} has been approved";
                string messageBody = $"Your attendance request on {StartDate.ToString("dd/MM/yyyy")} for {RequestType} has been approved";
                var dataObject = new { message = $"Your attendance request on {StartDate.ToString("dd/MM/yyyy")} for {RequestType} has been approved", ManagerId, EmployeeName, RequestType, StartDate, EndDate, ApplicationId };

                await SaveNotification(ApplicationId, EmployeeId, ManagerId, message, messageBody, (short)NotificationTypes.Attendance, EmployeeId);

                var sendStatus = await SendPushNotification(deviceTokens, message, messageBody, dataObject);

                result = true;
            }
            catch (Exception ex)
            {
                //_logger.LogInformation($"Logging from service {nameof(PushNotificationService)}");
                //_logger.LogError($"Notification send fail: {ex.Message}");
            }
            //}
            return result;
        }

        public async Task<bool> SendAttendanceDeclineNotification(Guid ManagerId, Guid EmployeeId, string RequestType, DateTime StartDate, DateTime EndDate, Guid ApplicationId)
        {
            var result = false;
            var EmployeeName = "";
            //if (!string.IsNullOrEmpty(EmployeeId))
            //{
            try
            {
                var employee = await (from e in _dataContext.Employees.AsNoTracking()
                                      where e.Id == EmployeeId
                                      select e).FirstOrDefaultAsync();
                if (employee != null)
                {
                    EmployeeName = employee.FullName;
                }
                var tokens = await (from ed in _dataContext.EmployeeDevices.AsNoTracking()
                                    where ed.EmployeeId == EmployeeId
                                    select new { ed.AccessToken }).ToListAsync();

                //var consumerIds = tokens.Select(c => c.ConsumerId).ToArray();
                var deviceTokens = tokens.Select(c => c.AccessToken).Distinct().ToArray();
                string message = $"Your attendance request on {StartDate.ToString("dd/MM/yyyy")} for {RequestType} has been declined";
                string messageBody = $"Your attendance request on {StartDate.ToString("dd/MM/yyyy")} for {RequestType} has been declined";
                var dataObject = new { message = $"Your attendance request on {StartDate.ToString("dd/MM/yyyy")} for {RequestType} has been declined", ManagerId, EmployeeName, RequestType, StartDate, EndDate, ApplicationId };

                await SaveNotification(ApplicationId, EmployeeId, ManagerId, message, messageBody, (short)NotificationTypes.Attendance, EmployeeId);

                var sendStatus = await SendPushNotification(deviceTokens, message, messageBody, dataObject);

                result = true;
            }
            catch (Exception ex)
            {
                //_logger.LogInformation($"Logging from service {nameof(PushNotificationService)}");
                //_logger.LogError($"Notification send fail: {ex.Message}");
            }
            //}
            return result;
        }

        // Bill
        public async Task<bool> SendBillApplyNotification(Guid ManagerId, Guid EmployeeId, decimal BillAmount, Guid ApplicationId)
        {
            var result = false;
            var EmployeeName = "";
            //if (!string.IsNullOrEmpty(EmployeeId))
            //{
            try
            {
                var employee = await (from e in _dataContext.Employees.AsNoTracking()
                                      where e.Id == EmployeeId
                                      select e).FirstOrDefaultAsync();
                if (employee != null)
                {
                    EmployeeName = employee.FullName;
                }
                //var tokens = await (from ed in _dataContext.EmployeeDevices.AsNoTracking()
                //                    join em in _dataContext.EmployeeManagers.AsNoTracking()
                //                        on ed.EmployeeId equals em.ManagerId
                //                    where em.EmployeeId == EmployeeId
                //                    select new { ed.AccessToken }).ToListAsync();

                var tokens = await (from ed in _dataContext.EmployeeDevices.AsNoTracking()
                                        //join em in _dataContext.EmployeeManagers.AsNoTracking()
                                        //    on ed.EmployeeId equals em.ManagerId
                                    where ed.EmployeeId == ManagerId
                                    select new { ed.AccessToken }).ToListAsync();

                //var consumerIds = tokens.Select(c => c.ConsumerId).ToArray();
                var deviceTokens = tokens.Select(c => c.AccessToken).Distinct().ToArray();
                string message = $"{EmployeeName} has claimed bill for {BillAmount}";
                string messageBody = $"{EmployeeName} has claimed bill for {BillAmount}";
                var dataObject = new { message = $"{EmployeeName} has claimed bill for {BillAmount}", ManagerId, EmployeeName, BillAmount, ApplicationId };

                await SaveNotification(ApplicationId, EmployeeId, ManagerId, message, messageBody, (short)NotificationTypes.Bill, ManagerId);

                var sendStatus = await SendPushNotification(deviceTokens, message, messageBody, dataObject);

                result = true;
            }
            catch (Exception ex)
            {
                //_logger.LogInformation($"Logging from service {nameof(PushNotificationService)}");
                //_logger.LogError($"Notification send fail: {ex.Message}");
            }
            return result;
        }

        public async Task<bool> SendBillApproveNotification(Guid ManagerId, Guid EmployeeId, decimal BillAmount, decimal ApprovedAmount, Guid ApplicationId)
        {
            var result = false;
            var EmployeeName = "";
            //if (!string.IsNullOrEmpty(EmployeeId))
            //{
            try
            {
                var employee = await (from e in _dataContext.Employees.AsNoTracking()
                                      where e.Id == EmployeeId
                                      select e).FirstOrDefaultAsync();
                if (employee != null)
                {
                    EmployeeName = employee.FullName;
                }
                var tokens = await (from ed in _dataContext.EmployeeDevices.AsNoTracking()
                                    where ed.EmployeeId == EmployeeId
                                    select new { ed.AccessToken }).ToListAsync();

                //var consumerIds = tokens.Select(c => c.ConsumerId).ToArray();
                var deviceTokens = tokens.Select(c => c.AccessToken).Distinct().ToArray();
                string message = "";
                if (BillAmount != ApprovedAmount)
                    message = $"Your claimed bill for {BillAmount} has been approved. Amount {ApprovedAmount}";
                else
                    message = $"Your claimed bill for {BillAmount} has been approved";
                string messageBody = message;
                var dataObject = new { message = message, EmployeeId, EmployeeName, BillAmount, ApprovedAmount, ApplicationId };

                await SaveNotification(ApplicationId, EmployeeId, ManagerId, message, messageBody, (short)NotificationTypes.Bill, EmployeeId);

                var sendStatus = await SendPushNotification(deviceTokens, message, messageBody, dataObject);

                result = true;
            }
            catch (Exception ex)
            {
                //_logger.LogInformation($"Logging from service {nameof(PushNotificationService)}");
                //_logger.LogError($"Notification send fail: {ex.Message}");
            }
            //}
            return result;
        }

        public async Task<bool> SendBillDeclineNotification(Guid ManagerId, Guid EmployeeId, Guid ApplicationId)
        {
            var result = false;
            var EmployeeName = "";
            //if (!string.IsNullOrEmpty(EmployeeId))
            //{
            try
            {
                var employee = await (from e in _dataContext.Employees.AsNoTracking()
                                      where e.Id == EmployeeId
                                      select e).FirstOrDefaultAsync();
                if (employee != null)
                {
                    EmployeeName = employee.FullName;
                }
                var tokens = await (from ed in _dataContext.EmployeeDevices.AsNoTracking()
                                    where ed.EmployeeId == EmployeeId
                                    select new { ed.AccessToken }).ToListAsync();

                //var consumerIds = tokens.Select(c => c.ConsumerId).ToArray();
                var deviceTokens = tokens.Select(c => c.AccessToken).Distinct().ToArray();
                string message = $"Your claimed bill for has been declined";
                string messageBody = $"Your claimed bill for has been declined";
                var dataObject = new { message = $"Your claimed bill for has been declined", EmployeeId, EmployeeName, ApplicationId };

                await SaveNotification(ApplicationId, EmployeeId, ManagerId, message, messageBody, (short)NotificationTypes.Bill, EmployeeId);

                var sendStatus = await SendPushNotification(deviceTokens, message, messageBody, dataObject);

                result = true;
            }
            catch (Exception ex)
            {
                //_logger.LogInformation($"Logging from service {nameof(PushNotificationService)}");
                //_logger.LogError($"Notification send fail: {ex.Message}");
            }
            //}
            return result;
        }

        //---- payment notificaton
        public async Task<bool> SendBillApproveNotificationForPayment(Guid ManagerId, Guid EmployeeId, decimal ApprovedAmount, Guid ApplicationId)
        {
            var result = false;
            var EmployeeName = "";
            //if (!string.IsNullOrEmpty(EmployeeId))
            //{
            try
            {
                var employee = await (from e in _dataContext.Employees.AsNoTracking()
                                      where e.Id == EmployeeId
                                      select e).FirstOrDefaultAsync();
                if (employee != null)
                {
                    EmployeeName = employee.FullName;
                }
                var tokens = await (from ed in _dataContext.EmployeeDevices.AsNoTracking()
                                    where ed.EmployeeId == ManagerId
                                    select new { ed.AccessToken }).ToListAsync();

                //var consumerIds = tokens.Select(c => c.ConsumerId).ToArray();
                var deviceTokens = tokens.Select(c => c.AccessToken).Distinct().ToArray();
                string message = "";

                message = $"A bill of {EmployeeName} amount of {ApprovedAmount} has been approved.";

                string messageBody = $"{message}. Please pay the amount to bill receiver";
                var dataObject = new { message = message, EmployeeId, EmployeeName, ApprovedAmount, ApplicationId };

                await SaveNotification(ApplicationId, EmployeeId, ManagerId, message, messageBody, (short)NotificationTypes.Bill, ManagerId);

                var sendStatus = await SendPushNotification(deviceTokens, message, messageBody, dataObject);

                result = true;
            }
            catch (Exception ex)
            {
                //_logger.LogInformation($"Logging from service {nameof(PushNotificationService)}");
                //_logger.LogError($"Notification send fail: {ex.Message}");
            }
            //}
            return result;
        }

        public async Task<bool> SendBillMarkPaidNotificationForApplicant(Guid ManagerId, Guid EmployeeId, decimal PaidAmount, Guid ApplicationId)
        {
            var result = false;
            var EmployeeName = "";
            var ManagerName = "";
            //if (!string.IsNullOrEmpty(EmployeeId))
            //{
            try
            {

                var manager = await (from e in _dataContext.Employees.AsNoTracking()
                                     where e.Id == ManagerId
                                     select e).FirstOrDefaultAsync();
                if (manager != null)
                {
                    ManagerName = manager.FullName;
                }

                var employee = await (from e in _dataContext.Employees.AsNoTracking()
                                      where e.Id == EmployeeId
                                      select e).FirstOrDefaultAsync();
                if (employee != null)
                {
                    EmployeeName = employee.FullName;
                }
                var tokens = await (from ed in _dataContext.EmployeeDevices.AsNoTracking()
                                    where ed.EmployeeId == EmployeeId
                                    select new { ed.AccessToken }).ToListAsync();

                //var consumerIds = tokens.Select(c => c.ConsumerId).ToArray();
                var deviceTokens = tokens.Select(c => c.AccessToken).Distinct().ToArray();
                string message = "";

                message = $"Your claimed bill for {PaidAmount} has been marked as paid.";

                string messageBody = $"{message}. Please contact {ManagerName}, if you did not receive the bill amount";
                var dataObject = new { message = message, EmployeeId, EmployeeName, PaidAmount, ApplicationId };

                await SaveNotification(ApplicationId, EmployeeId, ManagerId, message, messageBody, (short)NotificationTypes.Bill, EmployeeId);

                var sendStatus = await SendPushNotification(deviceTokens, message, messageBody, dataObject);

                result = true;
            }
            catch (Exception ex)
            {
                //_logger.LogInformation($"Logging from service {nameof(PushNotificationService)}");
                //_logger.LogError($"Notification send fail: {ex.Message}");
            }
            //}
            return result;
        }

        //=====================
        public async Task<bool> SendPushNotificationBulk()
        {
            var result = false;

            try
            {
                var pendingNotifications = await _mediator.Send(new Queries.GetPendingNotificationListToPush());
                if (!pendingNotifications.Any())
                    return false;

                var groups = from c in pendingNotifications
                             group c by new { c.NotificationTitle, c.NotificationDetail } into gcs
                             select new NotificationSummary()
                             {

                                 NotificationTitle = gcs.Key.NotificationTitle,
                                 NotificationDetail = gcs.Key.NotificationDetail,
                                 //DataList = gcs.ToList(),
                                 Tokens = gcs.Select(r => r.NotificationOwnerAccessToken).ToArray()
                             };

                foreach (var item in groups)
                {
                    SendPushNotification(item.Tokens, item.NotificationTitle, item.NotificationDetail, new { message = item.NotificationTitle });
                }

                var markBulkCommand = new Commands.V1.MarkAsPushedNotificationBulk
                {
                    Ids = pendingNotifications.Select(x => x.Id.Value).ToList()
                };
                await _mediator.Send(markBulkCommand);
                result = true;
            }
            catch (Exception ex)
            {

            }
            return result;
        }

        private async Task<Boolean> SaveNotification(Guid applicationId, Guid applicantId, Guid managerId, string title, string body,
            short notificationTypeId, Guid? notificationOwnerId)
        {
            var createNotification = new Commands.V1.CreateNotification
            {
                ApplicationId = applicationId,
                ApplicantId = applicantId,
                ManagerId = managerId,
                NotificationTitle = title,
                NotificationDetail = body,
                IsViewed = false,
                NotificationTypeId = notificationTypeId,
                NotificationOwnerId = notificationOwnerId,
                IsPushed = true,
                PushedTime = DateTime.Now
            };
            await _mediator.Send(createNotification);
            return true;
        }
    }
    public class NotificationSummary
    {
        public string NotificationTitle { get; set; }
        public string NotificationDetail { get; set; }
        public List<NotificationVM> DataList { get; set; }
        public string[] Tokens { get; set; }

    }
}
