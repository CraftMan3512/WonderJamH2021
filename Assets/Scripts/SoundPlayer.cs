using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;


public class SoundPlayer : MonoBehaviour
{

    public static SoundPlayer instance;
    
    private AudioSource source;

    [Range(0,1)]
    public float globalVolumeSet;
    float globalVolume; // global volume multiplier

    // singleton initializer
    private void Awake()
    {
        
        //singleton
        if (instance is null) instance = this;
        else Destroy(gameObject);

        source = GetComponent<AudioSource>();
        globalVolume = globalVolumeSet;
        
    }

    public static void PlaySFX(AudioClip sfx, float vol = 1f)
    {

        if (instance != null)
        {
            
            if (instance.source != null) instance.source.PlayOneShot(sfx,2*instance.globalVolume*vol);   
            
        }

    }

}
