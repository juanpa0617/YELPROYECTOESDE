﻿// <auto-generated />
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using YELPROYECTOESDE.Data;

#nullable disable

namespace YELPROYECTOESDE.Migrations
{
    [DbContext(typeof(AlojamientoDbContext))]
    [Migration("20241122123745_NewMigration22112024")]
    partial class NewMigration22112024
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.0")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("YELPROYECTOESDE.Models.Alojamiento", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("Capacidad")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ImagenUrl")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<int>("TipoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TipoId");

                    b.ToTable("Alojamientos");
                });

            modelBuilder.Entity("YELPROYECTOESDE.Models.Comodidad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Descripcion")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Comodidades");
                });

            modelBuilder.Entity("YELPROYECTOESDE.Models.DetalleAlojamientoComodidad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("ComodidadId")
                        .HasColumnType("int");

                    b.Property<int>("IdAlojamiento")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ComodidadId");

                    b.HasIndex("IdAlojamiento");

                    b.ToTable("DetallesAlojamientoComodidad");
                });

            modelBuilder.Entity("YELPROYECTOESDE.Models.Tipo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.ToTable("Tipos");
                });

            modelBuilder.Entity("YELPROYECTOESDE.Models.Alojamiento", b =>
                {
                    b.HasOne("YELPROYECTOESDE.Models.Tipo", "Tipo")
                        .WithMany("Alojamientos")
                        .HasForeignKey("TipoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tipo");
                });

            modelBuilder.Entity("YELPROYECTOESDE.Models.DetalleAlojamientoComodidad", b =>
                {
                    b.HasOne("YELPROYECTOESDE.Models.Comodidad", "Comodidad")
                        .WithMany("DetallesAlojamientoComodidad")
                        .HasForeignKey("ComodidadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("YELPROYECTOESDE.Models.Alojamiento", "Alojamiento")
                        .WithMany("DetallesAlojamientoComodidad")
                        .HasForeignKey("IdAlojamiento")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Alojamiento");

                    b.Navigation("Comodidad");
                });

            modelBuilder.Entity("YELPROYECTOESDE.Models.Alojamiento", b =>
                {
                    b.Navigation("DetallesAlojamientoComodidad");
                });

            modelBuilder.Entity("YELPROYECTOESDE.Models.Comodidad", b =>
                {
                    b.Navigation("DetallesAlojamientoComodidad");
                });

            modelBuilder.Entity("YELPROYECTOESDE.Models.Tipo", b =>
                {
                    b.Navigation("Alojamientos");
                });
#pragma warning restore 612, 618
        }
    }
}