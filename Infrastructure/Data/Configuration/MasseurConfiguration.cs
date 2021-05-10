using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class MasseurConfiguration : IEntityTypeConfiguration<Masseur>
    {
        public void Configure(EntityTypeBuilder<Masseur> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .IsRequired();
        }
    }
}