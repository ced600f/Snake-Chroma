﻿using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SceneMenu : Scene
{

    public override void Load()
    {
            
    }

    public override void Draw()
    {
        try
        {
            Texture2D imgMenu = ((Textures)Services.Get<Textures>()).GetTexture("Menu");
            Raylib.DrawTexture(imgMenu, 0, 0, Color.White);
        }
        catch { }
    }

    public override void Update()
    {
        if (Raylib.IsKeyPressed(KeyboardKey.Space))
        {
            StartGame();
        }
    }

    private void StartGame()
    {
        ((ISceneManager)Services.Get<ISceneManager>())?.Load<SceneSnake>();
    }
}
