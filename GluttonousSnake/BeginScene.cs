namespace GluttonousSnake;

public class BeginScene: BeginOrEndBaseScene
{
    public BeginScene()
    {
        strTitle = "贪吃蛇";
        strOne = "开始游戏";
    }
    
    public override void PressJ()
    {
        if (nowSceneIndex == 0)
        {
            Game.ChangeScene(ESceneType.Game);
        }
        else
        {
            Environment.Exit(0);
        }
    }
}