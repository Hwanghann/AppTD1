using System;
using System.Collections.Generic;

namespace TicTacToe
{
    internal class AIPlayer : Player
    {
        private static readonly Random _rng = new Random();

        public AIPlayer(char symbol, string name = "Bot") : base(symbol, name) { }

        public override int GetNextMove(Board board)
        {
            // Priorité : centre si libre
            if (board.IsCellEmpty(4)) return 4;

            // Sinon choisir une case libre au hasard
            var empties = new List<int>(9);
            for (int i = 0; i < 9; i++)
            {
                if (board.IsCellEmpty(i)) empties.Add(i);
            }

            if (empties.Count == 0) return -1;
            return empties[_rng.Next(empties.Count)];
        }
    }

}