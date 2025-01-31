using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Banana:Fruit
{
    public Banana(Tilemap grid):base(grid)
    {
        fruit = "Yellow";
        Type = TypeFruit.Banana;
        minDuration = 5;
    }

    public override int Eat(Snake snake, Timer? timerSnake = null, Timer? timerDuration = null)
    {
        int Score = base.Eat(snake, timerSnake, timerDuration);
        if (Score > 0)
        {
            snake.Growth(3 * (snake.Attributes.Contains("Scored") ? 2 : 1));
            snake.ResetSpeed();
            timerSnake?.SetDuration(snake.CurrentSpeed * 2);
            timerDuration?.SetDuration(minDuration);
            timerDuration?.Restart();
        }
        return Score;
    }
}
