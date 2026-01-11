using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kyueng.Migrations
{
    /// <inheritdoc />
    public partial class FixTicketId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueueCalls_QueueTickets_QueueTicketId",
                table: "QueueCalls");

            migrationBuilder.DropForeignKey(
                name: "FK_QueueTickets_Students_StudentId",
                table: "QueueTickets");

            migrationBuilder.RenameColumn(
                name: "QueueTicketId",
                table: "QueueCalls",
                newName: "TicketId");

            migrationBuilder.RenameIndex(
                name: "IX_QueueCalls_QueueTicketId",
                table: "QueueCalls",
                newName: "IX_QueueCalls_TicketId");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "QueueTickets",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_QueueCalls_QueueTickets_TicketId",
                table: "QueueCalls",
                column: "TicketId",
                principalTable: "QueueTickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QueueTickets_Students_StudentId",
                table: "QueueTickets",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueueCalls_QueueTickets_TicketId",
                table: "QueueCalls");

            migrationBuilder.DropForeignKey(
                name: "FK_QueueTickets_Students_StudentId",
                table: "QueueTickets");

            migrationBuilder.RenameColumn(
                name: "TicketId",
                table: "QueueCalls",
                newName: "QueueTicketId");

            migrationBuilder.RenameIndex(
                name: "IX_QueueCalls_TicketId",
                table: "QueueCalls",
                newName: "IX_QueueCalls_QueueTicketId");

            migrationBuilder.AlterColumn<int>(
                name: "StudentId",
                table: "QueueTickets",
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
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_QueueTickets_Students_StudentId",
                table: "QueueTickets",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
