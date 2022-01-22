using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoreGameLooper : MonoBehaviour
{
    //references
    GameController gc;
    CardServer cs;
    UI_Controller uic;

    //state
    Card activeCard;

    void Start()
    {
        gc = FindObjectOfType<GameController>();
        gc.OnStartNewGame += SetupNewGame;

        cs = FindObjectOfType<CardServer>();
        uic = FindObjectOfType<UI_Controller>();
    }


    private void SetupNewGame()
    {        
        DrawNewCard();
    }

    private void DrawNewCard()
    {
        string[] dummyKeywordsForTesting = { "TEST1", "TEST2" }; 
        activeCard = cs.GetRandomCard(0, dummyKeywordsForTesting);

        uic.UpdateCoreGameplayPanelWithCard(activeCard);
    }

    #region Public Methods

    public void SelectOptionA()
    {
        // implement outcome of Option A
        // record the keyword of Option A in case it is referenced by a future Card

        Debug.Log($"{activeCard.OptionAOutcome.Parameter} changed by {activeCard.OptionAOutcome.Magnitude}");
        DrawNewCard();
    }

    public void SelectOptionB()
    {
        // implement outcome of Option B
        // record the keyword of Option B in case it is referenced by a future Card

        Debug.Log($"{activeCard.OptionAOutcome.Parameter} changed by {activeCard.OptionAOutcome.Magnitude}");
        DrawNewCard();
    }

    #endregion
}
