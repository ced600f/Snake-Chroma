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

    #region timers
    private readonly float moveTimerDuration = 0.4f;
    private readonly float colorTimerDurationMin = 5;
    private readonly float colorTimerDurationMax = 15;
    private readonly float fruitTimerDurationMin = 3;
    private readonly float fruitTimerDurationMax = 10;
    private readonly float pauseTimerDuration = 2;
    private readonly float slowTimerDuration = 5;
    private readonly float levelUpTimerDurationMin = 15;
    private readonly float levelUpTimerDurationMax = 25;

    private Timer timerFruit;
    private Timer timerPause;
    private Timer timerSnake;
    private Timer timerDefaultDuration;
    private Timer timerLevelUp;
    private Timer timerColor;
    #endregion

    private int Score=0;

    private GameState gameState = GameState.Playing;
    public override void Load()
    {
        grid.position = new Vector2(128, 92);
        snake = new Snake(new(10,5),grid);
        OnFruitTriggered();
        Services.Get<SoundManager>().PlayMusic("Ressources/game.mp3");

        // Timers
        timerSnake = AddTimer(OnTimerTriggered, moveTimerDuration);

        timerColor = AddTimer(OnChangeColor, colorTimerDurationMin);
        timerColor.SetRandom((int)colorTimerDurationMin, (int)colorTimerDurationMax);

        timerFruit = AddTimer(OnFruitTriggered, fruitTimerDurationMin);
        timerFruit.SetRandom((int)fruitTimerDurationMin, (int)fruitTimerDurationMax);

        timerLevelUp = AddTimer(OnLevelUpTriggered, levelUpTimerDurationMin);
        timerLevelUp.SetRandom((int)levelUpTimerDurationMin, (int)levelUpTimerDurationMax);

        timerPause = AddTimer(OnEndPauseTriggered, pauseTimerDuration, false);
        timerPause.Stop();

        timerDefaultDuration = AddTimer(OnDefaultDurationTriggered, slowTimerDuration, false);
        timerDefaultDuration.Stop();
        Score = 0;

    }

    #region triggers
    public void OnDefaultDurationTriggered()
    {
        timerSnake.SetDuration(moveTimerDuration);
    }

    public void OnEndPauseTriggered()
    {
        gameState = GameState.Playing;
        timerSnake.Start();
    }

    public void OnFruitTriggered()
    {
        Fruit fruit = Fruit.Random(grid);
        // On teste si un fruit se superpose avec un autre fruit
        while (Fruit.IsColliding(fruits, fruit))
        {
            fruit = Fruit.Random(grid);
        }
        fruits.Add(fruit);
        timerFruit?.SetRandom((int)fruitTimerDurationMin, (int)fruitTimerDurationMax);
        timerFruit?.Restart();
    }

    public void OnLevelUpTriggered()
    {
        Fruit fruit = Fruit.RandomLevelUp(grid);
        // On teste si un fruit se superpose avec un autre fruit
        while (Fruit.IsColliding(fruits, fruit))
        {
            fruit = Fruit.RandomLevelUp(grid);
        }
        fruits.Add(fruit);
        timerLevelUp.SetRandom((int)levelUpTimerDurationMin, (int)levelUpTimerDurationMax);
        timerLevelUp.Restart();
    }
    public void OnChangeColor()
    {
        snake.RandomColor();
        timerColor.SetRandom((int)colorTimerDurationMin, (int)colorTimerDurationMax);
        timerColor.Restart();
    }
    public void OnTimerTriggered()
    {
        snake.Move();

        if (snake.IsOverlapping())
        {
            Services.Get<SoundManager>().PlayFX("Pain");
            snake.LoseQueue(snake.head);
            Score -= 5;
        }

        if (snake.IsOutOfBound())
        {
            Services.Get<SoundManager>().PlayFX("Collision");
            gameState = GameState.Paused;
            timerSnake.Stop();
            snake.Collision();
            timerPause.Restart();
            timerSnake.SetDuration(moveTimerDuration * 2);
            timerDefaultDuration.Restart();
            Score -= 5;
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
    #endregion

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
        Raylib.DrawText(Score.ToString(), 10, 10, 20, Color.White);
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
        for (int i = fruits.Count - 1; i >= 0; i--)
        {
            if (((Fruit)fruits[i]).isEaten)
            {
                fruits.RemoveAt(i);
            }
        }
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
        if (!fruit.isEaten)
        {
            Services.Get<SoundManager>().PlayFX("Eating");
            if (fruit.color.Contains(snake.SnakeColor) || fruit.color == Fruit.Rainbow)
            {
                switch(fruit.Type)
                {
                    case TypeFruit.Apple:
                        snake.Growth(2);
                        break;
                    case TypeFruit.Plum:
                        snake.Growth(2);
                        timerSnake.SetDuration(moveTimerDuration*0.5f);
                        timerDefaultDuration.SetDuration(slowTimerDuration);
                        timerDefaultDuration.Restart();
                        break;
                    case TypeFruit.Banana:
                        snake.Growth(3);
                        timerSnake.SetDuration(moveTimerDuration * 2);
                        timerDefaultDuration.SetDuration(slowTimerDuration);
                        timerDefaultDuration.Restart();
                        break;
                    case TypeFruit.WaterMelon:
                        break;
                    case TypeFruit.BlueBerry:
                        snake.Growth();
                        timerSnake.SetDuration(moveTimerDuration * 2);
                        timerDefaultDuration.SetDuration(slowTimerDuration-2);
                        timerDefaultDuration.Restart();
                        break;
                    default:
                        snake.Growth();
                        break;
                }
                Score += 10;
            }
            else
            {
                Services.Get<SoundManager>().PlayFX("Disgusted");
                //snake.RemoveElements(1);
                Score -= 5;
            }
            fruit.isEaten = true;
        }
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
