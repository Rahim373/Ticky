using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticky.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Add_PendingOrgCreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "PendingOrgCreation",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PendingOrgCreation",
                table: "AspNetUsers");
        }
    }
}
