using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class WaterDrop:Fruit
{
    public WaterDrop(Tilemap grid):base(grid)
    {
        fruit = "Blue2";
        Type = TypeFruit.WaterDrop;
    }

    public override int Eat(Snake snake, Timer? timerSnake = null, Timer? timerDuration = null)
    {
        int Score = base.Eat(snake, timerSnake, timerDuration);

        if (Score > 0)
        {
            if (!snake.Attributes.Contains("Crystal"))
                snake.Attributes.Add("Crystal");
        }
        Score = 0;
        return Score;
    }

}
