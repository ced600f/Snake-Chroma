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
    private string colorName = "Green";

    Queue<Coordinates> body = new Queue<Coordinates>();
    Coordinates currentDirection = Coordinates.right;
    Coordinates nextDirection = Coordinates.right;
    bool directionChanged;
    private int growing = 0;

    Color color = Color.Green;
    #endregion

    public int Length => body.Count;
    public string SnakeColor => colorName;
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
        int i = 0;
        foreach (Coordinates coordinate in body)
        {
            var posInWorld = grid.GridToWorld(coordinate);
            if (i == body.Count-1)
            {
                Texture2D texture = ((Textures)Services.Get<Textures>()).GetTexture("SnakeHead"+colorName);
                var inWorld = grid.GridToWorld(coordinate);
                Rectangle source = new Rectangle(0, 45, 60, 90);

                int DeltaX = (int)(grid.cellSize/2 - source.Width/2);
                int DeltaY = (int)(grid.cellSize/2 - source.Height/2);

                Rectangle dest = new Rectangle((int)inWorld.X+source.Width/2+DeltaX, (int)inWorld.Y+source.Height/2+DeltaY, source.Width, source.Height);
                Raylib.DrawTexturePro(texture, source, dest, new System.Numerics.Vector2(source.Width/2, source.Height/2),(float)Coordinates.GetAngle(this.currentDirection)-90 ,Color.White);

            }
            else if (i==0)
            {
                Texture2D texture = ((Textures)Services.Get<Textures>()).GetTexture("SnakeParts" + colorName);
                var inWorld = grid.GridToWorld(coordinate);
                Rectangle source = new Rectangle(84, 0, 84, 84);

                int DeltaX = (int)(grid.cellSize / 2 - source.Width / 2);
                int DeltaY = (int)(grid.cellSize / 2 - source.Height / 2);

                Rectangle dest = new Rectangle((int)inWorld.X + source.Width / 2 + DeltaX, (int)inWorld.Y + source.Height / 2 + DeltaY, source.Width, source.Height);
                Raylib.DrawTexturePro(texture, source, dest, new System.Numerics.Vector2(source.Width / 2, source.Height / 2), (float)Coordinates.GetAngle(coordinate, body.ElementAt(i + 1))+90, Color.White);
            }
            else
            {
                Texture2D texture = ((Textures)Services.Get<Textures>()).GetTexture("SnakeParts" + colorName);
                var inWorld = grid.GridToWorld(coordinate);
                Rectangle source = new Rectangle(0, 0, 84, 84);

                int DeltaX = (int)(grid.cellSize / 2 - source.Width / 2);
                int DeltaY = (int)(grid.cellSize / 2 - source.Height / 2);

                Rectangle dest = new Rectangle((int)inWorld.X + source.Width / 2 + DeltaX, (int)inWorld.Y + source.Height / 2 + DeltaY, source.Width, source.Height);
                Raylib.DrawTexturePro(texture, source, dest, new System.Numerics.Vector2(source.Width / 2, source.Height / 2), (float)Coordinates.GetAngle(coordinate, body.ElementAt(i+1)) - 90, Color.White);
            }
            i++;
        }
    }

    public bool IsOutOfBound()
    {
        return head.row<0 || head.column<0 ||head.row>=grid.rows || head.column>=grid.columns;
    }

    public bool IsOverlapping()
    {
        return body.Count != body.Distinct().Count();
    }

    public void RemoveElements(int nElements)
    {
        for(int i = 0; i < nElements; i++)
        {
            if (body.Count > 2)
                body.Dequeue();
        }
    }

    public void LoseQueue(Coordinates cut)
    {
        Coordinates coordinates = body.Dequeue();
        while (coordinates != cut)
        {
            coordinates = body.Dequeue();
        }
        RandomColor();
    }

    public void RandomColor()
    {
        Random random = new Random();
        int value = random.Next(0, 5);
        switch(value)
        {
            case 0:
                if (!SetColor("Purple", Color.Purple)) RandomColor(); 
                break;
            case 1:
                if (!SetColor("Red", Color.Red)) RandomColor();
                break;
            case 2:
                if (!SetColor("Green", Color.Green)) RandomColor();
                break;
            case 3:
                if (!SetColor("Blue", Color.Blue)) RandomColor();
                break;
            case 4:
                if (!SetColor("Yellow", Color.Yellow)) RandomColor();
                break;
            default:
                break;
        }
    }

    private bool SetColor(string name,  Color col)
    {
        if (name == colorName) return false;

        colorName = name;
        color = col;

        return true;
    }

    public void Growth(int nbSegments=1)
    {
        Console.WriteLine(nbSegments);
        growing = nbSegments;
    }
    public void Move()
    {
        currentDirection = nextDirection;
        var head = body.Last();
        var newHead = head + currentDirection;
        body.Enqueue(newHead);
        if (growing == 0)
        {
            body.Dequeue();
        }
        else
            growing--;
    }

    public void Collision(int nElements=1)
    {
        var head = body.Last();
        var newHead = head - currentDirection;
        body.Enqueue(newHead);
        RemoveElements(nElements);
    }

    public bool IsCollindingWith(Coordinates coordinates)
    {
        return body.Contains(coordinates);
    }

    public void ChangeDirection(Coordinates direction)
    {
        if (this.currentDirection == -direction || direction==Coordinates.zero) return;
        this.nextDirection = direction;
    }
}
