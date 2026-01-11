using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kyueng.Migrations
{
    /// <inheritdoc />
    public partial class AddCounterToSkip : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CounterId",
                table: "Skip",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Skip_CounterId",
                table: "Skip",
                column: "CounterId");

            migrationBuilder.AddForeignKey(
                name: "FK_Skip_Counters_CounterId",
                table: "Skip",
                column: "CounterId",
                principalTable: "Counters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Skip_Counters_CounterId",
                table: "Skip");

            migrationBuilder.DropIndex(
                name: "IX_Skip_CounterId",
                table: "Skip");

            migrationBuilder.DropColumn(
                name: "CounterId",
                table: "Skip");
        }
    }
}
