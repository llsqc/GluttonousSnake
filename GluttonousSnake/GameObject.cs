namespace GluttonousSnake;

public abstract class GameObject : IDraw
{
    public Position pos;

    public abstract void Draw();
}