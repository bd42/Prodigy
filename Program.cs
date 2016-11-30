using System;

namespace ConsoleApplication
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ChessBoard board = new ChessBoard();

            board.pieces[1, 0].Position.Rank = 3;
            Console.WriteLine(board.pieces[1, 0].Position.File + ":" + board.pieces[1, 0].Position.Rank);
            
            ChessMove[] moves = board.GetMoves();
            Console.WriteLine("Moves: " + moves.Length.ToString());

            foreach(ChessMove move in moves)
            {
                ChessPiece piece = board.pieces[board.onTurn, move.PieceID];
                string strMoveType = string.Empty;

                switch(move.Type)
                {
                    case ChessMoveType.Default:
                        strMoveType = "Default Move";
                        break;

                    case ChessMoveType.PawnAttack:
                        strMoveType = "Pawn Attack";
                        break;

                    case ChessMoveType.PawnDouble:
                        strMoveType = "Pawn Double Move";
                        break;

                    default:
                        strMoveType = "None";
                        break;
                }

                Console.WriteLine(//move.Origin.File + ":" + move.Origin.Rank + " - " + piece.ToString() + "\r\n" + 
                                move.Target.File + ":" + move.Target.Rank + " - " + strMoveType);
            }
        }
    }
}
