using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Testing_.net.Migrations
{
    /// <inheritdoc />
    public partial class AddData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DateTime",
                table: "Students",
                newName: "ERD");

            migrationBuilder.InsertData(
                table: "Students",
                columns: new[] { "Id", "Age", "ERD", "Email", "Name" },
                values: new object[,]
                {
                    { 1, 20, new DateTime(2020, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "a@gmail.com", "Alice" },
                    { 2, 20, new DateTime(2021, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "a@gmail.com", "Bob" },
                    { 3, 20, new DateTime(2022, 9, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "a@gmail.com", "Charlie" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Students",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.RenameColumn(
                name: "ERD",
                table: "Students",
                newName: "DateTime");
        }
    }
}
