using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class EndgamePanel : UI_Panel
{
    [SerializeField] TextMeshProUGUI mainTMP = null;
    [SerializeField] TextMeshProUGUI buttonTMP = null;

    //settings
    string next = "Next Page";
    string accept = "Accept Outcome";

    private enum Substate { Page0, Page1, Page2}  // 3 total pages to cover outcome seem good?
    string[] outcomePages;

    //state
    Substate currentSubstate;


    #region Button Handlers
    public void HandleButtonPress()
    {
        switch (currentSubstate)
        {
            case Substate.Page0:
                currentSubstate++;
                SetSubstate(currentSubstate);
                break;

            case Substate.Page1:
                currentSubstate++;
                SetSubstate(currentSubstate);
                break;

            case Substate.Page2:
                gcRef.SetNewState(GameController.State.Start);
                break;

        }

    }

    #endregion;

    #region Public Methods
    public void UpdateUIWithOutcome(string[] outcomePages)
    {
        this.outcomePages = outcomePages;
        SetSubstate(Substate.Page0);
    }

    #endregion
    private void SetSubstate(Substate newSubstate)
    {
        currentSubstate = newSubstate;
        switch (currentSubstate)
        {
            case Substate.Page0:
                mainTMP.text = outcomePages[0];
                buttonTMP.text = next;
                break;

            case Substate.Page1:
                mainTMP.text = outcomePages[1];
                buttonTMP.text = next;
                break;

            case Substate.Page2:
                mainTMP.text = outcomePages[2];
                buttonTMP.text = accept;
                break;
        }
    }
}
