using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Animal",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Discriminator = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CanFly = table.Column<bool>(type: "bit", nullable: true),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LikesToPlay = table.Column<bool>(type: "bit", nullable: true),
                    Breed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Weight = table.Column<int>(type: "int", nullable: true),
                    Dog_Breed = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Dog_Weight = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Animal", x => x.Id);
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

            migrationBuilder.CreateTable(
                name: "AnimalUser",
                columns: table => new
                {
                    AnimalsId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UsersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnimalUser", x => new { x.AnimalsId, x.UsersId });
                    table.ForeignKey(
                        name: "FK_AnimalUser_Animal_AnimalsId",
                        column: x => x.AnimalsId,
                        principalTable: "Animal",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AnimalUser_Users_UsersId",
                        column: x => x.UsersId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Animal",
                columns: new[] { "Id", "Dog_Breed", "Discriminator", "Name", "Dog_Weight" },
                values: new object[,]
                {
                    { new Guid("076fbcea-d41c-4f95-bf0e-f9941d2b0019"), "English Bulldog", "Dog", "Boss", 30 },
                    { new Guid("0d912f5f-299e-4c64-9a83-7022179c5fe8"), "Bernese Mountain Dog", "Dog", "Luffsen", 60 }
                });

            migrationBuilder.InsertData(
                table: "Animal",
                columns: new[] { "Id", "Breed", "Discriminator", "LikesToPlay", "Name", "Weight" },
                values: new object[] { new Guid("1f476589-3da3-4309-b319-b4d6514beb8f"), "British Shorthair", "Cat", true, "Kajsa", 4 });

            migrationBuilder.InsertData(
                table: "Animal",
                columns: new[] { "Id", "CanFly", "Color", "Discriminator", "Name" },
                values: new object[,]
                {
                    { new Guid("22b2a917-8163-41a8-bc47-6f7c8e3e7cd4"), true, "Blue", "Bird", "Peppe" },
                    { new Guid("38f15cf8-9741-4473-bc87-64243c9d1c60"), false, "Green", "Bird", "Kiwi" }
                });

            migrationBuilder.InsertData(
                table: "Animal",
                columns: new[] { "Id", "Dog_Breed", "Discriminator", "Name", "Dog_Weight" },
                values: new object[] { new Guid("63400f5d-0f1f-4693-add0-ceb3ab02dd55"), "Cocker Spaniel", "Dog", "Pim", 15 });

            migrationBuilder.InsertData(
                table: "Animal",
                columns: new[] { "Id", "CanFly", "Color", "Discriminator", "Name" },
                values: new object[] { new Guid("6f41b38a-287e-4d62-a40c-be4c7850cb5a"), true, "Yellow", "Bird", "Charlie" });

            migrationBuilder.InsertData(
                table: "Animal",
                columns: new[] { "Id", "Breed", "Discriminator", "LikesToPlay", "Name", "Weight" },
                values: new object[,]
                {
                    { new Guid("7c8687e8-646e-40e4-8317-564d5bc0a7da"), "Maine Coon", "Cat", true, "Sigge", 10 },
                    { new Guid("9c5cfaa8-a48b-422a-bd62-3fa26ee71e07"), "Ragdoll", "Cat", true, "Lisa", 8 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("c902017c-e795-40f2-bcc6-66d0119cd409"), "string", "string" },
                    { new Guid("ff2b332b-4f48-49b0-bd9a-935b025bc73c"), "admin", "admin" }
                });

            migrationBuilder.CreateIndex(
                name: "IX_AnimalUser_UsersId",
                table: "AnimalUser",
                column: "UsersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AnimalUser");

            migrationBuilder.DropTable(
                name: "Animal");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
