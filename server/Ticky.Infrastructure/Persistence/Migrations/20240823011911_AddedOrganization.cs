using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ticky.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddedOrganization : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: new Guid("02d767a9-23d5-45f0-a96a-ccbc64bb8a19"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetUsers",
                columns: new[] { "Id", "AccessFailedCount", "ConcurrencyStamp", "Email", "EmailConfirmed", "LockoutEnabled", "LockoutEnd", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "PhoneNumber", "PhoneNumberConfirmed", "SecurityStamp", "TwoFactorEnabled", "UserName" },
                values: new object[] { new Guid("02d767a9-23d5-45f0-a96a-ccbc64bb8a19"), 0, "2d49d0e8-6a84-4e30-9be0-19a16f2c096c", "rahim.prsf@gmail.com", false, false, null, "rahim.prsf@gmail.com", "rahim.prsf@gmail.com", "AQAAAAIAAYagAAAAEIJYIOibwpAaRwGz10A/1aRU4OA1WMVLBpIR79XyTeD/9QPUcCXkhWzZ9nebR6jWtA==", null, false, "02d767a9-23d5-45f0-a96a-ccbc64bb8a19", false, "rahim.prsf@gmail.com" });
        }
    }
}
