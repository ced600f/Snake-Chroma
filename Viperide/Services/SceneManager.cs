using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public interface ISceneManager
{
    public void Load<T>() where T : Scene, new();
}
public class SceneManager : ISceneManager
{
    private Scene? _currentScene;

    public SceneManager() 
    {
        Services.Register<ISceneManager>(this);
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
        Raylib.BeginDrawing();
        Raylib.ClearBackground(Color.Black);
        _currentScene?.Draw();
        Raylib.EndDrawing();
    }
}
