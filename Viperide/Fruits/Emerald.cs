using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Emerald:Fruit
{
    public Emerald(Tilemap grid):base(grid)
    {
        fruit = "Green2";
        Type = TypeFruit.Emerald;
    }

    public override int Eat(Snake snake, Timer? timerSnake = null, Timer? timerDuration = null)
    {
        int Score = base.Eat(snake, timerSnake, timerDuration);

        if (Score > 0 )
        {
            if (!snake.Attributes.Contains("Score"))
                snake.Attributes.Add("Score");
        }

        Score = 0;
        return Score;
    }
}
