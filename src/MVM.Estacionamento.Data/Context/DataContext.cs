using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using MVM.Estacionamento.Business.Models;
using MVM.Estacionamento.Core;

namespace MVM.Estacionamento.Data.Context;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> opt) : base(opt)
    {
    }

    public DbSet<Empresa> Empresas { get; set; }
    public DbSet<Veiculo> Veiculos { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Ignore<Entity>();

        modelBuilder.Entity<Empresa>(e =>
        {
            e.ToTable("Empresas");

            e.HasKey(e => e.Id);

            e.Property(e => e.Nome)
                .IsRequired()
                .HasColumnType("varchar(200)");

            e.Property(e => e.Cnpj)
                .IsRequired()
                .HasColumnType("varchar(14)");

            // Mapeamento de propriedades de objetos de valor (Endereco e Telefone)
            e.OwnsOne(e => e.Endereco, endereco =>
            {
                endereco.Property(end => end.Cep)
                    .IsRequired()
                    .HasColumnType("varchar(8)");

                endereco.Property(end => end.Numero)
                    .IsRequired()
                    .HasColumnType("varchar(10)");

                endereco.Property(end => end.Logradouro)
                    .IsRequired()
                    .HasColumnType("varchar(200)");

                endereco.Property(end => end.Complemento)
                    .HasColumnType("varchar(200)");

                endereco.Property(end => end.Bairro)
                    .IsRequired()
                    .HasColumnType("varchar(100)");

                endereco.Property(end => end.Cidade)
                    .IsRequired()
                    .HasColumnType("varchar(100)");
            });

            e.OwnsOne(e => e.Telefone, telefone =>
            {
                telefone.Property(t => t.Ddd)
                    .IsRequired()
                    .HasColumnType("varchar(2)");

                telefone.Property(t => t.TelefoneCelular)
                    .IsRequired()
                    .HasColumnType("varchar(9)");

                telefone.Property(t => t.TelefoneFixo)
                    .HasColumnType("varchar(9)");
            });

            // Relacionamento com Veiculos (assumindo que você possui um DbSet<Veiculo> no seu DbContext)
            e.HasMany(e => e.Veiculos)
                .WithOne(v => v.Empresa)
                .HasForeignKey(v => v.EmpresaId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Veiculo>(v =>
        {
            v.ToTable("Veiculos");
            v.HasKey(v => v.Id);

            v.Property(v => v.Marca)
                .IsRequired()
                .HasColumnType("varchar(50)");

            v.Property(v => v.Modelo)
                .IsRequired()
                .HasColumnType("varchar(50)");

            v.Property(v => v.Cor)
                .IsRequired()
                .HasColumnType("varchar(20)");

            v.Property(v => v.Placa)
                .IsRequired()
                .HasColumnType("varchar(10)");

            v.Property(v => v.Tipo)
                .IsRequired()
                .HasColumnType("int");

            v.HasOne(v => v.Empresa)
                .WithMany(e => e.Veiculos)
                .HasForeignKey(v => v.EmpresaId)
                .OnDelete(DeleteBehavior.Restrict);
        });
    }
}

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        var conectString = "Server=localhost;Port=3307;Database=meu-mysql;Uid=root;Pwd=8837;";

        optionsBuilder.UseMySql(conectString, ServerVersion.AutoDetect(conectString));

        return new DataContext(optionsBuilder.Options);
    }
}

