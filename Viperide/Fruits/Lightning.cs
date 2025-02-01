using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Lightning:Fruit
{
    public Lightning(Tilemap grid):base(grid)
    {
        fruit = "Yellow2";
        Type = TypeFruit.Lightning;
        minDuration = 10;
    }

    public override int Eat(Snake snake, Timer? timerSnake = null, Timer? timerDuration = null)
    {
        int score = base.Eat(snake, timerSnake, timerDuration);
        if (score > 0)
        {
            snake.Growth(6 * (snake.Attributes.Contains("Scored") ? 2 : 1));
            snake.ResetSpeed();
            timerSnake?.SetDuration(snake.CurrentSpeed * 2f);
            timerDuration?.SetDuration(minDuration);
            timerDuration?.Restart();
        }

        score = 0; // Pas de points sur un bonus
        return score;
    }
}
