using System;

public abstract class ChessPiece
{
    // Pawn
    public bool Captured = false;
    public bool HasMoved = false;

    // Rook, Bishop, Queen, King
    protected int MOVE_DISTANCE_MAX;
    protected bool MOVE_STRAIGHT;
    protected bool MOVE_DIAGONAL;

    public string Name;
    public char Symbol;

    public ChessBoardCell Position;
    public ChessMoveRelative[] Moves;

    public int Player;

    public void MoveCommit(ChessBoardCell target)
    {
        Position = target;
        HasMoved = true;
    }

    protected virtual void GenerateMoves()
    {
        int directions = (MOVE_STRAIGHT ? 4 : 0) + (MOVE_DIAGONAL ? 4 : 0);
        Moves = new ChessMoveRelative[directions * MOVE_DISTANCE_MAX];

        for (int i = 0; i < MOVE_DISTANCE_MAX; i++)
        {
            int p = 0;

            int one = (1 * (i + 1));

            if (MOVE_STRAIGHT)
            {
                Moves[i * directions]       = new ChessMoveRelative(   0,  one);
                Moves[(i * directions) + 1] = new ChessMoveRelative( one,    0);
                Moves[(i * directions) + 2] = new ChessMoveRelative(   0, -one);
                Moves[(i * directions) + 3] = new ChessMoveRelative(-one,    0);

                p += 4;
            }

            if (MOVE_DIAGONAL)
            {
                Moves[(i * directions) + p]     = new ChessMoveRelative( one,  one);
                Moves[(i * directions) + p + 1] = new ChessMoveRelative( one, -one);
                Moves[(i * directions) + p + 2] = new ChessMoveRelative(-one, -one);
                Moves[(i * directions) + p + 3] = new ChessMoveRelative(-one,  one);
            }
        }
    }

    public ChessMoveAbsolute MoveGetAbsolute(ChessMoveRelative relMove, int player = -1, int piece = -1)
    {
        return new ChessMoveAbsolute(Position, relMove, player, piece);
    }

    public override string ToString()
    {
        return Name;
    }
}

public class PieceNone : ChessPiece
{
    public PieceNone()
    {
        Name = "None";
        Symbol = '-';
        Player = -1;

        MOVE_DISTANCE_MAX = 0;
        MOVE_STRAIGHT = false;
        MOVE_DIAGONAL = false;

        GenerateMoves();
    }

    protected override void GenerateMoves()
    {
        Moves = new ChessMoveRelative[0];
    }
}

public class PiecePawn : ChessPiece
{
    private int PawnMoveY;

    public PiecePawn(int player)
    {
        Name = "Pawn";
        Symbol = 'P';
        Player = player;

        MOVE_DISTANCE_MAX = 0;
        MOVE_STRAIGHT = false;
        MOVE_DIAGONAL = false;

        if (Player == ChessPlayer.White)
            PawnMoveY = 1;
        else if (Player == ChessPlayer.Black)
            PawnMoveY = -1;
        else
            throw new ArgumentException();

        GenerateMoves();
    }

    protected override void GenerateMoves()
    {
        Moves = new ChessMoveRelative[4];

        Moves[0] = new ChessMoveRelative(-1, PawnMoveY);
        Moves[1] = new ChessMoveRelative( 0, PawnMoveY);
        Moves[2] = new ChessMoveRelative( 1, PawnMoveY);
        Moves[3] = new ChessMoveRelative( 0, PawnMoveY * 2);
    }
}

public class PieceRook : ChessPiece
{
    public PieceRook(int player)
    {
        Name = "Rook";
        Symbol = 'R';
        Player = player;

        MOVE_DISTANCE_MAX = 7;
        MOVE_STRAIGHT = true;
        MOVE_DIAGONAL = false;

        GenerateMoves();
    }
}

public class PieceKnight : ChessPiece
{
    public PieceKnight(int player)
    {
        Name = "Knight";
        Symbol = 'N';
        Player = player;

        MOVE_DISTANCE_MAX = 0;
        MOVE_STRAIGHT = false;
        MOVE_DIAGONAL = false;

        GenerateMoves();
    }

    protected override void GenerateMoves()
    {
        Moves = new ChessMoveRelative[8];

        for (int y = 0; y < 2; y++)
        {
            int yCoefficient = (y == 1 ? -1 : 1);

            for (int x = 0; x < 2; x++)
            {
                int xCoefficient = (x == 1 ? -1 : 1);
                int p = ((y * 2) + x) * 2;

                Moves[p] = new ChessMoveRelative(1 * xCoefficient, 2 * yCoefficient);
                Moves[p + 1] = new ChessMoveRelative(2 * xCoefficient, 1 * yCoefficient);
            }
        }
    }
}

public class PieceBishop : ChessPiece
{
    public PieceBishop(int player)
    {
        Name = "Bishop";
        Symbol = 'B';
        Player = player;

        MOVE_DISTANCE_MAX = 7;
        MOVE_STRAIGHT = false;
        MOVE_DIAGONAL = true;

        GenerateMoves();
    }
}

public class PieceQueen : ChessPiece
{
    public PieceQueen(int player)
    {
        Name = "Queen";
        Symbol = 'Q';
        Player = player;

        MOVE_DISTANCE_MAX = 7;
        MOVE_STRAIGHT = true;
        MOVE_DIAGONAL = true;

        GenerateMoves();
    }
}

public class PieceKing : ChessPiece
{
    public PieceKing(int player)
    {
        Name = "King";
        Symbol = 'K';
        Player = player;

        MOVE_DISTANCE_MAX = 1;
        MOVE_STRAIGHT = true;
        MOVE_DIAGONAL = true;

        GenerateMoves();
    }
}