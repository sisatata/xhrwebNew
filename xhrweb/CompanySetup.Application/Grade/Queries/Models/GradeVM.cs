using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.Grade .Queries.Models
{
    public class GradeVM
    {
         public Guid? Id {get; set;}
         public string GradeName {get; set;}
         public string GradeNameLocalized {get; set;}
         public Int32? Rank {get; set;}
         public Boolean IsDeleted {get; set;}
         public Guid? CompanyId {get; set;}
    }
}
