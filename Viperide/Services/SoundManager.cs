using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SoundManager
{
    Music music;

    public SoundManager()
    {
        Services.Register<SoundManager>(this);
        Raylib.InitAudioDevice();
    }

    ~SoundManager()
    {
        CloseDevice();
    }

    public void StopMusic()
    {
        Raylib.StopMusicStream(music);
        Raylib.UnloadMusicStream(music);
    }

    public void Update()
    {
        Raylib.UpdateMusicStream(music);    
    }

    public void PlayMusic(string music)
    {
        this.music = Raylib.LoadMusicStream(music);
        Raylib.SetMusicVolume(this.music, 0.2f);
        Raylib.StopMusicStream(this.music);
        Raylib.PlayMusicStream(this.music);
    }

    public void CloseDevice()
    { 
        Raylib.CloseAudioDevice();
    }
}
