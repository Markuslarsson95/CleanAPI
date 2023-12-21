using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class NewDbContext : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Animal",
                keyColumn: "Id",
                keyValue: new Guid("076fbcea-d41c-4f95-bf0e-f9941d2b0019"));

            migrationBuilder.DeleteData(
                table: "Animal",
                keyColumn: "Id",
                keyValue: new Guid("0d912f5f-299e-4c64-9a83-7022179c5fe8"));

            migrationBuilder.DeleteData(
                table: "Animal",
                keyColumn: "Id",
                keyValue: new Guid("1f476589-3da3-4309-b319-b4d6514beb8f"));

            migrationBuilder.DeleteData(
                table: "Animal",
                keyColumn: "Id",
                keyValue: new Guid("22b2a917-8163-41a8-bc47-6f7c8e3e7cd4"));

            migrationBuilder.DeleteData(
                table: "Animal",
                keyColumn: "Id",
                keyValue: new Guid("38f15cf8-9741-4473-bc87-64243c9d1c60"));

            migrationBuilder.DeleteData(
                table: "Animal",
                keyColumn: "Id",
                keyValue: new Guid("63400f5d-0f1f-4693-add0-ceb3ab02dd55"));

            migrationBuilder.DeleteData(
                table: "Animal",
                keyColumn: "Id",
                keyValue: new Guid("6f41b38a-287e-4d62-a40c-be4c7850cb5a"));

            migrationBuilder.DeleteData(
                table: "Animal",
                keyColumn: "Id",
                keyValue: new Guid("7c8687e8-646e-40e4-8317-564d5bc0a7da"));

            migrationBuilder.DeleteData(
                table: "Animal",
                keyColumn: "Id",
                keyValue: new Guid("9c5cfaa8-a48b-422a-bd62-3fa26ee71e07"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("c902017c-e795-40f2-bcc6-66d0119cd409"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("ff2b332b-4f48-49b0-bd9a-935b025bc73c"));

            migrationBuilder.InsertData(
                table: "Animal",
                columns: new[] { "Id", "Dog_Breed", "Discriminator", "Name", "Dog_Weight" },
                values: new object[] { new Guid("07001207-f0ca-47f4-8f98-305825f64679"), "Cocker Spaniel", "Dog", "Pim", 15 });

            migrationBuilder.InsertData(
                table: "Animal",
                columns: new[] { "Id", "CanFly", "Color", "Discriminator", "Name" },
                values: new object[] { new Guid("4ab87708-8db2-43a9-8a70-512a57fb7f28"), true, "Blue", "Bird", "Peppe" });

            migrationBuilder.InsertData(
                table: "Animal",
                columns: new[] { "Id", "Breed", "Discriminator", "LikesToPlay", "Name", "Weight" },
                values: new object[,]
                {
                    { new Guid("b8f5937a-2579-4bd0-b786-23bed0ca5b97"), "Maine Coon", "Cat", true, "Sigge", 10 },
                    { new Guid("cae1346c-deef-4180-9aec-12ba98ea55ef"), "Ragdoll", "Cat", true, "Lisa", 8 },
                    { new Guid("d0f80c75-bfa2-41b3-bfc5-79e72a0065fc"), "British Shorthair", "Cat", true, "Kajsa", 4 }
                });

            migrationBuilder.InsertData(
                table: "Animal",
                columns: new[] { "Id", "CanFly", "Color", "Discriminator", "Name" },
                values: new object[] { new Guid("d6cb732d-a317-41ee-a7c9-c944b03aabc3"), true, "Yellow", "Bird", "Charlie" });

            migrationBuilder.InsertData(
                table: "Animal",
                columns: new[] { "Id", "Dog_Breed", "Discriminator", "Name", "Dog_Weight" },
                values: new object[] { new Guid("e1dd04f3-e383-4423-882e-c0f4905a320a"), "Bernese Mountain Dog", "Dog", "Luffsen", 60 });

            migrationBuilder.InsertData(
                table: "Animal",
                columns: new[] { "Id", "CanFly", "Color", "Discriminator", "Name" },
                values: new object[] { new Guid("f50c5c81-756c-41f8-a53b-48a6017b0eff"), false, "Green", "Bird", "Kiwi" });

            migrationBuilder.InsertData(
                table: "Animal",
                columns: new[] { "Id", "Dog_Breed", "Discriminator", "Name", "Dog_Weight" },
                values: new object[] { new Guid("faf64302-6f53-4594-a436-fae4eb13e05f"), "English Bulldog", "Dog", "Boss", 30 });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Password", "UserName" },
                values: new object[,]
                {
                    { new Guid("0b4faff3-220b-458a-9b3c-6ccad4944a1e"), "$2a$11$cX3VvrsJs99sF/4hIdzLS.djNcNgpSM1YJ3a85U7LyV/m0spVPqJC", "string" },
                    { new Guid("16dfa628-e271-4e44-a468-776596ee3bf6"), "$2a$11$B6eiS8e2OrTVWBbeiymNyud38t3pKNULLgjrIeIq4hVS9Oi6.vUiu", "admin" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Animal",
                keyColumn: "Id",
                keyValue: new Guid("07001207-f0ca-47f4-8f98-305825f64679"));

            migrationBuilder.DeleteData(
                table: "Animal",
                keyColumn: "Id",
                keyValue: new Guid("4ab87708-8db2-43a9-8a70-512a57fb7f28"));

            migrationBuilder.DeleteData(
                table: "Animal",
                keyColumn: "Id",
                keyValue: new Guid("b8f5937a-2579-4bd0-b786-23bed0ca5b97"));

            migrationBuilder.DeleteData(
                table: "Animal",
                keyColumn: "Id",
                keyValue: new Guid("cae1346c-deef-4180-9aec-12ba98ea55ef"));

            migrationBuilder.DeleteData(
                table: "Animal",
                keyColumn: "Id",
                keyValue: new Guid("d0f80c75-bfa2-41b3-bfc5-79e72a0065fc"));

            migrationBuilder.DeleteData(
                table: "Animal",
                keyColumn: "Id",
                keyValue: new Guid("d6cb732d-a317-41ee-a7c9-c944b03aabc3"));

            migrationBuilder.DeleteData(
                table: "Animal",
                keyColumn: "Id",
                keyValue: new Guid("e1dd04f3-e383-4423-882e-c0f4905a320a"));

            migrationBuilder.DeleteData(
                table: "Animal",
                keyColumn: "Id",
                keyValue: new Guid("f50c5c81-756c-41f8-a53b-48a6017b0eff"));

            migrationBuilder.DeleteData(
                table: "Animal",
                keyColumn: "Id",
                keyValue: new Guid("faf64302-6f53-4594-a436-fae4eb13e05f"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("0b4faff3-220b-458a-9b3c-6ccad4944a1e"));

            migrationBuilder.DeleteData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("16dfa628-e271-4e44-a468-776596ee3bf6"));

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
        }
    }
}
