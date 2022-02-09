﻿// <auto-generated />
using System;
using AS3_RM5831.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace AS3_RM5831.Migrations
{
    [DbContext(typeof(AuditContext))]
    partial class AuditContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("ProductVersion", "5.0.12")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("AS3_RM5831.Models.Audit", b =>
                {
                    b.Property<int>("AuditId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<double?>("Amount")
                        .HasColumnType("float");

                    b.Property<string>("AuditTypeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime?>("CreatedDate")
                        .HasColumnType("datetime2");

                    b.Property<string>("PlayerName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AuditId");

                    b.HasIndex("AuditTypeId");

                    b.ToTable("Audits");
                });

            modelBuilder.Entity("AS3_RM5831.Models.AuditType", b =>
                {
                    b.Property<string>("AuditTypeId")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("AuditTypeId");

                    b.ToTable("AuditTypes");

                    b.HasData(
                        new
                        {
                            AuditTypeId = "CashIn",
                            Name = "Cash In"
                        },
                        new
                        {
                            AuditTypeId = "CashOut",
                            Name = "Cash Out"
                        },
                        new
                        {
                            AuditTypeId = "Win",
                            Name = "Win"
                        },
                        new
                        {
                            AuditTypeId = "Lose",
                            Name = "Lose"
                        });
                });

            modelBuilder.Entity("AS3_RM5831.Models.Audit", b =>
                {
                    b.HasOne("AS3_RM5831.Models.AuditType", "AuditType")
                        .WithMany()
                        .HasForeignKey("AuditTypeId");

                    b.Navigation("AuditType");
                });
#pragma warning restore 612, 618
        }
    }
}