using TicTacToe;

namespace TicTacToe_Test
{
    internal class TestPlayer : Player
    {
        private readonly Queue<int> _moves;

        public TestPlayer(char symbol, string name, IEnumerable<int> moves) : base(symbol, name)
        {
            _moves = new Queue<int>(moves);
        }

        public override int GetNextMove(Board board)
        {
            if (_moves.Count == 0) return -1;
            return _moves.Dequeue();
        }
    }

    public class GameAcceptanceTests
    {
        [Fact]
        public void Game_engine_runs_and_reports_winner_for_predetermined_moves()
        {
            // p1: 0,1,2 -> va gagner
            var p1 = new TestPlayer('X', "P1", new[] { 0, 1, 2 });
            var p2 = new TestPlayer('O', "P2", new[] { 3, 4 });

            var players = new Player[] { p1, p2 };
            var game = new Game(players)
            {
                WaitForExit = false
            };

            var sw = new StringWriter();
            var originalOut = Console.Out;
            try
            {
                Console.SetOut(sw);
                game.Play();
            }
            finally
            {
                Console.SetOut(originalOut);
            }

            var output = sw.ToString();
            Assert.Contains("a gagné", output); // message de victoire attendu
            Assert.Contains("Joueur X", output);
        }
    }
}
