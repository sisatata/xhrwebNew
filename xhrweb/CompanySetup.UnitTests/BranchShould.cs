using CompanySetup.Core.Entities;
using CompanySetup.Core.Entities.CompanyAggregate;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.UnitTests
{
    public class BranchShould
    {
        private Company _company;
        private Branch _branch;
        
        [SetUp]
        public void Setup()
        {
            _company = new Company(Guid.NewGuid());
            _branch = Branch.Create(_company.Id, "Uttara Branch", "UB", "Uttara Branch", 1);
        }

        [Test]
        public void Branch_Should_Not_Create_Without_Company()
        {
            Assert.Throws<ArgumentException>(() => { Branch.Create(Guid.Empty, "Uttara Branch", "UB", "Uttara Branch", 1); });
        }

        [Test]
        public void Branch_Should_Not_Create_Without_Name()
        {
            Setup();
            Assert.Throws<ArgumentException>(() => { Branch.Create(_company.Id, string.Empty, "UB", "Uttara Branch", 1); });
        }

        [Test]
        public void Branch_Should_Not_Update_With_Empty_Company()
        {
            Setup();
            Assert.Throws<ArgumentException>(() => { _branch.UpdateBranch(Guid.Empty,null,null,null,0); });
        }

        [Test]
        public void Branch_Delete_Should_Set_IsDeleted_Flag_True()
        {
            Setup();
            _branch.MarkBranchAsDeleted();

            Assert.AreEqual(true, _branch.IsDeleted);
        }
    }
}
