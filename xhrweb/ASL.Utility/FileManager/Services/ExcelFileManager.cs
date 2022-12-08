using ASL.Utility.ExtensionMethods;
using ASL.Utility.FileManager.Interfaces;
using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace ASL.Utility.FileManager.Services
{
    public class ExcelFileManager : IExcelFileManager
    {
        public void PrepareFile<T>(List<T> data, string filePath, string sheetName = null, List<string> excludeColumns = null) 
            where T : IExcelDataDynamicType
        {
            FileInfo file = new FileInfo(filePath);
            if (file.Exists)
            {
                file.Delete();
                file = new FileInfo(filePath);
            }

            try
            {
                //ExcelPackage.LicenseContext = LicenseContext.NonCommercial;

                using var package = new ExcelPackage(file);

                if (string.IsNullOrWhiteSpace(sheetName)) sheetName = "Sheet1";
                var workSheet = package.Workbook.Worksheets.Add(sheetName);
                workSheet.Cells.LoadFromCollection(data, true);

                var boldRows = new ArrayList();
                var removeColumns = new List<int>();

                int i = 1;
                foreach (var item in data)
                {
                    i++;
                    if (item.IsBoldRow) boldRows.Add(i);
                }

                ProcessExcelFile(workSheet, boldRows, excludeColumns);

                package.Save();
            }
            catch(Exception ex)
            {
                throw ex;
            }
            
        }

        private void ProcessExcelFile(ExcelWorksheet workSheet, ArrayList boldRows = null, List<string> excludeColumns = null)
        {
            var cells = workSheet.Cells;

            var datetimeColumns = new List<int>();


            workSheet.Row(1).Style.Font.Size = 14;
            workSheet.Row(1).Style.Font.Bold = true;

            foreach (var rowNo in boldRows ?? new ArrayList())
            {
                if (rowNo is int)
                {
                    workSheet.Row((int)rowNo).Style.Font.Size = 14;
                    workSheet.Row((int)rowNo).Style.Font.Bold = true;
                }
            }


            foreach (var name in excludeColumns ?? new List<string>())
            {
                int columnId = 0;
                try
                {
                    columnId = workSheet.GetColumnByName(name);
                }
                catch (Exception)
                {
                }

                if (columnId > 0)
                    workSheet.DeleteColumn(columnId);
            }


            foreach (var cell in cells)
            {
                if (cell.Value != null)
                {
                    if (cell.Value.GetType() == typeof(System.DateTime))
                    {
                        workSheet.Cells[cell.Address].Style.Numberformat.Format = "DD/MM/YYYY";
                        Console.WriteLine($"{{Cell: {cell.Address}, Display: {cell.Text}, Value: {cell.Value}}}");

                        if (!datetimeColumns.Any(x => x == cell.Start.Column))
                            datetimeColumns.Add(cell.Start.Column);
                    }
                    else if (cell.Value.GetType() == typeof(System.Boolean))
                    {
                        var text = (bool)cell.Value == true ? "Yes" : "No";
                        cell.Value = text;
                    }
                    else
                    {
                        Console.WriteLine($"{{Cell: {cell.Address}, Display: {cell.Text}, Value: {cell.Value}, Type: {cell.Value.GetType()}}}");
                    }
                    workSheet.Cells[cell.Address].Style.Font.Size = 12;
                }
            }

            foreach (var colNo in datetimeColumns)
            {
                workSheet.Column(colNo).Style.Numberformat.Format = "DD/MM/YYYY";
            }

            #region Ep Plus Auto Fit Width Section- keep it last of all

            //Autofit with minimum size for the column.
            double minimumSize = 10;
            workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns(minimumSize);

            //Autofit with minimum and maximum size for the column.
            double maximumSize = 35;
            workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns(minimumSize, maximumSize);

            #endregion
        }
    }
}
