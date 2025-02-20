﻿using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SceneGameOver : Scene
{
    public override void Load()
    {
        Services.Get<SoundManager>().PlayMusic("Ressources/gameover.mp3");
    }

    public override void Draw()
    {
        try
        {
            Texture2D imgMenu = assets.GetTextureByName("GameOver");
            Raylib.DrawTexture(imgMenu, 0, 0, Color.White);
        }
        catch { }
    }

    public override void Update()
    {
        base.Update();
        if (Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            StartMenu();
        }
    }

    private void StartMenu()
    {
        ((ISceneManager)Services.Get<ISceneManager>())?.Load<SceneMenu>();
    }
}
