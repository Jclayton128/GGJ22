using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    [SerializeField] AudioSource[] auso_SFX = null;
    [SerializeField] AudioSource auso_Music = null;
    [SerializeField] Typewriter typewriter = null;

    //state
    bool isSFXmuted = false;
    bool isMusicMuted = false;

    void Start()
    {
        
    }
    
    public void ToggleSFX()
    {
        isSFXmuted = !isSFXmuted;
        if (isSFXmuted)
        {
            foreach (var auso in auso_SFX)
            {
                auso.volume = 0;
            }
            typewriter.SetVolume(0);

        }
        else
        {
            foreach (var auso in auso_SFX)
            {
                auso.volume = 1;
            }
            typewriter.SetVolume(1);
        }
    }

    public void ToggleMusic()
    {
        isMusicMuted = !isMusicMuted;
        if (isMusicMuted)
        {
            auso_Music.volume = 0;
        }
        else
        {
            auso_Music.volume = 1;
        }
    }

}
