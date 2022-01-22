using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoreGameplayPanel : UI_Panel
{
    [SerializeField] TextMeshProUGUI mainTMP = null;
    [SerializeField] TextMeshProUGUI optionATMP = null;
    [SerializeField] TextMeshProUGUI optionBTMP = null;

    CoreGameLooper cgl;

    protected override void Start()
    {
        base.Start();
        cgl = FindObjectOfType<CoreGameLooper>();
    }


    #region New Card Loading

    public void DisplayNewCard(Card newCard)
    {
        mainTMP.text = newCard.MainProblemText;
        optionATMP.text = newCard.OptionAText;
        optionBTMP.text = newCard.OptionBText;
    }

    #endregion

    #region Button Press Handlers
    public void HandlePress_OptionA()
    {
        Debug.Log("option A was selected");
        cgl.SelectOptionA();
    }

    public void HandlePress_OptionB()
    {
        Debug.Log("option B was selected");
        cgl.SelectOptionB();
    }

    #endregion
}
