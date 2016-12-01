public class ChessMoveAbsolute
{
    public ChessBoardCell Origin;
    public ChessBoardCell Target;

    public int Player;
    public int Piece;

    public ChessMoveAbsolute(ChessBoardCell origin, int relFile, int relRank, int player, int piece)
    {
        Origin = origin;
        Target = new ChessBoardCell(Origin.File + relFile, Origin.Rank + relRank);
    }

    public ChessMoveAbsolute(ChessBoardCell origin, ChessMoveRelative relMove, int player, int piece)
    {
        Origin = origin;
        Target = new ChessBoardCell(Origin.File + relMove.Files, Origin.Rank + relMove.Ranks);
    }

    public override string ToString()
    {
        return string.Format("{0} -> {1}", Origin, Target);
    }
}

public class ChessMoveRelative
{
    public int Files;
    public int Ranks;

    public ChessMoveRelative(int files, int ranks)
    {
        Files = files;
        Ranks = ranks;
    }

    public override string ToString()
    {
        return string.Format("[X:{0}; Y:{1}]", Files, Ranks);
    }
}