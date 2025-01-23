using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

public class SceneSnake : Scene
{
    Grid grid = new Grid(26,14);
    Snake snake;
    Fruit fruit;

    float timer = 0;
    float interval = 0.5f;
    public override void Load()
    {
        grid.position = new Vector2(128, 92);
        snake = new Snake(new(10,5),grid);
        fruit = Fruit.Random(grid);

        Services.Get<SoundManager>().PlayMusic("Ressources/game.mp3");

    }

    public override void Draw()
    {
        grid.Draw();
        fruit.Draw();
        snake.Draw();
    }

    public override void Update()
    {
        snake.ChangeDirection(GetInputsDirection());
        timer += Raylib.GetFrameTime();
        
        if (timer > interval)
        {
            snake.Move();
            if (snake.head == fruit.coordinate)
            {
                EatFruit();
            }
            timer = 0;
        }    
    }

    private void EatFruit()
    {
        fruit = Fruit.Random(grid);
        snake.Growth();
    }

    private Coordinates GetInputsDirection()
    {
        var direction = Coordinates.zero;

        if (Raylib.IsKeyDown(KeyboardKey.Left))
        {
            direction = Coordinates.left;
        }
        if (Raylib.IsKeyDown(KeyboardKey.Right))
        {
            direction = Coordinates.right;
        }
        if (Raylib.IsKeyDown(KeyboardKey.Up))
        {
            direction = Coordinates.up;
        }
        if (Raylib.IsKeyDown(KeyboardKey.Down))
        {
            direction = Coordinates.down;
        }
        return direction;
    }
}
