﻿// <auto-generated />
using System;
using IUPPartyService.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace IUPPartyService.Migrations
{
    [DbContext(typeof(IUPPartyContext))]
    [Migration("20201101152741_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.9")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("IUPPartyService.Models.Event", b =>
                {
                    b.Property<string>("EventID")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("nvarchar(450)");

                    b.Property<DateTime>("DateEnd")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("DateStart")
                        .HasColumnType("datetime2");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Hidden")
                        .HasColumnType("bit");

                    b.Property<string>("Host")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("HostName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<byte[]>("ImageData")
                        .IsRequired()
                        .HasColumnType("varbinary(max)");

                    b.Property<double>("Latitude")
                        .HasColumnType("float");

                    b.Property<double>("Longitude")
                        .HasColumnType("float");

                    b.Property<int>("MaxPeople")
                        .HasColumnType("int");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Password")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("RequirePassword")
                        .HasColumnType("bit");

                    b.HasKey("EventID");

                    b.ToTable("Events");
                });

            modelBuilder.Entity("IUPPartyService.Models.Participant", b =>
                {
                    b.Property<string>("ParticipantKennitala")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("EventRef")
                        .IsRequired()
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("ParticipantName")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ParticipantKennitala");

                    b.HasIndex("EventRef");

                    b.ToTable("Participant");
                });

            modelBuilder.Entity("IUPPartyService.Models.Participant", b =>
                {
                    b.HasOne("IUPPartyService.Models.Event", "Event")
                        .WithMany("Participant")
                        .HasForeignKey("EventRef")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
