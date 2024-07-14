using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX : MonoBehaviour
{
    public string name; 
    
    private AudioSource audioSource;
    
    public bool multiple = false;
    
    // Start is called before the first frame update
    void Start()
    {
        SoundEffectsMaster.instance.registerSoundEffect(this);   
        audioSource = GetComponent<AudioSource>();
    }

    public void stopSoundEffect()
    {
        if (audioSource.isPlaying)
        {
            audioSource.Stop();
        }
    }
    
    public void playSoundEffect()
    {
        if (audioSource.isPlaying && !multiple)
        {
            return;
        }
        audioSource.Play();
    }
}
