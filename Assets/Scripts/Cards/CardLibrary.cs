using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLibrary
{

    //private List<string> list = null;
    //private int next = 0;

    public CardLibrary(TextAsset csv)
    {
        if (csv == null)
        {
            Debug.LogError($"{GetType().Name} constructor didn't receive a TextAsset.");
            return;
        }
    }

    public Card GetNextCard(int currentPhase, History history)
    {
        string mainText = "This is an interesting ethical dilemma about cats.";
        string optAText = "Traditional values are best!";
        string optAResult = "You chose traditional values.";
        int[] optAParams = new int[] { 0, 0, 0, -1 };
        string optBText = "Out with the old, in with the new!";
        string optBResult = "You chose the fresh newness.";
        int[] optBParams = new int[] { 0, 0, 0, 1 };
        Option optA = new Option(optAText, optAResult, optAParams);
        Option optB = new Option(optBText, optBResult, optBParams);
        Card debugCard = new Card(mainText, optA, optB);
        return debugCard;
    }

}
