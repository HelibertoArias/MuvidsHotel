using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Muvids.Persistence.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Room",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(200)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(200)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Room", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Start = table.Column<DateTime>(type: "datetime2", nullable: false),
                    End = table.Column<DateTime>(type: "datetime2", nullable: false),
                    RoomId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<string>(type: "varchar(200)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "varchar(200)", nullable: true),
                    LastModifiedDate = table.Column<DateTime>(type: "datetime2", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Room_RoomId",
                        column: x => x.RoomId,
                        principalTable: "Room",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Room",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "IsActive", "IsDeleted", "LastModifiedBy", "LastModifiedDate", "Name" },
                values: new object[] { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991850"), "Dummy", new DateTime(2022, 3, 21, 15, 25, 13, 898, DateTimeKind.Local).AddTicks(5835), false, false, null, null, "Room 01" });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "End", "IsActive", "IsDeleted", "LastModifiedBy", "LastModifiedDate", "RoomId", "Start" },
                values: new object[] { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991870"), "Dummy", new DateTime(2022, 3, 21, 15, 25, 13, 898, DateTimeKind.Local).AddTicks(4657), new DateTime(2022, 1, 2, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, null, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991850"), new DateTime(2022, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.InsertData(
                table: "Bookings",
                columns: new[] { "Id", "CreatedBy", "CreatedDate", "End", "IsActive", "IsDeleted", "LastModifiedBy", "LastModifiedDate", "RoomId", "Start" },
                values: new object[] { new Guid("c9d4c053-49b6-410c-bc78-2d54a9991871"), "Dummy", new DateTime(2022, 3, 21, 15, 25, 13, 898, DateTimeKind.Local).AddTicks(4667), new DateTime(2022, 2, 3, 0, 0, 0, 0, DateTimeKind.Unspecified), true, false, null, null, new Guid("c9d4c053-49b6-410c-bc78-2d54a9991850"), new DateTime(2022, 2, 1, 0, 0, 0, 0, DateTimeKind.Unspecified) });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_RoomId",
                table: "Bookings",
                column: "RoomId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Room");
        }
    }
}
