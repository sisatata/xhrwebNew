using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using CompanySetup.Core.Interfaces;
using System;


namespace CompanySetup.Core.Entities
{
    public class CommonLookUpType : BaseEntity<Guid>, IAggregateRoot
    {
        public string ShortCode { get; private set; }
        public string LookUpTypeName { get; private set; }
        public string LookUpTypeNameLC { get; private set; }
        public string Remarks { get; private set; }
        public Guid? CompanyId { get; private set; }
        public Guid? ParentId { get; private set; }
        public Int16 SortOrder { get; private set; }
        public bool IsDeleted { get; private set; }

        public CommonLookUpType(Guid id) : base(id) { }
        private CommonLookUpType() : base(Guid.NewGuid()) { }

        public static CommonLookUpType Create(

         string shortCode,
         string lookUpTypeName,
         string lookUpTypeNameLC,
         string remarks,
         Guid? companyId,
         Guid? parentId,
         Int16 sortOrder

        )

        {
            var oModel = new CommonLookUpType(Guid.NewGuid());

            oModel.ShortCode = shortCode;
            oModel.LookUpTypeName = lookUpTypeName;
            oModel.LookUpTypeNameLC = lookUpTypeNameLC;
            oModel.Remarks = remarks;
            if (companyId != null && companyId != Guid.Empty)
                oModel.CompanyId = companyId;
            oModel.ParentId = parentId;
            oModel.SortOrder = sortOrder;

            return oModel;

        }


        public void UpdateCommonLookUpType
            (

         string shortCode,
         string lookUpTypeName,
         string lookUpTypeNameLC,
         string remarks,
         Guid? companyId,
         Guid? parentId,
         Int16 sortOrder

        )
        {
            ShortCode = shortCode;
            LookUpTypeName = lookUpTypeName;
            LookUpTypeNameLC = lookUpTypeNameLC;
            Remarks = remarks;
            if (companyId != null && companyId != Guid.Empty)
            CompanyId = companyId;
            ParentId = parentId;
            SortOrder = sortOrder;
        }


        public void MarkAsDeleteCommonLookUpType()
        {
            IsDeleted = true;
        }


    }
}

