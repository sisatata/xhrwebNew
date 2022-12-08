using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ASL.Hrms.SharedKernel.Interfaces
{
    public interface IPushNotificationService
    {
        Task<bool> SendPushNotification(string[] accessTokens, string title, string body, object data);
        Task<bool> SendLeaveApplyNotification(Guid ManagerId, Guid EmployeeId, double NoOfDays, Guid ApplicationId,
            DateTime StartDate, DateTime EndDate, string Reason, Boolean IsHalfDayLeave);
        Task<bool> SendLeaveApproveNotification(Guid ManagerId, Guid EmployeeId, double NoOfDays, Guid ApplicationId,
            DateTime StartDate, DateTime EndDate, string Reason, Boolean IsHalfDayLeave);
        Task<bool> SendLeaveDeclineNotification(Guid ManagerId, Guid EmployeeId, double NoOfDays, Guid ApplicationId,
            DateTime StartDate, DateTime EndDate, string Reason, Boolean IsHalfDayLeave);

        //Attendance
        Task<bool> SendAttendanceApplyNotification(Guid ManagerId, Guid EmployeeId, string RequestType, DateTime StartDate, DateTime EndDate, Guid ApplicationId);
        Task<bool> SendAttendanceApproveNotification(Guid ManagerId, Guid EmployeeId, string RequestType, DateTime StartDate, DateTime EndDate, Guid ApplicationId);
        Task<bool> SendAttendanceDeclineNotification(Guid ManagerId, Guid EmployeeId, string RequestType, DateTime StartDate, DateTime EndDate, Guid ApplicationId);

        //Bill
        Task<bool> SendBillApplyNotification(Guid ManagerId, Guid EmployeeId, decimal BillAmount, Guid ApplicationId);
        Task<bool> SendBillApproveNotification(Guid ManagerId, Guid EmployeeId, decimal BillAmount, decimal ApprovedAmount, Guid ApplicationId);
        Task<bool> SendBillDeclineNotification(Guid ManagerId, Guid EmployeeId, Guid ApplicationId);

        Task<bool> SendBillApproveNotificationForPayment(Guid ManagerId, Guid EmployeeId,  decimal ApprovedAmount, Guid ApplicationId);
        Task<bool> SendBillMarkPaidNotificationForApplicant(Guid ManagerId, Guid EmployeeId, decimal PaidAmount, Guid ApplicationId);

        Task<bool> SendPushNotificationBulk();

        //Task<bool> CreateAndSendMobileNotification(long offerId, List<string> phoneNumbers, OfferStatus offerStatus, Guid offerAssignmentId = default(Guid));
        //Task<bool> SendPushNotification(SendExpirationPushNotificationToReceiverCommadOutput output);
        //Task<bool> CreateAndSendMobileNotificationForRewardPointEarning(List<string> phoneNumbers, string messageTitle, string messageBody, string merchantName, double earnedRewardPoint, string pointEarningReason);
    }
}
