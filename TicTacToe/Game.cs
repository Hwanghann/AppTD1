using System;
using TicTacToe.Models;
using TicTacToe.Repositories;

namespace TicTacToe
{
    public class Game
    {
        private readonly Board _board;
        private readonly Player[] _players;
        private int _currentIndex;
        private readonly IGameRepository? _repository;

        public Game(Player[] players, IGameRepository? repository = null)
        {
            _players = players ?? throw new ArgumentNullException(nameof(players));
            if (players.Length < 2) throw new ArgumentException("Au moins deux joueurs sont nécessaires.", nameof(players));
            _board = new Board();
            _currentIndex = 0;
            _repository = repository;
        }

        // Constructeur pour reprise (inchangé)
        public Game(Player[] players, Board board, int currentIndex, IGameRepository? repository = null)
        {
            _players = players ?? throw new ArgumentNullException(nameof(players));
            if (players.Length < 2) throw new ArgumentException("Au moins deux joueurs sont nécessaires.", nameof(players));
            _board = board ?? throw new ArgumentNullException(nameof(board));
            _currentIndex = currentIndex;
            _repository = repository;
        }

        public bool WaitForExit { get; set; } = true;

        public void Play()
        {
            SaveOngoing();

            while (true)
            {
                _board.Display();
                var current = _players[_currentIndex];
                int move = current.GetNextMove(_board);

                if (move == -2)
                {
                    // sauvegarder et quitter sans marquer la partie comme terminée
                    SaveOngoing();
                    Console.WriteLine("Partie sauvegardée. À bientôt.");
                    break;
                }

                if (move == -1)
                {
                    Console.WriteLine("Partie abandonnée.");
                    if (_repository != null)
                    {
                        var gr = CreateRecord(isDraw: false, winnerSymbol: null, winnerIsAI: null);
                        gr.IsCompleted = true;
                        _repository.SaveFinished(gr);
                    }
                    break;
                }

                if (move == -2)
                {
                    Console.WriteLine("Partie sauvegardée. À bientôt !");
                    break;
                }

                if (!_board.PlayMove(move, current.Symbol))
                {
                    Console.WriteLine("Mouvement invalide. Réessayez.");
                    continue;
                }

                SaveOngoing();

                if (_board.IsGameWon(current.Symbol))
                {
                    _board.Display();
                    Console.WriteLine($"Joueur {current.Symbol} ({current.Name}) a gagné !");
                    if (_repository != null)
                    {
                        var winnerIsAI = current is AIPlayer;
                        var gr = CreateRecord(isDraw: false, winnerSymbol: current.Symbol, winnerIsAI: winnerIsAI);
                        gr.IsCompleted = true;
                        _repository.SaveFinished(gr);
                    }
                    break;
                }

                if (_board.IsFull())
                {
                    _board.Display();
                    Console.WriteLine("Match nul.");
                    if (_repository != null)
                    {
                        var gr = CreateRecord(isDraw: true, winnerSymbol: null, winnerIsAI: null);
                        gr.IsCompleted = true;
                        _repository.SaveFinished(gr);
                    }
                    break;
                }

                SwitchPlayer();
            }

            if (WaitForExit)
            {
                Console.WriteLine("Fin de la partie. Appuyez sur Entrée pour quitter.");
                Console.ReadLine();
            }
            else
            {
                Console.WriteLine("Fin de la partie.");
            }
        }

        private void SaveOngoing()
        {
            if (_repository == null) return;
            var record = CreateRecord(isDraw: false, winnerSymbol: null, winnerIsAI: null);
            record.IsCompleted = false;
            _repository.SaveOrUpdateOngoing(record);
        }

        private GameRecord CreateRecord(bool isDraw, char? winnerSymbol, bool? winnerIsAI)
        {
            var r = new GameRecord
            {
                StartedAt = DateTime.UtcNow,
                BoardState = _board.ToStateString(),
                CurrentIndex = _currentIndex,
                Player1Name = _players[0].Name,
                Player1Symbol = _players[0].Symbol,
                Player1IsAI = _players[0] is AIPlayer,
                Player2Name = _players[1].Name,
                Player2Symbol = _players[1].Symbol,
                Player2IsAI = _players[1] is AIPlayer,
                IsDraw = isDraw,
                WinnerSymbol = winnerSymbol,
                WinnerIsAI = winnerIsAI
            };
            return r;
        }

        private void SwitchPlayer() => _currentIndex = (_currentIndex + 1) % _players.Length;

        public Board ExportBoard() => new Board(_board.ToStateString());
        public int CurrentIndex => _currentIndex;
    }
}