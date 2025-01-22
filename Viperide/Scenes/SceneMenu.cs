using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SceneMenu : Scene
{
    Texture2D imgMenu = Raylib.LoadTexture("Images/menu2.png");


    public override void Load()
    {
            
    }

    public override void Draw()
    {
        Raylib.DrawTexture(imgMenu, 0,0,Color.White);     
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
