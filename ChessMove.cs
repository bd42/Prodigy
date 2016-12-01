public class ChessMove
{
    public ChessBoardCell Origin;
    public ChessBoardCell Target;

    public ChessMove(ChessBoardCell _origin, int _relFile, int _relRank)
    {
        Origin = _origin;
        Target = new ChessBoardCell(Origin.File + _relFile, Origin.Rank + _relRank);
    }

    public override string ToString()
    {
        return string.Format("{0} -> {1}", Origin, Target);
    }
}