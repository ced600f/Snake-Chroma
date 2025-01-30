using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Orb:Fruit
{
    public Orb(Tilemap grid):base(grid)
    {
        fruit = "Purple2";
        Type = TypeFruit.Orb;
    }
}
