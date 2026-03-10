using System;
using System.Linq;
using System.Collections.Generic;
using TicTacToe.Data;
using TicTacToe.Models;
using Microsoft.EntityFrameworkCore;

namespace TicTacToe.Repositories
{
    public class GameRepository : IGameRepository
    {
        public void EnsureDatabaseCreated()
        {
            using var ctx = new GameDbContext();
            ctx.Database.EnsureCreated();
        }

        public void SaveOrUpdateOngoing(GameRecord record)
        {
            using var ctx = new GameDbContext();
            var existing = ctx.GameRecords.FirstOrDefault(r => !r.IsCompleted);
            if (existing == null)
            {
                ctx.GameRecords.Add(record);
            }
            else
            {
                existing.BoardState = record.BoardState;
                existing.CurrentIndex = record.CurrentIndex;
                existing.Player1Name = record.Player1Name;
                existing.Player2Name = record.Player2Name;
                existing.Player1IsAI = record.Player1IsAI;
                existing.Player2IsAI = record.Player2IsAI;
                existing.Player1Symbol = record.Player1Symbol;
                existing.Player2Symbol = record.Player2Symbol;
                existing.StartedAt = record.StartedAt;
            }
            ctx.SaveChanges();
        }

        public void SaveFinished(GameRecord record)
        {
            using var ctx = new GameDbContext();
            var ongoing = ctx.GameRecords.FirstOrDefault(r => !r.IsCompleted);
            if (ongoing != null)
            {
                ongoing.BoardState = record.BoardState;
                ongoing.CurrentIndex = record.CurrentIndex;
                ongoing.IsCompleted = true;
                ongoing.IsDraw = record.IsDraw;
                ongoing.WinnerSymbol = record.WinnerSymbol;
                ongoing.WinnerIsAI = record.WinnerIsAI;
                ongoing.EndedAt = DateTime.UtcNow;
            }
            else
            {
                record.IsCompleted = true;
                record.EndedAt = DateTime.UtcNow;
                ctx.GameRecords.Add(record);
            }
            ctx.SaveChanges();
        }

        public GameRecord? GetLatestOngoing()
        {
            using var ctx = new GameDbContext();
            return ctx.GameRecords
                      .Where(r => !r.IsCompleted)
                      .OrderByDescending(r => r.StartedAt)
                      .FirstOrDefault();
        }

        public IReadOnlyCollection<GameRecord> GetCompletedGames()
        {
            using var ctx = new GameDbContext();
            return ctx.GameRecords.Where(r => r.IsCompleted).ToList().AsReadOnly();
        }
    }
}