using FitnessClub.Domain.Data;

namespace FitnessClub.Infrastructure.EfCore;

using Microsoft.EntityFrameworkCore;
using FitnessClub.Domain.Models;

public class FitnessClubDbContext(DbContextOptions options) : DbContext(options)
{
    public DbSet<Client> Clients { get; set; }
    public DbSet<Trainer> Trainers { get; set; }
    public DbSet<GymHall> GymHalls { get; set; }
    public DbSet<Specialization> Specializations { get; set; }
    public DbSet<TrainingSession> TrainingSessions { get; set; }

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