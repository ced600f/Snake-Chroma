using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

public class Grid
{
    #region Properties
    public Vector2 position = Vector2.Zero;
    public int columns { get; private set; }
    public int rows { get; private set; }
    public int cellSize { get; private set; }

    private Cell[,] grid;
    #endregion

    public Grid(int columns = 10, int rows = 10, int cellSize = 64)
    {
        this.columns = columns;
        this.rows = rows;
        this.cellSize = cellSize;

        grid = new Cell[columns, rows];
    }

    public void Draw()
    {
        for (int x=0; x<this.columns; x++)
        {
            for (int y = 0; y < this.rows; y++)
            {
                (grid[x,y])?.Draw();
            }
        }

        for(int column=0; column<=this.columns; column++)
        {
            Raylib.DrawLineV(GridToWorld(new(column, 0)), GridToWorld(new (column, rows)), Color.Gray);
        }
        for(int row=0; row<=this.rows; row++)
        {
            Raylib.DrawLineV(GridToWorld(new(0, row)), GridToWorld(new (columns, row)), Color.Gray);
        }
    }

    public void SetCell<T>(Coordinates coordinate) where T : Cell , new()
    {
        if (!IsInBound(coordinate)) return;
        grid[coordinate.column, coordinate.row] = new T() { coordinate = coordinate, grid = this };
    }

    public Cell? GetCell(Coordinates coordinate)
    {
        if (!IsInBound(coordinate)) return null;
        return grid[coordinate.column, coordinate.row];
    }


    #region Coordinates conversion
    public Coordinates WorldToGrid(Vector2 pos)
    {
        pos -= position;
        pos /= cellSize;
        return new Coordinates((int)pos.X, (int)pos.Y);
    }

    public Vector2 GridToWorld(Coordinates coordinate)
    {
        coordinate *= cellSize;
        return coordinate.ToVector2 + position;
    }
    #endregion

    #region Helper Methods
    private bool IsInBound(Coordinates coordinate)
    {
        return coordinate.column >=0 && coordinate.column < this.columns && coordinate.row >= 0 && coordinate.row < this.rows;
    }
    #endregion
}
