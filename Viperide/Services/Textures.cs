using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;
using System.Collections;

public class Textures
{
    Dictionary <String,Texture2D>textures = new Dictionary <String,Texture2D> ();

    public Textures()
    {
        Services.Register<Textures>(this);
        LoadTextures();
    }

    public void LoadTextures()
    {
        Texture2D Apple = Raylib.LoadTexture("Images/Red.png");
        Texture2D Banana = Raylib.LoadTexture("Images/Yellow.png");
        Texture2D BlueBerry = Raylib.LoadTexture("Images/Blue.png");
        Texture2D Plum = Raylib.LoadTexture("Images/Purple.png");
        Texture2D WaterMelon = Raylib.LoadTexture("Images/Green.png");

        Texture2D SnakeHeadBlue = Raylib.LoadTexture("Images/SnakeHeadBlue.png");
        Texture2D SnakeHeadGreen = Raylib.LoadTexture("Images/SnakeHeadGreen.png");
        Texture2D SnakeHeadPurple = Raylib.LoadTexture("Images/SnakeHeadPurple.png");
        Texture2D SnakeHeadRed = Raylib.LoadTexture("Images/SnakeHeadRed.png");
        Texture2D SnakeHeadYellow = Raylib.LoadTexture("Images/SnakeHeadYellow.png");
        Texture2D SnakePartsBlue = Raylib.LoadTexture("Images/SnakePartsBlue.png");
        Texture2D SnakePartsGreen = Raylib.LoadTexture("Images/SnakePartsGreen.png");
        Texture2D SnakePartsPurple = Raylib.LoadTexture("Images/SnakePartsPurple.png");
        Texture2D SnakePartsRed = Raylib.LoadTexture("Images/SnakePartsRed.png");
        Texture2D SnakePartsYellow = Raylib.LoadTexture("Images/SnakePartsYellow.png");

        textures.Add("Red", Apple);
        textures.Add("Yellow", Banana);
        textures.Add("Blue", BlueBerry);
        textures.Add("Purple", Plum);
        textures.Add("Green", WaterMelon);
        textures.Add("SnakeHeadBlue", SnakeHeadBlue);
        textures.Add("SnakeHeadGreen", SnakeHeadGreen);
        textures.Add("SnakeHeadPurple", SnakeHeadPurple);
        textures.Add("SnakeHeadRed", SnakeHeadRed);
        textures.Add("SnakeHeadYellow", SnakeHeadYellow);
        textures.Add("SnakePartsBlue", SnakePartsBlue);
        textures.Add("SnakePartsGreen", SnakePartsGreen);
        textures.Add("SnakePartsPurple", SnakePartsPurple);
        textures.Add("SnakePartsRed", SnakePartsRed);
        textures.Add("SnakePartsYellow", SnakePartsYellow);

        Texture2D imgMenu = Raylib.LoadTexture("Images/menu2.png");
        textures.Add("Menu", imgMenu);
        Texture2D imgGameOver = Raylib.LoadTexture("Images/gameover.png");
        textures.Add("GameOver", imgGameOver);
    }

    public Texture2D GetTexture(String Name) => textures[Name];

}
