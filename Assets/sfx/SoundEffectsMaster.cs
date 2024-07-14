using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsMaster : MonoBehaviour
{
    public static SoundEffectsMaster instance;

    private static bool instanceExists = false;
    
    private Dictionary<string, SFX> soundEffects = new Dictionary<string, SFX>();
    
    private void Awake()
    {
        instance = this;
        instanceExists = true;
    }

    public static void stopSoundEffect(string soundEffect)
    {
        if (instanceExists)
        {
            instance._stopSoundEffect(soundEffect);
        }
    }
    
    private void _stopSoundEffect(string soundEffect)
    {
        if(soundEffects.ContainsKey(soundEffect))
        {
            soundEffects[soundEffect].stopSoundEffect();
        }
    }

    public static  void playSoundEffect(string sound)
    {
        if (instanceExists)
        {
            instance._playSoundEffect(sound);
        }
    }
    
private void _playSoundEffect(string soundEffect)
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
