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
    [SerializeField] GameObject[] optionButtons = null;
    [SerializeField] GameObject acceptOutcomeButton = null;

    CoreGameLooper cgl;
    private enum Substate { PresentCardScenario, ShowCardOutcome };
    string[] parameterPrefixes = { "Com: ", "Tra: ", "Exp: ", "Obj: " };

    //state
    Substate currentSubstate;


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
        ChangeSubstate(Substate.PresentCardScenario);
    }

    public void DisplayCardOutcome(string outcomeText)
    {
        mainTMP.text = outcomeText;
        ChangeSubstate(Substate.ShowCardOutcome);
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

    public void HandlePress_AcceptOutcome()
    {
        cgl.DrawNewCard();
    }
    #endregion

    #region Substate Helpers

    private void ChangeSubstate(Substate newSubstate)
    {
        currentSubstate = newSubstate;
        switch (currentSubstate)
        {
            case Substate.PresentCardScenario:
                foreach (var button in optionButtons)
                {
                    button.SetActive(true);
                }
                acceptOutcomeButton.SetActive(false);
                break;

            case Substate.ShowCardOutcome:
                foreach (var button in optionButtons)
                {
                    button.SetActive(false);
                }
                acceptOutcomeButton.SetActive(true);
                break;

        }
    }

    #endregion
}
