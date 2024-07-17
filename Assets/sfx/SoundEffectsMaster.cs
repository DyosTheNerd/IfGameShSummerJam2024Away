using System.Collections.Generic;
using UnityEngine;

public class SoundEffectsMaster : MonoBehaviour
{
    private static SoundEffectsMaster _instance; 
    public static SoundEffectsMaster Instance{
        get{if(_instance == null){Debug.Log("Missing SoundEffectMaster");} return _instance;}
        private set{if(_instance != null){Debug.Log("Extra SoundEffectMaster in scene.");} _instance = value;}
    }
    
    private Dictionary<string, SFX> soundEffects = new Dictionary<string, SFX>();
    
    private void Awake()
    {
        Instance = this;
    }

    public static void stopSoundEffect(string soundEffect)
    {
        if(_instance != null)
            Instance._stopSoundEffect(soundEffect);
        
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
        if (_instance != null)
        {
            Instance._playSoundEffect(sound);
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
