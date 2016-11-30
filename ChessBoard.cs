public class ChessBoard
{
    public ChessPiece[,] pieces = new ChessPiece[2, 16];
    private ChessMove lastMove = new ChessMove();
    public int onTurn = ChessPlayer.White;

    public ChessBoard()
    {
        LoadDefaultPositions();
    }

    private void LoadDefaultPositions()
    {
        int file = 0;
        int rank = 0;

        for(int i1 = 0; i1 < 2; i1++)
        {
            for(int i2 = 0; i2 < 16; i2++)
            {
                if(i1 == 0)
                    rank = 1 + (i2 / 8);
                else
                    rank = 8 - (i2 / 8);

                file = (i2 % 8) + 1;

                pieces[i1, i2] = GetDefaulPiece(file, rank);
                pieces[i1, i2].Position = new ChessBoardCell(file, rank);
            }
        }
    }

    private ChessPiece GetDefaulPiece(int file, int rank)
    {
        switch(rank)
        {
            case 1:
            case 8:
                switch(file)
                {
                    case 1:
                    case 8:
                        return new Rook();

                    case 2:
                    case 7:
                        return new Knight();

                    case 3:
                    case 6:
                        return new Bishop();

                    case 4:
                        return new Queen();

                    //case 5:
                    default:
                        return new King();
                }

            case 2:
                return new Pawn(ChessPlayer.White);
            //case 7:
            default:
                return new Pawn(ChessPlayer.Black);
        }
    }

    public ChessMove[] GetMoves()
    {
        ChessMove[] movesTemp = new ChessMove[1024];
        int movesCount = 0;

        // Get all the moves and remove illegal ones
        for (int i1 = 0; i1 < 16; i1++)
        {
            // Get all the possible moves for all allied pieces
            ChessMove[] movesPiece = pieces[onTurn, i1].GetMoves(i1);

            // Determine which moves are legal
            for (int i2 = 0; i2 < movesPiece.Length; i2++)
            {
                // =======================================
                // TODO: Pieces can't leap over each other
                // =======================================
                
                bool isLegal = true;
                bool isValidPawnAttack = false;

                if(movesPiece[i2].Target.File < 1 || movesPiece[i2].Target.File > 8 || movesPiece[i2].Target.Rank < 1 || movesPiece[i2].Target.Rank > 8)
                    isLegal = false;

                // Check for unwanted interference
                for(int i3 = 0; isLegal && !isValidPawnAttack && i3 < 16; i3++)
                {
                    // Pawn
                    if(pieces[onTurn, i1].GetType() == typeof(Pawn))
                    {
                        // Pawn can't attack without an attack move
                        if(movesPiece[i2].Type != ChessMoveType.PawnAttack && movesPiece[i2].Target.Interferes(pieces[GetOpponent(), i3].Position))
                            isLegal = false;

                        if(movesPiece[i2].Type == ChessMoveType.PawnAttack)
                        {
                            // Classic attack
                            if(movesPiece[i2].Target.Interferes(pieces[GetOpponent(), i3].Position) ||
                            // En passant
                            (lastMove.Type == ChessMoveType.PawnDouble && movesPiece[i2].Target.File == lastMove.Target.File &&
                            movesPiece[i2].Target.Rank == (onTurn == ChessPlayer.White ? 6 : 3)))
                                isValidPawnAttack = true;
                        }
                    }
                    
                    // Move is illegal if there's an allied pieces in target position
                    if(isLegal && movesPiece[i2].Target.Interferes(pieces[onTurn, i3].Position))
                        isLegal = false;
                }

                // Invalid pawn attack is illegal move
                if(isLegal && movesPiece[i2].Type == ChessMoveType.PawnAttack && !isValidPawnAttack)
                    isLegal = false;

                // Legal move is added to the list of all legal moves
                if(isLegal)
                {
                    movesTemp[movesCount] = movesPiece[i2];
                    movesCount++;
                }
            }
        }

        // Return legal moves
        ChessMove[] movesLegal = new ChessMove[movesCount];
        
        for (int i = 0; i < movesCount; i++)
            movesLegal[i] = movesTemp[i];

        return movesLegal;
    }

    private int GetOpponent()
    {
        if(onTurn == ChessPlayer.White)
            return ChessPlayer.Black;
        else
            return ChessPlayer.White;
    }
}

public class ChessBoardCell
{
    public int File;
    public int Rank;

    public ChessBoardCell(int _file, int _rank)
    {
        File = _file;
        Rank = _rank;
    }

    public bool Interferes(ChessBoardCell _cell)
    {
        return (File == _cell.File && Rank == _cell.Rank);
    }
}

public class ChessMove
{
    public int PieceID;
    public int Type;

    public ChessBoardCell Origin;
    public ChessBoardCell Target;

    public ChessMove()
    {
        Type = ChessMoveType.None;
    }

    public ChessMove(int _pieceID, ChessBoardCell _origin, int _relFile, int _relRank, int _type = ChessMoveType.Default)
    {
        PieceID = _pieceID;
        Type = _type;

        Origin = _origin;
        Target = new ChessBoardCell(Origin.File + _relFile, Origin.Rank + _relRank);
    }
}

public static class ChessMoveType
{
    public const int None = -1;
    public const int Default = 0;
    public const int PawnAttack = 1;
    public const int PawnDouble = 2;
    public const int Castling = 3;
}

public static class ChessPlayer
{
    public const int White = 0;
    public const int Black = 1;
}