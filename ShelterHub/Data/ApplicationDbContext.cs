using System;
using System.Collections.Generic;
using ShelterHub.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ShelterHub.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<ApplicationUser> Users { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Step> Steps { get; set; }
        public DbSet<StepType> StepTypes { get; set; }
        public DbSet<ClientGroup> ClientGroups { get; set; }
        public DbSet<ClientStep> ClientSteps { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //Create User
            ApplicationUser user = new ApplicationUser
            {
                UserName = "admin@admin.com",
                NormalizedUserName = "ADMIN@ADMIN.COM",
                Email = "admin@admin.com",
                NormalizedEmail = "ADMIN@ADMIN.COM",
                EmailConfirmed = true,
                LockoutEnabled = false,
                SecurityStamp = "7f434309-a4d9-48e9-9ebb-8803db794577",
                Id = "00000000-ffff-ffff-ffff-ffffffffffff",
                ShelterName = "Hope Shelter",
                Address = "123 Main St., Huntington, WV"
            };

            var passwordHash = new PasswordHasher<ApplicationUser>();
            user.PasswordHash = passwordHash.HashPassword(user, "Admin123!");
            modelBuilder.Entity<ApplicationUser>().HasData(user);

            //Create eight clients
            modelBuilder.Entity<Client>().HasData(
               new Client()
               {
                   Id = 1,
                   UserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                   FirstName = "Stephanie",
                   LastName = "Rodriguez",
                   Email = "sr22@gmail.com",
                   Phone = "381-999-0000",
                   OkToText = true,
                   EmergencyPhone = "333-444-3333",
                   EmergencyName = "Donna Rodriguez",
                   AssignedStaff = "Dreama South",
                   RoomNumber = 3,
                   IntakeComplete = true,
                   IntakeDate = new DateTime(2020, 01, 01),
                   DepartDate = null,
                   ClientImage = null
               },
               new Client()
               {
                   Id = 2,
                   UserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                   FirstName = "Jennifer",
                   LastName = "Hogsten",
                   Email = "jenny@gmail.com",
                   Phone = "315-874-2568",
                   OkToText = true,
                   EmergencyPhone = "318-784-8596",
                   EmergencyName = "Freda Lalo",
                   AssignedStaff = "Dreama South",
                   RoomNumber = 5,
                   IntakeComplete = true,
                   IntakeDate = new DateTime(2019, 09, 18),
                   DepartDate = null,
                   ClientImage = null
               },
               new Client()
               {
                   Id = 3,
                   UserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                   FirstName = "Nalah",
                   LastName = "Hammerstein",
                   Email = "ham@gmail.com",
                   Phone = "381-999-2222",
                   OkToText = true,
                   EmergencyPhone = "451-874-8563",
                   EmergencyName = "Susan Hammerstein",
                   AssignedStaff = "Tammy Johnson",
                   RoomNumber = 3,
                   IntakeComplete = false,
                   IntakeDate = new DateTime(2020, 01, 15),
                   DepartDate = null,
                   ClientImage = null
               },
               new Client()
               {
                   Id = 4,
                   UserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                   FirstName = "Judy",
                   LastName = "Jetson",
                   Email = "judy@gmail.com",
                   Phone = "381258-8445",
                   OkToText = true,
                   EmergencyPhone = "608-874-8547",
                   EmergencyName = "Hank Smith",
                   AssignedStaff = "Dreama South",
                   RoomNumber = 2,
                   IntakeComplete = true,
                   IntakeDate = new DateTime(2019, 06, 25),
                   DepartDate = null,
                   ClientImage = null
               },
               new Client()
               {
                   Id = 5,
                   UserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                   FirstName = "Melissa",
                   LastName = "Thompson",
                   Email = "mel@gmail.com",
                   Phone = "606-896-5987",
                   OkToText = true,
                   EmergencyPhone = "606-987-8755",
                   EmergencyName = "Jimmy John",
                   AssignedStaff = "Tammy Johnson",
                   RoomNumber = 1,
                   IntakeComplete = true,
                   IntakeDate = new DateTime(2020, 01, 18),
                   DepartDate = null,
                   ClientImage = null
               },
               new Client()
               {
                   Id = 6,
                   UserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                   FirstName = "Star",
                   LastName = "Smith",
                   Email = "star@gmail.com",
                   Phone = "308-784-5555",
                   OkToText = true,
                   EmergencyPhone = "360-928-5689",
                   EmergencyName = "Jordan Stinson",
                   AssignedStaff = "Tammy Johnson",
                   RoomNumber = 3,
                   IntakeComplete = true,
                   IntakeDate = new DateTime(2019, 11, 11),
                   DepartDate = null,
                   ClientImage = null
               },
               new Client()
               {
                   Id = 7,
                   UserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                   FirstName = "Jessica",
                   LastName = "Jackson",
                   Email = "jess@gmail.com",
                   Phone = "381-857-2356",
                   OkToText = false,
                   EmergencyPhone = "355-568-9999",
                   EmergencyName = "John Smith",
                   AssignedStaff = "Tammy Johnson",
                   RoomNumber = 2,
                   IntakeComplete = false,
                   IntakeDate = new DateTime(2020, 01, 06),
                   DepartDate = null,
                   ClientImage = null
               },
               new Client()
               {
                   Id = 8,
                   UserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                   FirstName = "Susan",
                   LastName = "Robinson",
                   Email = "susie1@gmail.com",
                   Phone = "381-765-8412",
                   OkToText = true,
                   EmergencyPhone = "389-874-5625",
                   EmergencyName = "Sammy Robinson",
                   AssignedStaff = "Dreama South",
                   RoomNumber = 5,
                   IntakeComplete = true,
                   IntakeDate = new DateTime(2019, 10, 01),
                   DepartDate = new DateTime(2019, 01, 10),
                   ClientImage = null
               }
               );

            //Create five groups
            modelBuilder.Entity<Group>().HasData(
               new Group()
               {
                   Id = 1,
                   UserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                   GroupName = "Substance Abuse Recovery",
                   MaxAttendees = 20,
                   Time = "6:oopm",
                   DayOfWeek = "Thursdays",
                   Place = "Park Avenue Christian Church"
               },
                new Group()
                {
                    Id = 2,
                    UserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                    GroupName = "Finances and Budgeting",
                    MaxAttendees = 12,
                    Time = "12:oopm",
                    DayOfWeek = "Mondays",
                    Place = "Hope Shelter Library"
                },
                 new Group()
                 {
                     Id = 3,
                     UserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                     GroupName = "Parenting During Difficult Times",
                     MaxAttendees = 15,
                     Time = "3:oopm",
                     DayOfWeek = "Tuesdays",
                     Place = "Park Avenue Christian Church"
                 },
                  new Group()
                  {
                      Id = 4,
                      UserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                      GroupName = "Resume Writing And Job Applications",
                      MaxAttendees = 20,
                      Time = "09:00am",
                      DayOfWeek = "Thursdays",
                      Place = "Downtown Library 3rd Floor"
                  },
                   new Group()
                   {
                       Id = 5,
                       UserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                       GroupName = "Narcotics Anonymous",
                       MaxAttendees = 100,
                       Time = "6:oopm",
                       DayOfWeek = "Saturdays",
                       Place = "Forest Park Presbytarian Church"
                   }
                   );

            //Create eight steps
            modelBuilder.Entity<Step>().HasData(
               new Step()
               {
                   Id = 1,
                   UserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                   StepName = "Apply for HUD Assistance",
                   IsArchived = false,
                   StepTypeId = 1
               },
                new Step()
                {
                    Id = 2,
                    UserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                    StepName = "Search for House or Apartment",
                    IsArchived = false,
                    StepTypeId = 1
                },
                 new Step()
                 {
                     Id = 3,
                     UserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                     StepName = "Apply for Jobs",
                     IsArchived = false,
                     StepTypeId = 2
                 },
                  new Step()
                  {
                      Id = 4,
                      UserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                      StepName = "Seach for Jobs",
                      IsArchived = false,
                      StepTypeId = 2
                  },
                   new Step()
                   {
                       Id = 5,
                       UserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                       StepName = "Apply for Birth Certificate",
                       IsArchived = false,
                       StepTypeId = 3
                   },
                    new Step()
                    {
                        Id = 6,
                        UserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                        StepName = "Apply for Driver's License or ID",
                        IsArchived = false,
                        StepTypeId = 3
                    },
                     new Step()
                     {
                         Id = 7,
                         UserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                         StepName = "Apply for Social Security Disability",
                         IsArchived = false,
                         StepTypeId = 4
                     },
                      new Step()
                      {
                          Id = 8,
                          UserId = "00000000-ffff-ffff-ffff-ffffffffffff",
                          StepName = "Apply for Food Stamps",
                          IsArchived = false,
                          StepTypeId = 4
                      }
                      );

            //Create eight clientgroups
            modelBuilder.Entity<ClientGroup>().HasData(
              new ClientGroup()
              {
                  Id = 1,
                  ClientId = 1,
                  GroupId = 2
              },
               new ClientGroup()
               {
                   Id = 2,
                   ClientId = 2,
                   GroupId = 2
               },
                new ClientGroup()
                {
                    Id = 3,
                    ClientId = 3,
                    GroupId = 4
                },
                 new ClientGroup()
                 {
                     Id = 4,
                     ClientId = 4,
                     GroupId = 3
                 },
                  new ClientGroup()
                  {
                      Id = 5,
                      ClientId = 3,
                      GroupId = 1
                  },
                   new ClientGroup()
                   {
                       Id = 6,
                       ClientId = 7,
                       GroupId = 1
                   },
                    new ClientGroup()
                    {
                        Id = 7,
                        ClientId = 7,
                        GroupId = 4
                    },
                     new ClientGroup()
                     {
                         Id = 8,
                         ClientId = 1,
                         GroupId = 3
                     }
                     );

            //Create eight clients steps
            modelBuilder.Entity<ClientStep>().HasData(
             new ClientStep()
             {
                 Id = 1,
                 ClientId = 1,
                 StepId = 2
             },
               new ClientStep()
               {
                   Id = 2,
                   ClientId = 2,
                   StepId = 1
               },
               new ClientStep()
               {
                   Id = 3,
                   ClientId = 3,
                   StepId = 4
               },
               new ClientStep()
               {
                   Id = 4,
                   ClientId = 3,
                   StepId = 6
               },
               new ClientStep()
               {
                   Id = 5,
                   ClientId = 5,
                   StepId = 3
               },
               new ClientStep()
               {
                   Id = 6,
                   ClientId = 7,
                   StepId = 7
               },
               new ClientStep()
               {
                   Id = 7,
                   ClientId = 7,
                   StepId = 8
               },
               new ClientStep()
               {
                   Id = 8,
                   ClientId = 1,
                   StepId = 5
               }
               );

            //Create four steptypes

            modelBuilder.Entity<StepType>().HasData(
             new StepType()
             {
                 Id = 1,
                 StepTypeName = "Housing"
             },
              new StepType()
              {
                  Id = 2,
                  StepTypeName = "Employment"
              },
               new StepType()
               {
                   Id = 3,
                   StepTypeName = "Documentation"
               },
                new StepType()
                {
                    Id = 4,
                    StepTypeName = "Other Services"
                }
                );
        }
    }
}
