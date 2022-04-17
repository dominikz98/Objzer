﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Core;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(DBContext))]
    [Migration("20220417192451_Init")]
    partial class Init
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.4");

            modelBuilder.Entity("api.Models.CTHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Changes")
                        .HasColumnType("TEXT");

                    b.Property<string>("Entity")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("EntityId");

                    b.ToTable("history", (string)null);
                });

            modelBuilder.Entity("api.Models.CTInterface", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<Guid?>("CTInterfaceAssignmentParentId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("CTInterfaceAssignmentParentId");

                    b.HasIndex("Deleted");

                    b.ToTable("interfaces", (string)null);
                });

            modelBuilder.Entity("api.Models.CTInterfaceAssignment", b =>
                {
                    b.Property<Guid>("ParentId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.HasKey("ParentId");

                    b.HasIndex("Deleted");

                    b.ToTable("interface_assignments", (string)null);
                });

            modelBuilder.Entity("api.Models.CTInterfaceProperty", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("InterfaceId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Required")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("Deleted");

                    b.HasIndex("InterfaceId");

                    b.ToTable("interface_properties", (string)null);
                });

            modelBuilder.Entity("api.Requests.GetContractsRequestHandler+InterfaceDTO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("InterfaceDTO");
                });

            modelBuilder.Entity("api.Requests.GetContractsRequestHandler+MinimalDTO", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("InterfaceDTOId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("InterfaceDTOId");

                    b.ToTable("MinimalDTO");
                });

            modelBuilder.Entity("api.Models.CTInterface", b =>
                {
                    b.HasOne("api.Models.CTInterfaceAssignment", null)
                        .WithMany("References")
                        .HasForeignKey("CTInterfaceAssignmentParentId");
                });

            modelBuilder.Entity("api.Models.CTInterfaceAssignment", b =>
                {
                    b.HasOne("api.Models.CTInterface", "Parent")
                        .WithOne("Implementations")
                        .HasForeignKey("api.Models.CTInterfaceAssignment", "ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("api.Models.CTInterfaceProperty", b =>
                {
                    b.HasOne("api.Models.CTInterface", "Interface")
                        .WithMany("Properties")
                        .HasForeignKey("InterfaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Interface");
                });

            modelBuilder.Entity("api.Requests.GetContractsRequestHandler+MinimalDTO", b =>
                {
                    b.HasOne("api.Requests.GetContractsRequestHandler+InterfaceDTO", null)
                        .WithMany("Properties")
                        .HasForeignKey("InterfaceDTOId");
                });

            modelBuilder.Entity("api.Models.CTInterface", b =>
                {
                    b.Navigation("Implementations")
                        .IsRequired();

                    b.Navigation("Properties");
                });

            modelBuilder.Entity("api.Models.CTInterfaceAssignment", b =>
                {
                    b.Navigation("References");
                });

            modelBuilder.Entity("api.Requests.GetContractsRequestHandler+InterfaceDTO", b =>
                {
                    b.Navigation("Properties");
                });
#pragma warning restore 612, 618
        }
    }
}