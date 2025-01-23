using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Fruit
{
    public Coordinates coordinate { get; private set; }
    Grid grid;
    protected string fruit;
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

}

