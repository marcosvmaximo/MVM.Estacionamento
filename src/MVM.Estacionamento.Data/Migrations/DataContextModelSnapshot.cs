﻿// <auto-generated />
using System;
using MVM.Estacionamento.Data.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace MVM.Estacionamento.Data.Migrations
{
    [DbContext(typeof(DataContext))]
    partial class DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.12")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("MVM.Estacionamento.Business.Models.Empresa", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Cnpj")
                        .IsRequired()
                        .HasColumnType("varchar(14)");

                    b.Property<string>("Nome")
                        .IsRequired()
                        .HasColumnType("varchar(200)");

                    b.Property<int>("QuantidadeVagasCarros")
                        .HasColumnType("int");

                    b.Property<int>("QuantidadeVagasMotos")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("Empresas", (string)null);
                });

            modelBuilder.Entity("MVM.Estacionamento.Business.Models.Veiculo", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("char(36)");

                    b.Property<string>("Cor")
                        .IsRequired()
                        .HasColumnType("varchar(20)");

                    b.Property<Guid>("EmpresaId")
                        .HasColumnType("char(36)");

                    b.Property<string>("Marca")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Modelo")
                        .IsRequired()
                        .HasColumnType("varchar(50)");

                    b.Property<string>("Placa")
                        .IsRequired()
                        .HasColumnType("varchar(10)");

                    b.Property<int>("Tipo")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmpresaId");

                    b.ToTable("Veiculos", (string)null);
                });

            modelBuilder.Entity("MVM.Estacionamento.Business.Models.Empresa", b =>
                {
                    b.OwnsOne("MVM.Estacionamento.Business.ValueObjects.Endereco", "Endereco", b1 =>
                        {
                            b1.Property<Guid>("EmpresaId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Bairro")
                                .IsRequired()
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Cep")
                                .IsRequired()
                                .HasColumnType("varchar(8)");

                            b1.Property<string>("Cidade")
                                .IsRequired()
                                .HasColumnType("varchar(100)");

                            b1.Property<string>("Complemento")
                                .IsRequired()
                                .HasColumnType("varchar(200)");

                            b1.Property<string>("Logradouro")
                                .IsRequired()
                                .HasColumnType("varchar(200)");

                            b1.Property<string>("Numero")
                                .IsRequired()
                                .HasColumnType("varchar(10)");

                            b1.HasKey("EmpresaId");

                            b1.ToTable("Empresas");

                            b1.WithOwner()
                                .HasForeignKey("EmpresaId");
                        });

                    b.OwnsOne("MVM.Estacionamento.Business.ValueObjects.Telefone", "Telefone", b1 =>
                        {
                            b1.Property<Guid>("EmpresaId")
                                .HasColumnType("char(36)");

                            b1.Property<string>("Ddd")
                                .IsRequired()
                                .HasColumnType("varchar(2)");

                            b1.Property<string>("TelefoneCelular")
                                .IsRequired()
                                .HasColumnType("varchar(9)");

                            b1.Property<string>("TelefoneFixo")
                                .IsRequired()
                                .HasColumnType("varchar(9)");

                            b1.HasKey("EmpresaId");

                            b1.ToTable("Empresas");

                            b1.WithOwner()
                                .HasForeignKey("EmpresaId");
                        });

                    b.Navigation("Endereco")
                        .IsRequired();

                    b.Navigation("Telefone")
                        .IsRequired();
                });

            modelBuilder.Entity("MVM.Estacionamento.Business.Models.Veiculo", b =>
                {
                    b.HasOne("MVM.Estacionamento.Business.Models.Empresa", "Empresa")
                        .WithMany("Veiculos")
                        .HasForeignKey("EmpresaId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Empresa");
                });

            modelBuilder.Entity("MVM.Estacionamento.Business.Models.Empresa", b =>
                {
                    b.Navigation("Veiculos");
                });
#pragma warning restore 612, 618
        }
    }
}