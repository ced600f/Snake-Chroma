using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Plum:Fruit
{
    public Plum(Grid grid):base(grid)
    {
        fruit = "Purple";
        Type = TypeFruit.Plum;
    }
}
