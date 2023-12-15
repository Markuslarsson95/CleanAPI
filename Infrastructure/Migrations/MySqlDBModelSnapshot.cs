﻿// <auto-generated />
using System;
using Infrastructure.RealDatabase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
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
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AnimalUser", b =>
                {
                    b.Property<Guid>("AnimalsId")
                        .HasColumnType("uniqueidentifier");

                    b.Property<Guid>("UsersId")
                        .HasColumnType("uniqueidentifier");

                    b.HasKey("AnimalsId", "UsersId");

                    b.HasIndex("UsersId");

                    b.ToTable("AnimalUser");
                });

            modelBuilder.Entity("Domain.Models.Animals.Animal", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Discriminator")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Animal");

                    b.HasDiscriminator<string>("Discriminator").HasValue("Animal");

                    b.UseTphMappingStrategy();
                });

            modelBuilder.Entity("Domain.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Users");

                    b.HasData(
                        new
                        {
                            Id = new Guid("ff2b332b-4f48-49b0-bd9a-935b025bc73c"),
                            Password = "admin",
                            UserName = "admin"
                        },
                        new
                        {
                            Id = new Guid("c902017c-e795-40f2-bcc6-66d0119cd409"),
                            Password = "string",
                            UserName = "string"
                        });
                });

            modelBuilder.Entity("Domain.Models.Animals.Bird", b =>
                {
                    b.HasBaseType("Domain.Models.Animals.Animal");

                    b.Property<bool>("CanFly")
                        .HasColumnType("bit");

                    b.Property<string>("Color")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasDiscriminator().HasValue("Bird");

                    b.HasData(
                        new
                        {
                            Id = new Guid("22b2a917-8163-41a8-bc47-6f7c8e3e7cd4"),
                            Name = "Peppe",
                            CanFly = true,
                            Color = "Blue"
                        },
                        new
                        {
                            Id = new Guid("6f41b38a-287e-4d62-a40c-be4c7850cb5a"),
                            Name = "Charlie",
                            CanFly = true,
                            Color = "Yellow"
                        },
                        new
                        {
                            Id = new Guid("38f15cf8-9741-4473-bc87-64243c9d1c60"),
                            Name = "Kiwi",
                            CanFly = false,
                            Color = "Green"
                        });
                });

            modelBuilder.Entity("Domain.Models.Animals.Cat", b =>
                {
                    b.HasBaseType("Domain.Models.Animals.Animal");

                    b.Property<string>("Breed")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("LikesToPlay")
                        .HasColumnType("bit");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.HasDiscriminator().HasValue("Cat");

                    b.HasData(
                        new
                        {
                            Id = new Guid("1f476589-3da3-4309-b319-b4d6514beb8f"),
                            Name = "Kajsa",
                            Breed = "British Shorthair",
                            LikesToPlay = true,
                            Weight = 4
                        },
                        new
                        {
                            Id = new Guid("7c8687e8-646e-40e4-8317-564d5bc0a7da"),
                            Name = "Sigge",
                            Breed = "Maine Coon",
                            LikesToPlay = true,
                            Weight = 10
                        },
                        new
                        {
                            Id = new Guid("9c5cfaa8-a48b-422a-bd62-3fa26ee71e07"),
                            Name = "Lisa",
                            Breed = "Ragdoll",
                            LikesToPlay = true,
                            Weight = 8
                        });
                });

            modelBuilder.Entity("Domain.Models.Animals.Dog", b =>
                {
                    b.HasBaseType("Domain.Models.Animals.Animal");

                    b.Property<string>("Breed")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Weight")
                        .HasColumnType("int");

                    b.ToTable("Animal", t =>
                        {
                            t.Property("Breed")
                                .HasColumnName("Dog_Breed");

                            t.Property("Weight")
                                .HasColumnName("Dog_Weight");
                        });

                    b.HasDiscriminator().HasValue("Dog");

                    b.HasData(
                        new
                        {
                            Id = new Guid("076fbcea-d41c-4f95-bf0e-f9941d2b0019"),
                            Name = "Boss",
                            Breed = "English Bulldog",
                            Weight = 30
                        },
                        new
                        {
                            Id = new Guid("0d912f5f-299e-4c64-9a83-7022179c5fe8"),
                            Name = "Luffsen",
                            Breed = "Bernese Mountain Dog",
                            Weight = 60
                        },
                        new
                        {
                            Id = new Guid("63400f5d-0f1f-4693-add0-ceb3ab02dd55"),
                            Name = "Pim",
                            Breed = "Cocker Spaniel",
                            Weight = 15
                        });
                });

            modelBuilder.Entity("AnimalUser", b =>
                {
                    b.HasOne("Domain.Models.Animals.Animal", null)
                        .WithMany()
                        .HasForeignKey("AnimalsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Domain.Models.User", null)
                        .WithMany()
                        .HasForeignKey("UsersId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
