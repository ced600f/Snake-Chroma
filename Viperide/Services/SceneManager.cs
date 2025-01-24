using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Formats.Asn1.AsnWriter;
using static Raylib_cs.Raylib;

public interface ISceneManager
{
    public void Load<T>() where T : Scene, new();
}
public class SceneManager : ISceneManager
{
    public static readonly int width = 1920;
    public static readonly int height = 1080;
    public static float scale => Math.Min(GetScreenWidth() / (float)width, GetScreenHeight() / (float)height);

    static float offsetX = 1f;
    static float offsetY = 1f;

    private Scene? _currentScene;
    static RenderTexture2D canvas;

    public SceneManager() 
    {
        Services.Register<ISceneManager>(this);
        canvas = LoadRenderTexture(width, height);
        SetTextureFilter(canvas.Texture, TextureFilter.Point);
    }

    ~SceneManager()
    {
        Services.Remove<SceneManager>();
    }

    public void Load<T>() where T : Scene, new()
    {
        _currentScene?.Unload();
        _currentScene = new T();
        _currentScene.Load();
    }

    public void Update()
    { 
        _currentScene?.Update();
    }

    public void Draw()
    {
        float scaleW = width * scale;
        float scaleH = height * scale;

        offsetX = (GetScreenWidth() / scaleW) * 0.5f;
        offsetY = (GetScreenHeight() / scaleH) * 0.5f;

        BeginTextureMode(canvas);
        ClearBackground(Color.Black);
        _currentScene?.Draw();
        EndTextureMode();

        BeginDrawing();
        ClearBackground(Color.Gray);
        DrawTexturePro(canvas.Texture, new Rectangle(0, 0, width, -height), new Rectangle(offsetX, offsetY, scaleW, scaleH), new System.Numerics.Vector2(0, 0), 0, Color.White);
        EndDrawing();
    }
}
