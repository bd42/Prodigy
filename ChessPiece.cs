public abstract class ChessPiece
{
    protected const int PLAYER_WHITE = 0;
    protected const int PLAYER_BLACK = 1;

    public int Player;
    public bool Captured = false;
    protected bool HasMoved = false;

    public ChessBoardCell Position;

    public ChessMove[] GetMoves()
    {
        if(Captured)
            return new ChessMove[0];
        else
            // TODO: Remove all moves that aren't legal
            return GetAllMoves();
    }

    protected abstract ChessMove[] GetAllMoves();

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

    protected int GetOpponent()
    {
        if(Player == PLAYER_WHITE)
            return PLAYER_BLACK;
        else
            return PLAYER_WHITE;
    }
}

public class Pawn : ChessPiece
{
    private int PawnMoveY;
    private int PawnMoveDoubleY;

    public Pawn()
    {
        if(Player == PLAYER_WHITE)
            PawnMoveY = 1;
        else if(Player == PLAYER_BLACK)
            PawnMoveY = -1;
        else
            PawnMoveY = 0;

        PawnMoveDoubleY = PawnMoveY * 2;
    }

    protected override ChessMove[] GetAllMoves()
    {
        ChessMove[] movesAll = new ChessMove[(HasMoved ? 3 : 4)];

        movesAll[0] = new ChessMove(Position, -1, PawnMoveY, true);
        movesAll[1] = new ChessMove(Position, 0, PawnMoveY, false);
        movesAll[2] = new ChessMove(Position, 1, PawnMoveY, true);

        if(!HasMoved)
            movesAll[4] = new ChessMove(Position, 0, 2 * PawnMoveDoubleY, false, true);

        int movesLegalCount = 0;

        for (int i = 0; i < movesAll.Length; i++)
        {
            if(/* TODO: Condition */)
            {
                movesAll[i].Legal = true;
                movesLegalCount++;
            }
            else
                movesAll[i].Legal = false;
        }

        if(movesLegalCount == movesAll.Length)
            return movesAll;

        ChessMove[] movesLegal = new ChessMove[movesLegalCount];
        int movesLegalIndex = 0;

        foreach (ChessMove move in movesAll)
        {
            if(move.Legal)
            {
                movesLegal[movesLegalIndex] = move;
                movesLegalIndex++;

                if(movesLegalIndex >= movesLegalCount)
                    break;
            }
        }

        return movesLegal;
    }
}

public class Rook : ChessPiece
{
    protected override ChessMove[] GetAllMoves()
    {
        return new ChessMove[0];
    }
}

public class Knight : ChessPiece
{
    protected override ChessMove[] GetAllMoves()
    {
        return new ChessMove[0];
    }
}

public class Bishop : ChessPiece
{
    protected override ChessMove[] GetAllMoves()
    {
        return new ChessMove[0];
    }
}

public class Queen : ChessPiece
{
    protected override ChessMove[] GetAllMoves()
    {
        return new ChessMove[0];
    }
}

public class King : ChessPiece
{
    protected override ChessMove[] GetAllMoves()
    {
        return new ChessMove[0];
    }
}