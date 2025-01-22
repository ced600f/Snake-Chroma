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

    }
}
