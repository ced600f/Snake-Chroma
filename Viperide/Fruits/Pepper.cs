using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Pepper:Fruit
{
    public Pepper(Grid grid):base(grid)
    {
        fruit = "Red2";
        Type = TypeFruit.Pepper;
    }
}
