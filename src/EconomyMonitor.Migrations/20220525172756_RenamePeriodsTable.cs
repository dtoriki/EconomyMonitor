using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EconomyMonitor.Data.Migrations
{
    /// <inheritdoc />
    public partial class RenamePeriodsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Periods");

            migrationBuilder.CreateTable(
                name: "DatePeriods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    StartingDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    EndingDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Income = table.Column<decimal>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatePeriods", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatePeriods");

            migrationBuilder.CreateTable(
                name: "Periods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndPeriod = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Income = table.Column<decimal>(type: "TEXT", nullable: false),
                    StartPeriod = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Periods", x => x.Id);
                });
        }
    }
}
