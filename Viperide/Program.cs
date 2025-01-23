﻿using Raylib_cs;

class Program
{
    static int Main()
    {
        int screenWidth = 1920;
        int screenHeight = 1080;

        Raylib.InitWindow(screenWidth, screenHeight, "Snake Chroma");
        //Raylib.ToggleFullscreen();
        Raylib.SetTargetFPS(60);

        SceneManager sceneManager = new SceneManager();
        Textures texturesManager = new Textures();
        SoundManager soundManager = new SoundManager();

        soundManager.PlayMusic("Ressources/menu.mp3");
        sceneManager.Load<SceneMenu>();


        while (!Raylib.WindowShouldClose())
        {
            sceneManager.Update();
            sceneManager.Draw();
        }

        Raylib.CloseWindow();

        // Pas d'erreur
        return 0;
    }
}