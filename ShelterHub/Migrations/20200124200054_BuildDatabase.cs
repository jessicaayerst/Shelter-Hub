using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShelterHub.Migrations
{
    public partial class BuildDatabase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Discriminator", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName", "Address", "ShelterName" },
                values: new object[] { "00000000-ffff-ffff-ffff-ffffffffffff", 0, "26f7ffe0-3a43-4ce1-9d8c-283d70f37aef", "ApplicationUser", "admin@admin.com", true, false, null, "ADMIN@ADMIN.COM", "ADMIN@ADMIN.COM", "AQAAAAEAACcQAAAAEFY3mo9dBk6FzwN735CwoBomd1fPY6RdTOwuz2QYhPg55oCGqbbri25C5E0EeF6l0A==", null, false, "7f434309-a4d9-48e9-9ebb-8803db794577", false, "admin@admin.com", "123 Main St., Huntington, WV", "Hope Shelter" });

            migrationBuilder.InsertData(
                table: "StepTypes",
                columns: new[] { "Id", "StepTypeName" },
                values: new object[,]
                {
                    { 1, "Housing" },
                    { 2, "Employment" },
                    { 3, "Documentation" },
                    { 4, "Other Services" }
                });

            migrationBuilder.InsertData(
                table: "Clients",
                columns: new[] { "Id", "AssignedStaff", "DepartDate", "Email", "EmergencyName", "EmergencyPhone", "FirstName", "IntakeComplete", "IntakeDate", "LastName", "OkToText", "Phone", "RoomNumber", "UserId" },
                values: new object[,]
                {
                    { 1, "Dreama South", null, "sr22@gmail.com", "Donna Rodriguez", "333-444-3333", "Stephanie", true, new DateTime(2020, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Rodriguez", true, "381-999-0000", 3, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 2, "Dreama South", null, "jenny@gmail.com", "Freda Lalo", "318-784-8596", "Jennifer", true, new DateTime(2019, 9, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hogsten", true, "315-874-2568", 5, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 3, "Tammy Johnson", null, "ham@gmail.com", "Susan Hammerstein", "451-874-8563", "Nalah", false, new DateTime(2020, 1, 15, 0, 0, 0, 0, DateTimeKind.Unspecified), "Hammerstein", true, "381-999-2222", 3, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 4, "Dreama South", null, "judy@gmail.com", "Hank Smith", "608-874-8547", "Judy", true, new DateTime(2019, 6, 25, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jetson", true, "381258-8445", 2, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 5, "Tammy Johnson", null, "mel@gmail.com", "Jimmy John", "606-987-8755", "Melissa", true, new DateTime(2020, 1, 18, 0, 0, 0, 0, DateTimeKind.Unspecified), "Thompson", true, "606-896-5987", 1, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 6, "Tammy Johnson", null, "star@gmail.com", "Jordan Stinson", "360-928-5689", "Star", true, new DateTime(2019, 11, 11, 0, 0, 0, 0, DateTimeKind.Unspecified), "Smith", true, "308-784-5555", 3, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 7, "Tammy Johnson", null, "jess@gmail.com", "John Smith", "355-568-9999", "Jessica", false, new DateTime(2020, 1, 6, 0, 0, 0, 0, DateTimeKind.Unspecified), "Jackson", false, "381-857-2356", 2, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 8, "Dreama South", new DateTime(2019, 1, 10, 0, 0, 0, 0, DateTimeKind.Unspecified), "susie1@gmail.com", "Sammy Robinson", "389-874-5625", "Susan", true, new DateTime(2019, 10, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "Robinson", true, "381-765-8412", 5, "00000000-ffff-ffff-ffff-ffffffffffff" }
                });

            migrationBuilder.InsertData(
                table: "Groups",
                columns: new[] { "Id", "DayOfWeek", "GroupName", "MaxAttendees", "Place", "Time", "UserId" },
                values: new object[,]
                {
                    { 5, "Saturdays", "Narcotics Anonymous", 100, "Forest Park Presbytarian Church", "6:oopm", "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 4, "Thursdays", "Resume Writing And Job Applications", 20, "Downtown Library 3rd Floor", "09:00am", "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 3, "Tuesdays", "Parenting During Difficult Times", 15, "Park Avenue Christian Church", "3:oopm", "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 2, "Mondays", "Finances and Budgeting", 12, "Hope Shelter Library", "12:oopm", "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 1, "Thursdays", "Substance Abuse Recovery", 20, "Park Avenue Christian Church", "6:oopm", "00000000-ffff-ffff-ffff-ffffffffffff" }
                });

            migrationBuilder.InsertData(
                table: "Steps",
                columns: new[] { "Id", "IsArchived", "StepName", "StepTypeId", "UserId" },
                values: new object[,]
                {
                    { 1, false, "Apply for HUD Assistance", 1, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 2, false, "Search for House or Apartment", 1, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 3, false, "Apply for Jobs", 2, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 4, false, "Seach for Jobs", 2, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 5, false, "Apply for Birth Certificate", 3, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 6, false, "Apply for Driver's License or ID", 3, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 7, false, "Apply for Social Security Disability", 4, "00000000-ffff-ffff-ffff-ffffffffffff" },
                    { 8, false, "Apply for Food Stamps", 4, "00000000-ffff-ffff-ffff-ffffffffffff" }
                });

            migrationBuilder.InsertData(
                table: "ClientGroups",
                columns: new[] { "Id", "ClientId", "GroupId" },
                values: new object[,]
                {
                    { 5, 3, 1 },
                    { 6, 7, 1 },
                    { 1, 1, 2 },
                    { 2, 2, 2 },
                    { 4, 4, 3 },
                    { 8, 1, 3 },
                    { 3, 3, 4 },
                    { 7, 7, 4 }
                });

            migrationBuilder.InsertData(
                table: "ClientSteps",
                columns: new[] { "Id", "ClientId", "DateCompleted", "DateStarted", "StepId" },
                values: new object[,]
                {
                    { 2, 2, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 1 },
                    { 1, 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 2 },
                    { 5, 5, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 3 },
                    { 3, 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 4 },
                    { 8, 1, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 5 },
                    { 4, 3, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 6 },
                    { 6, 7, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 7 },
                    { 7, 7, null, new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), 8 }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "ClientGroups",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ClientGroups",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ClientGroups",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ClientGroups",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ClientGroups",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ClientGroups",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ClientGroups",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ClientGroups",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "ClientSteps",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "ClientSteps",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "ClientSteps",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "ClientSteps",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "ClientSteps",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "ClientSteps",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "ClientSteps",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "ClientSteps",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Clients",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Groups",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Steps",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff");

            migrationBuilder.DeleteData(
                table: "StepTypes",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "StepTypes",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "StepTypes",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "StepTypes",
                keyColumn: "Id",
                keyValue: 4);
        }
    }
}
