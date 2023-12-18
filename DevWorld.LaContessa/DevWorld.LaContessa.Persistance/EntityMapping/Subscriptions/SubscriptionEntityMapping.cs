using System.Diagnostics.CodeAnalysis;
using DevWorld.LaContessa.Domain.Entities.Subscriptions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevWorld.LaContessa.Persistance.EntityMapping.Subscriptions;

[ExcludeFromCodeCoverage]
public class SubscriptionEntityMapping : IEntityTypeConfiguration<Subscription>
{
    public void Configure(EntityTypeBuilder<Subscription> builder)
    {
        builder.ToTable("subscriptions");

        builder.HasOne(x => x.User).WithMany();

        builder.Property<DateTime>("InsertRecordDateTimeUtc");
        builder.Property<DateTime>("UpdateRecordDateTimeUtc");
    }
}