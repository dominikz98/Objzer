﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using api.Core;

#nullable disable

namespace api.Migrations
{
    [DbContext(typeof(ObjzerContext))]
    partial class ObjzerContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder.HasAnnotation("ProductVersion", "6.0.3");

            modelBuilder.Entity("api.Models.CatalogueInterface", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("TEXT");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("HexColor")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.Property<string>("Short")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("interfaces", (string)null);
                });

            modelBuilder.Entity("api.Models.CatalogueObject", b =>
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

                    b.ToTable("objects", (string)null);
                });

            modelBuilder.Entity("CatalogueInterfaceCatalogueObject", b =>
                {
                    b.Property<Guid>("InterfacesId")
                        .HasColumnType("TEXT");

                    b.Property<Guid>("ObjectsId")
                        .HasColumnType("TEXT");

                    b.HasKey("InterfacesId", "ObjectsId");

                    b.HasIndex("ObjectsId");

                    b.ToTable("CatalogueInterfaceCatalogueObject");
                });

            modelBuilder.Entity("CatalogueInterfaceCatalogueObject", b =>
                {
                    b.HasOne("api.Models.CatalogueInterface", null)
                        .WithMany()
                        .HasForeignKey("InterfacesId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("api.Models.CatalogueObject", null)
                        .WithMany()
                        .HasForeignKey("ObjectsId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}
