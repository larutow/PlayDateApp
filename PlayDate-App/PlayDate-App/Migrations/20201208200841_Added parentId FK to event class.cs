using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayDate_App.Migrations
{
    public partial class AddedparentIdFKtoeventclass : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Events_Parents_ParentId",
                table: "Events");

            migrationBuilder.DropIndex(
                name: "IX_Events_ParentId",
                table: "Events");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "bfe02afd-1afb-4a45-a06f-bb53ae0b4cd3");

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "Events",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "2082b705-419a-4d79-b628-7b45b0f7d45e", "d8b8f6b6-24f9-4eb1-8902-72d0c8b1854b", "Parent", "PARENT" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "2082b705-419a-4d79-b628-7b45b0f7d45e");

            migrationBuilder.AlterColumn<int>(
                name: "ParentId",
                table: "Events",
                type: "int",
                nullable: true,
                oldClrType: typeof(int));

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "bfe02afd-1afb-4a45-a06f-bb53ae0b4cd3", "c04c5227-1403-4ab7-a742-9cce9a9ff1eb", "Parent", "PARENT" });

            migrationBuilder.CreateIndex(
                name: "IX_Events_ParentId",
                table: "Events",
                column: "ParentId");

            migrationBuilder.AddForeignKey(
                name: "FK_Events_Parents_ParentId",
                table: "Events",
                column: "ParentId",
                principalTable: "Parents",
                principalColumn: "ParentId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
