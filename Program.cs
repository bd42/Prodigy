using System;

#pragma warning disable CS1701 // Assuming assembly reference matches identity
public class Program
{
    public static void Main(string[] args)
    {
        ChessBoard board = new ChessBoard();

        board.UpdateCells();

        Console.WriteLine(board.ToString());

        Console.WriteLine();

        int piece = 0;
        foreach (ChessMoveRelative relMove in board.Pieces[piece].Moves)
        {
            if (board.MoveValidate(board.Pieces[piece], relMove))
                Console.WriteLine(board.Pieces[piece].MoveGetAbsolute(relMove).ToString());
        }

        Console.ReadKey();
    }
}
#pragma warning restore CS1701 // Assuming assembly reference matches identity