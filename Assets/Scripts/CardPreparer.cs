using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPreparer
{

    private History history;

    public Card GetCard(int phase)
    {
        return CardData.Instance.Cards.GetNextCard(phase, history);
    }
}
