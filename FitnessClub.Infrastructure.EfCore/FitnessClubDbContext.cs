using FitnessClub.Domain.Data;
using Microsoft.EntityFrameworkCore;
using FitnessClub.Domain.Models;

namespace FitnessClub.Infrastructure.EfCore;

/// <summary>
/// Контекст данных фитнес-клуба для Entity Framework Core
/// </summary>
public class FitnessClubDbContext(DbContextOptions<FitnessClubDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Таблица клиентов
    /// </summary>
    public DbSet<Client> Clients { get; set; }
    /// <summary>
    /// Таблица тренеров
    /// </summary>
    public DbSet<Trainer> Trainers { get; set; }
    /// <summary>
    /// Таблица спортивных залов
    /// </summary>
    public DbSet<GymHall> GymHalls { get; set; }
    /// <summary>
    /// Таблица специализаций
    /// </summary>
    public DbSet<Specialization> Specializations { get; set; }
    /// <summary>
    /// Таблица тренировок
    /// </summary>
    public DbSet<TrainingSession> TrainingSessions { get; set; }

    /// <summary>
    /// Настраивает связи между сущностями и заполняет базу начальными данными
    /// </summary>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var data = DataSeeder.Seed();
        
        modelBuilder.Entity<Client>(builder =>
        {
            builder.HasKey(c => c.Id);
            builder.HasData(data.Clients);
        });
        
        modelBuilder.Entity<GymHall>(builder =>
        {
            builder.HasKey(g => g.Id);
            builder.HasData(data.GymHalls);
        });
        
        modelBuilder.Entity<Specialization>(builder =>
        {
            builder.HasKey(s => s.Id);
            builder.HasData(data.Specializations);
        });
        
        modelBuilder.Entity<Trainer>(builder =>
        {
            builder.HasOne(t => t.Specialization)
                .WithMany()
                .HasForeignKey(t => t.SpecializationId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.HasKey(t => t.Id);
            builder.HasData(data.Trainers);
        });

        modelBuilder.Entity<TrainingSession>(builder =>
        {
            builder.HasOne(ts => ts.Client)
                .WithMany()
                .HasForeignKey(ts => ts.ClientId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(ts => ts.Trainer)
                .WithMany()
                .HasForeignKey(ts => ts.TrainerId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasOne(ts => ts.GymHall)
                .WithMany()
                .HasForeignKey(ts => ts.GymHallId)
                .OnDelete(DeleteBehavior.Cascade);
            builder.HasKey(ts => ts.Id);
            builder.HasData(data.TrainingSessions);
        });
    }
}