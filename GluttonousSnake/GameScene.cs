namespace GluttonousSnake;

public class GameScene : ISceneUpdate
{
    Map map;
    Snake snake;
    Food food;

    public GameScene()
    {
        map = new Map();
        snake = new Snake(40, 10);
        food = new Food(snake);
    }

    void HandleDirectionalInput()
    {
        switch (Console.ReadKey(true).Key)
        {
            case ConsoleKey.W:
                snake.ChangeDir(EMoveDir.Up);
                break;
            case ConsoleKey.A:
                snake.ChangeDir(EMoveDir.Left);
                break;
            case ConsoleKey.S:
                snake.ChangeDir(EMoveDir.Down);
                break;
            case ConsoleKey.D:
                snake.ChangeDir(EMoveDir.Right);
                break;
            default:
                throw new ArgumentOutOfRangeException();
        }
    }

    public void Update()
    {
        Thread directionInputThread = new Thread(HandleDirectionalInput)
        {
            IsBackground = true
        };
        directionInputThread.Start();

        map.Draw();
        food.Draw();

        snake.Move();
        snake.Draw();

        if (snake.CheckEnd(map))
        {
            Game.ChangeScene(ESceneType.End);
        }

        snake.CheckEatFood(food);
    }
}