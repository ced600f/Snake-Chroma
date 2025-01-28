using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class WaterDrop:Fruit
{
    public WaterDrop(Grid grid):base(grid)
    {
        fruit = "Blue2";
        Type = TypeFruit.WaterDrop;
    }
}
