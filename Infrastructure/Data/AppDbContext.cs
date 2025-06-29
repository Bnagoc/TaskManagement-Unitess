using Domain.Entities;
using Infrastructure.Data.Converters;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Data
{
    public class AppDbContext : DbContext
    {

        private readonly IConfiguration _configuration;

        public AppDbContext(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Domain.Entities.Task> Tasks { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseNpgsql(_configuration.GetConnectionString("Default"));
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            ConfigureUsersTable(modelBuilder);
            ConfigureTasksTable(modelBuilder);

            base.OnModelCreating(modelBuilder);
        }
        private void ConfigureUsersTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(u => u.Username)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(u => u.Password)
                    .IsRequired()
                    .HasConversion(new PasswordConverter());

                entity.Property(u => u.Email)
                    .IsRequired()
                    .HasConversion(new EmailConverter());

                entity.Property(u => u.Role)
                    .HasConversion<string>()
                    .IsRequired();

                entity.HasMany(u => u.Tasks)
                    .WithOne(t => t.CreatedBy)
                    .HasForeignKey(t => t.CreatedById)
                    .OnDelete(DeleteBehavior.Restrict);
            });
        }

        private void ConfigureTasksTable(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Domain.Entities.Task>(entity =>
            {
                entity.HasKey(x => x.Id);

                entity.Property(t => t.Title)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(t => t.Description)
                    .HasMaxLength(800);

                entity.Property(t => t.Status)
                    .HasConversion<string>()
                    .IsRequired();

                entity.HasOne(t => t.CreatedBy)
                    .WithMany(u => u.Tasks)
                    .HasForeignKey(t => t.CreatedById)
                    .OnDelete(DeleteBehavior.Restrict);

                entity.HasMany(t => t.Users)
                    .WithMany();
            });
        }

    }
}
