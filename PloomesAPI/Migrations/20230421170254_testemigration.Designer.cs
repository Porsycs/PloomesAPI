﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using PloomesAPI.Model.Context;

#nullable disable

namespace PloomesAPI.Migrations
{
    [DbContext(typeof(BancoContext))]
    [Migration("20230421170254_testemigration")]
    partial class testemigration
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("PloomesAPI.Common.Cliente", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uniqueidentifier");

                    b.Property<DateTime?>("Alteracao")
                        .HasColumnType("datetime2");

                    b.Property<Guid?>("CodigoExterno")
                        .HasColumnType("uniqueidentifier");

                    b.Property<bool>("Excluido")
                        .HasColumnType("bit");

                    b.Property<DateTime>("Inclusao")
                        .HasColumnType("datetime2");

                    b.HasKey("Id");

                    b.ToTable("Clientes");
                });
#pragma warning restore 612, 618
        }
    }
}