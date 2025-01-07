namespace chilipublish_challenge;

public class Domino(int leftSide, int rightSide)
{
    private readonly int _leftSide = leftSide - 1;
    private readonly int _rightSide = rightSide - 1;

    public int LeftSide => _leftSide + 1;

    public int RightSide => _rightSide + 1;

    public override string ToString()
    {
        return $"({LeftSide}, {RightSide})";
    }

    public override bool Equals(object? obj)
    {
        if (obj is not Domino other) return false;

        return (LeftSide == other.LeftSide && RightSide == other.RightSide) ||
               (LeftSide == other.RightSide && RightSide == other.LeftSide);
    }

    public override int GetHashCode()
    {
        return HashCode.Combine(LeftSide, RightSide);
    }
}