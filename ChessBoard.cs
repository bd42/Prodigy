public class ChessBoard
{
    public ChessPiece[,] pieces = new ChessPiece[2, 16];

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
                    rank = (i2 / 8) + 1;
                else
                    rank = (i2 / 8) + 7;

                file = (i2 % 8) + 1;

                pieces[i1, i2] = GetDefaulPiece(file, rank);
                pieces[i1, i2].Position = new ChessBoardCell(file, rank);
                pieces[i1, i2].Player = i1; 
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

            //case 2:
            //case 7:
            default:
                return new Pawn();
        }
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
}

public class ChessMove
{
    public bool Legal;
    public bool Attack;
    public bool Double;

    public ChessBoardCell Origin;
    public ChessBoardCell Target;

    public ChessMove(ChessBoardCell _origin, int _relFile, int _relRank, bool _attack = false, bool _double = false)
    {
        Legal = true;
        Attack = _attack;
        Double = _double;

        Origin = _origin;
        Target = new ChessBoardCell(Origin.File + _relFile, Origin.Rank + _relRank);
    }
}