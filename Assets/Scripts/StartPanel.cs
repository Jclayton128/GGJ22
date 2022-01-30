using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPanel : UI_Panel
{
    protected override void Start()
    {
        base.Start();
        elements.Add(GameObject.FindGameObjectWithTag("ShipImage"));
    }
    public void HandleStartGamePress()
    {
        gcRef.SetNewState(GameController.State.Intro);
    }
}
