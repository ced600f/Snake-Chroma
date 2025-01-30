using Raylib_cs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class SoundManager
{
    Music music;
    Dictionary<string, Sound> sounds = new System.Collections.Generic.Dictionary<string, Sound>();

    public SoundManager()
    {
        Services.Register<SoundManager>(this);
        Raylib.InitAudioDevice();
        sounds.Add("Eating", Raylib.LoadSound("Ressources/eating.mp3"));
        sounds.Add("Collision", Raylib.LoadSound("Ressources/collision.mp3"));
        sounds.Add("Pain", Raylib.LoadSound("Ressources/pain.mp3"));
        sounds.Add("Disgusted", Raylib.LoadSound("Ressources/disgust.mp3"));
        sounds.Add("Cut", Raylib.LoadSound("Ressources/cut.mp3"));
    }

    ~SoundManager()
    {
        foreach (var sound in sounds.Values)
        {
            Raylib.UnloadSound(sound);
        }
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

    public void PlayFX(string filename)
    {
        if (sounds.ContainsKey(filename))
        {
            Raylib.PlaySound(sounds[filename]);
        }
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
