﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;
using WebApi.Contexts;

namespace WebApi.Migrations.AppointmentDB
{
    [DbContext(typeof(AppointmentDBContext))]
    [Migration("20171103130638_idhMigration")]
    partial class idhMigration
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.0.0-rtm-26452")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("WebApi.Models.Appointment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<bool>("Accepted");

                    b.Property<int?>("BenchId");

                    b.Property<int>("ClientID");

                    b.Property<string>("Date");

                    b.Property<string>("HealthworkerName");

                    b.Property<string>("Time");

                    b.HasKey("Id");

                    b.HasIndex("BenchId");

                    b.ToTable("Appointments");
                });

            modelBuilder.Entity("WebApi.Models.Bench", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("District");

                    b.Property<string>("Housenumber");

                    b.Property<string>("Province");

                    b.Property<string>("Streetname");

                    b.HasKey("Id");

                    b.ToTable("Benches");
                });

            modelBuilder.Entity("WebApi.Models.Appointment", b =>
                {
                    b.HasOne("WebApi.Models.Bench", "Bench")
                        .WithMany()
                        .HasForeignKey("BenchId");
                });
#pragma warning restore 612, 618
        }
    }
}
