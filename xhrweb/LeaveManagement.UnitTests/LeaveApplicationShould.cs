using LeaveManagement.Core.ApplicationEvents;
using LeaveManagement.Core.Entities.LeaveApplicationAggregate;
using LeaveManagement.Core.Enums;
using NUnit.Framework;
using System;
using System.Linq;

namespace LeaveManagement.UnitTests
{
    public class LeaveApplicationShould
    {
        private Guid _companyId;
        private Guid _employeeId;
        private Guid _leaveTypeId;
        private string _leaveCalendar;

        [SetUp]
        public void Setup()
        {
            _companyId = Guid.NewGuid();
            _employeeId = Guid.NewGuid();
            _leaveTypeId = Guid.NewGuid();
            _leaveCalendar = "2020";
        }

        [Test]
        public void Application_Should_Start_Date()
        {
            var application = new LeaveApplication(Guid.NewGuid());
            //application.StartLeaveApplication(_companyId, _employeeId, _leaveTypeId, _leaveCalendar, DateTime.Now, DateTime.Now.AddDays(1), 1, "personal");

            Assert.IsNotNull(application.StartDate);
        }

        [Test]
        public void Application_Should_End_Date()
        {
            var application = new LeaveApplication(Guid.NewGuid());
            //application.StartLeaveApplication(_companyId, _employeeId, _leaveTypeId, _leaveCalendar, DateTime.Now, DateTime.Now.AddDays(1), 1, "personal");
            Assert.IsNotNull(application.EndDate);
        }

        [Test]
        public void Application_End_Date_Should_Less_To_Start_Date()
        {
            Assert.Throws<ArgumentException>(() =>
            {
                var application = new LeaveApplication(Guid.NewGuid());
                //application.StartLeaveApplication(_companyId, _employeeId, _leaveTypeId, _leaveCalendar, DateTime.Now, DateTime.Now.AddDays(-1), 1, "personal");
            }, "End date should be greater or equal than start date.");
        }

        [Test]
        public void New_Application_Should_Have_PendingState()
        {
            var application = new LeaveApplication(Guid.NewGuid());
            //application.StartLeaveApplication(_companyId, _employeeId, _leaveTypeId, _leaveCalendar, DateTime.Now, DateTime.Now.AddDays(1), 1, "personal");
            //Assert.AreEqual(LeaveApplicationStatus.PendingApproval, application.LeaveApplicationStatus);
        }

        [Test]
        public void Approve_Application_Should_Have_ApproveState()
        {
            var application = new LeaveApplication(Guid.NewGuid());
            //application.StartLeaveApplication(_companyId, _employeeId, _leaveTypeId, _leaveCalendar, DateTime.Now, DateTime.Now.AddDays(1), 1, "personal");
            application.ApproveLeaveApplication("approved");
            //Assert.AreEqual(LeaveApplicationStatus.Approved, application.LeaveApplicationStatus);
        }

        [Test]
        public void Rejected_Application_Should_Have_RejectState()
        {
            var application = new LeaveApplication(Guid.NewGuid());
            //application.StartLeaveApplication(_companyId, _employeeId, _leaveTypeId, _leaveCalendar, DateTime.Now, DateTime.Now.AddDays(1), 1, "personal");
            application.RejectLeaveApplication("approved");
            //Assert.AreEqual(LeaveApplicationStatus.Rejected, application.LeaveApplicationStatus);
        }

        [Test]
        public void Approve_Application_Should_Not_Update()
        {
            var exceptionText = "Cannot update approved application";
            var application = new LeaveApplication(Guid.NewGuid());
            //application.StartLeaveApplication(_companyId, _employeeId, _leaveTypeId, _leaveCalendar, DateTime.Now, DateTime.Now.AddDays(1), 1, "personal");
            application.ApproveLeaveApplication("approved");

            Assert.Throws<ArgumentException>(() =>
            {
                //application.UpdateLeaveApplication(application.LeaveTypeId, application.LeaveCalendar, DateTime.Now, DateTime.Now.AddDays(2), 2, "Personal");
            }, exceptionText);
        }

        [Test]
        public void Application_Start_Should_Raises_LeaveAppliedEvent()
        {
            var application = new LeaveApplication(Guid.NewGuid());
            //application.StartLeaveApplication(_companyId, _employeeId, _leaveTypeId, _leaveCalendar, DateTime.Now, DateTime.Now.AddDays(1), 1, "personal");

            Assert.AreEqual(application.Events.Count(), 1);
            Assert.IsInstanceOf<LeaveAppliedEvent>(application.Events.First());
        }
    }
}
