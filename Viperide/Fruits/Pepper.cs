using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Pepper:Fruit
{
    public Pepper(Tilemap grid):base(grid)
    {
        fruit = "Red2";
        Type = TypeFruit.Pepper;
        minDuration = 5;
    }

    public override int Eat(Snake snake, Timer? timerSnake = null, Timer? timerDuration = null)
    {
        int score = base.Eat(snake, timerSnake, timerDuration);
        if (score > 0)
        {
            snake.Growth(4);
            snake.ResetSpeed();
            timerSnake?.SetDuration(snake.CurrentSpeed * 0.15f);
            timerDuration?.SetDuration(minDuration);
            timerDuration?.Restart();
        }

        score = 0; // Pas de points sur un bonus
        return score;
    }
}
