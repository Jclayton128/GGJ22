using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Planet")]
public class Planet : ScriptableObject
{
    [SerializeField] Sprite planetSprite = null;

    [SerializeField] string planetName = "New Earth";

    [SerializeField] string[] adjectivesSource = { "dusty", "windswept" };

    List<string> remainingAdjectives = new List<string>();

    public void InitializePlanet()
    {
        remainingAdjectives.Clear();
        foreach (var adjective in adjectivesSource)
        {
            remainingAdjectives.Add(adjective);
        }
    }

    public string GetAdjective()
    {
        int rand = UnityEngine.Random.Range(0, remainingAdjectives.Count);
        string adjective = remainingAdjectives[rand];
        remainingAdjectives.RemoveAt(rand);
        return adjective;

    }

    public  Sprite GetSprite()
    {
        return planetSprite;
    }

    public string GetName()
    {
        return planetName;
    }
}
