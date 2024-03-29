﻿// <auto-generated />
using System;
using Firebird.Embedded.AspNetCore.Data;
using FirebirdSql.EntityFrameworkCore.Firebird.Metadata;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Firebird.Embedded.AspNetCore.Migrations.Data
{
    [DbContext(typeof(DataDbContext))]
    [Migration("20221125091721_InitModel")]
    partial class InitModel
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Fb:ValueGenerationStrategy", FbValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "6.0.11")
                .HasAnnotation("Relational:MaxIdentifierLength", 31);

            modelBuilder.Entity("Firebird.Embedded.AspNetCore.Data.Student", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("VARCHAR(256)");

                    b.Property<int?>("Age")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("IsRegularStudent")
                        .HasColumnType("BOOLEAN")
                        .HasDefaultValue(true);

                    b.HasKey("Id");

                    b.ToTable("Students");
                });
#pragma warning restore 612, 618
        }
    }
}
