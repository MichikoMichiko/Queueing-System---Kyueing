using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kyueng.Migrations
{
    /// <inheritdoc />
    public partial class RenameTicketIdToQueueTicketId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueueCalls_QueueTickets_TicketId",
                table: "QueueCalls");

            migrationBuilder.RenameColumn(
                name: "TicketId",
                table: "QueueCalls",
                newName: "QueueTicketId");

            migrationBuilder.RenameIndex(
                name: "IX_QueueCalls_TicketId",
                table: "QueueCalls",
                newName: "IX_QueueCalls_QueueTicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_QueueCalls_QueueTickets_QueueTicketId",
                table: "QueueCalls",
                column: "QueueTicketId",
                principalTable: "QueueTickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueueCalls_QueueTickets_QueueTicketId",
                table: "QueueCalls");

            migrationBuilder.RenameColumn(
                name: "QueueTicketId",
                table: "QueueCalls",
                newName: "TicketId");

            migrationBuilder.RenameIndex(
                name: "IX_QueueCalls_QueueTicketId",
                table: "QueueCalls",
                newName: "IX_QueueCalls_TicketId");

            migrationBuilder.AddForeignKey(
                name: "FK_QueueCalls_QueueTickets_TicketId",
                table: "QueueCalls",
                column: "TicketId",
                principalTable: "QueueTickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
