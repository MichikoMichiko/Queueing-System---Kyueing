using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kyueng.Migrations
{
    /// <inheritdoc />
    public partial class MakeCounterIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueueCalls_Counters_CounterId",
                table: "QueueCalls");

            migrationBuilder.AlterColumn<int>(
                name: "CounterId",
                table: "QueueCalls",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_QueueCalls_Counters_CounterId",
                table: "QueueCalls",
                column: "CounterId",
                principalTable: "Counters",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueueCalls_Counters_CounterId",
                table: "QueueCalls");

            migrationBuilder.AlterColumn<int>(
                name: "CounterId",
                table: "QueueCalls",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QueueCalls_Counters_CounterId",
                table: "QueueCalls",
                column: "CounterId",
                principalTable: "Counters",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
