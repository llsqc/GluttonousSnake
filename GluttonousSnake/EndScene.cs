namespace GluttonousSnake;

public class EndScene : BeginOrEndBaseScene
{
    public EndScene()
    {
        strTitle = "结束游戏";
        strOne = "回到开始界面";
    }

    public override void PressJ()
    {
        if (nowSceneIndex == 0)
        {
            Game.ChangeScene(ESceneType.Begin);
        }
        else
        {
            Environment.Exit(0);
        }
    }
}