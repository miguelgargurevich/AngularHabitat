﻿// <auto-generated />
using System;
using AngularCRUDvs.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AngularCRUDvs.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20231119192841_NuevaMigracion2")]
    partial class NuevaMigracion2
    {
        /// <inheritdoc />
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.14")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AngularCRUDvs.Entidades.Concepto", b =>
                {
                    b.Property<int>("ConceptoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ConceptoId"));

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<bool>("EsCalculado")
                        .HasColumnType("bit");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ConceptoId");

                    b.ToTable("Concepto");
                });

            modelBuilder.Entity("AngularCRUDvs.Entidades.Persona", b =>
                {
                    b.Property<int>("PersonaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PersonaId"));

                    b.Property<string>("ApellidoMaterno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ApellidoPaterno")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombres")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NroDocumento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Telefono")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("TipoDocumento")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UnidadId")
                        .HasColumnType("int");

                    b.HasKey("PersonaId");

                    b.ToTable("Persona");
                });

            modelBuilder.Entity("AngularCRUDvs.Entidades.Recibo", b =>
                {
                    b.Property<int>("ReciboId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReciboId"));

                    b.Property<int>("Anio")
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaEmision")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaVencimiento")
                        .HasColumnType("datetime2");

                    b.Property<int>("Mes")
                        .HasColumnType("int");

                    b.HasKey("ReciboId");

                    b.ToTable("Recibo");
                });

            modelBuilder.Entity("AngularCRUDvs.Entidades.ReciboConcepto", b =>
                {
                    b.Property<int>("ReciboConceptoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReciboConceptoId"));

                    b.Property<int>("ConceptoId")
                        .HasColumnType("int");

                    b.Property<int>("ReciboId")
                        .HasColumnType("int");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("UnidadId")
                        .HasColumnType("int");

                    b.HasKey("ReciboConceptoId");

                    b.ToTable("ReciboConcepto");
                });

            modelBuilder.Entity("AngularCRUDvs.Entidades.ReciboPago", b =>
                {
                    b.Property<int>("ReciboPagoId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReciboPagoId"));

                    b.Property<DateTime?>("FechaPago")
                        .HasColumnType("datetime2");

                    b.Property<decimal?>("MontoPago")
                        .HasColumnType("decimal(18,2)");

                    b.Property<int>("ReciboId")
                        .HasColumnType("int");

                    b.Property<int>("UnidadId")
                        .HasColumnType("int");

                    b.Property<string>("nombreVoucher")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("urlVoucher")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("ReciboPagoId");

                    b.ToTable("ReciboPago");
                });

            modelBuilder.Entity("AngularCRUDvs.Entidades.Role", b =>
                {
                    b.Property<int>("RoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("RoleId"));

                    b.Property<string>("Estado")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Name")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("RoleId");

                    b.ToTable("Role");
                });

            modelBuilder.Entity("AngularCRUDvs.Entidades.Unidad", b =>
                {
                    b.Property<int>("UnidadId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UnidadId"));

                    b.Property<string>("Block")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Dpto")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UnidadId");

                    b.ToTable("Unidad");
                });

            modelBuilder.Entity("AngularCRUDvs.Entidades.User", b =>
                {
                    b.Property<int>("UserId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserId"));

                    b.Property<bool?>("Estado")
                        .HasColumnType("bit");

                    b.Property<string>("PasswordHash")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("UserName")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("UserId");

                    b.ToTable("User");
                });

            modelBuilder.Entity("AngularCRUDvs.Entidades.UserRole", b =>
                {
                    b.Property<int>("UserRoleId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("UserRoleId"));

                    b.Property<int>("RoleId")
                        .HasColumnType("int");

                    b.Property<int>("UserId")
                        .HasColumnType("int");

                    b.HasKey("UserRoleId");

                    b.ToTable("UserRole");
                });
#pragma warning restore 612, 618
        }
    }
}
