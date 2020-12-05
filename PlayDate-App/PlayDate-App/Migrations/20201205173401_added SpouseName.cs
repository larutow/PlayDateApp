using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayDate_App.Migrations
{
    public partial class addedSpouseName : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "b7387a70-5e23-4917-bf5a-395331044f6b");

            migrationBuilder.AddColumn<string>(
                name: "SpouseName",
                table: "Parents",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6cda8712-a79f-437b-81bb-0c7453bc8899", "56d6b411-9d5c-4989-8f43-3c03505330f1", "Parent", "PARENT" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6cda8712-a79f-437b-81bb-0c7453bc8899");

            migrationBuilder.DropColumn(
                name: "SpouseName",
                table: "Parents");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "b7387a70-5e23-4917-bf5a-395331044f6b", "95e80ae6-b069-4edd-9a04-6b25e0f4233b", "Parent", "PARENT" });
        }
    }
}
