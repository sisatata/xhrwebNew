using LeaveManagement.Core.Entities.LeaveSetupAggregate;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace LeaveManagement.UnitTests
{
    public class LeaveTypeShould
    {

        private LeaveType _leaveType;
        
        [SetUp]
        public void Setup()
        {
            _leaveType = LeaveType.CreateLeaveType(Guid.NewGuid(), "Casual Leave", "CSL", "Casual Leave"
                , 10, false, false, false, false, 0, false, false, false, 0, false, false, false, false);
        }

        [Test]
        public void LeaveType_Should_Not_Create_Without_Company()
        {
            //var leaveType = LeaveType.CreateLeaveType(Guid.Empty, "Casual Leave", "CSL", "Casual Leave"
            //    , 10, false, false, false, false, 0, false, false, false, 0, false, false, false, false);

            Assert.Throws<ArgumentException>(() => {
                                LeaveType.CreateLeaveType(Guid.Empty, "Casual Leave", "CSL", "Casual Leave"
                                        , 10, false, false, false, false, 0, false, false, false, 0, false, false, false, false);
                            });
        }

        //[Test]
      //  public void Branch_Should_Not_Update_With_Empty_Company()
       // {
        //    Setup();
          //  Assert.Throws<ArgumentException>(() => { _branch.UpdateBranch(Guid.Empty, null, null, null, 0); });
       // }
       [Test]
       public void LeaveType_Should_Not_Update_Without_Company()
        {
            Setup();
            Assert.Throws<ArgumentException>(() => { _leaveType.UpdateLeaveType(Guid.Empty, "Sick Leave", "SL", "Sick Leave", 10,false,false,false,false,0,false,false,false,0,false,false,false,false); });
        }
    }
}
