namespace GluttonousSnake;

public abstract class BeginOrEndBaseScene : ISceneUpdate
{
    protected int nowSceneIndex = 0;
    protected string strTitle = "";
    protected string strOne = "";

    public abstract void PressJ();
    public void Update()
    {
        Console.ForegroundColor = ConsoleColor.White;
        Console.SetCursorPosition(Game.width / 2 - strTitle.Length, 5);
        Console.Write(strTitle);

        Console.SetCursorPosition(Game.width / 2 - strOne.Length, 8);
        Console.ForegroundColor = nowSceneIndex == 0 ? ConsoleColor.Red : ConsoleColor.White;
        Console.Write(strOne);

        Console.SetCursorPosition(Game.width / 2 - 4, 10);
        Console.ForegroundColor = nowSceneIndex == 1 ? ConsoleColor.Red : ConsoleColor.White;
        Console.Write("结束游戏");

        switch (Console.ReadKey(true).Key)
        {
            case ConsoleKey.W:
                --nowSceneIndex;
                if (nowSceneIndex < 0)
                {
                    nowSceneIndex = 0;
                }

                break;
            case ConsoleKey.S:
                ++nowSceneIndex;
                if (nowSceneIndex > 1)
                {
                    nowSceneIndex = 1;
                }

                break;
            case ConsoleKey.J:
                PressJ();
                break;
        }
    }
}