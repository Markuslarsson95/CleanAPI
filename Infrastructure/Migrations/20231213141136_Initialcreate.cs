using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initialcreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Birds",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CanFly = table.Column<bool>(type: "bit", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Birds", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Cats",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LikesToPlay = table.Column<bool>(type: "bit", nullable: false),
                    Breed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cats", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Dogs",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Breed = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Weight = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dogs", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Birds",
                columns: new[] { "Id", "CanFly", "Color", "Name" },
                values: new object[,]
                {
                    { new Guid("45a3614d-c01a-4459-877f-2d0f7e957474"), true, "Blue", "Peppe" },
                    { new Guid("db514d81-ab5e-4501-9d1d-8cf3fc478ec1"), false, "Green", "Kiwi" },
                    { new Guid("e8875b12-0630-4914-bcec-eb23a1cfb955"), true, "Yellow", "Charlie" }
                });

            migrationBuilder.InsertData(
                table: "Cats",
                columns: new[] { "Id", "Breed", "LikesToPlay", "Name", "Weight" },
                values: new object[,]
                {
                    { new Guid("0293b60e-0eb4-414b-a362-829338a1427b"), "Maine Coon", true, "Sigge", 10 },
                    { new Guid("58749c2d-3655-40a1-a93f-547ef1a0a1c5"), "Ragdoll", true, "Lisa", 8 },
                    { new Guid("abf7d09f-42d5-466f-adb6-fdb79cced91d"), "British Shorthair", true, "Kajsa", 4 }
                });

            migrationBuilder.InsertData(
                table: "Dogs",
                columns: new[] { "Id", "Breed", "Name", "Weight" },
                values: new object[,]
                {
                    { new Guid("1239796c-9907-40a9-b5cb-397617c5d4a3"), "Cocker Spaniel", "Pim", 15 },
                    { new Guid("39f670c9-b461-4c2c-8d2d-076bed02a58d"), "Bernese Mountain Dog", "Luffsen", 60 },
                    { new Guid("709f6285-e943-4d02-a1e3-a4f48088c39b"), "English Bulldog", "Boss", 30 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("611c82b3-50ee-4f58-a74d-c6a6f5d9f9f3"), "admin", "admin" },
                    { new Guid("e0e7ddcf-55d2-458c-9b6d-7703aca30d8a"), "string", "string" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Birds");

            migrationBuilder.DropTable(
                name: "Cats");

            migrationBuilder.DropTable(
                name: "Dogs");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
