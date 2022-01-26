using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameterTracker : MonoBehaviour
{
    GameController gcRef;
    UI_Controller uic;

    public enum Parameter { Community, Tradition, Exploration, Objectivity, ColonistCount, TechLevel, Morale}

    //settings
    int startingColonistCount = 100;
    int startingTechLevel = 100;
    int startingMorale = 100;

    //state
    Dictionary<Parameter, int> parameters = new Dictionary<Parameter, int>();

    private void Start()
    {
        gcRef = FindObjectOfType<GameController>();
        gcRef.OnStartNewGame += ResetParameters;

        uic = FindObjectOfType<UI_Controller>();

        PrepareParameters();
    }

    private void PrepareParameters()
    {
        int parameterOptions = Enum.GetNames(typeof (Parameter)).Length;
        for(int i = 0; i < parameterOptions; i++)
        {
            parameters.Add((Parameter)i, 0);
        }
    }

    private void ResetParameters()
    {
        int parameterOptions = Enum.GetNames(typeof(Parameter)).Length;
        for (int i = 0; i < parameterOptions; i++)
        {
            parameters[(Parameter)i] = 0;
        }

        parameters[Parameter.ColonistCount] = startingColonistCount;
        parameters[Parameter.TechLevel] = startingTechLevel;
        parameters[Parameter.Morale] = startingMorale;

        uic.UpdateCoreGameplayPanelWithParameters(parameters);
    }

    public void ModifyParameterLevel(Parameter parameterToAdjust, int amountToAdjust)
    {
        parameters[parameterToAdjust] += amountToAdjust;
    }

    public void PushParametersToUI()
    {
        uic.UpdateCoreGameplayPanelWithParameters(parameters);
    }

    public int GetParameterLevel(Parameter parameterToQuery)
    {
        return parameters[parameterToQuery];
    }
}
