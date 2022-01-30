using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanel : UI_Panel
{
    AudioController ac;
   
    protected override void Start()
    {
        base.Start();
        ac = FindObjectOfType<AudioController>();
    }

    public void HandleSFXPress()
    {
        ac.ToggleSFX();
    }

    public void HandleMusicPress()
    {
        ac.ToggleMusic();
    }

    public void HandleBackButton()
    {
        gcRef.RequestReturnToPreviousState();
    }
}
