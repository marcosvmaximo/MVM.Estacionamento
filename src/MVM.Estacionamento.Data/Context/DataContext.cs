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

            e.HasMany(e => e.Veiculos)
                .WithOne(v => v.Empresa)
                .HasForeignKey(v => v.EmpresaId)
                .OnDelete(DeleteBehavior.Cascade);
        });

        modelBuilder.Entity<Veiculo>(entity =>
        {
            entity.HasKey(v => v.Id);

            entity.Property(v => v.EmpresaId)
                .IsRequired();

            entity.Property(v => v.Marca)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(v => v.Modelo)
                .IsRequired()
                .HasMaxLength(100);

            entity.Property(v => v.Cor)
                .IsRequired()
                .HasMaxLength(50);

            entity.Property(v => v.Placa)
                .IsRequired()
                .HasMaxLength(7);

            entity.Property(v => v.Ano)
                .IsRequired();

            entity.Property(v => v.Tipo)
                .IsRequired();

            entity.Property(v => v.Status)
                .IsRequired();

            entity.HasMany(v => v.RegistrosEstacionamento)
                .WithOne(h => h.Veiculo)
                .HasForeignKey(h => h.VeiculoId);
        });

        modelBuilder.Entity<RegistroEstacionamento>(entity =>
        {
            entity.HasKey(h => h.Id);

            entity.Property(h => h.VeiculoId)
                .IsRequired();

            entity.Property(h => h.Data)
                .IsRequired();

            entity.Property(h => h.HorarioEntrada);

            entity.Property(h => h.HorarioSaida);

            entity.Property(h => h.TempoUtilizado);
        });
    }
}

public class DataContextFactory : IDesignTimeDbContextFactory<DataContext>
{
    public DataContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<DataContext>();
        var conectString = "Server=mvmmysqlestacionamento.mysql.database.azure.com;UserID=marcosvmaximo;Password=Ma88378621@;Database=mvm_estacionamento;";

        optionsBuilder.UseMySql(conectString, ServerVersion.AutoDetect(conectString));

        return new DataContext(optionsBuilder.Options);
    }
}

