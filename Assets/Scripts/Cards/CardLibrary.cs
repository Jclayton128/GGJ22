using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardLibrary
{

    //[TODO] Separate lists for each phase.
    //private Dictionary<int, List<Card>> cards

    private List<Card> list = null;
    private int next = 0;

    public CardLibrary(TextAsset csv)
    {
        list = new List<Card>();

        if (csv == null)
        {
            Debug.LogError($"{GetType().Name} constructor didn't receive a TextAsset.");
            return;
        }

        var content = CSVUtility.ParseCSV(csv);
        content.RemoveAt(0); // Remove heading.
        foreach (List<string> columns in content)
        {
            if (columns.Count < 16) continue;

            // 0     1  2      3        4 5        6           7           8             9            10 11       12          13          14            15
            // Phase,ID,Prereq,Scenario,A,A.Result,A.Community,A.Tradition,A.Exploration,A.Objectivity,B,B.Result,B.Community,B.Tradition,B.Exploration,B.Objectivity

            int phase = int.Parse(columns[0]);
            string id = columns[1];
            string prereq = columns[2];
            string mainText = columns[3];
            string optAText = columns[4];
            string optAResult = columns[5];
            Option optionA = new Option(optAText, optAResult, new int[] { int.Parse(columns[6]), int.Parse(columns[7]), int.Parse(columns[8]), int.Parse(columns[9]) });
            string optBText = columns[10];
            string optBResult = columns[11];
            Option optionB = new Option(optBText, optBResult, new int[] { int.Parse(columns[12]), int.Parse(columns[13]), int.Parse(columns[14]), int.Parse(columns[15]) });
            Card card = new Card(mainText, optionA, optionB);

            list.Add(card);
        }

        ListUtility.Shuffle(list);
    }

    public Card GetNextCard(int currentPhase, History history)
    {
        if (list == null || list.Count == 0) return default(Card);
        if (next >= list.Count)
        {
            ListUtility.Shuffle(list);
            next = 0;
        }
        return list[next++];
    }

}
