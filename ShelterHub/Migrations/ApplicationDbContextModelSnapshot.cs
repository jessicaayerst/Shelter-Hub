﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using ShelterHub.Data;

namespace ShelterHub.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex")
                        .HasFilter("[NormalizedName] IS NOT NULL");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("int");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("bit");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("bit");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("datetimeoffset");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("bit");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("bit");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(256)")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex")
                        .HasFilter("[NormalizedUserName] IS NOT NULL");

                    b.ToTable("AspNetUsers");

                    b.HasDiscriminator<string>("Discriminator").HasValue("IdentityUser");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("ClaimType")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("RoleId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(128)")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("ShelterHub.Models.Client", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("AssignedStaff")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime?>("DepartDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmergencyName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("EmergencyPhone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("FirstName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("IntakeComplete")
                        .HasColumnType("bit");

                    b.Property<DateTime>("IntakeDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("LastName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("OkToText")
                        .HasColumnType("bit");

                    b.Property<string>("Phone")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("RoomNumber")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Clients");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            AssignedStaff = "Dreama South",
                            Email = "sr22@gmail.com",
                            EmergencyName = "Donna Rodriguez",
                            EmergencyPhone = "333-444-3333",
                            FirstName = "Stephanie",
                            IntakeComplete = true,
                            IntakeDate = new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "Rodriguez",
                            OkToText = true,
                            Phone = "381-999-0000",
                            RoomNumber = 3,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            Id = 2,
                            AssignedStaff = "Dreama South",
                            Email = "jenny@gmail.com",
                            EmergencyName = "Freda Lalo",
                            EmergencyPhone = "318-784-8596",
                            FirstName = "Jennifer",
                            IntakeComplete = true,
                            IntakeDate = new DateTime(2019, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "Hogsten",
                            OkToText = true,
                            Phone = "315-874-2568",
                            RoomNumber = 5,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            Id = 3,
                            AssignedStaff = "Tammy Johnson",
                            Email = "ham@gmail.com",
                            EmergencyName = "Susan Hammerstein",
                            EmergencyPhone = "451-874-8563",
                            FirstName = "Nalah",
                            IntakeComplete = false,
                            IntakeDate = new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "Hammerstein",
                            OkToText = true,
                            Phone = "381-999-2222",
                            RoomNumber = 3,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            Id = 4,
                            AssignedStaff = "Dreama South",
                            Email = "judy@gmail.com",
                            EmergencyName = "Hank Smith",
                            EmergencyPhone = "608-874-8547",
                            FirstName = "Judy",
                            IntakeComplete = true,
                            IntakeDate = new DateTime(2019, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "Jetson",
                            OkToText = true,
                            Phone = "381258-8445",
                            RoomNumber = 2,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            Id = 5,
                            AssignedStaff = "Tammy Johnson",
                            Email = "mel@gmail.com",
                            EmergencyName = "Jimmy John",
                            EmergencyPhone = "606-987-8755",
                            FirstName = "Melissa",
                            IntakeComplete = true,
                            IntakeDate = new DateTime(2020, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "Thompson",
                            OkToText = true,
                            Phone = "606-896-5987",
                            RoomNumber = 1,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            Id = 6,
                            AssignedStaff = "Tammy Johnson",
                            Email = "star@gmail.com",
                            EmergencyName = "Jordan Stinson",
                            EmergencyPhone = "360-928-5689",
                            FirstName = "Star",
                            IntakeComplete = true,
                            IntakeDate = new DateTime(2019, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "Smith",
                            OkToText = true,
                            Phone = "308-784-5555",
                            RoomNumber = 3,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            Id = 7,
                            AssignedStaff = "Tammy Johnson",
                            Email = "jess@gmail.com",
                            EmergencyName = "John Smith",
                            EmergencyPhone = "355-568-9999",
                            FirstName = "Jessica",
                            IntakeComplete = false,
                            IntakeDate = new DateTime(2020, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "Jackson",
                            OkToText = false,
                            Phone = "381-857-2356",
                            RoomNumber = 2,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            Id = 8,
                            AssignedStaff = "Dreama South",
                            DepartDate = new DateTime(2019, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            Email = "susie1@gmail.com",
                            EmergencyName = "Sammy Robinson",
                            EmergencyPhone = "389-874-5625",
                            FirstName = "Susan",
                            IntakeComplete = true,
                            IntakeDate = new DateTime(2019, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            LastName = "Robinson",
                            OkToText = true,
                            Phone = "381-765-8412",
                            RoomNumber = 5,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        });
                });

            modelBuilder.Entity("ShelterHub.Models.ClientGroup", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<int>("GroupId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("GroupId");

                    b.ToTable("ClientGroups");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ClientId = 1,
                            GroupId = 2
                        },
                        new
                        {
                            Id = 2,
                            ClientId = 2,
                            GroupId = 2
                        },
                        new
                        {
                            Id = 3,
                            ClientId = 3,
                            GroupId = 4
                        },
                        new
                        {
                            Id = 4,
                            ClientId = 4,
                            GroupId = 3
                        },
                        new
                        {
                            Id = 5,
                            ClientId = 3,
                            GroupId = 1
                        },
                        new
                        {
                            Id = 6,
                            ClientId = 7,
                            GroupId = 1
                        },
                        new
                        {
                            Id = 7,
                            ClientId = 7,
                            GroupId = 4
                        },
                        new
                        {
                            Id = 8,
                            ClientId = 1,
                            GroupId = 3
                        });
                });

            modelBuilder.Entity("ShelterHub.Models.ClientStep", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<int>("ClientId")
                        .HasColumnType("int");

                    b.Property<DateTime?>("DateCompleted")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateStarted")
                        .HasColumnType("datetime2");

                    b.Property<int>("StepId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ClientId");

                    b.HasIndex("StepId");

                    b.ToTable("ClientSteps");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            ClientId = 1,
                            DateStarted = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StepId = 2
                        },
                        new
                        {
                            Id = 2,
                            ClientId = 2,
                            DateStarted = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StepId = 1
                        },
                        new
                        {
                            Id = 3,
                            ClientId = 3,
                            DateStarted = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StepId = 4
                        },
                        new
                        {
                            Id = 4,
                            ClientId = 3,
                            DateStarted = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StepId = 6
                        },
                        new
                        {
                            Id = 5,
                            ClientId = 5,
                            DateStarted = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StepId = 3
                        },
                        new
                        {
                            Id = 6,
                            ClientId = 7,
                            DateStarted = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StepId = 7
                        },
                        new
                        {
                            Id = 7,
                            ClientId = 7,
                            DateStarted = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StepId = 8
                        },
                        new
                        {
                            Id = 8,
                            ClientId = 1,
                            DateStarted = new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                            StepId = 5
                        });
                });

            modelBuilder.Entity("ShelterHub.Models.Group", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("DayOfWeek")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("GroupName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("MaxAttendees")
                        .HasColumnType("int");

                    b.Property<string>("Place")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Time")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("Groups");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            DayOfWeek = "Thursdays",
                            GroupName = "Substance Abuse Recovery",
                            MaxAttendees = 20,
                            Place = "Park Avenue Christian Church",
                            Time = "6:oopm",
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            Id = 2,
                            DayOfWeek = "Mondays",
                            GroupName = "Finances and Budgeting",
                            MaxAttendees = 12,
                            Place = "Hope Shelter Library",
                            Time = "12:oopm",
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            Id = 3,
                            DayOfWeek = "Tuesdays",
                            GroupName = "Parenting During Difficult Times",
                            MaxAttendees = 15,
                            Place = "Park Avenue Christian Church",
                            Time = "3:oopm",
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            Id = 4,
                            DayOfWeek = "Thursdays",
                            GroupName = "Resume Writing And Job Applications",
                            MaxAttendees = 20,
                            Place = "Downtown Library 3rd Floor",
                            Time = "09:00am",
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            Id = 5,
                            DayOfWeek = "Saturdays",
                            GroupName = "Narcotics Anonymous",
                            MaxAttendees = 100,
                            Place = "Forest Park Presbytarian Church",
                            Time = "6:oopm",
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        });
                });

            modelBuilder.Entity("ShelterHub.Models.Step", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("IsArchived")
                        .HasColumnType("bit");

                    b.Property<string>("StepName")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("StepTypeId")
                        .HasColumnType("int");

                    b.Property<string>("UserId")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("StepTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Steps");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            IsArchived = false,
                            StepName = "Apply for HUD Assistance",
                            StepTypeId = 1,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            Id = 2,
                            IsArchived = false,
                            StepName = "Search for House or Apartment",
                            StepTypeId = 1,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            Id = 3,
                            IsArchived = false,
                            StepName = "Apply for Jobs",
                            StepTypeId = 2,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            Id = 4,
                            IsArchived = false,
                            StepName = "Seach for Jobs",
                            StepTypeId = 2,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            Id = 5,
                            IsArchived = false,
                            StepName = "Apply for Birth Certificate",
                            StepTypeId = 3,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            Id = 6,
                            IsArchived = false,
                            StepName = "Apply for Driver's License or ID",
                            StepTypeId = 3,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            Id = 7,
                            IsArchived = false,
                            StepName = "Apply for Social Security Disability",
                            StepTypeId = 4,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        },
                        new
                        {
                            Id = 8,
                            IsArchived = false,
                            StepName = "Apply for Food Stamps",
                            StepTypeId = 4,
                            UserId = "00000000-ffff-ffff-ffff-ffffffffffff"
                        });
                });

            modelBuilder.Entity("ShelterHub.Models.StepType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("StepTypeName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("StepTypes");

                    b.HasData(
                        new
                        {
                            Id = 1,
                            StepTypeName = "Housing"
                        },
                        new
                        {
                            Id = 2,
                            StepTypeName = "Employment"
                        },
                        new
                        {
                            Id = 3,
                            StepTypeName = "Documentation"
                        },
                        new
                        {
                            Id = 4,
                            StepTypeName = "Other Services"
                        });
                });

            modelBuilder.Entity("ShelterHub.Models.ApplicationUser", b =>
                {
                    b.HasBaseType("Microsoft.AspNetCore.Identity.IdentityUser");

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ShelterName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("ApplicationUser");

                    b.HasData(
                        new
                        {
                            Id = "00000000-ffff-ffff-ffff-ffffffffffff",
                            AccessFailedCount = 0,
                            ConcurrencyStamp = "717bb5ef-6ef3-4214-ab83-de850a9cdf06",
                            Email = "admin@admin.com",
                            EmailConfirmed = true,
                            LockoutEnabled = false,
                            NormalizedEmail = "ADMIN@ADMIN.COM",
                            NormalizedUserName = "ADMIN@ADMIN.COM",
                            PasswordHash = "AQAAAAEAACcQAAAAEHcW9iSqU9SztGrkCeB76BTcvKuy9/czSMVCqQY+kGrqJ7FxrUmdRc8q53IyqsAOAA==",
                            PhoneNumberConfirmed = false,
                            SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794577",
                            TwoFactorEnabled = false,
                            UserName = "admin@admin.com",
                            Address = "123 Main St., Huntington, WV",
                            ShelterName = "Hope Shelter"
                        });
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ShelterHub.Models.Client", b =>
                {
                    b.HasOne("ShelterHub.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ShelterHub.Models.ClientGroup", b =>
                {
                    b.HasOne("ShelterHub.Models.Client", "Client")
                        .WithMany("ClientGroups")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShelterHub.Models.Group", "Group")
                        .WithMany("ClientGroups")
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ShelterHub.Models.ClientStep", b =>
                {
                    b.HasOne("ShelterHub.Models.Client", "Client")
                        .WithMany("ClientSteps")
                        .HasForeignKey("ClientId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShelterHub.Models.Step", "Step")
                        .WithMany("ClientSteps")
                        .HasForeignKey("StepId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("ShelterHub.Models.Group", b =>
                {
                    b.HasOne("ShelterHub.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("ShelterHub.Models.Step", b =>
                {
                    b.HasOne("ShelterHub.Models.StepType", "StepType")
                        .WithMany()
                        .HasForeignKey("StepTypeId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("ShelterHub.Models.ApplicationUser", "User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
#pragma warning restore 612, 618
        }
    }
}
