using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsMaster : MonoBehaviour
{
    public static SoundEffectsMaster instance;

    private Dictionary<string, SFX> soundEffects = new Dictionary<string, SFX>();
    
    private void Awake()
    {
        instance = this;
    }

public void playSoundEffect(string soundEffect)
    {
        if(soundEffects.ContainsKey(soundEffect))
        {
            soundEffects[soundEffect].playSoundEffect();
        }
    }
    public void registerSoundEffect( SFX target)
    {
        soundEffects[target.name] = target;
    }
}
