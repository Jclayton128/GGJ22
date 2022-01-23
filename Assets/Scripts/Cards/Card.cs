using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Card
{
    public string MainProblemText;
    public Option OptionA;
    public Option OptionB;

    public Card(
        string mainProblemText,
        Option optionA,
        Option optionB)
    {
        this.MainProblemText = mainProblemText;
        this.OptionA = optionA;
        this.OptionB = optionB;
    }

    //public string OptionAText;
    //public string OptionBText;
    //public Outcome OptionAOutcome;
    //public Outcome OptionBOutcome;

    //public Card(
    //    string mainProblemText, 
    //    string optionAText, 
    //    string optionBText,
    //    Outcome optionAOutcome,
    //    Outcome optionBOutcome)
    //{
    //    this.MainProblemText = mainProblemText;
    //    this.OptionAText = optionAText;
    //    this.OptionBText = optionBText;
    //    this.OptionAOutcome = optionAOutcome;
    //    this.OptionBOutcome = optionBOutcome;
    //}
}
