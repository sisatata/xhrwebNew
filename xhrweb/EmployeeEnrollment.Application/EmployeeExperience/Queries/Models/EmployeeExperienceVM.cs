using System;
using System.Collections.Generic;
using System.Text;

namespace EmployeeEnrollment.Application.EmployeeExperience .Queries.Models
{
    public class EmployeeExperienceVM
    {
         public Guid Id {get; set;}
         public Guid EmployeeId {get; set;}
         public string CompanyName {get; set;}
         public string Designation {get; set;}
         public string FunctionalDesignation {get; set;}
         public string Department {get; set;}
         public string Responsibilities {get; set;}
         public string CompanyAddress {get; set;}
         public string CompanyContactNo {get; set;}
         public string CompanyMobile {get; set;}
         public string CompanyEmail {get; set;}
         public string CompanyWeb {get; set;}
         public DateTime  FromDate {get; set;}
         public DateTime? ToDate {get; set;}
         public Boolean TilDate {get; set;}
         public Boolean IsDeleted {get; set;}
         public Guid CompanyId {get; set;}
    }
}
