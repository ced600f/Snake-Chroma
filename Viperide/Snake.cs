using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

public enum EnumDirection
{
    Left,
    Up,
    Right,
    Down
}

public enum EnumMoveType
{
    none,
    OutOfBound,
    Overlap,
    Fall
}

public class Snake
{
    #region Properties
    Tilemap tilemap;
    private string colorName = "Green";

    Queue<Coordinates> body = new Queue<Coordinates>();
    Coordinates currentDirection = Coordinates.right;
    Coordinates nextDirection = Coordinates.right;
    bool directionChanged;
    private int growing = 0;
    public EnumMoveType MoveType {  get; private set; }

    private readonly float defaultSpeed = 0.4f;
    public float CurrentSpeed { get; private set; }

    Color color = Color.Green;
    #endregion

    public int Length => body.Count;
    public string SnakeColor => colorName;
    public Coordinates head => body.Last();
    public Coordinates tail => body.First();
    public int LoseDurationDelta => (int)(body.Count()-2)/2;
    public Snake(Coordinates coordinate, Tilemap tilemap, int startSize=3)
    {
        this.tilemap = tilemap;
        CurrentSpeed = defaultSpeed;
        for(int i = startSize-1;i>0;i--)
            body.Enqueue(coordinate- currentDirection * i);
        
        body.Enqueue(coordinate);
    }


    #region draw
    public void DrawHead()
    {
        Coordinates coordinates = head;
        Texture2D texture = ((Textures)Services.Get<Textures>()).GetTexture("SnakeHead" + colorName);
        var inWorld = tilemap.MapToWorld(coordinates);
        Rectangle source = new Rectangle(0, 45, 60, 90);

        int DeltaX = (int)(tilemap.tileSize / 2 - source.Width / 2);
        int DeltaY = (int)(tilemap.tileSize / 2 - source.Height / 2);

        Rectangle dest = new Rectangle((int)inWorld.X + source.Width / 2 + DeltaX, (int)inWorld.Y + source.Height / 2 + DeltaY, source.Width, source.Height);
        Raylib.DrawTexturePro(texture, source, dest, new System.Numerics.Vector2(source.Width / 2, source.Height / 2), (float)Coordinates.GetAngle(this.currentDirection) - 90, Color.White);

    }

    public void DrawTail()
    {
        Coordinates coordinates = tail;
        Texture2D texture = ((Textures)Services.Get<Textures>()).GetTexture("SnakeParts" + colorName);
        var inWorld = tilemap.MapToWorld(coordinates);
        Rectangle source = new Rectangle(84, 0, 84, 84);

        int DeltaX = (int)(tilemap.tileSize / 2 - source.Width / 2);
        int DeltaY = (int)(tilemap.tileSize / 2 - source.Height / 2);

        Rectangle dest = new Rectangle((int)inWorld.X + source.Width / 2 + DeltaX, (int)inWorld.Y + source.Height / 2 + DeltaY, source.Width, source.Height);
        Raylib.DrawTexturePro(texture, source, dest, new System.Numerics.Vector2(source.Width / 2, source.Height / 2), (float)Coordinates.GetAngle(coordinates, body.ElementAt(1)) + 90, Color.White);
    }

    public void DrawElements()
    {
        var bodyArray = body.ToArray();
        for (int i = 1; i < bodyArray.Length - 1; i++)
        {
            Coordinates coordinates = bodyArray[i];
            Texture2D texture = ((Textures)Services.Get<Textures>()).GetTexture("SnakeParts" + colorName);
            var inWorld = tilemap.MapToWorld(coordinates);
            Rectangle source = new Rectangle(0, 0, 84, 84);

            int DeltaX = (int)(tilemap.tileSize / 2 - source.Width / 2);
            int DeltaY = (int)(tilemap.tileSize / 2 - source.Height / 2);

            Rectangle dest = new Rectangle((int)inWorld.X + source.Width / 2 + DeltaX, (int)inWorld.Y + source.Height / 2 + DeltaY, source.Width, source.Height);
            Raylib.DrawTexturePro(texture, source, dest, new System.Numerics.Vector2(source.Width / 2, source.Height / 2), (float)Coordinates.GetAngle(body.ElementAt(i - 1), coordinates, body.ElementAt(i + 1)) + 180, Color.White);
        }
    }

    public void Draw()
    {
        DrawHead();
        DrawElements();
        DrawTail();
    }
    #endregion

    public void ResetSpeed()
    {
        CurrentSpeed = defaultSpeed;
    }

    public bool IsOutOfBound(Coordinates coordinates)
    {
        return coordinates.row<0 || coordinates.column<0 || coordinates.row>= tilemap.rows || coordinates.column>= tilemap.columns;
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

    public bool IsCollidingWall(Coordinates newHead)
    {
        return tilemap.IsSolid(newHead);
    }

    public void LoseTail(Coordinates cut)
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
        MoveType = EnumMoveType.none;
        currentDirection = nextDirection;
        var head = body.Last();
        var newHead = head + currentDirection;

        if (tilemap.IsSolid(newHead, "Holes"))
        {
            MoveType = EnumMoveType.Fall;
        }
        else if (!IsOutOfBound(newHead) && !IsCollidingWall(newHead))
        {
            body.Enqueue(newHead);
            if (growing == 0)
            {
                body.Dequeue();
            }
            else
            {
                growing--;
            }

            if (IsOverlapping())
            {
                MoveType = EnumMoveType.Overlap;
            }
        }
        else
        {
            MoveType = EnumMoveType.OutOfBound;    
        }
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


    private int GetHeadTextureID()
    {
        if (currentDirection == Coordinates.left) return 1;
        if (currentDirection == Coordinates.up) return 2;
        if (currentDirection == Coordinates.right) return 4;
        if (currentDirection == Coordinates.down) return 8;
        return 0;
    }
    private int GetTailTextureID()
    {
        int id = 0;
        if (tail + Coordinates.left == body.ElementAt(1)) return 20;
        if (tail + Coordinates.up == body.ElementAt(1)) return 21;
        if (tail + Coordinates.right == body.ElementAt(1)) return 22;
        if (tail + Coordinates.down == body.ElementAt(1)) return 23;
        return id;
    }

    private int GetBodyTextureID(Coordinates previous, Coordinates current, Coordinates next)
    {
        int id = 0;
        if (current + Coordinates.left == next || current + Coordinates.left == previous) id += 1;
        if (current + Coordinates.up == next || current + Coordinates.up == previous) id += 2;
        if (current + Coordinates.right == next || current + Coordinates.right == previous) id += 4;
        if (current + Coordinates.down == next || current + Coordinates.down == previous) id += 8;
        
        return id;
    }

}
