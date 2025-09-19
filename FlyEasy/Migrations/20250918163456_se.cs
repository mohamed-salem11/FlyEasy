using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FlyEasy.Migrations
{
    /// <inheritdoc />
    public partial class se : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "baa86669-407e-4c88-bb54-347c96c4bb26", "ADMINFLYEASY@GMAIL.COM", "ADMINFLYEASY@GMAIL.COM", "AQAAAAIAAYagAAAAEJ62FKTbuU/puZp20eO9R+Gef1+dsUhPrAnON94sb0yqF1lZkxGForWExCEmPWb3aQ==", "bf5129c4-4add-40f6-b890-f2c338908af9", "adminFlyEasy@gmail.com" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "admin-user-id",
                columns: new[] { "ConcurrencyStamp", "NormalizedEmail", "NormalizedUserName", "PasswordHash", "SecurityStamp", "UserName" },
                values: new object[] { "f29de507-da1a-4daf-a6e1-c5c9942b612a", "ADMIN@FLYEASY.COM", "ADMIN", "AQAAAAIAAYagAAAAEGl/tMBwYcsDvrU04SrBhP+rOpaNtEy/1UM6yquQ1NFt0BlAzSDSu72AodpwebwNqg==", "87371884-0518-4156-89c1-5fd8655d1f9d", "admin" });
        }
    }
}
