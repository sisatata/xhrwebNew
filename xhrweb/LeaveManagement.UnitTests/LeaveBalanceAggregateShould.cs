using LeaveManagement.Core.Entities;
using LeaveManagement.Core.Entities.LeaveBalanceAggregate;
using LeaveManagement.Core.Entities.LeaveSetupAggregate;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LeaveManagement.UnitTests
{
    public class LeaveBalanceAggregateShould
    {

        private List<LeaveType> _leaveTypes;
        private List<Employee> _employees;
        private LeaveCalendar _leaveCalendar;
        private Guid _companyId;
        private Guid _cslId;
        private Guid _slId;
        
        [SetUp]
        public void Setup()
        {
            _companyId = Guid.NewGuid();
            
            _leaveTypes = new List<LeaveType>();
            var leaveType1 = LeaveType.CreateLeaveType(_companyId, "Casual Leave", "CSL", "Casual Leave"
                , 10, false, false, false, false, 0, false, true, false, 0, false, false, false, false);
            var leaveType2 = LeaveType.CreateLeaveType(_companyId, "Sick Leave", "SL", "Sick Leave"
                , 5, false, false, false, false, 0, false, false, false, 0, false, false, false, false);

            _cslId = leaveType1.Id;
            _slId = leaveType2.Id;

            _leaveTypes.Add(leaveType1);
            _leaveTypes.Add(leaveType2);

            _employees = new List<Employee>
            {
                new Employee(Guid.NewGuid())
                {
                    FullName = "Zafor",
                    JoiningDate = new DateTime(2018,03,01)
                }
            };

            _leaveCalendar = new LeaveCalendar(Guid.NewGuid())
            {
                Year = "2020",
                YearStartDate = new DateTime(2020, 01, 01),
                YearEndDate = new DateTime(2020, 12, 31)
            };
        }

        ////[Test]
        ////public void Old_Employee_Should_Get_Full_Leave()
        ////{
        ////    var leaveAggregate = new LeaveBalanceAggregate(_companyId, _leaveCalendar, _leaveTypes, _employees);
        ////    leaveAggregate.PrepareLeaveBalance();

        ////    var cl = leaveAggregate.LeaveBalances.FirstOrDefault(c => c.LeaveTypeId == _cslId);
        ////    var sl = leaveAggregate.LeaveBalances.FirstOrDefault(c => c.LeaveTypeId == _slId);

        ////    Assert.AreEqual(10, cl.MaximumBalance);
        ////    Assert.AreEqual(5, sl.MaximumBalance);
        ////}

        //[Test]
        //public void New_Employee_Should_Get_Partial_Leave()
        //{
        //    var employees = new List<Employee>
        //    {
        //        new Employee(Guid.NewGuid())
        //        {
        //            FullName = "Zafor",
        //            JoiningDate = new DateTime(2020,03,01)
        //        }
        //    };

        //    var leaveAggregate = new LeaveBalanceAggregate(_companyId, _leaveCalendar, _leaveTypes, employees);
        //    leaveAggregate.PrepareLeaveBalance();

        //    var cl = leaveAggregate.LeaveBalances.FirstOrDefault(c => c.LeaveTypeId == _cslId);
        //    var sl = leaveAggregate.LeaveBalances.FirstOrDefault(c => c.LeaveTypeId == _slId);

        //    Assert.AreEqual(10, cl.MaximumBalance);
        //    Assert.AreEqual(4, sl.MaximumBalance);
        //}
    }
}
