using ASL.Hrms.SharedKernel;
using CompanySetup.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace CompanySetup.Core.Entities
{
    public class OfficeNotice : BaseEntity<Guid>, IAggregateRoot
    {
        public Guid CompanyId { get; private set; }
        public string Subject { get; private set; }
        public string Details { get; private set; }
        public Boolean IsGeneral { get; private set; }
        public Boolean IsSectionSpecific { get; private set; }
        public string ApplicableSections { get; private set; }
        public DateTime? PublishDate { get; private set; }
        public DateTime? StartDate { get; private set; }
        public DateTime? EndDate { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public Boolean IsPublished { get; private set; }


        public OfficeNotice(Guid id) : base(id) { }
        private OfficeNotice() : base(Guid.NewGuid()) { }

        public static OfficeNotice Create(

         Guid companyId,
         string subject,
         string details,
         Boolean isGeneral,
         Boolean isSectionSpecific,
         string applicableSections,
         DateTime? publishDate,
         DateTime? startDate,
         DateTime? endDate,
         Boolean isDeleted,
         Boolean isPublished

        )

        {
            var oModel = new OfficeNotice(Guid.NewGuid());
            oModel.CompanyId = companyId;
            oModel.Subject = subject;
            oModel.Details = details;
            oModel.IsGeneral = isGeneral;
            oModel.IsSectionSpecific = isSectionSpecific;
            oModel.ApplicableSections = applicableSections;
            oModel.PublishDate = publishDate;
            oModel.StartDate = startDate;
            oModel.EndDate = endDate;
            oModel.IsDeleted = isDeleted;
            oModel.IsPublished = true;// isPublished;   will do hosted job service

            return oModel;

        }


        public void UpdateOfficeNotice
            (

         Guid companyId,
         string subject,
         string details,
         Boolean isGeneral,
         Boolean isSectionSpecific,
         string applicableSections,
         DateTime? publishDate,
         DateTime? startDate,
         DateTime? endDate,
         Boolean isDeleted,
         Boolean isPublished

        )
        {
            CompanyId = companyId;
            Subject = subject;
            Details = details;
            IsGeneral = isGeneral;
            IsSectionSpecific = isSectionSpecific;
            ApplicableSections = applicableSections;
            PublishDate = publishDate;
            StartDate = startDate;
            EndDate = endDate;
            IsDeleted = isDeleted;
            //IsPublished = isPublished;
        }


        public void MarkAsDeleteOfficeNotice()
        {
            IsDeleted = true;
        }


    }
}

