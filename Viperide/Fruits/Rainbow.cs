using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Rainbow:Fruit
{
    public Rainbow(Grid grid):base(grid)
    {
        fruit = "Rainbow";
        Type = TypeFruit.Rainbow;
    }
}
