using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanel : UI_Panel
{

    public void HandleStartGamePress()
    {
        gcRef.SetNewState(GameController.State.CoreGameplay);
    }
}
