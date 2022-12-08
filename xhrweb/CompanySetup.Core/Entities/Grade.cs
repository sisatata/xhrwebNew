using ASL.Hrms.SharedKernel;
using CompanySetup.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;


namespace CompanySetup.Core.Entities
{
    public class Grade : BaseEntity<Guid>, IAggregateRoot
    {
        public string GradeName { get; private set; }
        public string GradeNameLocalized { get; private set; }
        public Int32? Rank { get; private set; }
        public Boolean IsDeleted { get; private set; }
        public Guid? CompanyId { get; private set; }


        public Grade(Guid id) : base(id) { }
        private Grade() : base(Guid.NewGuid()) { }

        public static Grade Create(

         string gradeName,
         string gradeNameLocalized,
         Int32? rank,
         Boolean isDeleted,
         Guid? companyId

        )

        {
            var oModel = new Grade(Guid.NewGuid());

            oModel.GradeName = gradeName;
            oModel.GradeNameLocalized = gradeNameLocalized;
            oModel.Rank = rank;
            oModel.IsDeleted = isDeleted;
            oModel.CompanyId = companyId;

            return oModel;

        }


        public void UpdateGrade
            (

         string gradeName,
         string gradeNameLocalized,
         Int32? rank,
         Boolean isDeleted,
         Guid? companyId

        )
        {
            GradeName = gradeName;
            GradeNameLocalized = gradeNameLocalized;
            Rank = rank;
            IsDeleted = isDeleted;
            CompanyId = companyId;
        }


        public void MarkAsDeleteGrade()
        {
            IsDeleted = true;
        }


    }
}

