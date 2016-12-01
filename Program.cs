using System;

public class Program
{
    public static void Main(string[] args)
    {
        ChessBoard board = new ChessBoard();

        board.UpdateCells();
        Console.WriteLine(board.ToString());

        Console.ReadKey();
    }
}
