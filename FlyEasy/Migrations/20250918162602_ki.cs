using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlyEasy.Migrations
{
    /// <inheritdoc />
    public partial class ki : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "f29de507-da1a-4daf-a6e1-c5c9942b612a", "AQAAAAIAAYagAAAAEGl/tMBwYcsDvrU04SrBhP+rOpaNtEy/1UM6yquQ1NFt0BlAzSDSu72AodpwebwNqg==", "87371884-0518-4156-89c1-5fd8655d1f9d" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "dddc1da8-7aa7-428c-ad0b-d19da1fcb1e1", "AQAAAAIAAYagAAAAENIODe5eOr35jCn7IwuwA5TnieOTt81OnKLSfSnF1IFBJYO5NE7YyrjToW4bq5eBgg==", "cc786c6c-b7c2-4cae-8aee-b04d46587a26" });
        }
    }
}
