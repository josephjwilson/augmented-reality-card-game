using UnityEngine.Audio;
using System;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioMixer audioMixer_Game;

    public Sound[] sounds;

    //public static AudioManager instance;

    // Start is called before the first frame update
    void Awake()
    {
        //Checks if I wanted to add audio controller for all scene
        //It allows music transfer between scenes

        /*
        if (instance == null)
            instance = this;
        else
        {
            Destroy(gameObject);
            return;
        }

        DontDestroyOnLoad(gameObject);

        */

        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.outputAudioMixerGroup = s.output;

            s.source.loop = s.loop;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.spatialBlend = s.spatialBlend;
        }
    }

    void Start()
    {
        //Play main theme
        Play("BGM_Main");
    }

    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if(s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }

    public void Stop(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Stop();
    }

    public void SetMasterVolume(float volume)
    {
        audioMixer_Game.SetFloat("volumeMaster", volume);
    }
    
    public void SetBGMVolume(float volume)
    {
        audioMixer_Game.SetFloat("volumeBGM", volume);
    }

    public void SetSFXVolume(float volume)
    {
        audioMixer_Game.SetFloat("volumeSFX", volume);
    }

}
