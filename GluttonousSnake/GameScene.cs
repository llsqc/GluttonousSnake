namespace GluttonousSnake;

public class GameScene : ISceneUpdate
{
    Map map;
    Snake snake;
    Food food;
    private const int BaseSpeed = 220;    // 较慢的初始速度
    private const int MinSpeed = 35;      // 极限最小速度
    private const int CriticalLength = 50; // 目标挑战长度
    private const double DecayRate = 0.065; // 指数衰减系数
    private int _speedPenalty;            // 动态难度补偿
    
    
    
    public int currentSpeed;

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
        CalculateSpeed(snake.GetLength());

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

    private void CalculateSpeed(int currentLength)
    {
        // 指数衰减公式：speed = base * e^(-decay*length) + penalty
        double decayFactor = Math.Exp(-DecayRate * currentLength);
        int baseSpeedComponent = (int)(BaseSpeed * decayFactor);
    
        // 动态难度补偿：长度超过30后增加速度惩罚
        _speedPenalty = Math.Max(0, (currentLength - 30) / 5) * 3;
    
        // 综合计算最终速度
        currentSpeed = baseSpeedComponent - _speedPenalty;
    
        // 安全限制并确保最低速度
        currentSpeed = Math.Clamp(currentSpeed, MinSpeed, BaseSpeed);
    }

    public int GetcurrentSpeed()
    {
        return currentSpeed;
    }
}