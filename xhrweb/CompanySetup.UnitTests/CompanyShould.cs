using CompanySetup.Core.Entities.CompanyAggregate;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.UnitTests
{
    public class CompanyShould
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Company_Should_Not_Create_With_Null_Or_Empty_Name()
        {
            //CompanyAggregate c = new CompanyAggregate();
            //Assert.Throws<ArgumentException>(() => { Company.Create(string.Empty, "abc", "abc", "slow", "xyz", 1); });
        }

        [Test]
        public void Company_Should_Not_Update_With_Null_Or_Empty_Name()
        {
            //Assert.Throws<ArgumentException>(() => {
            //    var company = Company.Create("Aplectrum Solutions", "abc", "abc", "slow", "xyz", 1);
            //    company.UpdateCompany(string.Empty, "abc", "abc", "slow", "xyz", 1);
            //});
        }

        [Test]
        public void Company_Delete_Should_Set_IsDeleted_Flag_True()
        {
            //var company = Company.Create("Aplectrum Solutions", "abc", "abc", "slow", "xyz", 1);
            //company.MarkCompanyAsDeleted();

            //Assert.AreEqual(true, company.IsDeleted);
        }
    }
}
