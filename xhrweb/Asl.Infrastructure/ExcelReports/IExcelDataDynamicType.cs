using System;
using System.Collections.Generic;
using System.Text;

namespace Asl.Infrastructure.ExcelReports
{
    public interface IExcelDataDynamicType
    {
        bool IsBoldRow { get; set; }
        bool WillRemoveColumn { get; set; }
    }
}
