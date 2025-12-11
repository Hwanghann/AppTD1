using System;

namespace TicTacToe
{
    internal static class Program
    {
        private static void Main()
        {
            Player[] players = new Player[]
            {
                new HumanPlayer('X', "Joueur 1"),
                new HumanPlayer('O', "Joueur 2")
            };

            var game = new Game(players);
            game.Play();
        }
    }
}
