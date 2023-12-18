using System.Diagnostics.CodeAnalysis;
using DevWorld.LaContessa.Domain.Entities.Bookings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevWorld.LaContessa.Persistance.EntityMapping.Bookings;

[ExcludeFromCodeCoverage]
public class BookingEntityMapping : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.ToTable("bookings");

        builder.HasOne(x => x.User).WithMany();
        builder.HasOne(x => x.Activity).WithMany();

        builder.Property<DateTime>("InsertRecordDateTimeUtc");
        builder.Property<DateTime>("UpdateRecordDateTimeUtc");
    }
}