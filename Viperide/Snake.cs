using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum EnumDirection
{
    Left,
    Up,
    Right,
    Down
}

public class Snake
{
    #region Properties
    Grid grid;

    Queue<Coordinates> body = new Queue<Coordinates>();
    Coordinates currentDirection = Coordinates.right;
    Coordinates nextDirection = Coordinates.right;
    bool directionChanged;
    private bool growing = false;

    Color color = Color.White;
    #endregion

    public Coordinates head => body.Last();
    
    public Snake(Coordinates coordinate, Grid grid, int startSize=3)
    {
        this.grid = grid;
        for(int i = startSize-1;i>0;i--)
            body.Enqueue(coordinate- currentDirection * i);
        
        body.Enqueue(coordinate);
    }

    public void Draw()
    {
        foreach (Coordinates coordinate in body)
        {
            var posInWorld = grid.GridToWorld(coordinate);
            Raylib.DrawRectangle((int)posInWorld.X, (int)posInWorld.Y, grid.cellSize, grid.cellSize, color);
        }
    }

    public void RandomColor()
    {
        Random random = new Random();
        int value = random.Next(0, 5);
        switch(value)
        {
            case 0:
                color = Color.Purple; break;
            case 1:
                color = Color.Red; break;
            case 2: 
                color = Color.Green; break;
            case 3:
                color = Color.Blue; break;
            case 4:
                color = Color.Yellow; break;
            default:
                break;
        }
    }

    public void Growth()
    {
        growing = true;
    }
    public void Move()
    {
        currentDirection = nextDirection;
        var head = body.Last();
        var newHead = head + currentDirection;
        body.Enqueue(newHead);
        if (!growing)
            body.Dequeue();

        growing = false;
        RandomColor();
    }

    public void ChangeDirection(Coordinates direction)
    {
        if (this.currentDirection == -direction || direction==Coordinates.zero) return;
        this.nextDirection = direction;
    }
}
