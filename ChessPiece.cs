public abstract class ChessPiece
{
    public bool Captured = false;
    protected bool HasMoved = false;

    public ChessBoardCell Position;

    public abstract ChessMove[] GetMoves(int _pieceID);

    public bool Move(ChessMove move)
    {
        if(!Captured && move.Origin == Position)
        {
            Position = move.Target;
            HasMoved = true;
            return true;
        }
        else
            return false;
    }
}

public class Pawn : ChessPiece
{
    private int PawnMoveY;
    private int PawnMoveDoubleY;

    public Pawn(int _player)
    {
        if(_player == ChessPlayer.White)
            PawnMoveY = 1;
        else if(_player == ChessPlayer.Black)
            PawnMoveY = -1;
        else
            PawnMoveY = 0;

        PawnMoveDoubleY = PawnMoveY * 2;
    }

    public override ChessMove[] GetMoves(int _pieceID)
    {
        ChessMove[] movesAll = new ChessMove[(HasMoved ? 3 : 4)];

        movesAll[0] = new ChessMove(_pieceID, Position, -1, PawnMoveY, ChessMoveType.PawnAttack);
        movesAll[1] = new ChessMove(_pieceID, Position, 0, PawnMoveY);
        movesAll[2] = new ChessMove(_pieceID, Position, 1, PawnMoveY, ChessMoveType.PawnAttack);

        if(!HasMoved)
            movesAll[3] = new ChessMove(_pieceID, Position, 0, PawnMoveDoubleY, ChessMoveType.PawnDouble);

        return movesAll;
    }
}

public class Rook : ChessPiece
{
    public override ChessMove[] GetMoves(int _pieceID)
    {
        return new ChessMove[0];
    }
}

public class Knight : ChessPiece
{
    public override ChessMove[] GetMoves(int _pieceID)
    {
        return new ChessMove[0];
    }
}

public class Bishop : ChessPiece
{
    public override ChessMove[] GetMoves(int _pieceID)
    {
        return new ChessMove[0];
    }
}

public class Queen : ChessPiece
{
    public override ChessMove[] GetMoves(int _pieceID)
    {
        return new ChessMove[0];
    }
}

public class King : ChessPiece
{
    public override ChessMove[] GetMoves(int _pieceID)
    {
        return new ChessMove[0];
    }
}