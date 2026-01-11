using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kyueng.Migrations
{
    /// <inheritdoc />
    public partial class MakeStudentOptionalInQueueTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueueTickets_Students_StudentId",
                table: "QueueTickets");

            migrationBuilder.AddForeignKey(
                name: "FK_QueueTickets_Students_StudentId",
                table: "QueueTickets",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_QueueTickets_Students_StudentId",
                table: "QueueTickets");

            migrationBuilder.AddForeignKey(
                name: "FK_QueueTickets_Students_StudentId",
                table: "QueueTickets",
                column: "StudentId",
                principalTable: "Students",
                principalColumn: "Id");
        }
    }
}
