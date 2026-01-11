using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Kyueng.Migrations
{
    /// <inheritdoc />
    public partial class AddWasSkippedToQueueTicket : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "WasSkipped",
                table: "QueueTickets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WasSkipped",
                table: "QueueTickets");
        }
    }
}
