using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoreGameplayPanel : UI_Panel
{
    [SerializeField] TextMeshProUGUI mainTMP = null;
    [SerializeField] TextMeshProUGUI optionATMP = null;
    [SerializeField] TextMeshProUGUI optionBTMP = null;

    [SerializeField] TextMeshProUGUI[] parameterTMPs = null;

    CoreGameLooper cgl;

    string[] parameterPrefixes = { "Com: ", "Tra: ", "Exp: ", "Obj: " };


    protected override void Start()
    {
        base.Start();
        cgl = FindObjectOfType<CoreGameLooper>();
    }


    #region UI Updates

    public void DisplayNewCard(Card newCard)
    {
        mainTMP.text = newCard.MainProblemText;
        optionATMP.text = newCard.OptionA.OptionText;
        optionBTMP.text = newCard.OptionB.OptionText;
    }

    public void UpdateParametersOnPanel(ParameterPack newParameterPack)
    {
        for (int i = 0; i < parameterTMPs.Length; i++)
        {
            parameterTMPs[i].text = parameterPrefixes[i] + newParameterPack.Parameters[i].ToString();
        }

        // Push Colonist Count, and Tech, Morale, and Culture Levels to UI as well.
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
