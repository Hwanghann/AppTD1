using Microsoft.EntityFrameworkCore;
using TicTacToe.Models;

namespace TicTacToe.Data
{
    public class GameDbContext : DbContext
    {
        public DbSet<GameRecord> GameRecords { get; set; } = null!;

        public GameDbContext()
        {
        }

        public GameDbContext(DbContextOptions<GameDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                // Par défaut SQLite local file. Remplacez par UseNpgsql(...) pour PostgreSQL.
                optionsBuilder.UseSqlite("Data Source=tictactoe.db");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameRecord>().HasKey(g => g.Id);
            modelBuilder.Entity<GameRecord>().Property(g => g.BoardState).HasMaxLength(9);
            base.OnModelCreating(modelBuilder);
        }
    }
}