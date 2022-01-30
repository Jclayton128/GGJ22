using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IntroPanel : UI_Panel
{
    public void HandleButtonPress()
    {
        gcRef.SetNewState(GameController.State.CoreGameplay);
    }
}
