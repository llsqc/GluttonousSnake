﻿using System.Diagnostics;

namespace GluttonousSnake;

public enum ESceneType
{
    Begin,
    Game,
    End
}

public class Game
{
    // 游戏窗口宽高
    public const int width = 80;
    public const int height = 20;
    public int currentSpeed;

    // 当前选中的场景
    public static ISceneUpdate nowScene = new BeginScene();


    public Game()
    {
        Console.CursorVisible = false;
        Console.SetWindowSize(width, height);
        Console.SetBufferSize(width, height);

        ChangeScene(ESceneType.Begin);
    }

    // 游戏开始的方法
    public void Start()
    {
        // 游戏主循环
        while (true)
        {
            if (nowScene != null)
            {
                if (nowScene is GameScene)
                {
                    currentSpeed = (nowScene as GameScene).GetCurrentSpeed();
                    Debug.WriteLine($"Current Speed: {currentSpeed}ms");
                }

                Thread.Sleep(currentSpeed);
                nowScene.Update();
            }
        }
    }

    public static void ChangeScene(ESceneType type)
    {
        Console.Clear();
        switch (type)
        {
            case ESceneType.Begin:
                nowScene = new BeginScene();
                break;
            case ESceneType.Game:
                nowScene = new GameScene();
                break;
            case ESceneType.End:
                nowScene = new EndScene();
                break;
        }
    }
}