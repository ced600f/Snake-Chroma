using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;

enum GameState
{
    Playing,
    Paused,
    GameOver
}

public class SceneSnake : Scene
{
    Grid grid = new Grid(26,14);
    Snake snake;
    List<Fruit> fruits = new List<Fruit>();

    private GameState gameState = GameState.Playing;
    private Timer timerFruit;
    private Timer timerPause;
    private Timer timerSnake;
    public override void Load()
    {
        grid.position = new Vector2(128, 92);
        snake = new Snake(new(10,5),grid);
        OnFruitTriggered();
        Services.Get<SoundManager>().PlayMusic("Ressources/game.mp3");

        // Timers
        timerSnake = AddTimer(OnTimerTriggered, 0.4f);
        Timer timerColor = AddTimer(OnChangeColor, 10);
        timerColor.SetRandom(2, 15);

        timerFruit = AddTimer(OnFruitTriggered, 7);
        timerFruit.SetRandom(3, 10);

        timerPause = AddTimer(OnEndPauseTriggered, 2);
        timerPause.Stop();
    }

    public void OnEndPauseTriggered()
    {
        gameState = GameState.Playing;
        timerSnake.Start();
    }

    public void OnFruitTriggered()
    {
        Fruit fruit = Fruit.Random(grid);
        fruits.Add(fruit);
    }
    public void OnChangeColor()
    {
        snake.RandomColor();
    }
    public void OnTimerTriggered()
    {
        snake.Move();

        if (snake.IsOutOfBound())
        {
            Services.Get<SoundManager>().PlayFX("Collision");
            gameState = GameState.Paused;
            timerSnake.Stop();
            snake.Collision();
            timerPause.Restart();
        }

        if (snake.IsOverlapping())
        {
            Services.Get<SoundManager>().PlayFX("Collision");
            snake.LoseQueue(snake.head);
        }

        List <Fruit>lstTmp = fruits.ToList();
        foreach (var fruit in lstTmp)
        {
            if (snake.IsCollindingWith(fruit.coordinate))
            {
                EatFruit(fruit);
            }
        }

        if (snake.Length <= 2)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        ((ISceneManager)Services.Get<ISceneManager>())?.Load<SceneGameOver>();
        gameState = GameState.GameOver;
    }

    public override void Draw()
    {
        grid.Draw();
        foreach (var fruit in fruits)
        {
            fruit.Draw();
        }
        snake.Draw();
    }

    public override void Update()
    {
        base.Update();
        switch (gameState)
        {
            case GameState.Paused:
                UpdatePaused();
                break;
            case GameState.Playing:
                UpdatePlaying(); 
                break;
            case GameState.GameOver:
                break;
        }
    }

    private void UpdatePlaying()
    {
        snake.ChangeDirection(GetInputsDirection());
        if (Raylib.IsKeyDown(KeyboardKey.P))
        {
            Pause();
            gameState = GameState.Paused;
        }
    }
    private void UpdatePaused()
    {
        if (Raylib.IsKeyDown(KeyboardKey.P))
        {
            Start();
            gameState = GameState.Playing;
        }
    }

    private void EatFruit(Fruit fruit)
    {
        Services.Get<SoundManager>().PlayFX("Eating");
        for (int i = fruits.Count - 1; i >= 0; i--)
        {
            if (((Fruit)fruits[i]).isEaten)
            {
                fruits.RemoveAt(i);
            }
        }
        if (snake.SnakeColor == fruit.color)
        {
            snake.Growth();
        }
        else
        {
            snake.RemoveElements(1);
        }
        fruit.isEaten = true;
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
