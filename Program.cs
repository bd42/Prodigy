using System;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ChessBoard board = new ChessBoard();
            
            foreach (ChessPiece piece in board.pieces)
            {
                Console.WriteLine(piece.Position.X + ":" + piece.Position.Y + " - " + piece.ToString());   
            }
        }
    }
}
