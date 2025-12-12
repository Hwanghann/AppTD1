using System.IO;
using Xunit;
using TicTacToe;

namespace TicTacToe_Test
{
    public class HumanPlayerTests
    {
        [Fact]
        public void HumanPlayer_returns_minus1_on_q()
        {
            var board = new Board();
            using var input = new StringReader("q\n");
            var player = new HumanPlayer('X', "Test", input);

            int move = player.GetNextMove(board);
            Assert.Equal(-1, move);
        }

        [Fact]
        public void HumanPlayer_parses_valid_move()
        {
            var board = new Board();
            using var input = new StringReader("5\n");
            var player = new HumanPlayer('X', "Test", input);

            int move = player.GetNextMove(board);
            Assert.Equal(4, move); // position 5 -> index 4
        }

        [Fact]
        public void HumanPlayer_rejects_occupied_and_accepts_next()
        {
            var board = new Board();
            board.PlayMove(4, 'O'); // position 5 occupée

            using var input = new StringReader("5\n1\n");
            var player = new HumanPlayer('X', "Test", input);

            int move = player.GetNextMove(board);
            Assert.Equal(0, move); // 1 -> index 0
        }
    }
}