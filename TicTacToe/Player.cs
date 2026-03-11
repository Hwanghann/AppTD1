using System;
using System.IO;
using System.Threading.Tasks;

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

        // API synchrone existante (conserver pour compatibilité)
        public virtual int GetNextMove(Board board) => -1;

        // Nouvelle API asynchrone : par défaut délègue à la version synchrone
        public virtual Task<int> GetNextMoveAsync(Board board)
            => Task.Run(() => GetNextMove(board));
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
