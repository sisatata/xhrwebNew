using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Enums;
using CompanySetup.Core.Entities.CompanyAggregate;
using CompanySetup.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CompanySetup.Core.Entities.NotificationAggregate
{
    public class NotificationAggregate : BaseEntity<Guid>, IAggregateRoot
    {
        private IReadOnlyList<Company> _companies { get; }
        private IReadOnlyList<Holiday> _holidays { get; }
        private IReadOnlyList<OfficeNotice> _officeNotices { get; }
        private IReadOnlyList<EmployeeDevice> _employeeDevices { get; }
        public List<Notification> NotificationList { get; set; }
        private List<Notification> _notificationListDB { get; set; }
        public NotificationAggregate(IReadOnlyList<Company> companies,
            IReadOnlyList<Holiday> holidays,
            IReadOnlyList<EmployeeDevice> employeeDevices,
            List<Notification> notificationList)
        {
            _companies = companies;
            _holidays = holidays;
            _employeeDevices = employeeDevices;
            _notificationListDB = notificationList;
            NotificationList = new List<Notification>();
        }
        public NotificationAggregate(IReadOnlyList<Company> companies,
            IReadOnlyList<OfficeNotice> officeNotices,
            IReadOnlyList<EmployeeDevice> employeeDevices,
            List<Notification> notificationList)
        {
            _companies = companies;
            _officeNotices = officeNotices;
            _employeeDevices = employeeDevices;
            _notificationListDB = notificationList;
            NotificationList = new List<Notification>();
        }

        async public void StartHolidayNotificationBulk()
        {
            foreach (var _company in _companies)
            {
                var holiday = (from h in _holidays.ToList()
                               where h.CompanyId == _company.Id
                               select h).OrderBy(x => x.HolidayDate).FirstOrDefault();
                if (holiday == null)
                    continue;
                foreach (var employeeDevice in _employeeDevices)
                {
                    var notificationTitle = $"Notice for a holiday dated {holiday.HolidayDate.ToString("dd/MM/yyyy")}";
                    var notificationDetail = $"Please note that office will be closed on {holiday.HolidayDate.ToString("dd/MM/yyyy")} for {holiday.Reason}. We wish you all the warmest of holiday cheer!";
                    var employeeNotification = _notificationListDB?.FirstOrDefault(x => x.NotificationOwnerId == employeeDevice.EmployeeId
                && x.NotificationTitle == notificationTitle && x.NotificationDetail == notificationDetail);
                    TrackingState state = TrackingState.Added;

                    if (employeeNotification == null)
                    {
                        employeeNotification = new Notification(Guid.NewGuid());
                        state = TrackingState.Added;
                    }
                    else
                    {
                        state = TrackingState.Modified;
                    }
                    employeeNotification.StartBulk(
                                            notificationTitle,// string notificationTitle,
                                            notificationDetail, //string notificationDetail,
                                            (short)NotificationTypes.HolidayPush, // Int16 ? notificationTypeId,
                                            employeeDevice.EmployeeId,//  Guid ? notificationOwnerId
                                            employeeDevice.AccessToken,
                                            state
                                            );

                    NotificationList.Add(employeeNotification);

                }
            }
        }

        async public void StartOfficeNoticeNotificationBulk()
        {
            foreach (var _company in _companies)
            {
                var officeNotices = (from h in _officeNotices.ToList()
                                     where h.CompanyId == _company.Id
                                     select h).OrderBy(x => x.StartDate).ToList();
                if (!officeNotices.Any())
                    continue;
                foreach (var officeNotice in officeNotices)
                {

                    foreach (var employeeDevice in _employeeDevices)
                    {
                        var employeeNotification = _notificationListDB?.FirstOrDefault(x => x.NotificationOwnerId == employeeDevice.EmployeeId
                    && x.NotificationTitle == officeNotice.Subject && x.NotificationDetail == officeNotice.Details);
                        TrackingState state = TrackingState.Added;

                        if (employeeNotification == null)
                        {
                            employeeNotification = new Notification(Guid.NewGuid());
                            state = TrackingState.Added;
                        }
                        else
                        {
                            state = TrackingState.Modified;
                        }
                        employeeNotification.StartBulk(
                                                officeNotice.Subject,// string notificationTitle,
                                                officeNotice.Details, //string notificationDetail,
                                                (short)NotificationTypes.NoticeBoardPush, // Int16 ? notificationTypeId,
                                                employeeDevice.EmployeeId,//  Guid ? notificationOwnerId
                                                employeeDevice.AccessToken,
                                                state
                                                );

                        NotificationList.Add(employeeNotification);

                    }
                }

            }
        }
    }
}
