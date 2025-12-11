using System;

namespace TicTacToe
{
    internal class Game
    {
        private readonly Board _board;
        private readonly Player[] _players;
        private int _currentIndex;

        public Game(Player[] players)
        {
            _players = players ?? throw new ArgumentNullException(nameof(players));
            if (players.Length < 2) throw new ArgumentException("Au moins deux joueurs sont nécessaires.", nameof(players));
            _board = new Board();
            _currentIndex = 0;
        }

        public void Play()
        {
            while (true)
            {
                _board.Display();
                var current = _players[_currentIndex];
                int move = current.GetNextMove(_board);

                if (move == -1)
                {
                    Console.WriteLine("Partie abandonnée.");
                    break;
                }

                if (!_board.PlayMove(move, current.Symbol))
                {
                    Console.WriteLine("Mouvement invalide. Réessayez.");
                    continue;
                }

                if (_board.IsGameWon(current.Symbol))
                {
                    _board.Display();
                    Console.WriteLine($"Joueur {current.Symbol} ({current.Name}) a gagné !");
                    break;
                }

                if (_board.IsFull())
                {
                    _board.Display();
                    Console.WriteLine("Match nul.");
                    break;
                }

                SwitchPlayer();
            }

            Console.WriteLine("Fin de la partie. Appuyez sur Entrée pour quitter.");
            Console.ReadLine();
        }

        private void SwitchPlayer() => _currentIndex = (_currentIndex + 1) % _players.Length;
    }
}