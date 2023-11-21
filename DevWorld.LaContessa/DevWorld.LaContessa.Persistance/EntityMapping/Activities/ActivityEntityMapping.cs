using DevWorld.LaContessa.Domain.Entities.Activities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace DevWorld.LaContessa.Persistance.EntityMapping.Activities;

[ExcludeFromCodeCoverage]
public class ActivityEntityMapping : IEntityTypeConfiguration<Activity>
{
    public void Configure(EntityTypeBuilder<Activity> builder)
    {
        builder.ToTable("activities");

        builder.Property<DateTime>("InsertRecordDateTimeUtc");
        builder.Property<DateTime>("UpdateRecordDateTimeUtc");
    }
}