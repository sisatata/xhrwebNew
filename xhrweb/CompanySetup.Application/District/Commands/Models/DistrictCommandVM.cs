using System;


namespace CompanySetup.Application.District.Commands
{
    public class DistrictCommandVM
    {
        public Guid Id { get; set; }
        public bool Status { get; set; }
        public string Message { get; set; }
    }
}
