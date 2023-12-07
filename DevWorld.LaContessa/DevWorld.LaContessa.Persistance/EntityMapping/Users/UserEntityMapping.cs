using System.Diagnostics.CodeAnalysis;
using DevWorld.LaContessa.Domain.Entities.Users;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DevWorld.LaContessa.Persistance.EntityMapping.Users;

[ExcludeFromCodeCoverage]
public class UserEntityMapping : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.ToTable("users");

        builder.Property<DateTime>("InsertRecordDateTimeUtc");
        builder.Property<DateTime>("UpdateRecordDateTimeUtc");
    }
}
