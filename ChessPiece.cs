using System;

public abstract class ChessPiece
{
    public bool Captured = false;
    protected bool HasMoved = false;

    public ChessBoardCell Position;
    public ChessMove[] Moves;

    public abstract string Name { get; }
    public abstract char Symbol { get; }

    public abstract void UpdateMoves(ChessBoard board);

    public bool CommitMove(int moveID)
    {
        if(Captured)
            return false;

        Position = Moves[moveID].Target;
        HasMoved = true;
        return true;
    }
}

public class PieceNone : ChessPiece
{
    public override string Name { get { return "None"; } }
    public override char Symbol { get { return '-'; } }

    public override void UpdateMoves(ChessBoard board)
    {
        Moves = new ChessMove[0];
    }

    public override string ToString()
    {
        return Name;
    }
}

public class PiecePawn : ChessPiece
{
    public override string Name { get { return "Pawn"; } }
    public override char Symbol { get { return 'P'; } }

    private int PawnMoveY;
    private int PawnMoveDoubleY;

    public PiecePawn(int _player)
    {
        if (_player == ChessPlayer.White)
            PawnMoveY = 1;
        else if (_player == ChessPlayer.Black)
            PawnMoveY = -1;
        else
            throw new ArgumentException();

        PawnMoveDoubleY = PawnMoveY * 2;
    }

    public override void UpdateMoves(ChessBoard board)
    {
        Moves = new ChessMove[(HasMoved ? 3 : 4)];

        Moves[0] = new ChessMove(Position, -1, PawnMoveY);
        Moves[1] = new ChessMove(Position, 0, PawnMoveY);
        Moves[2] = new ChessMove(Position, 1, PawnMoveY);

        if(!HasMoved)
            Moves[3] = new ChessMove(Position, 0, PawnMoveDoubleY);
    }

    public override string ToString()
    {
        return Name;
    }
}

public class PieceRook : ChessPiece
{
    public override string Name { get { return "Rook"; } }
    public override char Symbol { get { return 'R'; } }

    public override void UpdateMoves(ChessBoard board)
    {

    }

    public override string ToString()
    {
        return Name;
    }
}

public class PieceKnight : ChessPiece
{
    public override string Name { get { return "Knight"; } }
    public override char Symbol { get { return 'N'; } }

    public override void UpdateMoves(ChessBoard board)
    {

    }

    public override string ToString()
    {
        return Name;
    }
}

public class PieceBishop : ChessPiece
{
    public override string Name { get { return "Bishop"; } }
    public override char Symbol { get { return 'B'; } }

    public override void UpdateMoves(ChessBoard board)
    {

    }

    public override string ToString()
    {
        return Name;
    }
}

public class PieceQueen : ChessPiece
{
    public override string Name { get { return "Queen"; } }
    public override char Symbol { get { return 'Q'; } }

    public override void UpdateMoves(ChessBoard board)
    {

    }

    public override string ToString()
    {
        return Name;
    }
}

public class PieceKing : ChessPiece
{
    public override string Name { get { return "King"; } }
    public override char Symbol { get { return 'K'; } }

    public override void UpdateMoves(ChessBoard board)
    {

    }

    public override string ToString()
    {
        return Name;
    }
}