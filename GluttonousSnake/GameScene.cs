namespace GluttonousSnake;

public class GameScene : ISceneUpdate
{
    Map map;
    Snake snake;
    Food food;

    int updateIndex = 0;

    public GameScene()
    {
        map = new Map();
        snake = new Snake(40, 10);
        food = new Food(snake);
    }

    public void Update()
    {
        if (++updateIndex % 3000 == 0)
        {
            map.Draw();
            food.Draw();
            
            snake.Move();
            snake.Draw();
            
            if (snake.CheckEnd(map))
            {
                Game.ChangeScene(ESceneType.End);
            }

            snake.CheckEatFood(food);
            updateIndex = 0;
        }

        if (Console.KeyAvailable)
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
    }
}