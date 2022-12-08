using CompanySetup.Core.Entities;
using CompanySetup.Core.Entities.CompanyAggregate;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.UnitTests
{
    public class DepartmentShould
    {
        private Company _company;
        private Branch _branch;
        private Department _department;

        [SetUp]
        public void Setup()
        {
            _company = new Company(Guid.NewGuid());
            _branch = new Branch(Guid.NewGuid());
            _department = Department.Create(_company.Id, _branch.Id, "Software Department", "SD", "Software Department", 1);
        }

        [Test]
        public void Department_Should_Not_Create_Without_Company()
        {
            Setup();
            Assert.Throws<ArgumentException>(() => { Department.Create(Guid.Empty, _branch.Id, "Software Department", "SD", "Software Department", 1); });
        }

        [Test]
        public void Department_Should_Not_Create_Without_Branch()
        {
            Setup();
            Assert.Throws<ArgumentException>(() => { Department.Create(_company.Id, Guid.Empty, "Software Department", "SD", "Software Department", 1); });
        }

        [Test]
        public void Department_Should_Not_Create_Without_Name()
        {
            Setup();
            Assert.Throws<ArgumentException>(() => { Department.Create(_company.Id, _branch.Id, string.Empty, "SD", "Software Department", 1); });
        }

        [Test]
        public void Department_Should_Not_Update_Without_Company()
        {
            Setup();
            Assert.Throws<ArgumentException>(() => { _department.UpdateDepartment(Guid.Empty, _branch.Id, "Software Department", "SD", "Software Department", 1); });
        }

        [Test]
        public void Department_Should_Not_Update_Without_Branch()
        {
            Setup();
            Assert.Throws<ArgumentException>(() => { _department.UpdateDepartment(_company.Id, Guid.Empty, "Software Department", "SD", "Software Department", 1); });
        }

        [Test]
        public void Department_Should_Not_Update_Without_Name()
        {
            Setup();
            Assert.Throws<ArgumentException>(() => { _department.UpdateDepartment(_company.Id, _branch.Id, string.Empty, "SD", "Software Department", 1); });
        }

        [Test]
        public void Department_Delete_Should_Set_IsDeleted_Flag_True()
        {
            Setup();
            _department.MarkDepartmentAsDeleted();

            Assert.AreEqual(true, _department.IsDeleted);
        }

    }
}
