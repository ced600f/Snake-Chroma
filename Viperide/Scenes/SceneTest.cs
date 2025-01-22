using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Text;
using System.Threading.Tasks;

public class SceneTest : Scene
{
    Grid grid = new Grid();

    public override void Load()
    {
        grid.position = new Vector2(100, 100);
        grid.SetCell<CellWhite>(new(5, 5));
    }
    public override void Draw()
    {
        grid.Draw();
    }

    public override void Update()
    {
            
    }
}