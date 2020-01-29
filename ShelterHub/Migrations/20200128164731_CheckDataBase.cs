using Microsoft.EntityFrameworkCore.Migrations;

namespace ShelterHub.Migrations
{
    public partial class CheckDataBase : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "717bb5ef-6ef3-4214-ab83-de850a9cdf06", "AQAAAAEAACcQAAAAEHcW9iSqU9SztGrkCeB76BTcvKuy9/czSMVCqQY+kGrqJ7FxrUmdRc8q53IyqsAOAA==" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "00000000-ffff-ffff-ffff-ffffffffffff",
                columns: new[] { "ConcurrencyStamp", "PasswordHash" },
                values: new object[] { "26f7ffe0-3a43-4ce1-9d8c-283d70f37aef", "AQAAAAEAACcQAAAAEFY3mo9dBk6FzwN735CwoBomd1fPY6RdTOwuz2QYhPg55oCGqbbri25C5E0EeF6l0A==" });
        }
    }
}
