using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EconomyMonitor.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddMoneyFlows : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "MoneyForDays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Date = table.Column<DateOnly>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyForDays", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MoneyDuringDays",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    Amount = table.Column<decimal>(type: "TEXT", nullable: false),
                    Time = table.Column<TimeOnly>(type: "TEXT", nullable: false),
                    MoneyForDayId = table.Column<Guid>(type: "TEXT", nullable: false),
                    Discriminator = table.Column<string>(type: "TEXT", nullable: false),
                    DateCreated = table.Column<DateTime>(type: "TEXT", nullable: false),
                    DateModified = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MoneyDuringDays", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MoneyDuringDays_MoneyForDays_MoneyForDayId",
                        column: x => x.MoneyForDayId,
                        principalTable: "MoneyForDays",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MoneyDuringDays_DateCreated",
                table: "MoneyDuringDays",
                column: "DateCreated");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyDuringDays_DateModified",
                table: "MoneyDuringDays",
                column: "DateModified");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyDuringDays_Id",
                table: "MoneyDuringDays",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoneyDuringDays_MoneyForDayId",
                table: "MoneyDuringDays",
                column: "MoneyForDayId");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyForDays_Date",
                table: "MoneyForDays",
                column: "Date",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_MoneyForDays_DateCreated",
                table: "MoneyForDays",
                column: "DateCreated");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyForDays_DateModified",
                table: "MoneyForDays",
                column: "DateModified");

            migrationBuilder.CreateIndex(
                name: "IX_MoneyForDays_Id",
                table: "MoneyForDays",
                column: "Id",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MoneyDuringDays");

            migrationBuilder.DropTable(
                name: "MoneyForDays");
        }
    }
}
