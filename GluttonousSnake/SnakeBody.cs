using System.Numerics;

namespace GluttonousSnake;

public enum ESnakeBodyType
{
    Head,
    Body
}

public class SnakeBody : GameObject
{
    private ESnakeBodyType type;

    public SnakeBody(ESnakeBodyType type, int x, int y)
    {
        this.type = type;
        this.pos = new Position(x, y);
    }

    public override void Draw()
    {
        Console.SetCursorPosition(pos.X, pos.Y);
        Console.ForegroundColor = type == ESnakeBodyType.Head ? ConsoleColor.Yellow : ConsoleColor.Green;
        Console.Write(type == ESnakeBodyType.Head ? "●" : "◎");
    }
}