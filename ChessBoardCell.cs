public class ChessBoardCell
{
    public int File; // X axis
    public int Rank; // Y axis

    public ChessBoardCell(int _file, int _rank)
    {
        File = _file;
        Rank = _rank;
    }

    public bool Equals(ChessBoardCell _cell)
    {
        return (File == _cell.File &&
                Rank == _cell.Rank);
    }

    public static bool Equals(ChessBoardCell _cell1, ChessBoardCell _cell2)
    {
        return (_cell1.File == _cell2.File &&
                _cell1.Rank == _cell2.Rank);
    }

    public override string ToString()
    {
        return string.Format("{0}:{1}",
                             (char)('a' + File),
                             Rank + 1);
    }
}