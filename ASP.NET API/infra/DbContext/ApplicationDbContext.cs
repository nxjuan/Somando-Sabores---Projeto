namespace infra.DbContext;
using Microsoft.EntityFrameworkCore;
using domain.Models;
using domain.Enums;

public class ApplicationDbContext(DbContextOptions options ) : DbContext(options)
{
    public DbSet<Aluno> Alunos { get; set; }
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Convidado> Convidados { get; set; }
    public DbSet<Evento> Eventos { get; set; }
    public DbSet<Reserva> Reservas { get; set; }
    public DbSet<Pagamento> Pagamentos { get; set; }
    public DbSet<Pacote> Pacotes { get; set; }
    public DbSet<Precificacao> Precificacoes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Pagamento>(entity =>
        {
            entity.ToTable("tb_pagamentos");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                .HasColumnName("id_pagamento")
                .ValueGeneratedOnAdd();

            entity.Property(p => p.DataPagamento)
                .HasColumnName("data_pagamento")
                .IsRequired();

            entity.Property(p => p.ValorTotal)
                .HasColumnName("valor_total")
                .IsRequired();

            entity.Property(p => p.FormaPagamento)
                .HasColumnName("forma_pagamento")
                .IsRequired();

            entity.Property(p => p.FormaPagamento)
                .HasColumnName("forma_pagamento")
                .HasMaxLength(100);

            entity.Property(p => p.AsaasId)
                .HasColumnName("asaas_id")
                .IsRequired()
                .HasMaxLength(40);

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

            entity.Property(p => p.PacoteId)
                .HasColumnName("pacote_id");

            entity.HasOne(p => p.Pacote)
                .WithMany()
                .HasForeignKey(p => p.PacoteId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.Entity<Reserva>(entity =>
        {
            entity.ToTable("tb_reservas");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                .HasColumnName("id_reserva")
                .ValueGeneratedOnAdd();

            entity.Property(p => p.DataReserva)
                .HasColumnName("data_reserva")
                .IsRequired();

            entity.Property(p => p.QtdConvidados)
                .HasColumnName("qtd_convidados")
                .IsRequired();

            entity.Property(p => p.ClienteId)
                .HasColumnName("cliente_id")
                .IsRequired();

            entity.HasOne(p => p.Cliente)
                .WithMany()
                .HasForeignKey(p => p.ClienteId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(p => p.PrecificacaoId)
                .HasColumnName("precificacao_id")
                .IsRequired();

            entity.HasOne(p => p.Precificacao)
                .WithMany()
                .HasForeignKey(p => p.PrecificacaoId)
                .OnDelete(DeleteBehavior.Restrict);

        });

        modelBuilder.Entity<Convidado>(entity =>
        {
            entity.ToTable("tb_convidados");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                .HasColumnName("id_convidado")
                .ValueGeneratedOnAdd();

            entity.Property(p => p.Nome)
                .HasColumnName("nome_completo")
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(p => p.ReservaId)
                .HasColumnName("reserva_id")
                .IsRequired();

            entity.HasOne(c => c.Reserva)
                .WithMany(r => r.Convidados)
                .HasForeignKey(c => c.ReservaId)
                .OnDelete(DeleteBehavior.Cascade);
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
                .IsRequired()
                .HasMaxLength(255);

            entity.Property(p => p.Email)
                .HasColumnName("email")
                .IsRequired()
                .HasMaxLength(100);
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
                .IsRequired()
                .HasMaxLength(8);
        });

        modelBuilder.Entity<Precificacao>(entity =>
        {
            entity.ToTable("tb_precificacao");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                .HasColumnName("id_precificacao")
                .ValueGeneratedOnAdd();

            entity.Property(p => p.TipoServico)
                .HasColumnName("tipo_servico")
                .IsRequired();

            entity.Property(p => p.Quantidade)
                .HasColumnName("quantidade")
                .IsRequired();

            entity.Property(p => p.Status)
                .HasColumnName("status_precificacao")
                .IsRequired();

            entity.Property(p => p.Total)
                .HasColumnName("total")
                .IsRequired();

            entity.Property(p => p.EmitirNF)
                .HasColumnName("emitir_nf")
                .IsRequired();
        });

        modelBuilder.Entity<Pacote>(entity =>
        {
            entity.ToTable("tb_pacotes");
            entity.HasKey(p => p.Id);

            entity.Property(p => p.Id)
                .HasColumnName("id_pacote")
                .ValueGeneratedOnAdd();

            entity.Property(p => p.AlunoId)
                .HasColumnName("aluno_id")
                .IsRequired();

            entity.HasOne(p => p.Aluno)
                .WithMany()
                .HasForeignKey(p => p.AlunoId)
                .OnDelete(DeleteBehavior.Restrict);

            entity.Property(p => p.PrecificacaoId)
                .HasColumnName("precificacao_id")
                .IsRequired();

            entity.HasOne(p => p.Precificacao)
                .WithMany()
                .HasForeignKey(p => p.PrecificacaoId)
                .OnDelete(DeleteBehavior.Restrict);
        });

        modelBuilder.HasPostgresEnum<StatusPrecificacao>();
        modelBuilder.HasPostgresEnum<OpcoesServico>();
    }
}