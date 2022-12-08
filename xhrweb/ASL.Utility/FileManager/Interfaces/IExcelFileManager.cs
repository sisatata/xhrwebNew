using System;
using System.Collections.Generic;
using System.Text;

namespace ASL.Utility.FileManager.Interfaces
{
    public interface IExcelFileManager
    {
        void PrepareFile<T>(List<T> data, string filePath, string sheetName = null, List<string> excludeColumns = null)
            where T : IExcelDataDynamicType;
    }
}
