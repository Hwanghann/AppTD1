using System;
using System.Linq;
using TicTacToe.Repositories;
using TicTacToe.Data;
using TicTacToe.Models;

namespace TicTacToe
{
    public static class Program
    {
        private static void Main()
        {
            IGameRepository repo = new GameRepository();
            repo.EnsureDatabaseCreated();

            ShowStats(repo);

            var ongoing = repo.GetLatestOngoing();
            Console.WriteLine();
            Console.WriteLine("1) Nouvelle partie");
            if (ongoing != null)
            {
                Console.WriteLine("2) Reprendre la partie en cours");
            }
            Console.Write("Choix: ");
            var key = Console.ReadLine();

            if (key == "2" && ongoing != null)
            {
                ResumeGame(ongoing, repo);
            }
            else
            {
                StartNewGame(repo);
            }
        }

        private static void ShowStats(IGameRepository repo)
        {
            var completed = repo.GetCompletedGames();
            int total = completed.Count;
            int humanWins = completed.Count(g => g.WinnerIsAI == false && !g.IsDraw && g.WinnerSymbol != null);
            int botWins = completed.Count(g => g.WinnerIsAI == true && !g.IsDraw && g.WinnerSymbol != null);
            int draws = completed.Count(g => g.IsDraw);

            Console.WriteLine("Statistiques :");
            Console.WriteLine($" Parties terminées : {total}");
            Console.WriteLine($" Victoires Humain : {humanWins}");
            Console.WriteLine($" Victoires Bot    : {botWins}");
            Console.WriteLine($" Nuls             : {draws}");
            if (total > 0)
            {
                Console.WriteLine($" Ratio humain : {(double)humanWins / total:P1}, ratio bot : {(double)botWins / total:P1}");
            }
        }

        private static void StartNewGame(IGameRepository repo)
        {
            Player[] players = new Player[]
            {
                new HumanPlayer('X', "Joueur 1"),
                new AIPlayer('O', "Bot")
            };

            var game = new Game(players, repo);
            game.Play();
        }

        private static void ResumeGame(GameRecord record, IGameRepository repo)
        {
            Console.WriteLine("Reprise de la partie en cours...");
            var p1 = record.Player1IsAI ? (Player)new AIPlayer(record.Player1Symbol, record.Player1Name) : new HumanPlayer(record.Player1Symbol, record.Player1Name);
            var p2 = record.Player2IsAI ? (Player)new AIPlayer(record.Player2Symbol, record.Player2Name) : new HumanPlayer(record.Player2Symbol, record.Player2Name);

            var players = new Player[] { p1, p2 };
            var board = new Board(record.BoardState);
            var game = new Game(players, board, record.CurrentIndex, repo);
            game.Play();
        }
    }
}