using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AplicacaoAPI;
using TecnologiaAPI;
using AplicacaoTecnologiaAPI.Entities;

namespace AplicacaoTecnologiaAPI.Data
{
   public class AppDbContext : DbContext
{
    public DbSet<Aplicacoes> Aplicacoes { get; set; }
    public DbSet<Tecnologia> Tecnologia { get; set; }
    public DbSet<AplicacaoTecnologia> Aplicacao_Tecnologia { get; set; } 

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AplicacaoTecnologia>()
            .ToTable("Aplicacao_Tecnologia"); 

        modelBuilder.Entity<AplicacaoTecnologia>()
            .HasKey(at => new { at.AplicacaoId, at.TecnologiaId });

        modelBuilder.Entity<AplicacaoTecnologia>()
            .Property(at => at.AplicacaoId).HasColumnName("aplicacao_id"); 

        modelBuilder.Entity<AplicacaoTecnologia>()
            .Property(at => at.TecnologiaId).HasColumnName("tecnologia_id"); 
    }
}
}
