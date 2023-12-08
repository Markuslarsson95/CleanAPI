using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UserAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("3ad15cef-53ac-4867-b563-b9c6cb250a48"));

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("7dbe6582-f1d8-4515-a5a1-05f6cade5f23"));

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("a51e8830-ca8d-4360-8232-4d5a05da1ec8"));

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "char(36)", nullable: false),
                    UserName = table.Column<string>(type: "longtext", nullable: false),
                    Password = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "Dogs",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("56f45a16-3f5f-4d24-b330-4211854f2f1c"), "Boss" },
                    { new Guid("8c54a701-6c28-4378-a6ce-ac5bb56cf47c"), "Pim" },
                    { new Guid("f28b9e55-24b9-4a17-8ae8-38669a5279c1"), "Luffsen" }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("5a58d680-1fbb-4112-9ee9-1490ffdc001b"), "string", "string" },
                    { new Guid("bbe94aed-a2e4-4d4f-9a75-5612f06cbeca"), "admin", "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("56f45a16-3f5f-4d24-b330-4211854f2f1c"));

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("8c54a701-6c28-4378-a6ce-ac5bb56cf47c"));

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("f28b9e55-24b9-4a17-8ae8-38669a5279c1"));

            migrationBuilder.InsertData(
                table: "Dogs",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3ad15cef-53ac-4867-b563-b9c6cb250a48"), "Boss" },
                    { new Guid("7dbe6582-f1d8-4515-a5a1-05f6cade5f23"), "Luffsen" },
                    { new Guid("a51e8830-ca8d-4360-8232-4d5a05da1ec8"), "Pim" }
                });
        }
    }
}
