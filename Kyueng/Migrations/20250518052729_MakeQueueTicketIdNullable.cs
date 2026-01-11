using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kyueng.Migrations
{
    /// <inheritdoc />
    public partial class MakeQueueTicketIdNullable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueueCalls_QueueTickets_QueueTicketId",
                table: "QueueCalls");

            migrationBuilder.AlterColumn<int>(
                name: "QueueTicketId",
                table: "QueueCalls",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_QueueCalls_QueueTickets_QueueTicketId",
                table: "QueueCalls",
                column: "QueueTicketId",
                principalTable: "QueueTickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueueCalls_QueueTickets_QueueTicketId",
                table: "QueueCalls");

            migrationBuilder.AlterColumn<int>(
                name: "QueueTicketId",
                table: "QueueCalls",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_QueueCalls_QueueTickets_QueueTicketId",
                table: "QueueCalls",
                column: "QueueTicketId",
                principalTable: "QueueTickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
