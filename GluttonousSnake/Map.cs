namespace GluttonousSnake;

public class Map : IDraw
{
    public Wall[] walls;

    public Map()
    {
        walls = new Wall[Game.width + (Game.height - 3) * 2];
        int index = 0;
        for (int i = 0; i < Game.width; i += 2)
        {
            walls[index] = new Wall(i, 0);
            ++index;
        }

        for (int i = 0; i < Game.width; i += 2)
        {
            walls[index] = new Wall(i, Game.height - 2);
            ++index;
        }

        for (int i = 1; i < Game.height - 2; i++)
        {
            walls[index] = new Wall(0, i);
            ++index;
        }

        for (int i = 1; i < Game.height - 2; i++)
        {
            walls[index] = new Wall(Game.width - 2, i);
            ++index;
        }
    }

    public void Draw()
    {
        for (int i = 0; i < walls.Length; i++)
        {
            walls[i].Draw();
        }
    }
}