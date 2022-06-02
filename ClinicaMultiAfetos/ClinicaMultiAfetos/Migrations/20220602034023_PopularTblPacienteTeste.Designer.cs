﻿// <auto-generated />
using System;
using ClinicaMultiAfetos.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace ClinicaMultiAfetos.Migrations
{
    [DbContext(typeof(AppDbContext))]
    [Migration("20220602034023_PopularTblPacienteTeste")]
    partial class PopularTblPacienteTeste
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder, 1L, 1);

            modelBuilder.Entity("ClinicaMultiAfetos.Models.DocumentoClinica", b =>
                {
                    b.Property<int>("DocumentoClinicaId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DocumentoClinicaId"), 1L, 1);

                    b.Property<DateTime>("DataCriacao")
                        .HasColumnType("datetime2");

                    b.Property<string>("Descricao")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<bool>("IsDocumentoFavorito")
                        .HasColumnType("bit");

                    b.HasKey("DocumentoClinicaId");

                    b.ToTable("DocumentosClinica");
                });

            modelBuilder.Entity("ClinicaMultiAfetos.Models.DocumentoPaciente", b =>
                {
                    b.Property<int>("DocumentoPacienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("DocumentoPacienteId"), 1L, 1);

                    b.Property<string>("DocumentoUrl")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<string>("NomeArquivo")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("PacienteId")
                        .HasColumnType("int");

                    b.HasKey("DocumentoPacienteId");

                    b.HasIndex("PacienteId");

                    b.ToTable("DocumentosPaciente");
                });

            modelBuilder.Entity("ClinicaMultiAfetos.Models.Paciente", b =>
                {
                    b.Property<int>("PacienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PacienteId"), 1L, 1);

                    b.Property<string>("Cid")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("Cpf")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.Property<DateTime>("DataNascimento")
                        .HasColumnType("datetime2");

                    b.Property<string>("Endereco")
                        .HasMaxLength(150)
                        .HasColumnType("nvarchar(150)");

                    b.Property<string>("EnderecoCep")
                        .HasMaxLength(8)
                        .HasColumnType("nvarchar(8)");

                    b.Property<string>("EnderecoCidade")
                        .HasMaxLength(50)
                        .HasColumnType("nvarchar(50)");

                    b.Property<string>("EnderecoNumero")
                        .HasMaxLength(15)
                        .HasColumnType("nvarchar(15)");

                    b.Property<string>("EnderecoUf")
                        .HasMaxLength(2)
                        .HasColumnType("nvarchar(2)");

                    b.Property<string>("NomeCompleto")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("PaiMae")
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Plano")
                        .HasMaxLength(30)
                        .HasColumnType("nvarchar(30)");

                    b.Property<string>("QueixaInicial")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Responsavel")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("nvarchar(100)");

                    b.Property<string>("Rg")
                        .HasMaxLength(10)
                        .HasColumnType("nvarchar(10)");

                    b.Property<string>("TelefoneContato")
                        .IsRequired()
                        .HasMaxLength(11)
                        .HasColumnType("nvarchar(11)");

                    b.HasKey("PacienteId");

                    b.ToTable("Pacientes");
                });

            modelBuilder.Entity("ClinicaMultiAfetos.Models.PlanoPaciente", b =>
                {
                    b.Property<int>("PlanoPacienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("PlanoPacienteId"), 1L, 1);

                    b.Property<bool>("IsPlanoFavorito")
                        .HasColumnType("bit");

                    b.Property<string>("NomePlano")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("PacienteId")
                        .HasColumnType("int");

                    b.HasKey("PlanoPacienteId");

                    b.HasIndex("PacienteId");

                    b.ToTable("PlanosPaciente");
                });

            modelBuilder.Entity("ClinicaMultiAfetos.Models.ReciboPaciente", b =>
                {
                    b.Property<int>("ReciboPacienteId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("ReciboPacienteId"), 1L, 1);

                    b.Property<string>("DocumentoUrl")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<bool>("IsReciboFavorito")
                        .HasColumnType("bit");

                    b.Property<string>("NomeArquivo")
                        .HasMaxLength(250)
                        .HasColumnType("nvarchar(250)");

                    b.Property<int>("PacienteId")
                        .HasColumnType("int");

                    b.HasKey("ReciboPacienteId");

                    b.HasIndex("PacienteId");

                    b.ToTable("RecibosPaciente");
                });

            modelBuilder.Entity("ClinicaMultiAfetos.Models.DocumentoPaciente", b =>
                {
                    b.HasOne("ClinicaMultiAfetos.Models.Paciente", "Paciente")
                        .WithMany("DocumentosPaciente")
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("ClinicaMultiAfetos.Models.PlanoPaciente", b =>
                {
                    b.HasOne("ClinicaMultiAfetos.Models.Paciente", "Paciente")
                        .WithMany("PlanosPaciente")
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("ClinicaMultiAfetos.Models.ReciboPaciente", b =>
                {
                    b.HasOne("ClinicaMultiAfetos.Models.Paciente", "Paciente")
                        .WithMany("RecibosPaciente")
                        .HasForeignKey("PacienteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Paciente");
                });

            modelBuilder.Entity("ClinicaMultiAfetos.Models.Paciente", b =>
                {
                    b.Navigation("DocumentosPaciente");

                    b.Navigation("PlanosPaciente");

                    b.Navigation("RecibosPaciente");
                });
#pragma warning restore 612, 618
        }
    }
}