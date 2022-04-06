﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Core;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(DBContext))]
    partial class DBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.3");

            modelBuilder.Entity("api.Models.CTAbstractionAssignment", b =>
                {
                    b.Property<Guid>("ParentId")
                        .HasColumnType("TEXT");

                    b.HasKey("ParentId");

                    b.ToTable("abstraction_assignments", (string)null);
                });

            modelBuilder.Entity("api.Models.CTEntity", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("CTEntity");
                });

            modelBuilder.Entity("api.Models.CTHistory", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Changes")
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

            modelBuilder.Entity("api.Models.CTInterfaceAssignment", b =>
                {
                    b.Property<Guid>("ParentId")
                        .HasColumnType("TEXT");

                    b.HasKey("ParentId");

                    b.ToTable("interface_assignments", (string)null);
                });

            modelBuilder.Entity("CTAbstractionCTObject", b =>
                {
                    b.Property<Guid>("AbstractionsId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ObjectsId")
                        .HasColumnType("TEXT");

                    b.HasKey("AbstractionsId", "ObjectsId");

                    b.HasIndex("ObjectsId");

                    b.ToTable("CTAbstractionCTObject");
                });

            modelBuilder.Entity("CTInterfaceCTObject", b =>
                {
                    b.Property<Guid>("InterfacesId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ObjectsId")
                        .HasColumnType("TEXT");

                    b.HasKey("InterfacesId", "ObjectsId");

                    b.HasIndex("ObjectsId");

                    b.ToTable("CTInterfaceCTObject");
                });

            modelBuilder.Entity("api.Models.CTAbstraction", b =>
                {
                    b.HasBaseType("api.Models.CTEntity");

                    b.Property<Guid?>("CTAbstractionAssignmentParentId")
                        .HasColumnType("TEXT");

                    b.HasIndex("CTAbstractionAssignmentParentId");

                    b.ToTable("abstractions", (string)null);
                });

            modelBuilder.Entity("api.Models.CTAbstractionProperty", b =>
                {
                    b.HasBaseType("api.Models.CTEntity");

                    b.Property<Guid>("AbstractionId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Required")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasIndex("AbstractionId");

                    b.ToTable("abstraction_properties", (string)null);
                });

            modelBuilder.Entity("api.Models.CTEnumeration", b =>
                {
                    b.HasBaseType("api.Models.CTEntity");

                    b.ToTable("enumerations", (string)null);
                });

            modelBuilder.Entity("api.Models.CTEnumerationProperty", b =>
                {
                    b.HasBaseType("api.Models.CTEntity");

                    b.Property<Guid>("EnumerationId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Required")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasIndex("EnumerationId");

                    b.ToTable("enumeration_properties", (string)null);
                });

            modelBuilder.Entity("api.Models.CTInterface", b =>
                {
                    b.HasBaseType("api.Models.CTEntity");

                    b.Property<Guid?>("CTInterfaceAssignmentParentId")
                        .HasColumnType("TEXT");

                    b.HasIndex("CTInterfaceAssignmentParentId");

                    b.ToTable("interfaces", (string)null);
                });

            modelBuilder.Entity("api.Models.CTInterfaceProperty", b =>
                {
                    b.HasBaseType("api.Models.CTEntity");

                    b.Property<Guid>("InterfaceId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Required")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasIndex("InterfaceId");

                    b.ToTable("interface_properties", (string)null);
                });

            modelBuilder.Entity("api.Models.CTObject", b =>
                {
                    b.HasBaseType("api.Models.CTEntity");

                    b.ToTable("objects", (string)null);
                });

            modelBuilder.Entity("api.Models.CTObjectProperty", b =>
                {
                    b.HasBaseType("api.Models.CTEntity");

                    b.Property<string>("Column")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<bool>("Key")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("MaxLength")
                        .HasColumnType("INTEGER");

                    b.Property<Guid?>("ObjectId")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Required")
                        .HasColumnType("INTEGER");

                    b.Property<int?>("StringLength")
                        .HasColumnType("INTEGER");

                    b.Property<int>("Type")
                        .HasColumnType("INTEGER");

                    b.HasIndex("ObjectId");

                    b.ToTable("object_properties", (string)null);
                });

            modelBuilder.Entity("api.Models.CTAbstractionAssignment", b =>
                {
                    b.HasOne("api.Models.CTAbstraction", "Parent")
                        .WithMany("Inheritances")
                        .HasForeignKey("ParentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Parent");
                });

            modelBuilder.Entity("api.Models.CTHistory", b =>
                {
                    b.HasOne("api.Models.CTEntity", "Entity")
                        .WithMany("History")
                        .HasForeignKey("EntityId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Entity");
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

            modelBuilder.Entity("CTAbstractionCTObject", b =>
                {
                    b.HasOne("api.Models.CTAbstraction", null)
                        .WithMany()
                        .HasForeignKey("AbstractionsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.CTObject", null)
                        .WithMany()
                        .HasForeignKey("ObjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("CTInterfaceCTObject", b =>
                {
                    b.HasOne("api.Models.CTInterface", null)
                        .WithMany()
                        .HasForeignKey("InterfacesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.CTObject", null)
                        .WithMany()
                        .HasForeignKey("ObjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api.Models.CTAbstraction", b =>
                {
                    b.HasOne("api.Models.CTAbstractionAssignment", null)
                        .WithMany("Children")
                        .HasForeignKey("CTAbstractionAssignmentParentId");

                    b.HasOne("api.Models.CTEntity", null)
                        .WithOne()
                        .HasForeignKey("api.Models.CTAbstraction", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api.Models.CTAbstractionProperty", b =>
                {
                    b.HasOne("api.Models.CTAbstraction", "Abstraction")
                        .WithMany("Properties")
                        .HasForeignKey("AbstractionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.CTEntity", null)
                        .WithOne()
                        .HasForeignKey("api.Models.CTAbstractionProperty", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Abstraction");
                });

            modelBuilder.Entity("api.Models.CTEnumeration", b =>
                {
                    b.HasOne("api.Models.CTEntity", null)
                        .WithOne()
                        .HasForeignKey("api.Models.CTEnumeration", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api.Models.CTEnumerationProperty", b =>
                {
                    b.HasOne("api.Models.CTEnumeration", "Enumeration")
                        .WithMany("Properties")
                        .HasForeignKey("EnumerationId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.CTEntity", null)
                        .WithOne()
                        .HasForeignKey("api.Models.CTEnumerationProperty", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Enumeration");
                });

            modelBuilder.Entity("api.Models.CTInterface", b =>
                {
                    b.HasOne("api.Models.CTInterfaceAssignment", null)
                        .WithMany("References")
                        .HasForeignKey("CTInterfaceAssignmentParentId");

                    b.HasOne("api.Models.CTEntity", null)
                        .WithOne()
                        .HasForeignKey("api.Models.CTInterface", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api.Models.CTInterfaceProperty", b =>
                {
                    b.HasOne("api.Models.CTEntity", null)
                        .WithOne()
                        .HasForeignKey("api.Models.CTInterfaceProperty", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.CTInterface", "Interface")
                        .WithMany("Properties")
                        .HasForeignKey("InterfaceId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Interface");
                });

            modelBuilder.Entity("api.Models.CTObject", b =>
                {
                    b.HasOne("api.Models.CTEntity", null)
                        .WithOne()
                        .HasForeignKey("api.Models.CTObject", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("api.Models.CTObjectProperty", b =>
                {
                    b.HasOne("api.Models.CTEntity", null)
                        .WithOne()
                        .HasForeignKey("api.Models.CTObjectProperty", "Id")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.CTObject", "Object")
                        .WithMany("Properties")
                        .HasForeignKey("ObjectId");

                    b.Navigation("Object");
                });

            modelBuilder.Entity("api.Models.CTAbstractionAssignment", b =>
                {
                    b.Navigation("Children");
                });

            modelBuilder.Entity("api.Models.CTEntity", b =>
                {
                    b.Navigation("History");
                });

            modelBuilder.Entity("api.Models.CTInterfaceAssignment", b =>
                {
                    b.Navigation("References");
                });

            modelBuilder.Entity("api.Models.CTAbstraction", b =>
                {
                    b.Navigation("Inheritances");

                    b.Navigation("Properties");
                });

            modelBuilder.Entity("api.Models.CTEnumeration", b =>
                {
                    b.Navigation("Properties");
                });

            modelBuilder.Entity("api.Models.CTInterface", b =>
                {
                    b.Navigation("Implementations")
                        .IsRequired();

                    b.Navigation("Properties");
                });

            modelBuilder.Entity("api.Models.CTObject", b =>
                {
                    b.Navigation("Properties");
                });
#pragma warning restore 612, 618
        }
    }
}
