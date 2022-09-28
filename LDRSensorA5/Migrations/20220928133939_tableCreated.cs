using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LDRSensorA5.Migrations
{
    public partial class tableCreated : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LDRData",
                columns: table => new
                {
                    TimeStamp = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Lux = table.Column<float>(type: "REAL", nullable: false),
                    Current = table.Column<float>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LDRData", x => x.TimeStamp);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LDRData");
        }
    }
}
