using DevWorld.LaContessa.Domain.Entities.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Diagnostics.CodeAnalysis;

namespace DevWorld.LaContessa.Persistance.EntityMapping.Subscriptions;

[ExcludeFromCodeCoverage]
public class SubscriptionEntityMapping : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.ToTable("subscriptions");

        builder.Property<DateTime>("InsertRecordDateTimeUtc");
        builder.Property<DateTime>("UpdateRecordDateTimeUtc");
    }
}