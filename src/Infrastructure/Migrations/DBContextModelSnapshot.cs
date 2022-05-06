﻿// <auto-generated />
using System;
using Infrastructure.Core;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Infrastructure.Migrations
{
    [DbContext(typeof(ObjzerContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.4");

            modelBuilder.Entity("Core.Models.CTHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<int>("Action")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Changes")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("EntityId")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Timestamp")
                        .HasColumnType("TEXT");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("EntityId");

                    b.ToTable("history", (string)null);
                });

            modelBuilder.Entity("Core.Models.CTInterface", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<DateOnly?>("Archived")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Locked")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("Deleted");

                    b.ToTable("interfaces", (string)null);
                });

            modelBuilder.Entity("Core.Models.CTInterfaceAssignment", b =>
                {
                    b.Property<Guid>("ReferenceId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("DestinationId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.HasKey("ReferenceId", "DestinationId");

                    b.HasIndex("Deleted");

                    b.HasIndex("DestinationId");

                    b.ToTable("interface_assignments", (string)null);
                });

            modelBuilder.Entity("Core.Models.CTInterfaceProperty", b =>
                {
                    b.Property<Guid>("ReferenceId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Deleted")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Required")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasKey("ReferenceId", "Name");

                    b.HasIndex("Deleted");

                    b.ToTable("interface_properties", (string)null);
                });

            modelBuilder.Entity("Core.Models.CTInterfaceAssignment", b =>
                {
                    b.HasOne("Core.Models.CTInterface", "Destination")
                        .WithMany("Usings")
                        .HasForeignKey("DestinationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Core.Models.CTInterface", "Source")
                        .WithMany("Includings")
                        .HasForeignKey("ReferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Destination");

                    b.Navigation("Source");
                });

            modelBuilder.Entity("Core.Models.CTInterfaceProperty", b =>
                {
                    b.HasOne("Core.Models.CTInterface", "Interface")
                        .WithMany("Properties")
                        .HasForeignKey("ReferenceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Interface");
                });

            modelBuilder.Entity("Core.Models.CTInterface", b =>
                {
                    b.Navigation("Includings");

                    b.Navigation("Properties");

                    b.Navigation("Usings");
                });
#pragma warning restore 612, 618
        }
    }
}