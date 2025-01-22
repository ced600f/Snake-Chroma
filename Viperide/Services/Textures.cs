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

        Texture2D SnakeHeadBlue = Raylib.LoadTexture("Images/SnakeHeadBlue");
        Texture2D SnakeHeadGreen = Raylib.LoadTexture("Images/SnakeHeadGreen");
        Texture2D SnakeHeadPurple = Raylib.LoadTexture("Images/SnakeHeadPurple");
        Texture2D SnakeHeadRed = Raylib.LoadTexture("Images/SnakeHeadRed");
        Texture2D SnakeHeadYellow = Raylib.LoadTexture("Images/SnakeHeadYellow");
        Texture2D SnakePartsBlue = Raylib.LoadTexture("Images/SnakePartsBlue");
        Texture2D SnakePartsGreen = Raylib.LoadTexture("Images/SnakePartsGreen");
        Texture2D SnakePartsPurple = Raylib.LoadTexture("Images/SnakePartsPurple");
        Texture2D SnakePartsRed = Raylib.LoadTexture("Images/SnakePartsRed");
        Texture2D SnakePartsYellow = Raylib.LoadTexture("Images/SnakePartsYellow");

        textures.Add("Red", Apple);
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
    }

    public Texture2D GetTexture(String Name) => textures[Name];

}
