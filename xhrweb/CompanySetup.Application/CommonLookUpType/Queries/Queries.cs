using CompanySetup.Application.CommonLookUpType.Queries.Models;
using MediatR;
using System;
using System.Collections.Generic;

namespace CompanySetup.Application.CommonLookUpType.Queries
{
    public static class Queries
    {
        public class GetCommonLookUpTypeListByCompany : IRequest<List<CommonLookUpTypeVM>>
        {
            public Guid CompanyId { get; set; }
        }

        public class GetCommonLookUpTypeListByParent : IRequest<List<CommonLookUpTypeVM>>
        {
            public Guid CompanyId { get; set; }
            public Guid ParentLookUpTypeId { get; set; }
        }

        public class GetCommonLookUpTypeListByParentCode : IRequest<List<CommonLookUpTypeVM>>
        {
            public Guid CompanyId { get; set; }
            public string ParentCode { get; set; }
        }
        public class GetCommonSettingsByCompany : IRequest<CommonSettingsVM>
        {
            public Guid CompanyId { get; set; }
            public Guid? EmployeeId { get; set; }
        }
    }
}
