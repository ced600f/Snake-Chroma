using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class WaterMelon:Fruit
{
    public WaterMelon(Tilemap grid):base(grid)
    {
        fruit = "Green";
        Type = TypeFruit.WaterMelon;
    }

    public override int Eat(Snake snake, Timer? timerSnake = null, Timer? timerDuration = null)
    {
        int Score = base.Eat(snake, timerSnake, timerDuration);

        if (Score > 0)
        {
            if (!snake.Attributes.Contains("Freeze"))
                snake.Attributes.Add("Freeze");
        }
        return Score;
    }
}
