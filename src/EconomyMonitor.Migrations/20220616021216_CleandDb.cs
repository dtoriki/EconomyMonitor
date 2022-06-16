using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EconomyMonitor.Data.Migrations
{
    /// <inheritdoc />
    public partial class CleandDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "DatePeriodConfigurations");

            migrationBuilder.DropTable(
                name: "DatePeriods");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DatePeriodConfigurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndPeriodDateExclusive = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    StartPeriodDateInclusive = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatePeriodConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DatePeriods",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndingDate = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    Income = table.Column<decimal>(type: "TEXT", nullable: false),
                    StartingDate = table.Column<DateOnly>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatePeriods", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DatePeriodConfigurations_EndPeriodDateExclusive",
                table: "DatePeriodConfigurations",
                column: "EndPeriodDateExclusive",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DatePeriodConfigurations_Id",
                table: "DatePeriodConfigurations",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DatePeriodConfigurations_StartPeriodDateInclusive",
                table: "DatePeriodConfigurations",
                column: "StartPeriodDateInclusive",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_DatePeriodConfigurations_StartPeriodDateInclusive_EndPeriodDateExclusive",
                table: "DatePeriodConfigurations",
                columns: new[] { "StartPeriodDateInclusive", "EndPeriodDateExclusive" },
                unique: true);
        }
    }
}
