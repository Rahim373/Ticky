using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticky.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class SeedDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("02d767a9-23d5-45f0-a96a-ccbc64bb8a19"), 0, "bd653921-7487-42bd-b4ac-7fa274fed7d7", "rahim.prsf@gmail.com", false, false, null, "rahim.prsf@gmail.com", "rahim.prsf@gmail.com", "AQAAAAIAAYagAAAAELOBhpvagqCmUPTKMOGomVRsRciJX50anN9ZPpQ4qfvWgQqkf7h9SDFDiW6Akm68HA==", null, false, "02d767a9-23d5-45f0-a96a-ccbc64bb8a19", false, "rahim.prsf@gmail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("02d767a9-23d5-45f0-a96a-ccbc64bb8a19"));
        }
    }
}
