namespace GluttonousSnake;

public struct Position
{
    public int X;
    public int Y;

    public Position(int x, int y)
    {
        this.X = x;
        this.Y = y;
    }

    public static bool operator ==(Position p1, Position p2)
    {
        return p1.X == p2.X && p1.Y == p2.Y;
    }

    public static bool operator !=(Position p1, Position p2)
    {
        return !(p1.X == p2.X && p1.Y == p2.Y);
    }

    public override bool Equals(object? obj)
    {
        if (obj is Position other)
        {
            return this == other;
        }

        return false;
    }

    public override int GetHashCode()
    {
        return X.GetHashCode() ^ Y.GetHashCode();
    }
}