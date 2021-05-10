using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class MassageTypeConfiguration : IEntityTypeConfiguration<MassageType>
    {
        public void Configure(EntityTypeBuilder<MassageType> builder)
        {
            builder.HasKey(x => x.Id);
        }
    }
}