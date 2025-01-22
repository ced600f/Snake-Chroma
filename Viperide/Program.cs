using Raylib_cs;
using Viperide.Scenes;

class Program
{
    static int Main()
    {
        //int screenWidth = 1920;
        //int screenHeight = 1080;
        int screenWidth = 1920;
        int screenHeight = 1080;

        Raylib.InitWindow(screenWidth, screenHeight, "Viperide");
        Raylib.SetTargetFPS(60);

        SceneManager sceneManager = new SceneManager();

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