using Microsoft.EntityFrameworkCore.Migrations;

namespace ShelterHub.Migrations
{
    public partial class BuildTables2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Groups_Clients_ClientId",
                table: "Groups");

            migrationBuilder.DropForeignKey(
                name: "FK_Steps_Clients_ClientId",
                table: "Steps");

            migrationBuilder.DropForeignKey(
                name: "FK_Steps_Clients_ClientId1",
                table: "Steps");

            migrationBuilder.DropIndex(
                name: "IX_Steps_ClientId",
                table: "Steps");

            migrationBuilder.DropIndex(
                name: "IX_Steps_ClientId1",
                table: "Steps");

            migrationBuilder.DropIndex(
                name: "IX_Groups_ClientId",
                table: "Groups");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Steps");

            migrationBuilder.DropColumn(
                name: "ClientId1",
                table: "Steps");

            migrationBuilder.DropColumn(
                name: "ClientId",
                table: "Groups");

            migrationBuilder.CreateIndex(
                name: "IX_Steps_StepTypeId",
                table: "Steps",
                column: "StepTypeId");

            migrationBuilder.AddForeignKey(
                name: "FK_Steps_StepTypes_StepTypeId",
                table: "Steps",
                column: "StepTypeId",
                principalTable: "StepTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Steps_StepTypes_StepTypeId",
                table: "Steps");

            migrationBuilder.DropIndex(
                name: "IX_Steps_StepTypeId",
                table: "Steps");

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Steps",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientId1",
                table: "Steps",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ClientId",
                table: "Groups",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Steps_ClientId",
                table: "Steps",
                column: "ClientId");

            migrationBuilder.CreateIndex(
                name: "IX_Steps_ClientId1",
                table: "Steps",
                column: "ClientId1");

            migrationBuilder.CreateIndex(
                name: "IX_Groups_ClientId",
                table: "Groups",
                column: "ClientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Groups_Clients_ClientId",
                table: "Groups",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Steps_Clients_ClientId",
                table: "Steps",
                column: "ClientId",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Steps_Clients_ClientId1",
                table: "Steps",
                column: "ClientId1",
                principalTable: "Clients",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
