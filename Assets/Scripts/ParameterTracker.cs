using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParameterTracker : MonoBehaviour
{
    GameController gcRef;
    UI_Controller uic;

    public enum Parameter { Community, Tradition, Exploration, Objectivity}

    //state
    Dictionary<Parameter, int> parameterLevels = new Dictionary<Parameter, int>();

    private void Start()
    {
        gcRef = FindObjectOfType<GameController>();
        gcRef.OnStartNewGame += ResetParameterLevels;

        uic = FindObjectOfType<UI_Controller>();

        PrepareParameterLevelsDictionary();
    }

    private void PrepareParameterLevelsDictionary()
    {
        int parameterOptions = Enum.GetNames(typeof (Parameter)).Length;
        for(int i = 0; i < parameterOptions; i++)
        {
            parameterLevels.Add((Parameter)i, 0);
        }

    }

    private void ResetParameterLevels()
    {
        int parameterOptions = Enum.GetNames(typeof(Parameter)).Length;
        for (int i = 0; i < parameterOptions; i++)
        {
            parameterLevels[(Parameter)i] = 0;
        }
    }

    private ParameterPack CreateParameterPack()
    {
        int p1 = parameterLevels[(Parameter)0];
        int p2 = parameterLevels[(Parameter)1];
        int p3 = parameterLevels[(Parameter)2];
        int p4 = parameterLevels[(Parameter)3];

        int dummyHolder = 123;

        ParameterPack parameterPack = new ParameterPack(p1, p2, p3, p4, dummyHolder, dummyHolder, dummyHolder, dummyHolder);
        return parameterPack;
    }

    public void ModifyParameterLevel(Parameter parameterToAdjust, int amountToAdjust)
    {
        parameterLevels[parameterToAdjust] += amountToAdjust;

        ParameterPack newParameterPack = CreateParameterPack();
        uic.UpdateCoreGameplayPanelWithParameterPack(newParameterPack);
    }
}
