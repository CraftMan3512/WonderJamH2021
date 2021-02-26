using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public enum Songs
{
    
    ADRENALINE,
    GameplaySong
    
}

public enum SoundEffects
{
    
    MenuButtonPress,
    JoinGame,
    
}


public class SoundPlayer : MonoBehaviour
{

    public static SoundPlayer instance;
    
    private AudioSource source;

    static Dictionary<Songs, AudioClip> songs;
    static Dictionary<SoundEffects, AudioClip> effects;
    
    [Range(0,1)]
    public float globalVolumeSet;
    float globalVolume; // global volume multiplier

    // singleton initializer
    private void Awake()
    {
        
        //singleton
        if (instance is null) instance = this;
        else Destroy(gameObject);

        InitializeSounds();

        source = GetComponent<AudioSource>();
        globalVolume = globalVolumeSet;
        
    }

    public void SetMusic(Songs song, bool startPlaying = true)
    {

        if (songs.ContainsKey(song))
        {

            if (source.isPlaying) {source.Stop();}
            source.clip = songs[song];
            if (startPlaying) {source.Play();}

        }

    }

    public void PlaySFX(SoundEffects sfx, float vol = 1f)
    {

        if (effects.ContainsKey(sfx))
        {
            
            source.PlayOneShot(effects[sfx],2*globalVolume*vol);   
            
        }

    }

    void InitializeSounds()
    {
        
        //put songs in this list
        songs = new Dictionary<Songs, AudioClip>()
        {
            {Songs.ADRENALINE, Resources.Load<AudioClip>("Sound/Music/ADRENALINE")},
            {Songs.GameplaySong, Resources.Load<AudioClip>("Music/Forest")},
            //...
        };

        //put sfx in this list
        effects = new Dictionary<SoundEffects, AudioClip>()
        {

            {SoundEffects.MenuButtonPress, Resources.Load<AudioClip>("Music/Forest")},
            {SoundEffects.JoinGame, Resources.Load<AudioClip>("Music/Forest")},
            //...

        };
        
    }
    
}
