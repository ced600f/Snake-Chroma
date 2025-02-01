using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Rainbow:Fruit
{
    public Rainbow(Tilemap grid):base(grid)
    {
        fruit = "Rainbow";
        Type = TypeFruit.Rainbow;
    }
    public override int Eat(Snake snake, Timer? timerSnake = null, Timer? timerDuration = null)
    {
        int Score = base.Eat(snake, timerSnake, timerDuration);

        if (Score > 0)
        {
            if (!snake.Attributes.Contains("Rainbow"))
                snake.Attributes.Add("Rainbow");
        }
        Score = 0;
        return Score;
    }

}
