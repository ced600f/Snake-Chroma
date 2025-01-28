using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Lightning:Fruit
{
    public Lightning(Grid grid):base(grid)
    {
        fruit = "Yellow2";
        Type = TypeFruit.Lightning;
    }
}
