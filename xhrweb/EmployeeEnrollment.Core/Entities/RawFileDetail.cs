using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.Interfaces;
using EmployeeEnrollment.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace EmployeeEnrollment.Core.Entities
{
    public class RawFileDetail : BaseEntity<Guid>, IAggregateRoot, IAuditable
    {
        //public Guid? Id { get; private set; }
        public string FileName { get; private set; }
        public string FileType { get; private set; }
        public string Comments { get; private set; }
        public Guid? CompanyId { get; private set; }

        public int TotalRecord { get; set; }
        public int TotalSuccess { get; set; }
        public int TotalFail { get; set; }
        public string Status { get; set; }
        public RawFileDetail(Guid id) : base(id) { }
        private RawFileDetail() : base(Guid.NewGuid()) { }

        public static RawFileDetail Create(

         string fileName,
         string fileType,
         string comments,
         Guid? companyId

        )

        {
            var oModel = new RawFileDetail(Guid.NewGuid());

            oModel.FileName = fileName;
            oModel.FileType = fileType;
            oModel.Comments = comments;
            oModel.CompanyId = companyId;

            return oModel;

        }


        public void UpdateRawFileDetail
            (

         string fileName,
         string fileType,
         string comments,
         Guid? companyId

        )
        {
            FileName = fileName;
            FileType = fileType;
            Comments = comments;
            CompanyId = companyId;
        }

        public void UpdateTotalRecords
            (

         int totalRecords

        )
        {
            TotalRecord = totalRecords;
            Status = "R";
        }


        public void CompleteProcessing
             (

          int totalSuccess,
          int totalFail

         )
        {
            TotalSuccess = totalSuccess;
            TotalFail = totalFail;
            Status = "R";
        }


    }
}

