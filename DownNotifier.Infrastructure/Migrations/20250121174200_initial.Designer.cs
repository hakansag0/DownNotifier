﻿// <auto-generated />
using System;
using DownNotifier.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace DownNotifier.Infrastructure.Migrations
{
    [DbContext(typeof(DownNotifierDbContext))]
    [Migration("20250121174200_initial")]
    partial class initial
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("DownNotifier.Domain.Entities.TargetApp", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedDate")
                        .HasColumnOrder(7);

                    b.Property<DateTime>("LastCheckDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("LastCheckDate")
                        .HasColumnOrder(6);

                    b.Property<int>("MonitoringIntervalInSeconds")
                        .HasColumnType("int")
                        .HasColumnName("MonitoringIntervalInSeconds")
                        .HasColumnOrder(5);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name")
                        .HasColumnOrder(3);

                    b.Property<string>("URL")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("URL")
                        .HasColumnOrder(4);

                    b.Property<int>("UserId")
                        .HasColumnType("int")
                        .HasColumnName("UserID")
                        .HasColumnOrder(2);

                    b.HasKey("Id");

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("targetApps", (string)null);
                });

            modelBuilder.Entity("DownNotifier.Domain.Entities.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("Id")
                        .HasColumnOrder(1);

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("CreatedDate")
                        .HasColumnType("datetime2")
                        .HasColumnName("CreatedDate")
                        .HasColumnOrder(6);

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)")
                        .HasColumnName("Email")
                        .HasColumnOrder(2);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)")
                        .HasColumnName("Name")
                        .HasColumnOrder(3);

                    b.Property<byte[]>("PasswordHash")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("PasswordHash")
                        .HasColumnOrder(4);

                    b.Property<byte[]>("PasswordSalt")
                        .IsRequired()
                        .HasColumnType("varbinary(max)")
                        .HasColumnName("PasswordSalt")
                        .HasColumnOrder(5);

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("Id")
                        .IsUnique();

                    b.ToTable("users", (string)null);
                });
#pragma warning restore 612, 618
        }
    }
}
