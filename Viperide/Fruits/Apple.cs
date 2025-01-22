using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Apple:Fruit
{
    public Apple(Grid grid):base(grid)
    {
        fruit = "Red";
    }
}
