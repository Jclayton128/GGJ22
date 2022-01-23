using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreGameLooper : MonoBehaviour
{
    //references
    GameController gc;
    CardServer cs;
    UI_Controller uic;
    ParameterTracker pt;

    //state
    Card activeCard;

    void Start()
    {
        gc = FindObjectOfType<GameController>();
        gc.OnStartNewGame += SetupNewGame;

        cs = FindObjectOfType<CardServer>();
        uic = FindObjectOfType<UI_Controller>();
        pt = FindObjectOfType<ParameterTracker>();
    }


    private void SetupNewGame()
    {
        DrawNewCard();
    }


    #region Public Methods
    public void DrawNewCard()
    {
        string[] dummyKeywordsForTesting = { "TEST1", "TEST2" };
        activeCard = cs.GetRandomCard(0, dummyKeywordsForTesting);

        uic.UpdateCoreGameplayPanelWithCard(activeCard);
    }

    public void SelectOptionA()
    {
        // implement outcome of Option A
        ModifyParameters(activeCard.OptionA.ParameterChanges);
        //Outcome outcome = activeCard.OptionAOutcome;
        //pt.ModifyParameterLevel(outcome.Parameter, outcome.Magnitude);

        // record the keyword of Option A in case it is referenced by a future Card

        //Debug.Log($"{activeCard.OptionAOutcome.Parameter} changed by {activeCard.OptionAOutcome.Magnitude}");
        //Debug.Log(activeCard.OptionA.ResultText);
        uic.UpdateCoreGameplayPanelWithOutcome(activeCard.OptionA.ResultText);
        //DrawNewCard();
    }

    public void SelectOptionB()
    {
        // implement outcome of Option B
        ModifyParameters(activeCard.OptionB.ParameterChanges);
        //Outcome outcome = activeCard.OptionBOutcome;
        //pt.ModifyParameterLevel(outcome.Parameter, outcome.Magnitude);

        // record the keyword of Option B in case it is referenced by a future Card

        //Debug.Log($"{activeCard.OptionAOutcome.Parameter} changed by {activeCard.OptionAOutcome.Magnitude}");
        //Debug.Log(activeCard.OptionB.ResultText);
        uic.UpdateCoreGameplayPanelWithOutcome(activeCard.OptionB.ResultText);
        //DrawNewCard();
    }

    // Modify all parameter values. Allows an option to affect multiple parameters.
    private void ModifyParameters(int[] parameterChanges)
    {
        int n = Mathf.Min(System.Enum.GetValues(typeof(ParameterTracker.Parameter)).Length, parameterChanges.Length);
        for (int i = 0; i < n; i++)
        {
            pt.ModifyParameterLevel((ParameterTracker.Parameter)i, parameterChanges[i]);
        }
    }

    #endregion
}
