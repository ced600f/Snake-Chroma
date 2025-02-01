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
    public override int Eat(Snake snake, Timer? timerSnake = null, Timer? timerDuration = null)
    {
        int Score = base.Eat(snake, timerSnake, timerDuration);

        if (Score > 0)
        {
            if (!snake.Attributes.Contains("Shield"))
                snake.Attributes.Add("Shield");
        }
        Score = 0;
        return Score;
    }
}
