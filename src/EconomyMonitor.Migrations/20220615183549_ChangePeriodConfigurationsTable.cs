using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EconomyMonitor.Data.Migrations
{
    /// <inheritdoc />
    public partial class ChangePeriodConfigurationsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeriodSplits");

            migrationBuilder.DropTable(
                name: "SpendingQuotas");

            migrationBuilder.DropColumn(
                name: "IsDefault",
                table: "DatePeriodConfigurations");

            migrationBuilder.DropColumn(
                name: "MonthDay",
                table: "DatePeriodConfigurations");

            migrationBuilder.DropColumn(
                name: "MonthDayType",
                table: "DatePeriodConfigurations");

            migrationBuilder.AddColumn<DateOnly>(
                name: "EndPeriodDateExclusive",
                table: "DatePeriodConfigurations",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.AddColumn<DateOnly>(
                name: "StartPeriodDateInclusive",
                table: "DatePeriodConfigurations",
                type: "TEXT",
                nullable: false,
                defaultValue: new DateOnly(1, 1, 1));

            migrationBuilder.CreateIndex(
                name: "IX_DatePeriodConfigurations_EndPeriodDateExclusive",
                table: "DatePeriodConfigurations",
                column: "EndPeriodDateExclusive",
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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_DatePeriodConfigurations_EndPeriodDateExclusive",
                table: "DatePeriodConfigurations");

            migrationBuilder.DropIndex(
                name: "IX_DatePeriodConfigurations_StartPeriodDateInclusive",
                table: "DatePeriodConfigurations");

            migrationBuilder.DropIndex(
                name: "IX_DatePeriodConfigurations_StartPeriodDateInclusive_EndPeriodDateExclusive",
                table: "DatePeriodConfigurations");

            migrationBuilder.DropColumn(
                name: "EndPeriodDateExclusive",
                table: "DatePeriodConfigurations");

            migrationBuilder.DropColumn(
                name: "StartPeriodDateInclusive",
                table: "DatePeriodConfigurations");

            migrationBuilder.AddColumn<bool>(
                name: "IsDefault",
                table: "DatePeriodConfigurations",
                type: "INTEGER",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MonthDay",
                table: "DatePeriodConfigurations",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "MonthDayType",
                table: "DatePeriodConfigurations",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "PeriodSplits",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DatePeriodOptionId = table.Column<Guid>(type: "TEXT", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    MonthDay = table.Column<int>(type: "INTEGER", nullable: true),
                    MonthDayType = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodSplits", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeriodSplits_DatePeriodConfigurations_DatePeriodOptionId",
                        column: x => x.DatePeriodOptionId,
                        principalTable: "DatePeriodConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpendingQuotas",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    DatePeriodOptionId = table.Column<Guid>(type: "TEXT", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Percent = table.Column<decimal>(type: "TEXT", nullable: true),
                    Quota = table.Column<decimal>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpendingQuotas", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpendingQuotas_DatePeriodConfigurations_DatePeriodOptionId",
                        column: x => x.DatePeriodOptionId,
                        principalTable: "DatePeriodConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PeriodSplits_DatePeriodOptionId",
                table: "PeriodSplits",
                column: "DatePeriodOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SpendingQuotas_DatePeriodOptionId",
                table: "SpendingQuotas",
                column: "DatePeriodOptionId");
        }
    }
}
