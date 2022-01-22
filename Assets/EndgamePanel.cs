using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndgamePanel : UI_Panel
{

    public void HandleAcceptOutcomePress()
    {
        gcRef.SetNewState(GameController.State.Start);
    }
}
