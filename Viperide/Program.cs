using Raylib_cs;
using static Raylib_cs.Raylib;

class Program
{
    static SceneManager sceneManager = new SceneManager();

    static int Main()
    {
        SetConfigFlags(ConfigFlags.ResizableWindow | ConfigFlags.VSyncHint);

        int screenWidth = 1280;
        int screenHeight = 900;

        InitWindow(screenWidth, screenHeight, "Snake Chroma");
        //Raylib.ToggleFullscreen();
        SetTargetFPS(60);

        Textures texturesManager = new Textures();
        SoundManager soundManager = new SoundManager();

        sceneManager.Load<SceneMenu>();
        
        while (!WindowShouldClose())
        {
            sceneManager.Update();
            sceneManager.Draw();
        }

        CloseWindow();

        // Pas d'erreur
        return 0;
    }
}