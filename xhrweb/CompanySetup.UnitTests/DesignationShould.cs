using CompanySetup.Core.Entities;
using CompanySetup.Core.Entities.CompanyAggregate;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.UnitTests
{
    public class DesignationShould
    {
        private Company _company;
        private Branch _branch;
        private Department _department;
        private Designation _designation;

        [SetUp]
        public void Setup()
        {
            _company = new Company(Guid.NewGuid());
            _branch = new Branch(Guid.NewGuid());
            _department = new Department(Guid.NewGuid());
            _designation = Designation.Create(_department.Id, _department.GetType().Name, "Software Engineer", "Software Engineer", "SE", 1);
        }

        [Test]
        public void Designation_Should_Not_Create_Without_LinkedEntity()
        {
            Setup();
            Assert.Throws<ArgumentException>(() => { Designation.Create(Guid.Empty, _department.GetType().Name, "Software Engineer", "Software Engineer", "SE", 1); });
        }

        [Test]
        public void Designation_LinkedEntityType_Should_Match()
        {
            Setup();
            Assert.AreEqual("Department", _designation.LinkedEntityType);
        }

        [Test]
        public void Designation_Should_Not_Create_Without_LinkedEntityType()
        {
            Setup();
            Assert.Throws<ArgumentException>(() => { Designation.Create(_department.Id, string.Empty, "Software Engineer", "Software Engineer", "SE", 1); });
        }

        [Test]
        public void Designation_Should_Not_Create_Without_Name()
        {
            Setup();
            Assert.Throws<ArgumentException>(() => { Designation.Create(_department.Id, _department.GetType().Name, string.Empty, "Software Engineer", "SE", 1); });
        }

        [Test]
        public void Designation_Delete_Should_Set_IsDeleted_Flag_True()
        {
            Setup();
            _designation.MarkDesignationAsDeleted();

            Assert.AreEqual(true, _designation.IsDeleted);
        }

    }
}
