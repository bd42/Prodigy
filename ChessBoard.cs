public class ChessBoard
{
    public ChessPiece[,] Cells;
    public ChessPiece[,] Pieces;
    public int PlayerOnTurn;
    
    private ChessMove LastMove;

    public ChessBoard()
    {
        Cells = new ChessPiece[8, 8];
        Pieces = new ChessPiece[2, 16];
        PlayerOnTurn = ChessPlayer.White;
        LastMove = null;

        LoadDefaultPositions();
    }

    private void LoadDefaultPositions()
    {
        int file = 0;
        int rank = 0;

        for (int iPlayer = 0; iPlayer < 2; iPlayer++)
        {
            for (int iPiece = 0; iPiece < 16; iPiece++)
            {
                if (iPlayer == 0)
                    rank = (iPiece / 8);
                else
                    rank = 7 - (iPiece / 8);

                file = iPiece % 8;

                Pieces[iPlayer, iPiece] = GetDefaulPiece(file, rank);
                Pieces[iPlayer, iPiece].Position = new ChessBoardCell(file, rank);
            }
        }
    }

    private ChessPiece GetDefaulPiece(int file, int rank)
    {
        switch(rank)
        {
            case 0:
            case 7:
                switch(file)
                {
                    case 0:
                    case 7:
                        return new PieceRook();

                    case 1:
                    case 6:
                        return new PieceKnight();

                    case 2:
                    case 5:
                        return new PieceBishop();

                    case 3:
                        return new PieceQueen();
                    case 4:
                        return new PieceKing();

                    default:
                        return new PieceNone();
                }

            case 1:
                return new PiecePawn(ChessPlayer.White);
            case 6:
                return new PiecePawn(ChessPlayer.Black);

            default:
                return new PieceNone();
        }
    }

    public void UpdateCells()
    {
        // Clear the board
        for (int iRank = 0; iRank < 8; iRank++)
            for (int iFile = 0; iFile < 8; iFile++)
                Cells[iFile, iRank] = new PieceNone();

        // Fill the board with non-captured pieces
        for (int iPlayer = 0; iPlayer < 2; iPlayer++)
            for (int iPiece = 0; iPiece < 16; iPiece++)
            {
                ChessPiece piece = Pieces[iPlayer, iPiece];

                if (!piece.Captured)
                    Cells[piece.Position.File, piece.Position.Rank] = piece;
            }
    }

    /*public ChessMove[] GetMoves()
    {
        ChessMove[] movesTemp = new ChessMove[1024];
        int movesCount = 0;

        // Get all the moves and remove illegal ones
        for (int i1 = 0; i1 < 16; i1++)
        {
            // Get all the possible moves for all allied pieces
            ChessMove[] movesPiece = Pieces[PlayerOnTurn, i1].GetMoves(i1);

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
                    if(Pieces[PlayerOnTurn, i1].GetType() == typeof(Pawn))
                    {
                        // Pawn can't attack without an attack move
                        if(movesPiece[i2].Type != ChessMoveType.PawnAttack && movesPiece[i2].Target.Interferes(Pieces[GetOpponent(), i3].Position))
                            isLegal = false;

                        if(movesPiece[i2].Type == ChessMoveType.PawnAttack)
                        {
                            // Classic attack
                            if(movesPiece[i2].Target.Interferes(Pieces[GetOpponent(), i3].Position) ||
                            // En passant
                            (LastMove.Type == ChessMoveType.PawnDouble && movesPiece[i2].Target.File == LastMove.Target.File &&
                            movesPiece[i2].Target.Rank == (PlayerOnTurn == ChessPlayer.White ? 6 : 3)))
                                isValidPawnAttack = true;
                        }
                    }
                    
                    // Move is illegal if there's an allied pieces in target position
                    if(isLegal && movesPiece[i2].Target.Interferes(Pieces[PlayerOnTurn, i3].Position))
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
    }*/

    private int GetOpponent()
    {
        if (PlayerOnTurn == ChessPlayer.White)
            return ChessPlayer.Black;
        else
            return ChessPlayer.White;
    }

    public override string ToString()
    {
        string strBoardMatrix = string.Empty;

        for (int iRank = 7; iRank >= 0; iRank--)
        {
            for (int iFile = 0; iFile < 8; iFile++)
            {
                strBoardMatrix += Cells[iFile, iRank].Symbol;

                if (iFile < 7)
                    strBoardMatrix += ' ';
            }

            if(iRank > 0)
                strBoardMatrix += System.Environment.NewLine;
        }

        return strBoardMatrix;
    }
}