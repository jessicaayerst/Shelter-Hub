using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ShelterHub.Migrations
{
    public partial class AddedDataProperty : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "ClientImage",
                table: "Clients",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "886980a3-5422-46a0-bdbf-cfb004d84f2b", "AQAAAAEAACcQAAAAEAa4ulwZIWL3p6+N/pTP8A3C7hZLt3cpXnP71tVA36DoHndhA6X9JTyglM55qwjYYA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ClientImage",
                table: "Clients");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "717bb5ef-6ef3-4214-ab83-de850a9cdf06", "AQAAAAEAACcQAAAAEHcW9iSqU9SztGrkCeB76BTcvKuy9/czSMVCqQY+kGrqJ7FxrUmdRc8q53IyqsAOAA==" });
        }
    }
}
