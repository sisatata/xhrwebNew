using System;
using System.Collections.Generic;
using System.Text;

namespace CompanySetup.Application.Chart.Model
{
  public  class CurrentYearJoinedEmployeesVM
    {
        public string FullName { get; set; }
        public DateTime? JoiningDate { get; set; }
        public string DesignationName { get; set; }
    }
}
