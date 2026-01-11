using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kyueng.Migrations
{
    /// <inheritdoc />
    public partial class RemoveCascadeDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueueCalls_QueueTickets_QueueTicketId",
                table: "QueueCalls");

            migrationBuilder.AddForeignKey(
                name: "FK_QueueCalls_QueueTickets_QueueTicketId",
                table: "QueueCalls",
                column: "QueueTicketId",
                principalTable: "QueueTickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueueCalls_QueueTickets_QueueTicketId",
                table: "QueueCalls");

            migrationBuilder.AddForeignKey(
                name: "FK_QueueCalls_QueueTickets_QueueTicketId",
                table: "QueueCalls",
                column: "QueueTicketId",
                principalTable: "QueueTickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
