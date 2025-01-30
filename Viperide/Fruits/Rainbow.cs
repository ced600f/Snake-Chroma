using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Rainbow:Fruit
{
    public Rainbow(Tilemap grid):base(grid)
    {
        fruit = "Rainbow";
        Type = TypeFruit.Rainbow;
    }
}
