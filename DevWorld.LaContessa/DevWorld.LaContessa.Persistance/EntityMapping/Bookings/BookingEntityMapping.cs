using DevWorld.LaContessa.Domain.Entities.Bookings; 
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace DevWorld.LaContessa.Persistance.EntityMapping.Bookings; 

[ExcludeFromCodeCoverage]
public class BookingEntityMapping : IEntityTypeConfiguration<Booking> 
{
    public void Configure(EntityTypeBuilder<Booking> builder) 
    {
        builder.ToTable("bookings"); 

        builder.Property<DateTime>("InsertRecordDateTimeUtc");
        builder.Property<DateTime>("UpdateRecordDateTimeUtc");
    }
}