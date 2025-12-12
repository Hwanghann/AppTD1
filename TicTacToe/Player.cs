using System;
using System.IO;

namespace TicTacToe
{
    public abstract class Player
    {
        protected readonly TextReader _input;

        public char Symbol { get; }
        public string Name { get; }

        protected Player(char symbol, string name, TextReader? input = null)
        {
            Symbol = symbol;
            Name = name;
            _input = input ?? Console.In;
        }

        // Retourne l'index de case (0-8) ou -1 si le joueur abandonne
        public abstract int GetNextMove(Board board);
    }

    public class HumanPlayer : Player
    {
        public HumanPlayer(char symbol, string name, TextReader? input = null)
            : base(symbol, name, input)
        {
        }

        public override int GetNextMove(Board board)
        {
            while (true)
            {
                Console.Write($"{Name} ({Symbol}) — entrez une case (1-9) ou 'q' pour quitter : ");
                string? input = _input.ReadLine()?.Trim();

                if (string.IsNullOrEmpty(input)) continue;
                if (input.Equals("q", StringComparison.OrdinalIgnoreCase)) return -1;

                if (!int.TryParse(input, out int pos) || pos < 1 || pos > 9)
                {
                    Console.WriteLine("Saisie invalide. Utilisez un nombre entre 1 et 9.");
                    continue;
                }

                int idx = pos - 1;
                if (!board.IsCellEmpty(idx))
                {
                    Console.WriteLine("Case déjà occupée. Choisissez une autre case.");
                    continue;
                }

                return idx;
            }
        }
    }
}
