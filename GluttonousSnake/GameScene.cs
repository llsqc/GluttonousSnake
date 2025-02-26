namespace GluttonousSnake;

public class GameScene : ISceneUpdate
{
    Map map;
    Snake snake;
    Food food;
    private const int BaseSpeed = 220; // 较慢的初始速度
    private const int MinSpeed = 35; // 极限最小速度
    private const double DecayRate = 0.065; // 指数衰减系数
    private int _speedPenalty; // 动态难度补偿

    // 类级别添加这两个字段
    private CancellationTokenSource _inputCts;
    private Thread _directionInputThread;

    public GameScene()
    {
        map = new Map();
        snake = new Snake(40, 10);
        food = new Food(snake);
    }

    public void Update()
    {
        // 初始化输入线程（仅一次）
        if (_directionInputThread == null)
        {
            _inputCts = new CancellationTokenSource();
            _directionInputThread = new Thread(() => InputHandler(_inputCts.Token))
            {
                IsBackground = true
            };
            _directionInputThread.Start();
        }

        CalculateSpeed(snake.GetLength());

        map.Draw();
        food.Draw();

        snake.Move();
        snake.Draw();

        if (snake.CheckEnd(map))
        {
            // 安全终止输入线程
            _inputCts?.Cancel();
            _directionInputThread?.Join(100); // 等待100ms确保退出

            Game.ChangeScene(ESceneType.End);
            return; // 提前退出避免后续逻辑
        }

        snake.CheckEatFood(food);
    }

    private void InputHandler(CancellationToken token)
    {
        try
        {
            while (!token.IsCancellationRequested)
            {
                if (Console.KeyAvailable)
                {
                    var key = Console.ReadKey(true).Key;
                    HandleDirectionalInput(key);
                }

                Thread.Sleep(50); // 降低CPU占用
            }
        }
        catch (OperationCanceledException)
        {
            // 正常退出
        }
    }

    private void HandleDirectionalInput(ConsoleKey key)
    {
        switch (key)
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
        }
    }

    private int currentSpeed;

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

    public int GetCurrentSpeed() => currentSpeed;
}