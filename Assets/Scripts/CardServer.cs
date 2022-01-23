using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardServer : MonoBehaviour
{
    CardPreparer cp;

    //state
    List<List<Card>> cards = new List<List<Card>>();
    

    void Start()
    {
        cp = new CardPreparer();
        //cp = GetComponent<CardPreparer>();
        //cp.OnCardsPrepared += GatherPreparedCards;
    }

    /// <summary>
    /// This function subscribes to CardPreparer's OnCardsPrepared Action, which is invoked once the CSV is converted into Cards
    /// </summary>
    private void GatherPreparedCards()
    {
        // Pull a list or array from the CardPreparer
        // Organize that list/array into different "decks" that correspond to each phase.
    }

    /// <summary>
    /// This takes in the currentPhase and an array of keywords that correspond to what choices the player made on certain
    /// previous actions that are part of a storyline. The returned Card will either not have a requirement, or
    /// its requirement matches a keyword that is passed in.
    /// </summary>
    /// <param name="currentPhase"></param>
    /// <returns></returns>
    public Card GetRandomCard(int currentPhase, string[] keywords)
    {
        return cp.GetCard(currentPhase);

        //For debug

        //int rand = UnityEngine.Random.Range(0, 2);
        //if (rand == 0)
        //{
        //    string main = "This is an interesting ethical dilemma about cats.";
        //    string optA = "Traditional values are best!";
        //    string optB = "Out with the old, in with the new!";
        //    Outcome optAOutcome = new Outcome(ParameterTracker.Parameter.Objectivity, -1);
        //    Outcome optBOutcome = new Outcome(ParameterTracker.Parameter.Objectivity, 1);
        //    Card debugCard = new Card(main, optA, optB, optAOutcome, optBOutcome);
        //    return debugCard;
        //}
        //else
        //{
        //    string main = "This is a tough decision between an old man and a young girl";
        //    string optA = "Age over beauty!";
        //    string optB = "The young are our future!";
        //    Outcome optAOutcome = new Outcome(ParameterTracker.Parameter.Tradition, -1);
        //    Outcome optBOutcome = new Outcome(ParameterTracker.Parameter.Tradition, 1);
        //    Card debugCard = new Card(main, optA, optB, optAOutcome, optBOutcome);
        //    return debugCard;
        //}


    }


}
