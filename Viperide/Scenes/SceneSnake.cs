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
    //Grid grid = new Grid(26,14);
    Snake snake;
    List<Fruit> fruits = new List<Fruit>();
    Tilemap tilemap = new Tilemap(30, 17, 64);
    #region timers
    private readonly float colorTimerDurationMin = 5;
    private readonly float colorTimerDurationMax = 15;
    private readonly float fruitTimerDurationMin = 3;
    private readonly float fruitTimerDurationMax = 10;
    private readonly float pauseTimerDuration = 2;
    private readonly float levelUpTimerDurationMin = 15;
    private readonly float levelUpTimerDurationMax = 25;
    private readonly int loseTimerDuration = 30;
    private readonly int holeTimerDuration = 10;
    private readonly int freezeTimerDuration = 10;
    private readonly int scoredTimerDuration = 10;
    private readonly int shieldTimerDuration = 20;
    private readonly int rainbowTimerDuration = 10;

    private Timer timerFruit;
    private Timer timerPause;
    private Timer timerSnake;
    private Timer timerDefaultDuration;
    private Timer timerLevelUp;
    private Timer timerColor;
    private Timer timerLoseSegment;
    private Timer timerHole;
    private Timer timerFreeze;
    private Timer timerScored;
    private Timer timerShield;
    private Timer timerRainbow;
    #endregion

    private int Score=0;

    private GameState gameState = GameState.Playing;
    public override void Load()
    {
        tilemap.AddLayer("Floor", "Floor", Color.SkyBlue);
        tilemap.AddLayer("Walls", "Walls", Color.DarkBlue);
        LevelLoader.Load(tilemap, "Walls", 1);
        tilemap.AutoTiling("Walls", Coordinates.mooreNeightborhood);
        tilemap.AddLayer("Holes", "Holes", Color.White);
        LevelLoader.Load(tilemap, "Holes", 11);
        tilemap.AutoTiling("Holes", Coordinates.mooreNeightborhood);
//        tilemap.CleanMapSolid("Holes");

        snake = new Snake(new(10,5),tilemap);
        OnFruitTriggered();
        Services.Get<SoundManager>().PlayMusic("Ressources/game.mp3");

        // Timers
        timerSnake = AddTimer(OnTimerTriggered, snake.CurrentSpeed);

        timerColor = AddTimer(OnChangeColor, colorTimerDurationMin);
        timerColor.SetRandom((int)colorTimerDurationMin, (int)colorTimerDurationMax);

        timerFruit = AddTimer(OnFruitTriggered, fruitTimerDurationMin);
        timerFruit.SetRandom((int)fruitTimerDurationMin, (int)fruitTimerDurationMax);

        timerLevelUp = AddTimer(OnLevelUpTriggered, levelUpTimerDurationMin);
        timerLevelUp.SetRandom((int)levelUpTimerDurationMin, (int)levelUpTimerDurationMax);

        timerPause = AddTimer(OnEndPauseTriggered, pauseTimerDuration, false);
        timerPause.Stop();

        timerShield = AddTimer(OnShieldTriggered, shieldTimerDuration, false);
        timerShield.Stop();

        timerLoseSegment = AddTimer(OnLoseSegmentTriggered, loseTimerDuration, false);
        timerLoseSegment.Stop();

        timerFreeze = AddTimer(OnFreezeTriggered, freezeTimerDuration, false);
        timerFreeze.Stop();

        timerScored = AddTimer(OnScoredTriggered, scoredTimerDuration, false);
        timerScored.Stop();

        timerRainbow = AddTimer(OnRainbowTriggered, rainbowTimerDuration, false);
        timerRainbow.Stop();

        timerDefaultDuration = AddTimer(OnDefaultDurationTriggered, 0, false);
        timerDefaultDuration.Stop();
        Score = 0;

        timerHole = AddTimer(OnHoleTriggered, holeTimerDuration, true);

    }

    #region triggers
    public void OnRainbowTriggered()
    {
        timerShield.Stop();
        snake.Attributes.Remove("RainbowON");
    }
    public void OnShieldTriggered()
    {
        timerShield.Stop();
        snake.Attributes.Remove("ShieldON");
    }
    public void OnFreezeTriggered()
    {
        timerFreeze.Stop();
        timerLoseSegment.Start();
    }
    public void OnScoredTriggered()
    {
        timerScored.Stop();
        snake.Attributes.Remove("Scored");
    }

    public void OnHoleTriggered()
    {
        Coordinates coordinate = Coordinates.Random(tilemap.columns, tilemap.rows);
        // On teste si un fruit se superpose avec un autre fruit
        while (tilemap.IsSolid(coordinate))
        {
            coordinate = Coordinates.Random(tilemap.columns, tilemap.rows);
        }
        tilemap.SetTile(coordinate, "Holes", true);
    }
    public void OnLoseSegmentTriggered()
    {
        Services.Get<SoundManager>().PlayFX("Cut");
        snake.RemoveElements(1);
        timerLoseSegment.SetDuration(loseTimerDuration - snake.LoseDurationDelta);
        timerLoseSegment.Restart();
    }

    public void OnDefaultDurationTriggered()
    {
        timerSnake.SetDuration(snake.CurrentSpeed);
    }

    public void OnEndPauseTriggered()
    {
        gameState = GameState.Playing;
        timerSnake.Start();
    }

    public void OnFruitTriggered()
    {
        Fruit fruit = Fruit.Random(tilemap);
        // On teste si un fruit se superpose avec un autre fruit
        while (Fruit.IsColliding(fruits, fruit))
        {
            fruit = Fruit.Random(tilemap);
        }
        fruits.Add(fruit);
        timerFruit?.SetRandom((int)fruitTimerDurationMin, (int)fruitTimerDurationMax);
        timerFruit?.Restart();
    }

    public void OnLevelUpTriggered()
    {
        Fruit fruit = Fruit.RandomLevelUp(tilemap);
        // On teste si un fruit se superpose avec un autre fruit
        while (Fruit.IsColliding(fruits, fruit))
        {
            fruit = Fruit.RandomLevelUp(tilemap);
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

        if (snake.MoveType == EnumMoveType.Fall)
        {
            if (snake.Attributes.Contains("ShieldON"))
            {
                tilemap.SetTile(snake.head, "Holes", false);
            }
            else
            {
                GameOver();
            }
        }

        if (snake.IsOverlapping())
        {
            Services.Get<SoundManager>().PlayFX("Pain");
            snake.LoseTail(snake.head);
            Score -= 5;
        }

        if (snake.MoveType == EnumMoveType.OutOfBound)
        {
            Services.Get<SoundManager>().PlayFX("Collision");
            gameState = GameState.Paused;
            if (snake.Length > 3)
                snake.RemoveElements(1);
            timerSnake.Stop();
            timerPause.Restart();
            snake.ResetSpeed();
            timerSnake.SetDuration(snake.CurrentSpeed * 2);
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
        tilemap.Draw();
        foreach (var fruit in fruits)
        {
            fruit.Draw();
        }
        snake.Draw();
        Raylib.DrawText("Score : "+Score, 10, 10, 30, Color.White);
        Color textColor = Color.Blue;
        if (timerLoseSegment.RemainingTime <= 5) textColor = Color.Red;
        Raylib.DrawText(timerLoseSegment.RemainingTime.ToString(), 10, 50, 30, textColor);

        // Display snake's bonus
        if (timerFreeze.isRunning)
        {
            Raylib.DrawText(timerFreeze.RemainingTime.ToString(), 10, 80, 30, Color.White);
        }
        if (snake.Attributes.Contains("Scored"))
        {
            textColor = Color.Red;
            Raylib.DrawText("X2", 100, 10, 30, textColor);
        }
        if (snake.Attributes.Contains("ShieldON"))
        {
            Texture2D texture = assets.GetTextureByName("Purple2");
            Raylib.DrawTexture(texture, 180, 0, Color.White);
        }
        
        if (snake.Attributes.Contains("RainbowON"))
        {
            Texture2D texture = assets.GetTextureByName("Rainbow");
            Raylib.DrawTexture(texture, 250, 0, Color.White);
        }
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
        int value = fruit.Eat(snake, timerSnake, timerDefaultDuration);
        /*if (!timerFreeze.isRunning)
        {
            timerLoseSegment.SetDuration(loseTimerDuration - snake.LoseDurationDelta);
            timerLoseSegment.Restart();
        }*/
        Score += value;

        // Applying Attributes
        if (snake.Attributes.Contains("Freeze"))
        {
            snake.Attributes.Remove("Freeze");
            timerLoseSegment.Stop();
            timerFreeze.SetDuration(freezeTimerDuration);
            timerFreeze.Restart();
        }
        if (snake.Attributes.Contains("Crystal"))
        {
            snake.Attributes.Remove("Crystal");
            timerLoseSegment.Stop();
            timerFreeze.SetDuration(freezeTimerDuration+5);
            timerFreeze.Restart();
        }
        if (snake.Attributes.Contains("Score"))
        {
            snake.Attributes.Remove("Score");
            snake.Attributes.Add("Scored");
            timerScored.Restart();
        }
        if (snake.Attributes.Contains("Shield"))
        {
            snake.Attributes.Remove("Shield");
            snake.Attributes.Add("ShieldON");
            timerShield.Restart();
        }
        if (snake.Attributes.Contains("Rainbow"))
        {
            snake.Attributes.Remove("Rainbow");
            snake.Attributes.Add("RainbowON");
            timerRainbow.Restart();
        }
        if (snake.Attributes.Contains("Scored"))
        {
            Score += value;
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
