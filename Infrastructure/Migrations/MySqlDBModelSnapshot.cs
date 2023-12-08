﻿// <auto-generated />
using System;
using Infrastructure.RealDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(MySqlDB))]
    partial class MySqlDBModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Domain.Models.Bird", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("CanFly")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Birds");
                });

            modelBuilder.Entity("Domain.Models.Cat", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<bool>("LikesToPlay")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Cats");
                });

            modelBuilder.Entity("Domain.Models.Dog", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Dogs");

                    b.HasData(
                        new
                        {
                            Id = new Guid("56f45a16-3f5f-4d24-b330-4211854f2f1c"),
                            Name = "Boss"
                        },
                        new
                        {
                            Id = new Guid("f28b9e55-24b9-4a17-8ae8-38669a5279c1"),
                            Name = "Luffsen"
                        },
                        new
                        {
                            Id = new Guid("8c54a701-6c28-4378-a6ce-ac5bb56cf47c"),
                            Name = "Pim"
                        });
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("5a58d680-1fbb-4112-9ee9-1490ffdc001b"),
                            Password = "string",
                            UserName = "string"
                        },
                        new
                        {
                            Id = new Guid("bbe94aed-a2e4-4d4f-9a75-5612f06cbeca"),
                            Password = "admin",
                            UserName = "admin"
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
