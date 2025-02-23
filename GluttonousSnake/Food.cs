namespace GluttonousSnake;

public class Food : GameObject
{
    public Food(Snake snake)
    {
        RandomPos(snake);
    }

    public override void Draw()
    {
        Console.SetCursorPosition(pos.X, pos.Y);
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.Write("¤");
    }

    public void RandomPos(Snake snake)
    {
        Random rnd = new Random();
        int x = rnd.Next(2, Game.width / 2 - 1) * 2;
        int y = rnd.Next(1, Game.height - 4);
        pos = new Position(x, y);
        if (snake.CheckSamePos(pos))
        {
            RandomPos(snake);
        }
    }
}