using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlyEasy.Migrations
{
    /// <inheritdoc />
    public partial class normalizedHandeled : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dddc1da8-7aa7-428c-ad0b-d19da1fcb1e1", "ADMIN", "AQAAAAIAAYagAAAAENIODe5eOr35jCn7IwuwA5TnieOTt81OnKLSfSnF1IFBJYO5NE7YyrjToW4bq5eBgg==", "cc786c6c-b7c2-4cae-8aee-b04d46587a26" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "NormalizedUserName", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7050eafb-31b9-4fa8-9e72-5b168d8a3d39", "ADMIN@FLYEASY.COM", "AQAAAAIAAYagAAAAEEvv60/YnZgLQRvP6yIf5k+hssiEJJ908M8k3YHcKCWs2AL0eFd9Viv+vqCB6rtjRQ==", "5eeb771f-1d2c-4fae-a342-0c2c38d9c260" });
        }
    }
}
