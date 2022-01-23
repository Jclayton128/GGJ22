using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Option
{
    public string OptionText;
    public string ResultText;
    public int[] ParameterChanges; // Mirrors size and order of Parameter enum.

    public Option(
        string optionText,
        string resultText,
        int[] parameterChanges)
    {
        this.OptionText = optionText;
        this.ResultText = resultText;
        this.ParameterChanges = parameterChanges;
    }

}
