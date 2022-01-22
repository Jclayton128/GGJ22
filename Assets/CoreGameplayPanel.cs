using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoreGameplayPanel : UI_Panel
{
    [SerializeField] TextMeshProUGUI optionATMP = null;
    [SerializeField] TextMeshProUGUI optionBTMP = null;

    #region New Card Loading

    public void DisplayNewCard(Card newCard)
    {

    }

    #endregion

    #region Button Press Handlers
    public void HandlePress_OptionA()
    {
        Debug.Log("option A was selected");
        //Procee Option A selection

        gcRef.SetNewState(GameController.State.Endgame);  // For testing.
    }

    public void HandlePress_OptionB()
    {
        Debug.Log("option B was selected");
        //Process Option B selection
    }

    #endregion
}
