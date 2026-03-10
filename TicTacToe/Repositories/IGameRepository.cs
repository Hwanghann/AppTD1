using System.Collections.Generic;
using TicTacToe.Models;

namespace TicTacToe.Repositories
{
    public interface IGameRepository
    {
        void EnsureDatabaseCreated();
        void SaveOrUpdateOngoing(GameRecord record);
        void SaveFinished(GameRecord record);
        GameRecord? GetLatestOngoing();
        IReadOnlyCollection<GameRecord> GetCompletedGames();
    }
}