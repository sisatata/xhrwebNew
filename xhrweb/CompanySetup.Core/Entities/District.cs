using ASL.Hrms.SharedKernel;
using CompanySetup.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace CompanySetup.Core.Entities
{
    public class District : BaseEntity<Guid>, IAggregateRoot
    {
        //public Int16? Id { get; private set; }
        public Guid DivisionId { get; private set; }
        public string Name { get; private set; }
        public string NameLocalized { get; private set; }
        public float? Latitude { get; private set; }
        public float? Longitude { get; private set; }
        public string Website { get; private set; }
        public bool IsDeleted { get; private set; }

        public District(Guid id) : base(id) { }
        private District() : base(Guid.NewGuid()) { }

        public static District Create(


         Guid divisionId,
         string name,
         string nameLocalized,
         float? latitude,
         float? longitude,
         string website

        )

        {
            var oModel = new District(Guid.NewGuid());
            oModel.DivisionId = divisionId;
            oModel.Name = name;
            oModel.NameLocalized = nameLocalized;
            oModel.Latitude = latitude;
            oModel.Longitude = longitude;
            oModel.Website = website;

            return oModel;

        }


        public void UpdateDistrict
            (
         Guid divisionId,
         string name,
         string nameLocalized,
         float? latitude,
         float? longitude,
         string website
        )
        {
            DivisionId = divisionId;
            Name = name;
            NameLocalized = nameLocalized;
            Latitude = latitude;
            Longitude = longitude;
            Website = website;
        }


        public void MarkAsDeleteDistrict()
        {
            IsDeleted = true;
        }


    }
}

