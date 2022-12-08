using CompanySetup.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CompanySetup.Persistence.Configurations
{

    public class CommonLookUpTypeConfiguration : IEntityTypeConfiguration<CommonLookUpType>
    {

        public void Configure(EntityTypeBuilder<CommonLookUpType> builder)
        {
            builder.Property(hr => hr.ShortCode).HasMaxLength(20);
            builder.Property(hr => hr.LookUpTypeName).HasMaxLength(50);
            builder.Property(hr => hr.LookUpTypeNameLC).HasMaxLength(50);
            builder.Property(hr => hr.Remarks).HasMaxLength(250);


        }
    }
}

