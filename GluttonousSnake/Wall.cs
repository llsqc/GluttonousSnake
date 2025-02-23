namespace GluttonousSnake;

public class Wall : GameObject
{
    public Wall(int x, int y)
    {
        pos = new Position(x, y);
    }

    public override void Draw()
    {
        Console.SetCursorPosition(pos.X, pos.Y);
        Console.ForegroundColor = ConsoleColor.Red;
        Console.Write("■");
    }
}