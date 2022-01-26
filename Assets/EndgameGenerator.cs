using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndgameGenerator : MonoBehaviour
{
    UI_Controller uic;
    ParameterTracker pt;
    [SerializeField] Planet[] planets = null;
    [SerializeField] TextAsset landingSource = null;
    [SerializeField] TextAsset colonistCountSource = null;
    [SerializeField] TextAsset techLevelSource = null;
    [SerializeField] TextAsset moraleSource = null;

    List<List<string>> colonistCountPhrases;
    List<List<string>> techLevelPhrases;
    List<List<string>> moralePhrases;

    //settings
    int threshold_low = 33;  // a parameter below this level is considered "low"
    int threshold_high = 66; // a parameter above this level is considered "high". All other values are "medium"


    private void Start()
    {
        uic = FindObjectOfType<UI_Controller>();
        pt = FindObjectOfType<ParameterTracker>();
        colonistCountPhrases = ParseConcretePhrases(colonistCountSource);
        techLevelPhrases = ParseConcretePhrases(techLevelSource);
        moralePhrases = ParseConcretePhrases(moraleSource);
    }

    public void GenerateEndGame()
    {
        string[] endgamePages = new string[4];

        Planet planet = GetRandomPlanet();
        endgamePages[0] = CreateLandingPage(planet);
        endgamePages[1] = CreateConcretesPage();
        endgamePages[2] = CreateFirstSubjectivesPage();
        endgamePages[3] = CreateSecondSubjectivePage();

        uic.UpdateEndgamePanel(endgamePages, planet.GetSprite());
    }

    private string CreateFirstSubjectivesPage()
    {
        return "first subjective page";
    }

    private string CreateSecondSubjectivePage()
    {
        return "second subjective page";
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

    private List<List<string>> ParseConcretePhrases(TextAsset source)
    {
        List<string> lowPhrases = new List<string>();
        List<string> mediumPhrases = new List<string>();
        List<string> highPhrases = new List<string>();
        string[] rows = source.text.Split('\n');
        foreach(var row in rows)
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

    private string CreateLandingPage(Planet planet)
    {
        string[] possibleLandingPages = landingSource.text.Split('\n');
        int rand = UnityEngine.Random.Range(0, possibleLandingPages.Length); 
        return string.Format(possibleLandingPages[rand], planet.GetName(), planet.GetAdjective(), planet.GetAdjective()); ;
    }

    private Planet GetRandomPlanet()
    {
        int rand = UnityEngine.Random.Range(0, planets.Length);
        planets[rand].InitializePlanet();
        return planets[rand];
    }
}
