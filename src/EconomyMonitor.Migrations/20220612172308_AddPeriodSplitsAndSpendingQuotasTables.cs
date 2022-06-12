using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EconomyMonitor.Data.Migrations
{
    /// <inheritdoc />
    public partial class AddPeriodSplitsAndSpendingQuotasTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeriodSplitEntity_DatePeriodConfigurations_DatePeriodOptionId",
                table: "PeriodSplitEntity");

            migrationBuilder.DropForeignKey(
                name: "FK_SpendingQuotaEntity_DatePeriodConfigurations_DatePeriodOptionId",
                table: "SpendingQuotaEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpendingQuotaEntity",
                table: "SpendingQuotaEntity");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PeriodSplitEntity",
                table: "PeriodSplitEntity");

            migrationBuilder.RenameTable(
                name: "SpendingQuotaEntity",
                newName: "SpendingQuotas");

            migrationBuilder.RenameTable(
                name: "PeriodSplitEntity",
                newName: "PeriodSplits");

            migrationBuilder.RenameIndex(
                name: "IX_SpendingQuotaEntity_DatePeriodOptionId",
                table: "SpendingQuotas",
                newName: "IX_SpendingQuotas_DatePeriodOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_PeriodSplitEntity_DatePeriodOptionId",
                table: "PeriodSplits",
                newName: "IX_PeriodSplits_DatePeriodOptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpendingQuotas",
                table: "SpendingQuotas",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PeriodSplits",
                table: "PeriodSplits",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PeriodSplits_DatePeriodConfigurations_DatePeriodOptionId",
                table: "PeriodSplits",
                column: "DatePeriodOptionId",
                principalTable: "DatePeriodConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpendingQuotas_DatePeriodConfigurations_DatePeriodOptionId",
                table: "SpendingQuotas",
                column: "DatePeriodOptionId",
                principalTable: "DatePeriodConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PeriodSplits_DatePeriodConfigurations_DatePeriodOptionId",
                table: "PeriodSplits");

            migrationBuilder.DropForeignKey(
                name: "FK_SpendingQuotas_DatePeriodConfigurations_DatePeriodOptionId",
                table: "SpendingQuotas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_SpendingQuotas",
                table: "SpendingQuotas");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PeriodSplits",
                table: "PeriodSplits");

            migrationBuilder.RenameTable(
                name: "SpendingQuotas",
                newName: "SpendingQuotaEntity");

            migrationBuilder.RenameTable(
                name: "PeriodSplits",
                newName: "PeriodSplitEntity");

            migrationBuilder.RenameIndex(
                name: "IX_SpendingQuotas_DatePeriodOptionId",
                table: "SpendingQuotaEntity",
                newName: "IX_SpendingQuotaEntity_DatePeriodOptionId");

            migrationBuilder.RenameIndex(
                name: "IX_PeriodSplits_DatePeriodOptionId",
                table: "PeriodSplitEntity",
                newName: "IX_PeriodSplitEntity_DatePeriodOptionId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_SpendingQuotaEntity",
                table: "SpendingQuotaEntity",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PeriodSplitEntity",
                table: "PeriodSplitEntity",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_PeriodSplitEntity_DatePeriodConfigurations_DatePeriodOptionId",
                table: "PeriodSplitEntity",
                column: "DatePeriodOptionId",
                principalTable: "DatePeriodConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_SpendingQuotaEntity_DatePeriodConfigurations_DatePeriodOptionId",
                table: "SpendingQuotaEntity",
                column: "DatePeriodOptionId",
                principalTable: "DatePeriodConfigurations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
