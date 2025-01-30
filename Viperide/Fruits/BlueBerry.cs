﻿using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class BlueBerry:Fruit
{
    public BlueBerry(Tilemap grid):base(grid)
    {
        fruit = "Blue";
        Type = TypeFruit.BlueBerry;
        minDuration = 3;
    }

    public override int Eat(Snake snake, Timer? timerSnake = null, Timer? timerDuration = null)
    {
        int Score = base.Eat(snake, timerSnake, timerDuration);
        if (Score > 0)
        {
            snake.Growth();
            snake.ResetSpeed();
            timerSnake?.SetDuration(snake.CurrentSpeed * 2);
            timerDuration?.SetDuration(minDuration);
            timerDuration?.Restart();
        }
        return Score;
    }
}
