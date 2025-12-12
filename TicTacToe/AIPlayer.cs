using System;
using System.Text;
using System.Threading.Tasks;

namespace TicTacToe
{
    public class AIPlayer : Player
    {
        // durée totale de réflexion en ms (modifiable)
        public int ThinkMilliseconds { get; set; } = 1200;

        public AIPlayer(char symbol, string name, int thinkMs = 1200) : base(symbol, name)
        {
            ThinkMilliseconds = thinkMs;
        }

        // Option asynchrone avec animation de 33 points
        public override async Task<int> GetNextMoveAsync(Board board)
        {
            const int dots = 33;
            int delay = Math.Max(1, ThinkMilliseconds / dots);

            if (!Console.IsOutputRedirected)
            {
                var sb = new StringBuilder();
                Console.Write($"{Name} pense ");
                for (int i = 0; i < dots; i++)
                {
                    Console.Write('.');
                    await Task.Delay(delay).ConfigureAwait(false);
                }
                Console.WriteLine();
            }
            else
            {
                // si la sortie est redirigée (tests), on attend sans écrire
                await Task.Delay(ThinkMilliseconds).ConfigureAwait(false);
            }

            // logique simple de choix : premier emplacement vide
            for (int i = 0; i < 9; i++)
            {
                if (board.IsCellEmpty(i)) return i;
            }

            return -1;
        }

        // (optionnel) garder la compatibilité synchrone si on veut
        public override int GetNextMove(Board board)
            => GetNextMoveAsync(board).GetAwaiter().GetResult();
    }
}