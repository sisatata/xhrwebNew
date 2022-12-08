using LeaveManagement.Core.Entities.LeaveApplicationAggregate;
using LeaveManagement.Core.Entities.LeaveBalanceAggregate;
using LeaveManagement.Core.Entities.LeaveSetupAggregate;
using LeaveManagement.Core.Events;
using NUnit.Framework;
using System;
using System.Linq;

namespace LeaveManagement.UnitTests
{
    public class LeaveApplicationAggregateShould
    {
        private Guid _companyId;
        private Guid _employeeId;
        private LeaveType _leaveType1;
        private LeaveType _leaveType2;
        private LeaveBalance _leaveBalance1;
        private LeaveBalance _leaveBalance2;

        [SetUp]
        public void Setup()
        {
            _companyId = Guid.NewGuid();
            _employeeId = Guid.NewGuid();

            _leaveType1 = LeaveType.CreateLeaveType(companyId: _companyId, leaveTypeName: "Casual Leave", leaveTypeShortName: "CSL", leaveTypeLocalizedName: "Casual Leave",
                balance: 10, isCarryForward: false, isFemaleSpecific: false, isPaid: false, isEncashable: false, encashReserveBalance: 0,
                isDependOnDateOfConfirmation: false, isLeaveDaysFixed: false, isBasedWorkingDays: false, consecutiveLimit: 0, isAllowAdvanceLeaveApply: true, isAllowWithPrecedingHoliday: false,
                isAllowOpeningBalance: false, isPreventPostLeaveApply: true);

            _leaveType2 = LeaveType.CreateLeaveType(companyId: _companyId, leaveTypeName: "Sick", leaveTypeShortName: "SL", leaveTypeLocalizedName: "Sick Leave",
                balance: 5, isCarryForward: false, isFemaleSpecific: false, isPaid: false, isEncashable: false, encashReserveBalance: 0,
                isDependOnDateOfConfirmation: false, isLeaveDaysFixed: false, isBasedWorkingDays: false, consecutiveLimit: 0, isAllowAdvanceLeaveApply: false, isAllowWithPrecedingHoliday: false,
                isAllowOpeningBalance: false, isPreventPostLeaveApply: false);

            _leaveBalance1 = LeaveBalance.CreateLeaveBalance(companyId: _companyId, employeeId: _employeeId, leaveTypeId: _leaveType1.Id, 
                leaveCalendar: "2020", maximumBalance: 10, usedBalance: 8, remainingBalance: 2);

            _leaveBalance2 = LeaveBalance.CreateLeaveBalance(companyId: _companyId, employeeId: _employeeId, leaveTypeId: _leaveType2.Id,
                leaveCalendar: "2020", maximumBalance: 5, usedBalance: 0, remainingBalance: 5);

        }

        [Test]
        public void Application_Should_Not_Accept_For_Insufficient_Balance()
        {
            var leaveApplication = new LeaveApplicationAggregate(_companyId, _employeeId, _leaveType1, _leaveBalance1);

            Assert.Throws<ArgumentException>(() =>
            {
                //leaveApplication.NewLeaveApplication(DateTime.Now, DateTime.Now.AddDays(3), 3, "2020", "personal");
            });
        }

        [Test]
        public void Advance_Application_Should_Not_Accept() 
        {
            var leaveApplication = new LeaveApplicationAggregate(_companyId, _employeeId, _leaveType2, _leaveBalance2);

            Assert.Throws<ArgumentException>(() =>
            {
                //leaveApplication.NewLeaveApplication(DateTime.Now.AddDays(1), DateTime.Now.AddDays(1), 1, "2020", "personal");
            });
        }

        [Test]
        public void Post_Application_Should_Not_Accept()
        {
            var leaveApplication = new LeaveApplicationAggregate(_companyId, _employeeId, _leaveType1, _leaveBalance1);

            Assert.Throws<ArgumentException>(() =>
            {
                //leaveApplication.NewLeaveApplication(DateTime.Now.AddDays(-1), DateTime.Now.AddDays(-1), 1, "2020", "personal");
            });
        }

        
    }
}
