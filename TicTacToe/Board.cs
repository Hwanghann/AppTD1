using System;
using System.Collections.Generic;
using System.Text;

namespace TicTacToe
{
    internal class Board
    {
        private readonly char[] _cells = new char[9];

        private static readonly int[][] Wins = new int[][]
        {
            new[] {0,1,2},
            new[] {3,4,5},
            new[] {6,7,8},
            new[] {0,3,6},
            new[] {1,4,7},
            new[] {2,5,8},
            new[] {0,4,8},
            new[] {2,4,6}
        };

        public Board()
        {
            for (int i = 0; i < 9; i++) _cells[i] = ' ';
        }

        public void Display()
        {
            Console.Clear();
            Console.WriteLine();
            Console.WriteLine($" {GetDisplayChar(0)} | {GetDisplayChar(1)} | {GetDisplayChar(2)} ");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {GetDisplayChar(3)} | {GetDisplayChar(4)} | {GetDisplayChar(5)} ");
            Console.WriteLine("---+---+---");
            Console.WriteLine($" {GetDisplayChar(6)} | {GetDisplayChar(7)} | {GetDisplayChar(8)} ");
            Console.WriteLine();
            Console.WriteLine("Positions : 1 2 3 4 5 6 7 8 9");
            Console.WriteLine();
        }

        public char GetDisplayChar(int i) => _cells[i] == ' ' ? (char)('1' + i) : _cells[i];

        public bool PlayMove(int index, char symbol)
        {
            if (index < 0 || index > 8) return false;
            if (_cells[index] != ' ') return false;
            _cells[index] = symbol;
            return true;
        }

        public bool IsCellEmpty(int index) => _cells[index] == ' ';

        public bool IsFull()
        {
            for (int i = 0; i < 9; i++)
                if (_cells[i] == ' ') return false;
            return true;
        }

        public bool IsGameWon(char p)
        {
            foreach (var w in Wins)
            {
                if (_cells[w[0]] == p && _cells[w[1]] == p && _cells[w[2]] == p)
                    return true;
            }
            return false;
        }

        public bool IsDraw() => IsFull() && !IsGameWon('X') && !IsGameWon('O');
    }
}
