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
    private IAssetsManager assets = Services.Get<IAssetsManager>();

    public Coordinates coordinate { get; private set; }
    public TypeFruit Type { get; init; }

    protected int minDuration = 0;
    protected int maxDuration = 0;

    Tilemap tilemap;
    protected string fruit;
    public bool isEaten = false;
    
    public string color => fruit;

    public Fruit(Tilemap tilemap)
    {
        this.tilemap = tilemap;
        Respawn();
    }

    public void Respawn()
    {
        coordinate = Coordinates.Random(tilemap.columns, tilemap.rows);
        // On teste si un fruit se superpose avec un autre fruit
        while (tilemap.IsSolid(coordinate))
        {
            Respawn();
        }
    }

    public virtual void Draw()
    {
        try
        {
            Texture2D texture = assets.GetTextureByName(fruit);
            var inWorld = tilemap.MapToWorld(coordinate);
            int deltaX = (tilemap.tileSize - texture.Width) / 2;
            int deltaY = (tilemap.tileSize - texture.Height) / 2;
            Raylib.DrawTexture(texture, (int)inWorld.X + deltaX, (int)inWorld.Y + deltaY, Color.White);
        }
        catch (Exception ex) { }
    }

    public static Fruit Random(Tilemap tilemap)
    {
        Random random = new Random();
        int type = random.Next(0, 5);
        switch(type)
        {
            case 0:
                return new Apple(tilemap);
            case 1:
                return new BlueBerry(tilemap);
            case 2:
                return new Banana(tilemap);
            case 3:
                return new Plum(tilemap);
            default:
                return new WaterMelon(tilemap);
        }
    }

    public static Fruit RandomLevelUp(Tilemap tilemap)
    {
        Random random = new Random();
        int type = random.Next(0, 5);
        int rare = random.Next(0, 15);
        if (rare <= 12)
        {
            switch (type)
            {
                case 0:
                    return new Pepper(tilemap);
                case 1:
                    return new WaterDrop(tilemap);
                case 2:
                    return new Lightning(tilemap);
                case 3:
                    return new Orb(tilemap);
                default:
                    return new Emerald(tilemap);
            }
        }
        else
            return new Rainbow(tilemap);
    }

    public virtual int Eat(Snake snake, Timer ?timerSnake=null, Timer ?timerDuration=null)
    {
        if (!isEaten)
        {
            Services.Get<SoundManager>().PlayFX("Eating");
            isEaten = true;
            if (color.Contains(snake.SnakeColor) || color == Fruit.Rainbow)
            {
                return 10;
            }
            else
            {
                Services.Get<SoundManager>().PlayFX("Disgusted");
                return -5;
            }
        }
        return 0;
    }

    public static bool IsColliding(List<Fruit> fruits, Fruit fruit) => fruits.Contains(fruit);

    public override bool Equals([NotNullWhen(true)] object? obj)
    => obj is Fruit other && coordinate == other.coordinate;

}

