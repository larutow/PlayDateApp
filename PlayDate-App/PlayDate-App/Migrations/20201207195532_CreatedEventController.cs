using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayDate_App.Migrations
{
    public partial class CreatedEventController : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6cda8712-a79f-437b-81bb-0c7453bc8899");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9aea44e7-2de0-4f92-b6fd-09aa69019107", "e49b616c-0d7c-4654-8062-22e6cb567832", "Parent", "PARENT" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9aea44e7-2de0-4f92-b6fd-09aa69019107");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6cda8712-a79f-437b-81bb-0c7453bc8899", "56d6b411-9d5c-4989-8f43-3c03505330f1", "Parent", "PARENT" });
        }
    }
}
