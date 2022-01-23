using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Holds the library of all cards for all phases. GetNextCard() returns
/// the next card to play for a phase.
/// </summary>
public class CardLibrary
{

    private Dictionary<int, List<Card>> cards = null;
    private int next = 0;

    private static Card DefaultCard = new Card(
        "Default",
        "",
        "Default scenario text.",
        new Option(
            "Option A",
            "Option A Result",
            new int[4]),
        new Option(
            "Option B",
            "Option B Result",
            new int[4])
        );

    public CardLibrary(TextAsset csv)
    {
        cards = new Dictionary<int, List<Card>>();
        if (csv == null)
        {
            Debug.LogError($"{GetType().Name} constructor didn't receive a TextAsset.");
            return;
        }
        var content = CSVUtility.ParseCSV(csv);
        content.RemoveAt(0); // Remove heading.
        AddRows(content);
        Shuffle();
    }

    private void AddRows(List<List<string>> rows)
    {
        foreach (List<string> columns in rows)
        {
            if (columns.Count < 16) continue;
            int phase;
            Card card = ColumnsToCard(columns, out phase);
            if (!cards.ContainsKey(phase)) cards[phase] = new List<Card>();
            cards[phase].Add(card);
        }
    }

    private Card ColumnsToCard(List<string> columns, out int phase)
    {
        // 0     1  2      3        4 5        6           7           8             9            10 11       12          13          14            15
        // Phase,ID,Prereq,Scenario,A,A.Result,A.Community,A.Tradition,A.Exploration,A.Objectivity,B,B.Result,B.Community,B.Tradition,B.Exploration,B.Objectivity
        phase = int.Parse(columns[0]);
        string id = columns[1];
        string prereq = columns[2];
        string mainText = columns[3];
        string optAText = columns[4];
        string optAResult = columns[5];
        Option optionA = new Option(optAText, optAResult, new int[] { int.Parse(columns[6]), int.Parse(columns[7]), int.Parse(columns[8]), int.Parse(columns[9]) });
        string optBText = columns[10];
        string optBResult = columns[11];
        Option optionB = new Option(optBText, optBResult, new int[] { int.Parse(columns[12]), int.Parse(columns[13]), int.Parse(columns[14]), int.Parse(columns[15]) });
        return new Card(id, prereq, mainText, optionA, optionB);
    }

    private void Shuffle()
    {
        foreach (List<Card> list in cards.Values)
        {
            ListUtility.Shuffle(list);
        }
    }

    public Card GetNextCard(int phase, History history)
    {
        if (cards == null || !cards.ContainsKey(phase) || cards[phase].Count == 0) return DefaultCard;
        List<Card> list = cards[phase];
        // If we're at the end of the list, shuffle the cards:
        if (next >= list.Count)
        {
            ListUtility.Shuffle(list);
            next = 0;
        }
        // Pass all cards whose prereqs aren't met:
        int safeguard = 0;
        while (!list[next].IsPrereqMet(history) && safeguard < list.Count)
        {
            safeguard++;
            Card unmetCard = list[next];
            list.RemoveAt(next);
            list.Add(unmetCard);
        }
        return list[next++];
    }

}
