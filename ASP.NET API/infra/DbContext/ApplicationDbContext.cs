namespace infra.DbContext;
using Microsoft.EntityFrameworkCore;
using domain.Models;

public class ApplicationDbContext(DbContextOptions options ) : DbContext(options)
{
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Evento> Eventos { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<Pagamentos> Pagamentos { get; set; }
    public DbSet<Pacotes> Pacotes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pagamentos>(entity =>
        {
            entity.ToTable("tb_pagamentos");
            entity.HasKey(p => p.Id);

            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                .HasColumnName("id")
                .ValueGeneratedOnAdd();

            entity.Property(p => p.DataPagamento)
                .HasColumnName("data_pagamento")
                .IsRequired();

            entity.Property(p => p.Valor)
                .HasColumnName("valor")
                .IsRequired();

            entity.Property(p => p.FormaPagamento)
                .HasColumnName("forma_pagamento")
                .IsRequired();

            entity.Property(p => p.FormaPagamento)
                .HasColumnName("forma_pagamento")
                .HasMaxLength(100);

            entity.Property(p => p.ReservaId)
                .HasColumnName("reserva_id");

            entity.HasOne(p => p.Reserva)
                .WithMany()
                .HasForeignKey(p => p.ReservaId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(p => p.ClienteId)
                .HasColumnName("cliente_id");

            entity.HasOne(p => p.Cliente)
                .WithMany()
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(p => p.AlunoId)
                .HasColumnName("aluno_id");

            entity.HasOne(p => p.Aluno)
                .WithMany()
                .HasForeignKey(p => p.AlunoId)
                .OnDelete(DeleteBehavior.Restrict);
        });


        modelBuilder.Entity<Cliente>(entity =>
        {
            entity.ToTable("tb_clientes");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                .HasColumnName("id_cliente")
                .ValueGeneratedOnAdd();

            entity.Property(p => p.Nome)
                .HasColumnName("nome_completo")
                .IsRequired();

            entity.Property(p => p.Email)
                .HasColumnName("email")
                .IsRequired();
        });

        modelBuilder.Entity<Aluno>(entity =>
        {
            entity.ToTable("tb_alunos");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                .HasColumnName("id_aluno")
                .ValueGeneratedOnAdd();

            entity.Property(p => p.ClienteId)
                .HasColumnName("cliente_id")
                .IsRequired();

            entity.HasOne(p => p.Cliente)
                .WithMany()
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(p => p.RA)
                .HasColumnName("ra")
                .IsRequired();
        });
    }
}