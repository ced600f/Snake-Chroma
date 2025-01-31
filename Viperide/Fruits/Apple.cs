using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Apple:Fruit
{
    public Apple(Tilemap grid):base(grid)
    {
        fruit = "Red";
        Type = TypeFruit.Apple;
    }

    public override int Eat(Snake snake, Timer ?timerSnake = null, Timer ?timerDuration = null)
    {
        int Score = base.Eat(snake, timerSnake, timerDuration);

        if (Score > 0 ) snake.Growth(2*(snake.Attributes.Contains("Scored")?2:1));

        return Score;
    }

}
