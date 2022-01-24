using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class GameController : MonoBehaviour
{
    public enum State { Start, CoreGameplay, Endgame, Settings};
    UI_Controller uic;
    EndgameGenerator eg;
    public Action OnStartNewGame;

    //settings
    int[] questionThresholds = { 5, 10 }; // once Question Count >= the current phase's threshold, advance to next phase
    int[] monthsRangePerQuestion = { 2, 8 }; // possible time range between Cards.

    //state
    State previousState;
    State currentState;
    public int QuestionCount { get; private set; }
    public int MonthsElapsed { get; private set; } // This is a variable that roughly matches Question Count for the UI.
    public int CurrentPhase { get; private set; }

    /// <summary>
    /// **This bool could be rendered obsolete by simply checking if number of questions presented to player is > zero;
    /// </summary>
    bool isCurrentlyInGame = false;

    void Start()
    {
        uic = FindObjectOfType<UI_Controller>();
        SetNewState(State.Start);

        eg = FindObjectOfType<EndgameGenerator>();
    }

    #region Public Methods

    /// <summary>
    /// Workhorse method of the Game Controller. Allows other classes to switch the state. Privately handles
    /// the entry and exit work needed for any particular state.
    /// </summary>
    /// <param name="newState"></param>
    public void SetNewState(State newState)
    {
        if (newState == currentState)
        {
            Debug.Log("New state is the same as the current state");
            return;
        }
        else
        {
            previousState = currentState;
            currentState = newState;
        }

        EnterState(currentState);
        ExitState(previousState);


    }

    /// <summary>
    /// This is needed by the persistent settings button in since buttons can't take in enums within the editor.
    /// </summary>
    public void SetStateAsSettings() 
    {
        SetNewState(State.Settings);
    }

    /// <summary>
    /// This allows the state to rollback to the previous state, like a back button
    /// </summary>
    public void RequestReturnToPreviousState()
    {
        //if (previousState == State.Endgame)
        //{
        //    // Perhaps there are some states you can't return to? Endgame?
        //    return
        //}

        SetNewState(previousState);
    }

    public void IncrementQuestionCount()
    {
        QuestionCount++;
        MonthsElapsed += UnityEngine.Random.Range(monthsRangePerQuestion[0], monthsRangePerQuestion[1]);
        uic.UpdateCoreGameplayPanelOnNewQuestionCount();
        UpdateCurrentPhase();
        CheckForEndgamePhase();

        // [TODO] update the stardate on UI for to expose game progress to player (sort of).
    }

    #endregion

    #region Phase Helpers
    private void CheckForEndgamePhase()
    {
        if (CurrentPhase >= questionThresholds.Length)
        {
            Debug.Log($"End game reached via {QuestionCount} questions answered.");
            // [TODO] enter the endgame phase victoriously - the planet was reached via sufficient questions answered.
            SetNewState(State.Endgame);
        }
    }

    private void UpdateCurrentPhase()
    {
        if (QuestionCount > questionThresholds[CurrentPhase])
        {
            CurrentPhase++;
            Debug.Log($"Advanced a phase. Now in phase {CurrentPhase}");
        }
    }

    #endregion

    #region State Helpers
    private void EnterState(State newState)
    {
        uic.UpdateUIWithNewState(newState);
        switch (newState)
        {
            case State.Start:
                // do things related to entering the start state
                QuestionCount = 0;
                MonthsElapsed = 0;
                CurrentPhase = 0;
                break;

            case State.CoreGameplay:

                if (QuestionCount > 0) // Must be in existing game if the question count is more than zero;
                {
                    break;
                }
                else
                {
                    // Do things to set up the core gameplay state
                    OnStartNewGame?.Invoke();
                    break;
                }

            case State.Endgame:
               
                eg.GenerateEndGame();
                break;

            case State.Settings:

                break;

        }

    }


    private void ExitState(State previousState)
    {
        switch (previousState)
        {
            case State.Start:
                // cleanup anything related to old state
                return;

            case State.CoreGameplay:

                return;

            case State.Endgame:

                return;

            case State.Settings:

                return;

        }
    }

    #endregion

}
