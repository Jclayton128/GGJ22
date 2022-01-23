public struct Card
{
    public string ID;
    public string Prereq;
    public Choice PrereqChoice;
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
        this.Prereq = GetPrereqID(prereq);
        this.PrereqChoice = GetPrereqChoice(prereq);
        this.MainProblemText = mainProblemText;
        this.OptionA = optionA;
        this.OptionB = optionB;
    }

    public Card(Card source)
    {
        this.ID = source.ID;
        this.Prereq = source.Prereq;
        this.PrereqChoice = source.PrereqChoice;
        this.MainProblemText = source.MainProblemText;
        this.OptionA = new Option(source.OptionA);
        this.OptionB = new Option(source.OptionB);
    }

    private static string GetPrereqID(string prereq)
    {
        if (string.IsNullOrEmpty(prereq)) return string.Empty;
        int dividerPos = prereq.IndexOf(':');
        return (dividerPos == -1) ? prereq : prereq.Substring(0, dividerPos);
    }

    private static Choice GetPrereqChoice(string prereq)
    {
        if (string.IsNullOrEmpty(prereq)) return Choice.None;
        int dividerPos = prereq.IndexOf(':');
        return (dividerPos == -1) ? Choice.None
            : prereq.EndsWith("A") ? Choice.A : Choice.B;
    }

    public void ReplaceVariables(History history)
    {
        if (history == null) return;
        MainProblemText = history.ReplaceVariables(ID, MainProblemText);
        OptionA.ReplaceVariables(ID, history);
        OptionB.ReplaceVariables(ID, history);
    }

    public bool IsPrereqMet(History history)
    {
        return history.IsPrereqMet(Prereq, PrereqChoice);
    }

}
