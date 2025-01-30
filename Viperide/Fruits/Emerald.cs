using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Emerald:Fruit
{
    public Emerald(Tilemap grid):base(grid)
    {
        fruit = "Green2";
        Type = TypeFruit.Emerald;
    }
}
