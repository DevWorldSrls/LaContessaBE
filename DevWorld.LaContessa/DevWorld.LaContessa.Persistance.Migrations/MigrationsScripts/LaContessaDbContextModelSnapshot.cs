﻿// <auto-generated />
using System;
using DevWorld.LaContessa.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace DevWorld.LaContessa.Persistance.Migrations.MigrationsScripts
{
    [DbContext(typeof(LaContessaDbContext))]
    partial class LaContessaDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("DevWorld.LaContessa.Domain.Entities.Activities.Activity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("ActivityImg")
                        .HasColumnType("text");

                    b.Property<int>("BookingType")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<string>("Duration")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ExpirationDate")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsOutdoor")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsSubscriptionRequired")
                        .HasColumnType("boolean");

                    b.Property<int?>("Limit")
                        .HasColumnType("integer");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<double?>("Price")
                        .HasColumnType("double precision");

                    b.HasKey("Id");

                    b.ToTable("Activities");
                });

            modelBuilder.Entity("DevWorld.LaContessa.Domain.Entities.Banners.Banner", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("BannerImg")
                        .HasColumnType("text");

                    b.Property<string>("BannerImgExt")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Description")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Banners");
                });

            modelBuilder.Entity("DevWorld.LaContessa.Domain.Entities.Bookings.Booking", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ActivityId")
                        .HasColumnType("uuid");

                    b.Property<string>("BookingName")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<long>("BookingPrice")
                        .HasColumnType("bigint");

                    b.Property<string>("Date")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsLesson")
                        .HasColumnType("boolean");

                    b.Property<string>("PaymentIntentId")
                        .HasColumnType("text");

                    b.Property<long?>("PaymentPrice")
                        .HasColumnType("bigint");

                    b.Property<string>("PhoneNumber")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<int>("Status")
                        .HasColumnType("integer");

                    b.Property<string>("TimeSlot")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.HasIndex("UserId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("DevWorld.LaContessa.Domain.Entities.Subscriptions.Subscription", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ActivityId")
                        .HasColumnType("uuid");

                    b.Property<int?>("CardNumber")
                        .HasColumnType("integer");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("ExpirationDate")
                        .HasColumnType("text");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsValid")
                        .HasColumnType("boolean");

                    b.Property<string>("MedicalCertificateDueDate")
                        .HasColumnType("text");

                    b.Property<bool>("MedicalCertificateExpired")
                        .HasColumnType("boolean");

                    b.Property<int?>("NumberOfIngress")
                        .HasColumnType("integer");

                    b.Property<int>("SubType")
                        .HasColumnType("integer");

                    b.Property<long?>("SubscriptionPrice")
                        .HasColumnType("bigint");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.HasKey("Id");

                    b.HasIndex("ActivityId");

                    b.HasIndex("UserId");

                    b.ToTable("Subscriptions");
                });

            modelBuilder.Entity("DevWorld.LaContessa.Domain.Entities.Users.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("AppleUserId")
                        .HasColumnType("text");

                    b.Property<string>("CardNumber")
                        .HasColumnType("text");

                    b.Property<string>("CustomerId")
                        .HasColumnType("text");

                    b.Property<DateTimeOffset?>("DeletedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GoogleUserId")
                        .HasColumnType("text");

                    b.Property<string>("ImageProfile")
                        .HasColumnType("text");

                    b.Property<bool>("IsAdmin")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsDeleted")
                        .HasColumnType("boolean");

                    b.Property<bool>("IsPro")
                        .HasColumnType("boolean");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("PaymentMethodId")
                        .HasColumnType("text");

                    b.Property<bool>("PeriodicBookingsEnabled")
                        .HasColumnType("boolean");

                    b.Property<string>("RefreshToken")
                        .HasColumnType("text");

                    b.Property<string>("Surname")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("DevWorld.LaContessa.Domain.Entities.Activities.Activity", b =>
                {
                    b.OwnsMany("DevWorld.LaContessa.Domain.Entities.Activities.ActivityDate", "DateList", b1 =>
                        {
                            b1.Property<Guid>("ActivityId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<string>("Date")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("ActivityId", "Id");

                            b1.ToTable("ActivityDate");

                            b1.WithOwner()
                                .HasForeignKey("ActivityId");

                            b1.OwnsMany("DevWorld.LaContessa.Domain.Entities.Activities.ActivityTimeSlot", "TimeSlotList", b2 =>
                                {
                                    b2.Property<Guid>("ActivityDateActivityId")
                                        .HasColumnType("uuid");

                                    b2.Property<int>("ActivityDateId")
                                        .HasColumnType("integer");

                                    b2.Property<int>("Id")
                                        .ValueGeneratedOnAdd()
                                        .HasColumnType("integer");

                                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b2.Property<int>("Id"));

                                    b2.Property<bool>("IsAlreadyBooked")
                                        .HasColumnType("boolean");

                                    b2.Property<string>("TimeSlot")
                                        .IsRequired()
                                        .HasColumnType("text");

                                    b2.HasKey("ActivityDateActivityId", "ActivityDateId", "Id");

                                    b2.ToTable("ActivityTimeSlot");

                                    b2.WithOwner()
                                        .HasForeignKey("ActivityDateActivityId", "ActivityDateId");
                                });

                            b1.Navigation("TimeSlotList");
                        });

                    b.OwnsMany("DevWorld.LaContessa.Domain.Entities.Activities.ActivityVariant", "ActivityVariants", b1 =>
                        {
                            b1.Property<Guid>("ActivityId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<double>("Price")
                                .HasColumnType("double precision");

                            b1.Property<string>("Variant")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("ActivityId", "Id");

                            b1.ToTable("ActivityVariant");

                            b1.WithOwner()
                                .HasForeignKey("ActivityId");
                        });

                    b.OwnsMany("DevWorld.LaContessa.Domain.Entities.Activities.Service", "ServiceList", b1 =>
                        {
                            b1.Property<Guid>("ActivityId")
                                .HasColumnType("uuid");

                            b1.Property<int>("Id")
                                .ValueGeneratedOnAdd()
                                .HasColumnType("integer");

                            NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b1.Property<int>("Id"));

                            b1.Property<string>("Icon")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.Property<string>("ServiceName")
                                .IsRequired()
                                .HasColumnType("text");

                            b1.HasKey("ActivityId", "Id");

                            b1.ToTable("Service");

                            b1.WithOwner()
                                .HasForeignKey("ActivityId");
                        });

                    b.Navigation("ActivityVariants");

                    b.Navigation("DateList");

                    b.Navigation("ServiceList");
                });

            modelBuilder.Entity("DevWorld.LaContessa.Domain.Entities.Bookings.Booking", b =>
                {
                    b.HasOne("DevWorld.LaContessa.Domain.Entities.Activities.Activity", "Activity")
                        .WithMany()
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DevWorld.LaContessa.Domain.Entities.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");

                    b.Navigation("User");
                });

            modelBuilder.Entity("DevWorld.LaContessa.Domain.Entities.Subscriptions.Subscription", b =>
                {
                    b.HasOne("DevWorld.LaContessa.Domain.Entities.Activities.Activity", "Activity")
                        .WithMany()
                        .HasForeignKey("ActivityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("DevWorld.LaContessa.Domain.Entities.Users.User", "User")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Activity");

                    b.Navigation("User");
                });
#pragma warning restore 612, 618
        }
    }
}
