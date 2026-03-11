using System;

namespace TicTacToe.Models
{
    public class GameRecord
    {
        public int Id { get; set; }
        public DateTime StartedAt { get; set; } = DateTime.UtcNow;
        public DateTime? EndedAt { get; set; }
        public bool IsCompleted { get; set; }
        public bool IsDraw { get; set; }

        // board state: exactly 9 chars (' ', 'X', 'O')
        public string BoardState { get; set; } = new string(' ', 9);

        public int CurrentIndex { get; set; }

        public string Player1Name { get; set; } = string.Empty;
        public char Player1Symbol { get; set; }
        public bool Player1IsAI { get; set; }

        public string Player2Name { get; set; } = string.Empty;
        public char Player2Symbol { get; set; }
        public bool Player2IsAI { get; set; }

        // winner stored as symbol 'X' or 'O', null if draw or not finished
        public char? WinnerSymbol { get; set; }
        public bool? WinnerIsAI { get; set; }
    }
}