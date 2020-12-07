using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayDate_App.Migrations
{
    public partial class AddedAddressNametolocationcs : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9aea44e7-2de0-4f92-b6fd-09aa69019107");

            migrationBuilder.AddColumn<string>(
                name: "AddressName",
                table: "Locations",
                nullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "57e57f25-d969-49be-a088-3155e7718227", "a3e780ee-d90b-451e-8781-91f477d6d7d9", "Parent", "PARENT" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "57e57f25-d969-49be-a088-3155e7718227");

            migrationBuilder.DropColumn(
                name: "AddressName",
                table: "Locations");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "9aea44e7-2de0-4f92-b6fd-09aa69019107", "e49b616c-0d7c-4654-8062-22e6cb567832", "Parent", "PARENT" });
        }
    }
}
