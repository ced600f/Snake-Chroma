using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class Timer
{
    private float elapsedTime = 0;
    public float duration {  get; private set; }
    public bool isRunning { get; private set; }
    public bool isLooping;
    public bool isFinished => elapsedTime >= duration;
    public Action? Callback { private get; set; }
    public int min { get; private set; }
    public int max { get; private set; }
    public bool isRandom { get; private set; }

    public Timer(Action? callback, float duration, bool isLooping=true)
    {
        this.duration = duration;
        this.isLooping = isLooping;
        Callback = callback;
        elapsedTime = 0;
        isRunning = true;
    }

    public void Update(float dt)
    {
        if (!isRunning) return;

        elapsedTime += dt;
        if (elapsedTime > duration)
        {
            Callback?.Invoke();
            if (isLooping)
            {
                elapsedTime = 0;
                if (isRandom) 
                {
                    Random random = new Random();
                    duration = random.Next(min, max);
                }
            }
            else
                Stop();
        }
    }

    public void Stop()
    {
        isRunning = false;
    }
    public void Start()
    {
        isRunning = true;
    }

    public void Restart()
    {
        isRunning = true;
        elapsedTime = 0;
    }

    public void SetDuration(float duration)
    {
        this.duration = duration;
    }

    public void SetRandom(int min, int max)
    {
        this.min = min;
        this.max = max;
        this.isRandom = true;
    }
}

