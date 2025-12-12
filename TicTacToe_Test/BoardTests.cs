using Xunit;
using TicTacToe;

namespace TicTacToe_Test
{
    public class BoardTests
    {
        [Fact]
        public void PlayMove_and_IsCellEmpty_and_IsFull_and_IsGameWon_behave_correctly()
        {
            var board = new Board();

            // initialement toutes les cases sont vides
            for (int i = 0; i < 9; i++)
                Assert.True(board.IsCellEmpty(i));

            // jouer un coup valide
            Assert.True(board.PlayMove(0, 'X'));
            Assert.False(board.IsCellEmpty(0));

            // rejouer sur la même case doit échouer
            Assert.False(board.PlayMove(0, 'O'));

            // remplir ligne 0,1,2 pour X et vérifier victoire
            board.PlayMove(1, 'X');
            board.PlayMove(2, 'X');
            Assert.True(board.IsGameWon('X'));

            // vérifier IsFull false (pas rempli)
            Assert.False(board.IsFull());

            // remplir le reste
            for (int i = 3; i < 9; i++)
                if (board.IsCellEmpty(i)) board.PlayMove(i, 'O');

            Assert.True(board.IsFull());
        }

        [Fact]
        public void PlayMove_InvalidIndex_ReturnsFalse()
        {
            var board = new Board();
            Assert.False(board.PlayMove(-1, 'X'));
            Assert.False(board.PlayMove(9, 'X'));
        }

        [Fact]
        public void GetDisplayChar_empty_and_occupied_behaviour()
        {
            var board = new Board();
            Assert.Equal('1', board.GetDisplayChar(0)); // case vide -> '1'
            board.PlayMove(0, 'O');
            Assert.Equal('O', board.GetDisplayChar(0)); // case occupée -> symbole
        }

        [Fact]
        public void IsDraw_when_full_and_no_winner()
        {
            var board = new Board();
            // Configuration d'un match nul connu :
            // X O X
            // X O O
            // O X X
            board.PlayMove(0, 'X');
            board.PlayMove(1, 'O');
            board.PlayMove(2, 'X');
            board.PlayMove(3, 'X');
            board.PlayMove(4, 'O');
            board.PlayMove(5, 'O');
            board.PlayMove(6, 'O');
            board.PlayMove(7, 'X');
            board.PlayMove(8, 'X');

            Assert.True(board.IsFull());
            Assert.False(board.IsGameWon('X'));
            Assert.False(board.IsGameWon('O'));
            Assert.True(board.IsDraw());
        }

        [Fact]
        public void PlayMove_on_full_board_returns_false()
        {
            var board = new Board();
            for (int i = 0; i < 9; i++) board.PlayMove(i, i % 2 == 0 ? 'X' : 'O');
            Assert.True(board.IsFull());
            Assert.False(board.PlayMove(0, 'X'));
        }

        [Fact]
        public void Display_does_not_throw_when_output_redirected()
        {
            var board = new Board();
            using var sw = new StringWriter();
            var original = Console.Out;
            try
            {
                Console.SetOut(sw);
                // Should not throw (and should write something)
                board.Display();
                var output = sw.ToString();
                Assert.Contains("Positions", output);
            }
            finally
            {
                Console.SetOut(original);
            }
        }

        [Fact]
        public void IsCellEmpty_throws_on_invalid_index()
        {
            var board = new Board();
            Assert.Throws<IndexOutOfRangeException>(() => board.IsCellEmpty(-1));
            Assert.Throws<IndexOutOfRangeException>(() => board.IsCellEmpty(9));
        }
    }
}
