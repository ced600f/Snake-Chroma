using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Plum:Fruit
{
    public Plum(Tilemap grid):base(grid)
    {
        fruit = "Purple";
        Type = TypeFruit.Plum;
        minDuration = 5;
    }

    public override int Eat(Snake snake, Timer? timerSnake = null, Timer? timerDuration = null)
    {
        int Score = base.Eat(snake, timerSnake, timerDuration);
        if (Score > 0)
        {
            snake.Growth(2);
            snake.ResetSpeed();
            timerSnake?.SetDuration(snake.CurrentSpeed * 0.5f);
            timerDuration?.SetDuration(minDuration);
            timerDuration?.Restart();
        }
        return Score;
    }
}
