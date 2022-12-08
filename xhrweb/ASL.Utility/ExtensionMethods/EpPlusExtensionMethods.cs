using OfficeOpenXml;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
//using Asl.Infrastructure.ExcelReports;

namespace ASL.Utility.ExtensionMethods
{
    public static class EpPlusExtensionMethods
    {
        public static int GetColumnByName(this ExcelWorksheet ws, string columnName)
        {
            if (ws == null) throw new ArgumentNullException(nameof(ws));
            return ws.Cells["1:1"].First(c => c.Value.ToString() == columnName).Start.Column;
        }

        public static void ProcessExcelFile(ExcelWorksheet workSheet, ArrayList boldRows = null, List<string> removeColumnNames = null)
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


            foreach (var name in removeColumnNames ?? new List<string>())
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
                        //Console.WriteLine($"{{Cell: {cell.Address}, Display: {cell.Text}, Value: {cell.Value}}}");
                        Console.WriteLine($"{{Cell: {cell.Address}, Display: {text}, Value: {text}, Type: {"string"}}}");

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
            // workSheet.Cells.AutoFitColumns();
            //Make all text fit the cells
            //workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns();

            //Autofit with minimum size for the column.
            double minimumSize = 10;
            workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns(minimumSize);

            //Autofit with minimum and maximum size for the column.
            double maximumSize = 35;
            workSheet.Cells[workSheet.Dimension.Address].AutoFitColumns(minimumSize, maximumSize);

            ////optional use this to make all columms just a bit wider, text would sometimes still overflow after AutoFitColumns().
            //for (int col = 1; col <= workSheet.Dimension.End.Column; col++)
            //{
            //    workSheet.Column(col).Width = workSheet.Column(col).Width + 1;
            //} 
            #endregion
        }


        //public static void ProcessExcelFileFromList<T>(string folder, string excelName, List<T> list, string sheetName, List<string> removeColumnNames) where T : IExcelDataDynamicType
        //{
        //    FileInfo file = new FileInfo(Path.Combine(folder, excelName));
        //    if (file.Exists)
        //    {
        //        file.Delete();
        //        file = new FileInfo(Path.Combine(folder, excelName));
        //    }

        //    using (var package = new ExcelPackage(file))
        //    {
        //        if (string.IsNullOrWhiteSpace(sheetName)) sheetName = "Sheet1";

        //        var workSheet = package.Workbook.Worksheets.Add(sheetName);

        //        workSheet.Cells.LoadFromCollection(list, true);

        //        var boldRows = new ArrayList();
        //        var removeColumns = new List<int>();

        //        int i = 1;


        //        foreach (var item in list)
        //        {
        //            i++;
        //            if (item.IsBoldRow) boldRows.Add(i);
        //        }

        //        ProcessExcelFile(workSheet, boldRows, removeColumnNames);

        //        package.Save();
        //    }
        //}


        //public static void ProcessExcelFileFromList<T>(string folder, string excelName, List<T> activeConsumerGiftlist, List<T> activeCorporateGiftlist, List<T> inActiveConsumerGiftlist, List<T> inActiveCorporateGiftlist, List<string> removeColumnNames) where T : IExcelDataDynamicType
        //{
        //    FileInfo file = new FileInfo(Path.Combine(folder, excelName));
        //    if (file.Exists)
        //    {
        //        file.Delete();
        //        file = new FileInfo(Path.Combine(folder, excelName));
        //    }

        //    using (var package = new ExcelPackage(file))
        //    {
        //        if (activeConsumerGiftlist.Count() > 0)
        //        {
        //            var sheetName = "Active Consumer Gifts";

        //            var workSheet = package.Workbook.Worksheets.Add(sheetName);

        //            workSheet.Cells.LoadFromCollection(activeConsumerGiftlist, true);

        //            var boldRows = new ArrayList();
        //            var removeColumns = new List<int>();

        //            int i = 1;


        //            foreach (var item in activeConsumerGiftlist)
        //            {
        //                i++;
        //                if (item.IsBoldRow) boldRows.Add(i);
        //            }

        //            ProcessExcelFile(workSheet, boldRows, removeColumnNames);
        //        }

        //        if (activeCorporateGiftlist.Count() > 0)
        //        {
        //            var sheetName = "Active Corporate Gifts";

        //            var workSheet = package.Workbook.Worksheets.Add(sheetName);

        //            workSheet.Cells.LoadFromCollection(activeCorporateGiftlist, true);

        //            var boldRows = new ArrayList();
        //            var removeColumns = new List<int>();

        //            int i = 1;


        //            foreach (var item in activeCorporateGiftlist)
        //            {
        //                i++;
        //                if (item.IsBoldRow) boldRows.Add(i);
        //            }

        //            ProcessExcelFile(workSheet, boldRows, removeColumnNames);
        //        }
        //        if (inActiveConsumerGiftlist.Count() > 0)
        //        {
        //            var sheetName = "Inactive consumer Gifts";

        //            var workSheet = package.Workbook.Worksheets.Add(sheetName);

        //            workSheet.Cells.LoadFromCollection(inActiveConsumerGiftlist, true);

        //            var boldRows = new ArrayList();
        //            var removeColumns = new List<int>();

        //            int i = 1;


        //            foreach (var item in inActiveConsumerGiftlist)
        //            {
        //                i++;
        //                if (item.IsBoldRow) boldRows.Add(i);
        //            }

        //            ProcessExcelFile(workSheet, boldRows, removeColumnNames);
        //        }
        //        if (inActiveCorporateGiftlist.Count() > 0)
        //        {
        //            var sheetName = "Inactive Corporate Gifts";

        //            var workSheet = package.Workbook.Worksheets.Add(sheetName);

        //            workSheet.Cells.LoadFromCollection(inActiveCorporateGiftlist, true);

        //            var boldRows = new ArrayList();
        //            var removeColumns = new List<int>();

        //            int i = 1;


        //            foreach (var item in inActiveCorporateGiftlist)
        //            {
        //                i++;
        //                if (item.IsBoldRow) boldRows.Add(i);
        //            }

        //            ProcessExcelFile(workSheet, boldRows, removeColumnNames);
        //        }

        //        if (activeConsumerGiftlist.Count > 0 || activeCorporateGiftlist.Count > 0 || inActiveConsumerGiftlist.Count > 0 ||
        //                inActiveCorporateGiftlist.Count > 0)
        //        {
        //            try
        //            {
        //                package.Save();
        //            }
        //            catch (Exception ex)
        //            {

        //                throw;
        //            }
        //        }
        //    }
        //}



    }
}
