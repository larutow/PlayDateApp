using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PlayDate_App.Migrations
{
    public partial class Southpark : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "AspNetRoles",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    LocationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    AddressName = table.Column<string>(nullable: true),
                    Lat = table.Column<double>(nullable: false),
                    Lng = table.Column<double>(nullable: false),
                    ThumbsUp = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.LocationId);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetUserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AspNetUserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserRoles",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "AspNetRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AspNetUserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUserTokens",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AspNetUserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Parents",
                columns: table => new
                {
                    ParentId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IdentityUserId = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: true),
                    SpouseName = table.Column<string>(nullable: true),
                    LocationZip = table.Column<int>(nullable: false),
                    ImagePath = table.Column<string>(nullable: true),
                    EmailAddress = table.Column<string>(nullable: true),
                    Lat = table.Column<double>(nullable: false),
                    Lng = table.Column<double>(nullable: false),
                    ThumbsUp = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parents", x => x.ParentId);
                    table.ForeignKey(
                        name: "FK_Parents_AspNetUsers_IdentityUserId",
                        column: x => x.IdentityUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Events",
                columns: table => new
                {
                    EventId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LocationId = table.Column<int>(nullable: false),
                    TimeAndDate = table.Column<DateTime>(nullable: false),
                    ConfirmedEvent = table.Column<bool>(nullable: false),
                    IsPrivate = table.Column<bool>(nullable: false),
                    Capacity = table.Column<int>(nullable: false),
                    ParentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Events", x => x.EventId);
                    table.ForeignKey(
                        name: "FK_Events_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "LocationId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Friendships",
                columns: table => new
                {
                    FriendshipId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentOneId = table.Column<int>(nullable: false),
                    ParentTwoId = table.Column<int>(nullable: false),
                    FriendshipRequest = table.Column<bool>(nullable: false),
                    FriendshipConfirmed = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendships", x => x.FriendshipId);
                    table.ForeignKey(
                        name: "FK_Friendships_Parents_ParentOneId",
                        column: x => x.ParentOneId,
                        principalTable: "Parents",
                        principalColumn: "ParentId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Friendships_Parents_ParentTwoId",
                        column: x => x.ParentTwoId,
                        principalTable: "Parents",
                        principalColumn: "ParentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Kids",
                columns: table => new
                {
                    KidId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(nullable: false),
                    Age = table.Column<int>(nullable: false),
                    Immunized = table.Column<bool>(nullable: false),
                    WearsMask = table.Column<bool>(nullable: false),
                    SpecialNeeds = table.Column<bool>(nullable: false),
                    Notes = table.Column<string>(nullable: true),
                    ParentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Kids", x => x.KidId);
                    table.ForeignKey(
                        name: "FK_Kids_Parents_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Parents",
                        principalColumn: "ParentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EventRegistrations",
                columns: table => new
                {
                    EventRegistrationId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentId = table.Column<int>(nullable: false),
                    EventId = table.Column<int>(nullable: false),
                    Accepted = table.Column<bool>(nullable: false),
                    Role = table.Column<string>(nullable: false),
                    ConfirmedAttendance = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventRegistrations", x => x.EventRegistrationId);
                    table.ForeignKey(
                        name: "FK_EventRegistrations_Events_EventId",
                        column: x => x.EventId,
                        principalTable: "Events",
                        principalColumn: "EventId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_EventRegistrations_Parents_ParentId",
                        column: x => x.ParentId,
                        principalTable: "Parents",
                        principalColumn: "ParentId",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[] { "e2418188-d014-441b-bddc-8177d024dadc", "e063beb6-af60-4b61-abb5-23093391cc2c", "Parent", "PARENT" });

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                table: "AspNetRoles",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserClaims_UserId",
                table: "AspNetUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserLogins_UserId",
                table: "AspNetUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUserRoles_RoleId",
                table: "AspNetUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_EventId",
                table: "EventRegistrations",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_EventRegistrations_ParentId",
                table: "EventRegistrations",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Events_LocationId",
                table: "Events",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_ParentOneId",
                table: "Friendships",
                column: "ParentOneId");

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_ParentTwoId",
                table: "Friendships",
                column: "ParentTwoId");

            migrationBuilder.CreateIndex(
                name: "IX_Kids_ParentId",
                table: "Kids",
                column: "ParentId");

            migrationBuilder.CreateIndex(
                name: "IX_Parents_IdentityUserId",
                table: "Parents",
                column: "IdentityUserId");
            
            migrationBuilder.InsertData(
               table: "Parents",
               columns: new[] { "ParentId", "EmailAddress", "FirstName", "IdentityUserId", "ImagePath", "LastName", "Lat", "Lng", "LocationZip", "SpouseName", "ThumbsUp" },
               values: new object[,]
               {
                    { 1, null, "Randy", null, "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/84707089-0625-40f8-9416-da48a7e76fa8/dcquhfx-4bada4d3-f309-49e9-ac8f-0a1b98352d4d.png/v1/fill/w_1024,h_707,strp/marsh_family_by_cartman1235_dcquhfx-fullview.png?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOiIsImlzcyI6InVybjphcHA6Iiwib2JqIjpbW3sicGF0aCI6IlwvZlwvODQ3MDcwODktMDYyNS00MGY4LTk0MTYtZGE0OGE3ZTc2ZmE4XC9kY3F1aGZ4LTRiYWRhNGQzLWYzMDktNDllOS1hYzhmLTBhMWI5ODM1MmQ0ZC5wbmciLCJoZWlnaHQiOiI8PTcwNyIsIndpZHRoIjoiPD0xMDI0In1dXSwiYXVkIjpbInVybjpzZXJ2aWNlOmltYWdlLndhdGVybWFyayJdLCJ3bWsiOnsicGF0aCI6Ilwvd21cLzg0NzA3MDg5LTA2MjUtNDBmOC05NDE2LWRhNDhhN2U3NmZhOFwvY2FydG1hbjEyMzUtNC5wbmciLCJvcGFjaXR5Ijo5NSwicHJvcG9ydGlvbnMiOjAuNDUsImdyYXZpdHkiOiJjZW50ZXIifX0.zkp1XDHFKAzqlO3PtOGbyuCGq0uC-eHrqA4HDjKydMg", "Marsh", 0.0, 0.0, 80420, "Sharon", 0 },
                    { 2, null, "Sheila", null, "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/84707089-0625-40f8-9416-da48a7e76fa8/dcvmrx1-2e05aa33-f414-4055-acf6-493afca6ca77.png?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOiIsImlzcyI6InVybjphcHA6Iiwib2JqIjpbW3sicGF0aCI6IlwvZlwvODQ3MDcwODktMDYyNS00MGY4LTk0MTYtZGE0OGE3ZTc2ZmE4XC9kY3ZtcngxLTJlMDVhYTMzLWY0MTQtNDA1NS1hY2Y2LTQ5M2FmY2E2Y2E3Ny5wbmcifV1dLCJhdWQiOlsidXJuOnNlcnZpY2U6ZmlsZS5kb3dubG9hZCJdfQ.aeLD9BLPAZTnvNO43bo3Ow3BtTmmIZQC95YStgXHPWA", "Broflovski", 0.0, 0.0, 80420, "Gerald", 0 },
                    { 3, null, "Liane", null, "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/84707089-0625-40f8-9416-da48a7e76fa8/dcwgnwo-1786ae03-d283-4e2b-9e5f-1d9fa70612c2.png?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOiIsImlzcyI6InVybjphcHA6Iiwib2JqIjpbW3sicGF0aCI6IlwvZlwvODQ3MDcwODktMDYyNS00MGY4LTk0MTYtZGE0OGE3ZTc2ZmE4XC9kY3dnbndvLTE3ODZhZTAzLWQyODMtNGUyYi05ZTVmLTFkOWZhNzA2MTJjMi5wbmcifV1dLCJhdWQiOlsidXJuOnNlcnZpY2U6ZmlsZS5kb3dubG9hZCJdfQ.36xTtJwKZBggaUT7RsOkVR2CLPYEOm7iRThHcXbdzr0", "Cartman", 0.0, 0.0, 80420, "Jack", 0 },
                    { 4, null, "Stuart", null, "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/84707089-0625-40f8-9416-da48a7e76fa8/dcunj0l-b2e9fdf1-9c13-4a1d-8972-e5bd0d42d9bf.png?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOiIsImlzcyI6InVybjphcHA6Iiwib2JqIjpbW3sicGF0aCI6IlwvZlwvODQ3MDcwODktMDYyNS00MGY4LTk0MTYtZGE0OGE3ZTc2ZmE4XC9kY3VuajBsLWIyZTlmZGYxLTljMTMtNGExZC04OTcyLWU1YmQwZDQyZDliZi5wbmcifV1dLCJhdWQiOlsidXJuOnNlcnZpY2U6ZmlsZS5kb3dubG9hZCJdfQ.vV-J2oHmdNzzGqqQLA4o8y6rIYD-b_XI0eknuTB7Lkk", "McCormick", 0.0, 0.0, 80420, "Carol", 0 },
                    { 5, null, "Linda", null, "https://images-wixmp-ed30a86b8c4ca887773594c2.wixmp.com/f/84707089-0625-40f8-9416-da48a7e76fa8/dcxn9bj-758955ee-d06c-42f9-89a2-c77415ed9935.png?token=eyJ0eXAiOiJKV1QiLCJhbGciOiJIUzI1NiJ9.eyJzdWIiOiJ1cm46YXBwOiIsImlzcyI6InVybjphcHA6Iiwib2JqIjpbW3sicGF0aCI6IlwvZlwvODQ3MDcwODktMDYyNS00MGY4LTk0MTYtZGE0OGE3ZTc2ZmE4XC9kY3huOWJqLTc1ODk1NWVlLWQwNmMtNDJmOS04OWEyLWM3NzQxNWVkOTkzNS5wbmcifV1dLCJhdWQiOlsidXJuOnNlcnZpY2U6ZmlsZS5kb3dubG9hZCJdfQ.U1EOZRdIaRYEudnhk7NXO3bSICNSLBA32x1_aSeIkAo", "Stotch", 0.0, 0.0, 80420, "Stephen", 0 }
               });

            migrationBuilder.InsertData(
                table: "Kids",
                columns: new[] { "KidId", "Age", "FirstName", "Immunized", "Notes", "ParentId", "SpecialNeeds", "WearsMask" },
                values: new object[,]
                {
                    { 1, 10, "Stan", false, "'Oh my god, they killed Kenny!'", 1, false, false },
                    { 2, 13, "Shelly", false, "She's got huge headgear braces on her teeth, and enjoys using the word 'turds'", 1, false, false },
                    { 3, 10, "Kyle", false, "Kyle's generally quite calm and patient with those around him... when Cartman isn't involved, anyhow.", 2, false, false },
                    { 4, 3, "Ike", false, "Ike has had no shortage of adventure and mischief in his life", 2, false, false },
                    { 5, 10, "Eric", false, "His mother seems to be the sole person he genuinely cares about", 3, false, false },
                    { 6, 10, "Kenny", false, "'Mrrph rmph rmmph mrrphh!'", 4, false, false },
                    { 7, 13, "Kevin", false, "He likes frozen waffles and reacts with horror to his brother's deaths", 4, false, false },
                    { 8, 6, "Karen", false, "Karen is Kenny's rarely-seen little sister", 4, false, false },
                    { 9, 10, "Leopold 'Butters'", false, "He briefly explored his feminine alter-ego as Marjorine. And he absolutely loves Bennigan's", 5, false, false }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserClaims");

            migrationBuilder.DropTable(
                name: "AspNetUserLogins");

            migrationBuilder.DropTable(
                name: "AspNetUserRoles");

            migrationBuilder.DropTable(
                name: "AspNetUserTokens");

            migrationBuilder.DropTable(
                name: "EventRegistrations");

            migrationBuilder.DropTable(
                name: "Friendships");

            migrationBuilder.DropTable(
                name: "Kids");

            migrationBuilder.DropTable(
                name: "AspNetRoles");

            migrationBuilder.DropTable(
                name: "Events");

            migrationBuilder.DropTable(
                name: "Parents");

            migrationBuilder.DropTable(
                name: "Locations");

            migrationBuilder.DropTable(
                name: "AspNetUsers");
        }
    }
}
