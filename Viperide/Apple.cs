using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Apple
{
    public Coordinates coordinate { get; private set; }
    Grid grid;

    public Apple(Grid grid)
    {
        this.grid = grid;
        coordinate = Coordinates.Random(grid.columns, grid.rows);
    }

    public void Respawn()
    {
        coordinate = Coordinates.Random(grid.columns, grid.rows);
    }
    public void Draw()
    {
        var inWorld = grid.GridToWorld(coordinate);
        Raylib.DrawCircle((int)inWorld.X+grid.cellSize/2, (int)inWorld.Y+grid.cellSize/2, grid.cellSize/2, Color.Red);
    }
}
