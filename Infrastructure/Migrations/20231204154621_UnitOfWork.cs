using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class UnitOfWork : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("3ef1cf93-7750-450a-ba97-02e6c9642a5c"));

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("6d34d3cb-7b97-4cd3-9229-79866f06fa6e"));

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("93eec0b5-9b59-4923-89ef-3234abe2e0f9"));

            migrationBuilder.InsertData(
                table: "Dogs",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("0ec31046-0cd6-4c9b-b8c9-129320dc6485"), "Luffsen" },
                    { new Guid("10e381fe-4f67-4252-8616-f4add9c979fb"), "Boss" },
                    { new Guid("fe2b9459-8c60-4454-b9f0-7adf70e447d0"), "Pim" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("0ec31046-0cd6-4c9b-b8c9-129320dc6485"));

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("10e381fe-4f67-4252-8616-f4add9c979fb"));

            migrationBuilder.DeleteData(
                table: "Dogs",
                keyColumn: "Id",
                keyValue: new Guid("fe2b9459-8c60-4454-b9f0-7adf70e447d0"));

            migrationBuilder.InsertData(
                table: "Dogs",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("3ef1cf93-7750-450a-ba97-02e6c9642a5c"), "Boss" },
                    { new Guid("6d34d3cb-7b97-4cd3-9229-79866f06fa6e"), "Luffsen" },
                    { new Guid("93eec0b5-9b59-4923-89ef-3234abe2e0f9"), "Pim" }
                });
        }
    }
}
