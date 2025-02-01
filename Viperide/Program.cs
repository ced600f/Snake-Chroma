using Raylib_cs;
using static Raylib_cs.Raylib;

class Program
{
    static SceneManager sceneManager = new SceneManager();
    static AssetsManager assetsManager = new AssetsManager();

    static void LoadAssets()
    {


        assetsManager.AddTextureSet("Floor", new List<(int id, string name, string path)>
        {
            (0,"Floor","Images/floor.png")
        });
        assetsManager.AddTextureSet("Holes", new List<(int id, string name, string path)>
        {
            (-1,"Void","Images/Walls/void.png"),
            (0,"Hole","Images/hole.png")
        });

        assetsManager.AddTextureSet("Background", new List<(int id, string name, string path)>
        {
            (0, "Menu" ,"Images/menu2.png"),
            (1, "GameOver", "Images/gameover.png")
        });

        assetsManager.AddTextureSet("Snake", new List<(int id, string name, string path)>
        {
            (0, "SnakeHeadBlue","Images/SnakeHeadBlue.png"),
            (1, "SnakeHeadGreen","Images/SnakeHeadGreen.png"),
            (2, "SnakeHeadPurple","Images/SnakeHeadPurple.png"),
            (3, "SnakeHeadRed","Images/SnakeHeadRed.png"),
            (4 ,"SnakeHeadYellow","Images/SnakeHeadYellow.png"),
            (5, "SnakePartsBlue","Images/SnakePartsBlue.png"),
            (6, "SnakePartsGreen","Images/SnakePartsGreen.png"),
            (7, "SnakePartsPurple","Images/SnakePartsPurple.png"),
            (8, "SnakePartsRed","Images/SnakePartsRed.png"),
            (9, "SnakePartsYellow","Images/SnakePartsYellow.png")
        });

        assetsManager.AddTextureSet("Fruit", new List<(int id, string name, string path)>
        {
            (1, "Red", "Images/Red.png"),
            (2, "Yellow","Images/Yellow.png"),
            (3, "Blue","Images/Blue.png"),
            (4, "Purple", "Images/Purple.png"),
            (5, "Green","Images/Green.png"),
            (6, "Red2", "Images/Red2.png"),
            (7, "Yellow2", "Images/Yellow2.png"),
            (8, "Blue2", "Images/Blue2.png"),
            (9, "Purple2", "Images/Purple2.png"),
            (10, "Green2", "Images/Green2.png"),
            (11, "Rainbow", "Images/Rainbow.png")
        });

        assetsManager.AddTextureSet("Walls", new List<(int id, string name, string path)>
        {
            (-1,"Void","Images/Walls/void.png"),
            (0, "Wall_center","Images/Walls/wall.png"),
            (255, "Wall","Images/Walls/wall_MM.png"),
            (22, "Wall_extCornerBottomLeft","Images/Walls/wall_BG.png"),
            (23, "Wall_extCornerBottomLeft","Images/Walls/wall_BG.png"),
            (11, "Wall_extCornerBottomRight","Images/Walls/wall_BD.png"),
            (208, "Wall_extCornerTopLeft","Images/Walls/wall_HG.png"),
            (240, "Wall_extCornerTopLeft","Images/Walls/wall_HG.png"),
            (104, "Wall_extCornerTopRight","Images/Walls/wall_HD.png"),
            (223, "Wall_intCornerBottomLeft","Images/Walls/wall_ibg.png"),
            (127, "Wall_intCornerBottomRight","Images/Walls/wall_ibd.png"),
            (254, "Wall_intCornerTopLeft","Images/Walls/wall_ihg.png"),
            (251, "Wall_intCornerTopRight","Images/Walls/wall_ihd.png"),

            (8, "Wall_Right","Images/Walls/wall_D.png"),
            (16, "Wall_Left","Images/Walls/wall_G.png"),
            (2, "Wall_Bottom","Images/Walls/wall_B.png"),
            (64, "Wall_Up","Images/Walls/wall_H.png"),

            (31, "Wall_sideBottom","Images/Walls/wall_BM.png"),
            (63, "Wall_sideBottom","Images/Walls/wall_BM.png"),
            (159, "Wall_sideBottom","Images/Walls/wall_BM.png"),
            (214, "Wall_sideLeft","Images/Walls/wall_MG.png"),
            (215, "Wall_sideLeft","Images/Walls/wall_MG.png"),
            (246, "Wall_sideLeft","Images/Walls/wall_MG.png"),
            (107, "Wall_sideRight","Images/Walls/wall_MD.png"),
            (235, "Wall_sideRight","Images/Walls/wall_MD.png"),
            (111, "Wall_sideRight","Images/Walls/wall_MD.png"),
            (248, "Wall_sideTop","Images/Walls/wall_HM.png"),
            (249, "Wall_sideTop","Images/Walls/wall_HM.png"),
            (252, "Wall_sideTop","Images/Walls/wall_HM.png"),

        });
    }

    static int Main()
    {
        SetConfigFlags(ConfigFlags.ResizableWindow | ConfigFlags.VSyncHint);

        int screenWidth = 1280;
        int screenHeight = 1024;

        InitWindow(screenWidth, screenHeight, "Snake Chroma");
        //Raylib.ToggleFullscreen();
        SetTargetFPS(60);

        LoadAssets();

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