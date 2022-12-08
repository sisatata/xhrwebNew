using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.District .Queries.Models
{
    public class DistrictVM
    {
         public Guid Id {get; set;}
         public Guid DivisionId {get; set;}
         public string Name {get; set;}
         public string NameLocalized {get; set;}
         public float? Latitude {get; set;}
         public float? Longitude {get; set;}
         public string Website {get; set;}
    }
}
