public struct Card
{
    public string ID;
    public string Prereq;
    public string MainProblemText;
    public Option OptionA;
    public Option OptionB;

    public Card(
        string id,
        string prereq,
        string mainProblemText,
        Option optionA,
        Option optionB)
    {
        this.ID = id;
        this.Prereq = prereq;
        this.MainProblemText = mainProblemText;
        this.OptionA = optionA;
        this.OptionB = optionB;
    }

    public Card(Card source)
    {
        this.ID = source.ID;
        this.Prereq = source.Prereq;
        this.MainProblemText = source.MainProblemText;
        this.OptionA = new Option(source.OptionA);
        this.OptionB = new Option(source.OptionB);
    }

    public void ReplaceVariables(History history)
    {
        if (history == null) return;
        MainProblemText = history.ReplaceVariables(ID, MainProblemText);
        OptionA.ReplaceVariables(ID, history);
        OptionB.ReplaceVariables(ID, history);
    }

}
