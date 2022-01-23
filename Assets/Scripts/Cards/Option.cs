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

    public Option(Option source)
    {
        this.OptionText = source.OptionText;
        this.ResultText = source.ResultText;
        this.ParameterChanges = new int[source.ParameterChanges.Length];
        for (int i = 0; i < source.ParameterChanges.Length; i++)
        {
            this.ParameterChanges[i] = source.ParameterChanges[i];
        }
    }

    public void ReplaceVariables(string cardID, History history)
    {
        if (history == null) return;
        OptionText = history.ReplaceVariables(cardID, OptionText);
        ResultText = history.ReplaceVariables(cardID, ResultText);
    }

}
