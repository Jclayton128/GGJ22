public class CardPreparer
{

    private History history = new History();

    public History History { get { return history; } }

    public Card GetCard(int phase)
    {
        Card template = CardData.Instance.Cards.GetNextCard(phase, history);
        Card card = new Card(template);
        card.ReplaceVariables(history);
        return card;
    }
}
