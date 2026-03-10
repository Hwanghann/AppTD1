using Microsoft.EntityFrameworkCore;
using TicTacToe.Models;

namespace TicTacToe.Data
{
    public class GameDbContext : DbContext
    {
        public DbSet<GameRecord> GameRecords { get; set; } = null!;

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // SQLite local file par défaut. Remplacez par UseNpgsql(...) pour PostgreSQL.
            optionsBuilder.UseSqlite("Data Source=tictactoe.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GameRecord>().HasKey(g => g.Id);
            modelBuilder.Entity<GameRecord>().Property(g => g.BoardState).HasMaxLength(9);
            base.OnModelCreating(modelBuilder);
        }
    }
}