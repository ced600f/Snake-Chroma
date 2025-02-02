using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Raylib_cs;

public abstract class Scene
{
    private List<Timer> timers = new List<Timer>();
    protected IAssetsManager assets = Services.Get<IAssetsManager>();

    public Timer AddTimer(Action? callback, float duration, bool isLooping = true)
    {
        Timer timer = new Timer(callback, duration, isLooping);
        timers.Add(timer);

        return timer;
    }

    public void RemoveTimer(Timer timer)
    {
        timers.Remove(timer);
    }

    public void UpdateTimers()
    {
        foreach (Timer timer in timers)
        {
            timer.Update(Raylib.GetFrameTime());
        }
    }
    public virtual void Load() { }
    public virtual void Update()
    {
        Services.Get<SoundManager>().Update();
        UpdateTimers();
    }

    public void Pause()
    {
        foreach(Timer timer in timers)
            timer.Stop();
    }

    public void Start()
    {
        foreach(Timer timer in timers)
            timer.Start();
    }

    public abstract void Draw();
    public virtual void Unload() { }
}
