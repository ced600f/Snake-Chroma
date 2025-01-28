using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public enum TypeFruit
{
    Apple,
    Banana,
    BlueBerry,
    Plum,
    WaterMelon,
    Pepper,
    WaterDrop,
    Lightning,
    Orb,
    Emerald,
    Rainbow
}

public class Fruit
{
    #region constant
    public static readonly string Rainbow = "Rainbow";
    #endregion

    public Coordinates coordinate { get; private set; }
    public TypeFruit Type { get; init; }

    Grid grid;
    protected string fruit;
    public bool isEaten = false;
    
    public string color => fruit;

    public Fruit(Grid grid)
    {
        this.grid = grid;
        coordinate = Coordinates.Random(grid.columns, grid.rows);
    }

    public void Respawn()
    {
        coordinate = Coordinates.Random(grid.columns, grid.rows);
    }

    public virtual void Draw()
    {
        try
        {
            Texture2D texture = ((Textures)Services.Get<Textures>()).GetTexture(fruit);
            var inWorld = grid.GridToWorld(coordinate);
            int deltaX = (grid.cellSize - texture.Width) / 2;
            int deltaY = (grid.cellSize - texture.Height) / 2;
            Raylib.DrawTexture(texture, (int)inWorld.X + deltaX, (int)inWorld.Y + deltaY, Color.White);
        }
        catch (Exception ex) { }
    }

    public static Fruit Random(Grid grid)
    {
        Random random = new Random();
        int type = random.Next(0, 5);
        switch(type)
        {
            case 0:
                return new Apple(grid);
            case 1:
                return new BlueBerry(grid);
            case 2:
                return new Banana(grid);
            case 3:
                return new Plum(grid);
            default:
                return new WaterMelon(grid);
        }
    }

    public static Fruit RandomLevelUp(Grid grid)
    {
        Random random = new Random();
        int type = random.Next(0, 5);
        int rare = random.Next(0, 15);
        if (rare <= 12)
        {
            switch (type)
            {
                case 0:
                    return new Pepper(grid);
                case 1:
                    return new WaterDrop(grid);
                case 2:
                    return new Lightning(grid);
                case 3:
                    return new Orb(grid);
                default:
                    return new Emerald(grid);
            }
        }
        else
            return new Rainbow(grid);
    }

    public static bool IsColliding(List<Fruit> fruits, Fruit fruit) => fruits.Contains(fruit);

    public override bool Equals([NotNullWhen(true)] object? obj)
    => obj is Fruit other && coordinate == other.coordinate;

}

