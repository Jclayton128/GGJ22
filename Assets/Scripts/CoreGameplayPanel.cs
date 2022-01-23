using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CoreGameplayPanel : UI_Panel
{
    [SerializeField] TextMeshProUGUI mainTMP = null;
    [SerializeField] TextMeshProUGUI optionATMP = null;
    [SerializeField] TextMeshProUGUI optionBTMP = null;
    [SerializeField] TextMeshProUGUI[] subjectiveParameterTMPs = null;
    [SerializeField] TextMeshProUGUI monthsElapsedTMP = null;
    [SerializeField] TextMeshProUGUI colonistCountTMP = null;

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
        gcRef.OnStartNewGame += UpdateMonthsElapsed;

    }


    #region Public UI Updates

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

    public void UpdateParametersOnPanel(Dictionary<ParameterTracker.Parameter, int> parameters)
    {
        for (int i = 0; i < subjectiveParameterTMPs.Length; i++)
        {
            subjectiveParameterTMPs[i].text = parameterPrefixes[i] + 
parameters[(ParameterTracker.Parameter)i].ToString();  // Sheesh. If I have to write "parameter" one more time...
        }

        colonistCountTMP.text = "Colonists: " + parameters[ParameterTracker.Parameter.ColonistCount].ToString();

        // [TODO] Need to find a nice way to expose this in the UI. Slider? Number? Iconography? Happy/sad face?
        // techLevelTMP.text = parameters[ParameterTracker.Parameter.TechLevel].ToString();
        // moraleTMP.text = parameters[ParameterTracker.Parameter.Morale].ToString();
    }

    public void UpdateMonthsElapsed()
    {
        int years = gcRef.MonthsElapsed / 12;
        int months = gcRef.MonthsElapsed % 12;

        monthsElapsedTMP.text = $"Time: {years}y {months}m";

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

    #region UI Helpers

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
