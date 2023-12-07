#nullable disable

using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace DevWorld.LaContessa.Persistance.Migrations.MigrationsScripts;

/// <inheritdoc />
public partial class updatedEntities : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            "Activities",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                Name = table.Column<string>("text", nullable: false),
                IsOutdoor = table.Column<bool>("boolean", nullable: false),
                Description = table.Column<string>("text", nullable: false),
                ActivityImg = table.Column<string>("text", nullable: false),
                IsDeleted = table.Column<bool>("boolean", nullable: false),
                DeletedAt = table.Column<DateTimeOffset>("timestamp with time zone", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_Activities", x => x.Id); });

        migrationBuilder.CreateTable(
            "Bookings",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                UserId = table.Column<string>("text", nullable: false),
                Date = table.Column<string>("text", nullable: false),
                activityID = table.Column<string>("text", nullable: false),
                timeSlot = table.Column<string>("text", nullable: false),
                bookingName = table.Column<string>("text", nullable: false),
                phoneNumber = table.Column<string>("text", nullable: false),
                price = table.Column<double>("double precision", nullable: false),
                IsLesson = table.Column<bool>("boolean", nullable: false),
                IsDeleted = table.Column<bool>("boolean", nullable: false),
                DeletedAt = table.Column<DateTimeOffset>("timestamp with time zone", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_Bookings", x => x.Id); });

        migrationBuilder.CreateTable(
            "Subscriptions",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                UserId = table.Column<string>("text", nullable: false),
                CardNumber = table.Column<int>("integer", nullable: false),
                Valid = table.Column<bool>("boolean", nullable: false),
                ExpirationDate = table.Column<string>("text", nullable: false),
                SubscriptionType = table.Column<string>("text", nullable: false),
                IsDeleted = table.Column<bool>("boolean", nullable: false),
                DeletedAt = table.Column<DateTimeOffset>("timestamp with time zone", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_Subscriptions", x => x.Id); });

        migrationBuilder.CreateTable(
            "Users",
            table => new
            {
                Id = table.Column<Guid>("uuid", nullable: false),
                Name = table.Column<string>("text", nullable: false),
                Surname = table.Column<string>("text", nullable: false),
                CardNumber = table.Column<string>("text", nullable: false),
                IsPro = table.Column<bool>("boolean", nullable: false),
                Email = table.Column<string>("text", nullable: false),
                Password = table.Column<string>("text", nullable: false),
                ImageProfile = table.Column<string>("text", nullable: false),
                IsDeleted = table.Column<bool>("boolean", nullable: false),
                DeletedAt = table.Column<DateTimeOffset>("timestamp with time zone", nullable: true)
            },
            constraints: table => { table.PrimaryKey("PK_Users", x => x.Id); });

        migrationBuilder.CreateTable(
            "ActivityDate",
            table => new
            {
                ActivityId = table.Column<Guid>("uuid", nullable: false),
                Id = table.Column<int>("integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Date = table.Column<string>("text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ActivityDate", x => new { x.ActivityId, x.Id });
                table.ForeignKey(
                    "FK_ActivityDate_Activities_ActivityId",
                    x => x.ActivityId,
                    "Activities",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "Service",
            table => new
            {
                ActivityId = table.Column<Guid>("uuid", nullable: false),
                Id = table.Column<int>("integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                Icon = table.Column<string>("text", nullable: false),
                ServiceName = table.Column<string>("text", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Service", x => new { x.ActivityId, x.Id });
                table.ForeignKey(
                    "FK_Service_Activities_ActivityId",
                    x => x.ActivityId,
                    "Activities",
                    "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateTable(
            "ActivityTimeSlot",
            table => new
            {
                ActivityDateActivityId = table.Column<Guid>("uuid", nullable: false),
                ActivityDateId = table.Column<int>("integer", nullable: false),
                Id = table.Column<int>("integer", nullable: false)
                    .Annotation("Npgsql:ValueGenerationStrategy",
                        NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                TimeSlot = table.Column<string>("text", nullable: false),
                IsAlreadyBooked = table.Column<bool>("boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_ActivityTimeSlot", x => new { x.ActivityDateActivityId, x.ActivityDateId, x.Id });
                table.ForeignKey(
                    "FK_ActivityTimeSlot_ActivityDate_ActivityDateActivityId_Activi~",
                    x => new { x.ActivityDateActivityId, x.ActivityDateId },
                    "ActivityDate",
                    new[] { "ActivityId", "Id" },
                    onDelete: ReferentialAction.Cascade);
            });
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            "ActivityTimeSlot");

        migrationBuilder.DropTable(
            "Bookings");

        migrationBuilder.DropTable(
            "Service");

        migrationBuilder.DropTable(
            "Subscriptions");

        migrationBuilder.DropTable(
            "Users");

        migrationBuilder.DropTable(
            "ActivityDate");

        migrationBuilder.DropTable(
            "Activities");
    }
}