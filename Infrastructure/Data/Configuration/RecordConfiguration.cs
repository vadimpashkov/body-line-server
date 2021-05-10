using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Infrastructure.Data.Configuration
{
    public class RecordConfiguration : IEntityTypeConfiguration<Record>
    {
        public void Configure(EntityTypeBuilder<Record> builder)
        {
            builder.HasKey(x => x.Id);

            builder
                .HasOne(x => x.MassageType)
                .WithMany()
                .HasForeignKey(x => x.MassageTypeId)
                .IsRequired();

            builder
                .HasOne(x => x.User)
                .WithMany()
                .HasForeignKey(x => x.UserId)
                .IsRequired();

            builder
                .HasOne(x => x.Messeur)
                .WithMany()
                .HasForeignKey(x => x.MesseurId)
                .IsRequired();

            builder.Property(x => x.Cancelled).IsRequired();
        }
    }
}
