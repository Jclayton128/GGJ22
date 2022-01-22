using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPanel : UI_Panel
{

    public void HandleSFXPress()
    {
        Debug.Log("SFX button was pressed");
    }

    public void HandleMusicPress()
    {
        Debug.Log("Music button was pressed");
    }

    public void HandleBackButton()
    {
        gcRef.RequestReturnToPreviousState();
    }
}
