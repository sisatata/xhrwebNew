using ASL.Hrms.SharedKernel;
using CompanySetup.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace CompanySetup.Core.Entities
{
    public class Upazila : BaseEntity<Guid>, IAggregateRoot
    {
        public Guid DistrictId { get; private set; }
        public string UpazilaName { get; private set; }
        public string UpazilaNameLocalized { get; private set; }
        public bool IsDeleted { get; private set; }

        public Upazila(Guid id) : base(id) { }
        private Upazila() : base(Guid.NewGuid()) { }

        public static Upazila Create(

         Guid districtId,
         string upazilaName,
         string upazilaNameLocalized

        )

        {
            var oModel = new Upazila(Guid.NewGuid());

            oModel.DistrictId = districtId;
            oModel.UpazilaName = upazilaName;
            oModel.UpazilaNameLocalized = upazilaNameLocalized;

            return oModel;

        }


        public void UpdateUpazila
            (

         Guid districtId,
         string upazilaName,
         string upazilaNameLocalized

        )
        {
            DistrictId = districtId;
            UpazilaName = upazilaName;
            UpazilaNameLocalized = upazilaNameLocalized;
        }


        public void MarkAsDeleteUpazila()
        {
            IsDeleted = true;
        }


    }
}

