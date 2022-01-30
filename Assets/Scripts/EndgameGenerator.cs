using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndgameGenerator : MonoBehaviour
{
    UI_Controller uic;
    ParameterTracker pt;
    GameController gcRef;
    [SerializeField] Planet[] planets = null;
    [SerializeField] TextAsset landingSource = null;
    [SerializeField] TextAsset colonistCountSource = null;
    [SerializeField] TextAsset techLevelSource = null;
    [SerializeField] TextAsset moraleSource = null;
    [SerializeField] TextAsset positiveSource = null;
    [SerializeField] TextAsset negativeSource = null;
    [SerializeField] TextAsset failureSource = null;
    [SerializeField] Sprite failureSprite = null;

    List<List<string>> colonistCountPhrases;
    List<List<string>> techLevelPhrases;
    List<List<string>> moralePhrases;

    Dictionary<ParameterTracker.Parameter, List<string>> failurePhrases = new Dictionary<ParameterTracker.Parameter, List<string>>();
    Dictionary<ParameterTracker.Parameter, List<string>> positivePhrases = new Dictionary<ParameterTracker.Parameter, List<string>>();
    Dictionary<ParameterTracker.Parameter, List<string>> negativePhrases = new Dictionary<ParameterTracker.Parameter, List<string>>();

    //settings
    int threshold_low = 50;  // a parameter below this level is considered "low"
    int threshold_high = 70; // a parameter above this level is considered "high". All other values are "medium"
    string[] parameterNames = new string[4];

    //state
    Planet currentPlanet;



    private void Start()
    {
        gcRef = FindObjectOfType<GameController>();
        uic = FindObjectOfType<UI_Controller>();
        pt = FindObjectOfType<ParameterTracker>();
        colonistCountPhrases = ParseConcretePhrases(colonistCountSource);
        techLevelPhrases = ParseConcretePhrases(techLevelSource);
        moralePhrases = ParseConcretePhrases(moraleSource);
        parameterNames = InitializeParameterNames();
        positivePhrases = ParseSubjectivePhrases(positiveSource);
        negativePhrases = ParseSubjectivePhrases(negativeSource);
        failurePhrases = ParseFailurePhrases(failureSource);
    }



    #region Preparation Methods
    private string[] InitializeParameterNames()
    {
        string[] names = new string[4];
        for (int i = 0; i < names.Length; i++)
        {
            names[i] = Enum.GetName(typeof(ParameterTracker.Parameter), i);
        }
        return names;
    }

    private Dictionary<ParameterTracker.Parameter, List<string>> ParseFailurePhrases(TextAsset failureSource)
    {
        Dictionary<ParameterTracker.Parameter, List<string>> dict = new Dictionary<ParameterTracker.Parameter, List<string>>();
        List<string> list1 = new List<string>();
        List<string> list2 = new List<string>();
        List<string> list3 = new List<string>();
        dict.Add(ParameterTracker.Parameter.ColonistCount, list1);
        dict.Add(ParameterTracker.Parameter.TechLevel, list2);
        dict.Add(ParameterTracker.Parameter.Morale, list3);
        string[] rows = failureSource.text.Split('\n');
        foreach (var row in rows)
        {
            if (String.IsNullOrEmpty(row)) break;
            string[] parts = row.Split(';');
            if (parts[0] == "colonists") dict[ParameterTracker.Parameter.ColonistCount].Add(parts[1]);
            if (parts[0] == "tech") dict[ParameterTracker.Parameter.TechLevel].Add(parts[1]);
            if (parts[0] == "morale") dict[ParameterTracker.Parameter.Morale].Add(parts[1]);
        }
        return dict;
    }

    private List<List<string>> ParseConcretePhrases(TextAsset source)
    {
        List<string> lowPhrases = new List<string>();
        List<string> mediumPhrases = new List<string>();
        List<string> highPhrases = new List<string>();
        string[] rows = source.text.Split('\n');
        foreach (var row in rows)
        {
            if (String.IsNullOrEmpty(row)) break;
            string[] parts = row.Split(';');
            if (parts[0] == "low") lowPhrases.Add(parts[1]);
            if (parts[0] == "medium") mediumPhrases.Add(parts[1]);
            if (parts[0] == "high") highPhrases.Add(parts[1]);
        }
        List<List<string>> phrases = new List<List<string>>();
        phrases.Add(lowPhrases);
        phrases.Add(mediumPhrases);
        phrases.Add(highPhrases);
        return phrases;
    }

    private Dictionary<ParameterTracker.Parameter, List<string>> ParseSubjectivePhrases(TextAsset source)
    {
        Dictionary<ParameterTracker.Parameter, List<string>> dict = new Dictionary<ParameterTracker.Parameter, List<string>>();
        for (int i = 0; i < 4; i++)
        {
            List<string> list = new List<string>();
            dict.Add((ParameterTracker.Parameter)i, list);
        }
        string[] rows = source.text.Split('\n');
        foreach (var row in rows)
        {
            if (String.IsNullOrEmpty(row)) break;
            string[] parts = row.Split(';');
            for (int i = 0; i < parameterNames.Length; i++)
            {
                if (parts[0] == parameterNames[i])
                {
                    dict[(ParameterTracker.Parameter)i].Add(parts[1]);
                    break;
                }
                if (i == parameterNames.Length - 1 && !String.IsNullOrWhiteSpace(parts[0]))
                    Debug.Log($"{parts[0]} doesn't match anything any parameters");
            }
        }
        return dict;
    }

    #endregion

    public void GenerateEndGame()
    {
        if (pt.IsFailed)
        {   
            uic.UpdateEndgamePanel(CreateFailurePages(), failureSprite);
        }
        else
        {
            string[] endgamePages = new string[5];
            currentPlanet = GetRandomPlanet();
            endgamePages[0] = CreateLandingPage();
            endgamePages[1] = CreateConcretesPage();
            endgamePages[2] = CreateFirstSubjectivesPage();
            endgamePages[3] = CreateSecondSubjectivePage();
            endgamePages[4] = CreateScorePage();
            uic.UpdateEndgamePanel(endgamePages, currentPlanet.GetSprite());
        }
    }

    private Planet GetRandomPlanet()
    {
        int rand = UnityEngine.Random.Range(0, planets.Length);
        planets[rand].InitializePlanet();
        return planets[rand];
    }
    private string CreateLandingPage()
    {
        string[] possibleLandingPages = landingSource.text.Split('\n');
        int rand = UnityEngine.Random.Range(0, possibleLandingPages.Length);
        return string.Format(possibleLandingPages[rand], 
            currentPlanet.GetName(), currentPlanet.GetAdjective(), currentPlanet.GetAdjective()); ;
    }

    private string CreateConcretesPage()
    {
        string morale = GetMoraleSubpage();
        string colonists = GetColonistSubpage();
        string tech = GetTechLevelSubpage();

        string page = morale + "\n" + colonists + "\n" + tech;
        return page;
    }

    private string GetColonistSubpage()
    {
        int n = pt.GetParameterLevel(ParameterTracker.Parameter.ColonistCount);
        if (n < threshold_low)
        {
            int rand = UnityEngine.Random.Range(0, colonistCountPhrases[0].Count);
            return colonistCountPhrases[0][rand];
        }
        if (n > threshold_high)
        {
            int rand = UnityEngine.Random.Range(0, colonistCountPhrases[2].Count);
            return colonistCountPhrases[2][rand];
        }
        else
        {
            int rand = UnityEngine.Random.Range(0, colonistCountPhrases[1].Count);
            return colonistCountPhrases[1][rand];
        }
    }
    private string GetTechLevelSubpage()
    {
        int n = pt.GetParameterLevel(ParameterTracker.Parameter.TechLevel);
        if (n < threshold_low)
        {
            int rand = UnityEngine.Random.Range(0, techLevelPhrases[0].Count);
            return techLevelPhrases[0][rand];
        }
        if (n > threshold_high)
        {
            int rand = UnityEngine.Random.Range(0, techLevelPhrases[2].Count);
            return techLevelPhrases[2][rand];
        }
        else
        {
            int rand = UnityEngine.Random.Range(0, techLevelPhrases[1].Count);
            return techLevelPhrases[1][rand];
        }
    }
    private string GetMoraleSubpage()
    {
        int n = pt.GetParameterLevel(ParameterTracker.Parameter.Morale);
        if (n < threshold_low)
        {
            int rand = UnityEngine.Random.Range(0, moralePhrases[0].Count);
            return moralePhrases[0][rand];
        }
        if (n > threshold_high)
        {
            int rand = UnityEngine.Random.Range(0, moralePhrases[2].Count);
            return moralePhrases[2][rand];
        }
        else
        {
            int rand = UnityEngine.Random.Range(0, moralePhrases[1].Count);
            return moralePhrases[1][rand];
        }
    }
    private string CreateFirstSubjectivesPage()
    {
        string subpage_0 = GetParameterSubpage(0);
        string subpage_1 = GetParameterSubpage(1);
        return subpage_0 + "\n" + subpage_1;
    }

    private string CreateSecondSubjectivePage()
    {
        string subpage_2 = GetParameterSubpage(2);
        string subpage_3 = GetParameterSubpage(3);
        return subpage_2 + "\n" + subpage_3;
    }

    private string GetParameterSubpage(int parameterIndex)
    {
        int magnitude = pt.GetParameterLevel((ParameterTracker.Parameter)parameterIndex);
        if (magnitude > 0)
        {
            int rand = UnityEngine.Random.Range(0,positivePhrases[(ParameterTracker.Parameter)parameterIndex].Count);
            return positivePhrases[(ParameterTracker.Parameter)parameterIndex][rand];
        }
        else
        {
            int rand = UnityEngine.Random.Range(0,negativePhrases[(ParameterTracker.Parameter)parameterIndex].Count);
            return negativePhrases[(ParameterTracker.Parameter)parameterIndex][rand];
        }
    }

    private string CreateScorePage()
    {
        int score = pt.GetParameterLevel(ParameterTracker.Parameter.ColonistCount);
        score *= pt.GetParameterLevel(ParameterTracker.Parameter.TechLevel);
        score *= pt.GetParameterLevel(ParameterTracker.Parameter.Morale);

        string scoreBlurb = $"Due to your leadership, the last remnant of mankind remains established" +
            $" on {currentPlanet.GetName()} for {Mathf.RoundToInt(score/36f)} years.";
        return scoreBlurb;
    }

    private string[] CreateFailurePages()
    {
        string[] pages = new string[2];
        int rand = UnityEngine.Random.Range(0, failurePhrases[pt.GetFailingParameter()].Count);
        pages[0] = failurePhrases[pt.GetFailingParameter()][rand];

        pages[1] = $"At least you made it {gcRef.MonthsElapsed} months longer than the last captain...";
        return pages;
    }

}
