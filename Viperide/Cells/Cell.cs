using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public abstract class Cell
{
    public Coordinates coordinate { get; init; }

    public Grid? grid { get; init; }
    public bool isWalkable = true;

    public Cell()
    {
        grid = null;
    }

    public Cell(Coordinates coordinate, Grid grid)
    {
        this.coordinate = coordinate;
        this.grid = grid;
    }

    public void Draw()
    {
        var celPosInWorld = grid.GridToWorld(coordinate);
        Color color = isWalkable ? Color.White : Color.Red;
        Raylib.DrawRectangle((int)celPosInWorld.X, (int)celPosInWorld.Y, grid.cellSize, grid.cellSize, color);
    }
}
