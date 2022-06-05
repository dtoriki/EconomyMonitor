using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EconomyMonitor.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddDatePeriodConfigurationsTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DatePeriodConfigurations",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    MonthDayType = table.Column<int>(type: "INTEGER", nullable: false),
                    MonthDay = table.Column<int>(type: "INTEGER", nullable: true),
                    IsDefault = table.Column<bool>(type: "INTEGER", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DatePeriodConfigurations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PeriodSplitEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    MonthDayType = table.Column<int>(type: "INTEGER", nullable: false),
                    MonthDay = table.Column<int>(type: "INTEGER", nullable: true),
                    DatePeriodOptionId = table.Column<Guid>(type: "TEXT", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeriodSplitEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeriodSplitEntity_DatePeriodConfigurations_DatePeriodOptionId",
                        column: x => x.DatePeriodOptionId,
                        principalTable: "DatePeriodConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "SpendingQuotaEntity",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Quota = table.Column<decimal>(type: "TEXT", nullable: false),
                    DatePeriodOptionId = table.Column<Guid>(type: "TEXT", nullable: true),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SpendingQuotaEntity", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SpendingQuotaEntity_DatePeriodConfigurations_DatePeriodOptionId",
                        column: x => x.DatePeriodOptionId,
                        principalTable: "DatePeriodConfigurations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_DatePeriodConfigurations_Id",
                table: "DatePeriodConfigurations",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_PeriodSplitEntity_DatePeriodOptionId",
                table: "PeriodSplitEntity",
                column: "DatePeriodOptionId");

            migrationBuilder.CreateIndex(
                name: "IX_SpendingQuotaEntity_DatePeriodOptionId",
                table: "SpendingQuotaEntity",
                column: "DatePeriodOptionId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PeriodSplitEntity");

            migrationBuilder.DropTable(
                name: "SpendingQuotaEntity");

            migrationBuilder.DropTable(
                name: "DatePeriodConfigurations");
        }
    }
}
