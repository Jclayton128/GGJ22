using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardPreparer
{

    private History history;

    public Card GetCard(int phase)
    {
        Card card = CardData.Instance.Cards.GetNextCard(phase, history);
        // [TODO] Prepare card.
        return card;
    }
}
