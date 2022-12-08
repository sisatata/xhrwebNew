using Ardalis.GuardClauses;
using ASL.Hrms.SharedKernel;
using ASL.Hrms.SharedKernel.ExtensionMethods;
using CompanySetup.Core.Interfaces;
using System;

namespace CompanySetup.Core.Entities
{
    public class Designation : BaseEntity<Guid>, IAggregateRoot
    {
        public Guid LinkedEntityId { get; private set; }
        public string LinkedEntityType { get; private set; }
        public string DesignationName { get; private set; }
        public string DesignationLocalizedName { get; private set; }
        public string ShortName { get; private set; }
        public uint SortOrder { get; private set; }
        public bool IsDeleted { get; private set; }

        public Designation(Guid id) : base(id) { }
        private Designation() : base(Guid.NewGuid()) { }

        // Factory method to create designation
        public static Designation Create(Guid linkedEntityId, string linkedEntityType, 
            string designationName, string designationLocalizedName, string shortName, uint sortOrder)
        {

            Guard.Against.NullOrEmptyGuid(linkedEntityId, "linkedEntityId");
            Guard.Against.NullOrWhiteSpace(linkedEntityType, "linkedEntityType");
            Guard.Against.NullOrWhiteSpace(designationName, "designationName");

            var designation = new Designation(Guid.NewGuid())
            {
                LinkedEntityId = linkedEntityId,
                LinkedEntityType = linkedEntityType,
                DesignationName = designationName,
                DesignationLocalizedName = designationLocalizedName,
                ShortName = shortName,
                SortOrder = sortOrder
            };
            return designation;
        }

        public void UpdateDesignation(Guid linkedEntityId, string linkedEntityType, string designationName, 
            string designationLocalizedName, string shortName, uint sortOrder)
        {
            Guard.Against.NullOrEmptyGuid(linkedEntityId, "linkedEntityId");
            Guard.Against.NullOrWhiteSpace(linkedEntityType, "linkedEntityType");
            Guard.Against.NullOrWhiteSpace(designationName, "designationName");

            LinkedEntityId = linkedEntityId;
            LinkedEntityType = linkedEntityType;
            DesignationName = designationName;
            DesignationLocalizedName = designationLocalizedName;
            ShortName = shortName;
            SortOrder = sortOrder;
        }

        public void MarkDesignationAsDeleted()
        {
            IsDeleted = true;
        }
    }
}
