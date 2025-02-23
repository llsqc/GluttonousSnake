namespace GluttonousSnake;

public enum EMoveDir
{
    Up,
    Down,
    Left,
    Right
}

public class Snake : IDraw
{
    private SnakeBody[] bodies;

    private int nowLength;

    EMoveDir moveDir;

    public Snake(int x, int y)
    {
        bodies = new SnakeBody[200];
        bodies[0] = new SnakeBody(ESnakeBodyType.Head, x, y);
        nowLength = 1;
        moveDir = EMoveDir.Right;
    }

    public void Draw()
    {
        for (int i = 0; i < nowLength; i++)
        {
            bodies[i].Draw();
        }
    }

    public void Move()
    {
        SnakeBody lastBody = bodies[nowLength - 1];
        Console.SetCursorPosition(lastBody.pos.X, lastBody.pos.Y);
        Console.Write("  ");

        for (int i = nowLength - 1; i > 0; i--)
        {
            bodies[i].pos = bodies[i - 1].pos;
        }

        switch (moveDir)
        {
            case EMoveDir.Up:
                bodies[0].pos.Y--;
                break;
            case EMoveDir.Down:
                bodies[0].pos.Y++;
                break;
            case EMoveDir.Left:
                bodies[0].pos.X -= 2;
                break;
            case EMoveDir.Right:
                bodies[0].pos.X += 2;
                break;
        }
    }

    public void ChangeDir(EMoveDir newDir)
    {
        if (newDir == moveDir ||
            nowLength > 1 &&
            (newDir == EMoveDir.Right && moveDir == EMoveDir.Left ||
             newDir == EMoveDir.Up && moveDir == EMoveDir.Down ||
             newDir == EMoveDir.Down && moveDir == EMoveDir.Up ||
             newDir == EMoveDir.Left && moveDir == EMoveDir.Right))
        {
            return;
        }

        moveDir = newDir;
    }

    public bool CheckEnd(Map map)
    {
        for (int i = 0; i < map.walls.Length; i++)
        {
            if (bodies[0].pos == map.walls[i].pos)
            {
                return true;
            }
        }

        for (int i = 1; i < nowLength; i++)
        {
            if (bodies[0].pos == bodies[i].pos)
            {
                return true;
            }
        }

        return false;
    }

    public bool CheckSamePos(Position pos)
    {
        for (int i = 0; i < nowLength; i++)
        {
            if (bodies[i].pos == pos)
            {
                return true;
            }
        }

        return false;
    }

    public void CheckEatFood(Food food)
    {
        if (bodies[0].pos == food.pos)
        {
            food.RandomPos(this);
            AddBody();
        }
    }

    private void AddBody()
    {
        SnakeBody frontBody = bodies[nowLength - 1];
        bodies[nowLength] = new SnakeBody(ESnakeBodyType.Body, frontBody.pos.X, frontBody.pos.Y);
        nowLength++;
    }
}