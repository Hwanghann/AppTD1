using System;

char[] board = new char[9];
for (int i = 0; i < 9; i++) board[i] = ' ';

char current = 'X';

while (true)
{
    PrintBoard();
    Console.Write($"Joueur {current} — entrez une case (1-9) ou 'q' pour quitter : ");
    string? input = Console.ReadLine()?.Trim();

    if (string.IsNullOrEmpty(input)) continue;
    if (input.Equals("q", StringComparison.OrdinalIgnoreCase)) break;

    if (!int.TryParse(input, out int pos) || pos < 1 || pos > 9)
    {
        Console.WriteLine("Saisie invalide. Utilisez un nombre entre 1 et 9.");
        continue;
    }

    if (!TryPlaceMove(pos - 1, current))
    {
        Console.WriteLine("Case déjà occupée. Choisissez une autre case.");
        continue;
    }

    if (CheckWin(current))
    {
        PrintBoard();
        Console.WriteLine($"Joueur {current} a gagné !");
        break;
    }

    if (IsFull())
    {
        PrintBoard();
        Console.WriteLine("Match nul.");
        break;
    }

    current = (current == 'X') ? 'O' : 'X';
}

Console.WriteLine("Fin de la partie. Appuyez sur Entrée pour quitter.");
Console.ReadLine();

void PrintBoard()
{
    Console.Clear();
    Console.WriteLine();
    Console.WriteLine($" {Display(0)} | {Display(1)} | {Display(2)} ");
    Console.WriteLine("---+---+---");
    Console.WriteLine($" {Display(3)} | {Display(4)} | {Display(5)} ");
    Console.WriteLine("---+---+---");
    Console.WriteLine($" {Display(6)} | {Display(7)} | {Display(8)} ");
    Console.WriteLine();
    Console.WriteLine("Positions : 1 2 3 4 5 6 7 8 9");
    Console.WriteLine();
}

char Display(int i) => board[i] == ' ' ? (char)('1' + i) : board[i];

bool TryPlaceMove(int index, char player)
{
    if (board[index] != ' ') return false;
    board[index] = player;
    return true;
}

bool IsFull()
{
    for (int i = 0; i < 9; i++)
        if (board[i] == ' ') return false;
    return true;
}

bool CheckWin(char p)
{
    int[][] wins = new int[][]
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

    foreach (var w in wins)
    {
        if (board[w[0]] == p && board[w[1]] == p && board[w[2]] == p)
            return true;
    }
    return false;
}
