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
                new AIPlayer('O', "Bot")
            };

            var game = new Game(players);
            game.Play();
        }
    }
}