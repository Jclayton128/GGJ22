using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Card
{
    public string MainProblemText;
    public string OptionAText;
    public string OptionBText;

    public Card(string mainProblemText, string optionAText, string optionBText)
    {
        this.MainProblemText = mainProblemText;
        this.OptionAText = optionAText;
        this.OptionBText = optionBText;

        //Option A outcome = ...
        //Option B outcome = ...
    }
}
