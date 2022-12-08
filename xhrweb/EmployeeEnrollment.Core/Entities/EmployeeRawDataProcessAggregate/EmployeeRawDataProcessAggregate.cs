using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Models;
using EmployeeEnrollment.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using OfficeOpenXml;
using System.Linq;
using ASL.Hrms.SharedKernel.Enums;

namespace EmployeeEnrollment.Core.Entities.EmployeeRawDataProcessAggregate
{
    public class EmployeeRawDataProcessAggregate : BaseEntity<Guid>, IAggregateRoot
    {
        private readonly PlanetHRSettings _planetHRSettings;
        private string _fileName { get; set; }
        private Guid _companyId { get; set; }
        private Guid _rawFileId { get; set; }
        public List<EmployeeRawDataPrep> EmployeeRawDataPrepList { get; set; }
        public EmployeeRawDataProcessAggregate(string fileName, Guid companyId, Guid rawFileId,
            PlanetHRSettings planetHRSettings)
        {
            _fileName = fileName;
            _companyId = companyId;
            _rawFileId = rawFileId;
            _planetHRSettings = planetHRSettings;
            EmployeeRawDataPrepList = new List<EmployeeRawDataPrep>();
        }

        public async void StartEmployeeImportProcess()
        {
            var fileName = $"{_planetHRSettings.ContentRoot.RootPath}{_planetHRSettings.ContentRoot.EmployeeImagePath}{_fileName}";

            try
            {


                using (ExcelPackage xlPackage = new ExcelPackage(new FileInfo(fileName)))
                {
                    var myWorksheet = xlPackage.Workbook.Worksheets.First(); //select sheet here
                    var totalRows = myWorksheet.Dimension.End.Row;
                    var totalColumns = myWorksheet.Dimension.End.Column;

                    var start = myWorksheet.Dimension.Start;
                    var end = myWorksheet.Dimension.End;
                    if (end.Row > 2)
                    {
                        for (int row = 3; row <= end.Row; row++)
                        { // Row by row...
                            EmployeeRawDataPrep employeeRawDataPrep = new EmployeeRawDataPrep(0);
                            for (int col = start.Column; col <= end.Column; col++)
                            { // ... Cell by cell...
                                try
                                {
                                    int i = col;
                                    switch (col)
                                    {
                                        case 1:
                                            employeeRawDataPrep.EmployeeCode = myWorksheet.Cells[row, col].Text.Trim();
                                            break;
                                        case 2:
                                            employeeRawDataPrep.FullName = myWorksheet.Cells[row, col].Text.Trim();
                                            break;
                                        case 3:
                                            employeeRawDataPrep.Company = myWorksheet.Cells[row, col].Text.Trim();
                                            break;
                                        case 4:
                                            employeeRawDataPrep.Branch = myWorksheet.Cells[row, col].Text.Trim();
                                            break;
                                        case 5:
                                            employeeRawDataPrep.Department = myWorksheet.Cells[row, col].Text.Trim();
                                            break;
                                        case 6:
                                            employeeRawDataPrep.Position = myWorksheet.Cells[row, col].Text.Trim();
                                            break;
                                        case 7:
                                            employeeRawDataPrep.JoiningDate = myWorksheet.Cells[row, col].Text.Trim();
                                            break;

                                        case 8:
                                            employeeRawDataPrep.ConfirmationRuleName = myWorksheet.Cells[row, col].Text.Trim();
                                            break;





                                        case 9:
                                            employeeRawDataPrep.PermanentDate = myWorksheet.Cells[row, col].Text.Trim();
                                            break;
                                        case 10:
                                            employeeRawDataPrep.Grade = myWorksheet.Cells[row, col].Text.Trim();
                                            break;

                                        case 11:
                                            employeeRawDataPrep.LeaveTypeGroupName = myWorksheet.Cells[row, col].Text.Trim();
                                            break;

                                        case 12:
                                            employeeRawDataPrep.Gross = myWorksheet.Cells[row, col].Text.Trim();
                                            break;

                                        case 13:
                                            employeeRawDataPrep.SalaryStructureName = myWorksheet.Cells[row, col].Text.Trim();
                                            break;

                                        case 14:
                                            employeeRawDataPrep.PaymentOptionName = myWorksheet.Cells[row, col].Text.Trim();
                                            break;

                                        case 15:
                                            employeeRawDataPrep.BankName = myWorksheet.Cells[row, col].Text.Trim();
                                            break;
                                        case 16:
                                            employeeRawDataPrep.BankAccount = myWorksheet.Cells[row, col].Text.Trim();
                                            break;
                                        case 17:
                                            employeeRawDataPrep.NameofFather = myWorksheet.Cells[row, col].Text.Trim();
                                            break;
                                        case 18:
                                            employeeRawDataPrep.NameofMother = myWorksheet.Cells[row, col].Text.Trim();
                                            break;
                                        case 19:
                                            employeeRawDataPrep.NameofSpouce = myWorksheet.Cells[row, col].Text.Trim();
                                            break;
                                        case 20:
                                            employeeRawDataPrep.DOB = myWorksheet.Cells[row, col].Text.Trim();
                                            break;

                                        case 21:
                                            employeeRawDataPrep.NID = myWorksheet.Cells[row, col].Text.Trim();
                                            break;

                                        case 22:
                                            employeeRawDataPrep.BID = myWorksheet.Cells[row, col].Text.Trim();
                                            break;

                                        case 23:
                                            employeeRawDataPrep.Emailaddress = myWorksheet.Cells[row, col].Text.Trim();
                                            break;

                                        case 24:
                                            employeeRawDataPrep.MobileNo = myWorksheet.Cells[row, col].Text.Trim();
                                            break;
                                        case 25:
                                            employeeRawDataPrep.EmergencyContact = myWorksheet.Cells[row, col].Text.Trim();
                                            break;
                                        case 26:
                                            employeeRawDataPrep.BloodGroup = myWorksheet.Cells[row, col].Text.Trim();
                                            break;
                                        case 27:
                                            employeeRawDataPrep.Gender = myWorksheet.Cells[row, col].Text.Trim();
                                            break;
                                        case 28:
                                            employeeRawDataPrep.MaritalStatus = myWorksheet.Cells[row, col].Text.Trim();
                                            break;
                                        case 29:
                                            employeeRawDataPrep.Religion = myWorksheet.Cells[row, col].Text.Trim();
                                            break;
                                        case 30:
                                            employeeRawDataPrep.Nationality = myWorksheet.Cells[row, col].Text.Trim();
                                            break;
                                        case 31:
                                            employeeRawDataPrep.PermanentCountry = myWorksheet.Cells[row, col].Text.Trim();
                                            break;
                                        case 32:
                                            employeeRawDataPrep.PermanentDistrict = myWorksheet.Cells[row, col].Text.Trim();
                                            break;

                                        case 33:
                                            employeeRawDataPrep.PermanentThana = myWorksheet.Cells[row, col].Text.Trim();
                                            break;
                                        case 34:
                                            employeeRawDataPrep.PermanentPostOffice = myWorksheet.Cells[row, col].Text.Trim();
                                            break;
                                        case 35:
                                            employeeRawDataPrep.PermanentVillage = myWorksheet.Cells[row, col].Text.Trim();
                                            break;
                                        case 36:
                                            employeeRawDataPrep.PermanentAddressLine1 = myWorksheet.Cells[row, col].Text.Trim();
                                            break;
                                        case 37:
                                            employeeRawDataPrep.PermanentAddressLine2 = myWorksheet.Cells[row, col].Text.Trim();
                                            break;
                                        case 38:
                                            employeeRawDataPrep.PresentCountry = myWorksheet.Cells[row, col].Text.Trim();
                                            break;
                                        case 39:
                                            employeeRawDataPrep.PresentDistrict = myWorksheet.Cells[row, col].Text.Trim();
                                            break;
                                        case 40:
                                            employeeRawDataPrep.PresentThana = myWorksheet.Cells[row, col].Text.Trim();
                                            break;

                                        case 41:
                                            employeeRawDataPrep.PresentPostOffice = myWorksheet.Cells[row, col].Text.Trim();
                                            break;

                                        case 42:
                                            employeeRawDataPrep.PresentVillage = myWorksheet.Cells[row, col].Text.Trim();
                                            break;

                                        case 43:
                                            employeeRawDataPrep.PresentAddressLine1 = myWorksheet.Cells[row, col].Text.Trim();
                                            break;
                                        case 44:
                                            employeeRawDataPrep.PresentAddressLine2 = myWorksheet.Cells[row, col].Text.Trim();
                                            break;


                                        default:
                                            break;
                                    }
                                }
                                catch (Exception ex)
                                {
                                    continue;
                                    //throw ex;
                                }
                                //object cellValue = myWorksheet.Cells[row, col].Text.Trim(); // This got me the actual value I needed.
                            }
                            employeeRawDataPrep.RawFileDetailId = _rawFileId;
                            employeeRawDataPrep.FileName = _fileName;
                            employeeRawDataPrep.Status = "P";
                            employeeRawDataPrep.State = TrackingState.Added;
                            EmployeeRawDataPrepList.Add(employeeRawDataPrep);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
