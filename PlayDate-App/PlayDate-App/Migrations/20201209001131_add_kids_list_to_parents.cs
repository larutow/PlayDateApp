using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayDate_App.Migrations
{
    public partial class add_kids_list_to_parents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "6f62b922-6076-40ba-b858-676858730b8e");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "7c1dc554-e82b-4ddb-9b57-375dac04a28d", "e7f301e2-35f3-4097-9f80-d3b65dc7591c", "Parent", "PARENT" });

            migrationBuilder.CreateIndex(
                name: "IX_Kids_ParentId",
                table: "Kids",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Kids_Parents_ParentId",
                table: "Kids",
                column: "ParentId",
                principalTable: "Parents",
                principalColumn: "ParentId",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Kids_Parents_ParentId",
                table: "Kids");

            migrationBuilder.DropIndex(
                name: "IX_Kids_ParentId",
                table: "Kids");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "7c1dc554-e82b-4ddb-9b57-375dac04a28d");

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "6f62b922-6076-40ba-b858-676858730b8e", "5a5ddb97-1ce1-492d-9a2a-9446501ec5d1", "Parent", "PARENT" });
        }
    }
}
